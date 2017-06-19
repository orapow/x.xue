using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.paper.question {
    public class save:xmg {
        public int id { get; set; }
        [ParmsAttr(name = "科目", min = 1)]
        public int sub { get; set; }
        [ParmsAttr(name = "教材", min = 1)]
        public int bk { get; set; }
        [ParmsAttr(name = "章节", min = 1)]
        public int cp { get; set; }
        public int topic { get; set; }
        public int type { get; set; }
        public int easy { get; set; }
        [ParmsAttr(name = "知识点", req = true)]
        public string kg { get; set; }
        public string name { get; set; }
        public string src { get; set; }
        public int score { get; set; }

        public int p_id { get; set; }

        protected override XResp Execute() {
            x_question que = null;
            if (id > 0)
                que = DB.x_question.FirstOrDefault(o => o.question_id == id);
            if (que == null)
                que = new x_question() { ctime = DateTime.Now };

            que.book = bk;
            que.chapter = cp;
            que.easy = easy;
            if (id == 0)
                que.hits = 0;
            if (!string.IsNullOrEmpty(kg)) {
                que.knowledge = kg;
                var rg = new Regex("[\\d]+");
                que.knowledgecount = rg.Matches(kg).Count;
            }
            que.mtime = DateTime.Now;
            que.src = src;
            que.subject = sub;
            que.title = name;
            que.topic = topic;
            que.type = type;
            que.score = score;


            if (id == 0)
                DB.x_question.InsertOnSubmit(que);
            SubmitDBChanges();

            //试卷添加试题
            //试题ID
            var qid = que.question_id;
            //题型
            int  pic = Int32.Parse(que.topic+"");
            //试卷ID
            var pid = p_id;

            var q = DB.x_paper.FirstOrDefault(o => o.paper_id == pid);
            
            var ids = X.Core.Utility.Serialize.FromJson<Dictionary<string, int>>(q.qids);
            if (ids == null)
                ids = new Dictionary<string, int>();
            ids.Add(qid + "", pic);

            q.qcount = ids.Count;
            string s = X.Core.Utility.Serialize.ToJson(ids);
            q.qids = s;
            
            SubmitDBChanges();

            return new XResp() { msg = que.question_id + "" };

        }
    }
}
