using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.user {
    public class favsshiti : _web {
        public int page { get; set; }
        protected override bool nduser {
            get {
                return true;
            }
        }
        //public int tp { get; set; }
        //protected override string GetParmNames {
        //    get {
        //        return "tp";
        //    }
        //}
        protected override void InitDict() {
            base.InitDict();
            var q = from p in cu.x_fav
                    where p.type == 1
                    select p;
            var s = q.Count();
            dict.Add("pc", q.Count());

            if (1 == 1) {
                dict.Add("ps", q.OrderByDescending(o => o.ctime)
                    .Skip((page - 1)).Take(10)
                    .ToList()
                    .Select(o => new {
                        id = o.fav_id,
                        qid = o.cid,
                        to = GetDictName("question.topic", DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).topic),
                        ty = GetDictName("question.type", DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).type),
                        ey = GetDictName("question.easy", DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).easy),
                        content = Context.Server.HtmlDecode(DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).title),
                        DB.x_question.FirstOrDefault(qu => qu.question_id == o.cid).hits,
                        o.user_id,
                        o.type,
                        o.group,
                        sub = GetDictName("xx.subject", o.subject),
                    }));
            }


            //   dict.Add("favs", "using-active");
        }
    }
}
