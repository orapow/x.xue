using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;

namespace X.App.Views.mgr.paper.question
{
    public class qedit : xmg
    {
        public int p_id { get; set; }
        public int qid { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "qid-p_id";
            }
        }

        protected override void InitDict()
        {
            base.InitDict();
            if (p_id == 0) throw new XExcep("T试卷参数丢失");

            var p = DB.x_paper.FirstOrDefault(o => o.paper_id == p_id);
            if (p == null) throw new XExcep("T试卷不存在");
            dict.Add("paper", p);

            if (qid > 0)
            {
                var q = DB.x_question.FirstOrDefault(o => o.question_id == qid);
                dict.Add("item", q);
                dict.Add("kg", q.knowledge + "|" + GetDictName("xx.knowledge", q.knowledge));
            }
        }
    }
}
