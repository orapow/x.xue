using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.paper.question
{
    public class save : xmg
    {
        public int id { get; set; }
        public int topic { get; set; }
        public int type { get; set; }
        public int easy { get; set; }
        [ParmsAttr(name = "知识点", req = true)]
        public string kg { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        [ParmsAttr(name = "试卷ID", min = 1)]
        public int p_id { get; set; }

        protected override XResp Execute()
        {
            x_question que = null;
            var paper = DB.x_paper.FirstOrDefault(o => o.paper_id == p_id);
            if (paper == null) throw new XExcep("T试卷不存在");

            if (id > 0)
                que = DB.x_question.FirstOrDefault(o => o.question_id == id);
            if (que == null)
                que = new x_question() { ctime = DateTime.Now };

            que.book = paper.book;
            que.subject = paper.subject;
            que.easy = easy;

            if (id == 0) que.hits = 0;
            if (!string.IsNullOrEmpty(kg))
            {
                que.knowledge = kg;
                var rg = new Regex("[\\d]+");
                que.knowledgecount = rg.Matches(kg).Count;
            }
            que.mtime = DateTime.Now;
            que.src = paper.topic;
            que.subject = paper.subject;
            que.title = name;
            que.topic = topic;
            que.type = type;
            que.score = score;

            if (id == 0)
                DB.x_question.InsertOnSubmit(que);
            SubmitDBChanges();

            Dictionary<string, int> qids = null;
            if (!string.IsNullOrEmpty(paper.qids)) qids = Serialize.FromJson<Dictionary<string, int>>(paper.qids);
            if (qids == null) qids = new Dictionary<string, int>();

            if (qids.ContainsKey(que.question_id + "")) qids[que.question_id + ""] = que.score.Value;
            else qids.Add(que.question_id + "", que.score.Value);

            paper.qcount = qids.Count;
            paper.qids = Serialize.ToJson(qids);

            SubmitDBChanges();

            return new XResp();
        }
    }
}
