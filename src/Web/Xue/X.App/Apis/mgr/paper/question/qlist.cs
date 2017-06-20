using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.paper.question
{
    public class qlist : xmg
    {
        public int pid { get; set; }
        public int page { get; set; }
        public int limit { get; set; }

        protected override XResp Execute()
        {
            var p = DB.x_paper.FirstOrDefault(o => o.paper_id == pid);
            if (p == null) throw new XExcep("T试卷不存在");

            var r = new Resp_List();
            r.page = page;
            r.count = 0;

            var ids = Serialize.FromJson<Dictionary<string, int>>(p.qids);
            if (ids == null) return r;

            var q = DB.x_question
                .Where(o => ids.Keys.Contains(o.question_id + ""));

            r.items = q.Select(o => new
            {
                qid = o.question_id,
                cot = Context.Server.HtmlDecode(o.title),
                score = o.score,
                topic = GetDictName("question.topic", o.topic),
                type = GetDictName("question.type", o.type),
                easy = GetDictName("question.easy", o.easy)
            });

            r.count = q.Count();

            return r;

        }
    }
}
