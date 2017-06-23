using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.user
{
    public class fav : _web
    {
        public int page { get; set; }
        public int ag { get; set; }
        public int ssb { get; set; }
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        public int tp { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "tp-page-ag-ssb";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();

            if (tp == 1)
            {
                var q = from p in cu.x_fav
                        where p.type == 1
                        select p;

                dict.Add("pc", q.Count());

                dict.Add("ags", GetDictList("xx.age", "0"));
                if (ag > 0)
                {
                    var sbs = GetDictList("xx.subject", "0").Where(o => o.f3 == ag);
                    q = q.Where(o => sbs.Select(s => s.value).Contains(o.subject + ""));
                    dict.Add("subs", sbs.ToList());
                }
                if (ssb > 0) q = q.Where(o => o.subject == ssb);

                dict.Add("ps", q.OrderByDescending(o => o.ctime)
                    .Skip((page - 1)).Take(10)
                    .ToList()
                    .Select(o => new
                    {
                        id = o.fav_id,
                        sub = o.subject,
                        qid = o.cid,
                        to = GetDictName("question.topic", DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).topic),
                        ty = GetDictName("question.type", DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).type),
                        ey = GetDictName("question.easy", DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).easy),
                        content = Context.Server.HtmlDecode(DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).title),
                        DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).hits,
                        o.user_id,
                        o.type,
                        o.group
                    }));
            }
            else if (tp == 2)
            {
                var q = from p in cu.x_fav
                        where p.type == 2
                        select p;

                dict.Add("pc", q.Count());

                dict.Add("ps",
                    q.OrderByDescending(o => o.ctime)
                    .Skip((page - 1)).Take(10)
                    .ToList()
                    .Select(o => new
                    {
                        id = o.fav_id,
                        title = DB.x_paper.FirstOrDefault(p => p.paper_id == o.cid).topic,
                        type = GetDictName("paper.type", DB.x_paper.FirstOrDefault(p => p.paper_id == o.cid).type),
                        date = DB.x_down.FirstOrDefault(d => d.paper_id == o.cid),
                        o.user_id,
                        o.group,
                        sub = GetDictName("xx.subject", o.subject),
                        o.cid
                    }));
            }

            dict.Add("favs" + tp, "using-active");
        }
        public override string GetTplFile()
        {
            return "user/fav-" + tp;
        }
    }
}
