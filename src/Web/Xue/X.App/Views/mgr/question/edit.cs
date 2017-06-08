using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.question
{
    public class edit : xmg
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }

        protected override void InitDict()
        {
            base.InitDict();
            if (id > 0)
            {
                var q = DB.x_question.FirstOrDefault(o => o.question_id == id);
                
                dict.Add("item", q);
                dict.Add("sub", q.subject + "|" + GetDictName("xx.subject", q.subject));
                dict.Add("bk", q.book + "|" + GetDictName("xx.book", q.book));
                dict.Add("cp", q.chapter + "|" + GetDictName("xx.chapter", q.chapter));
                dict.Add("kg", q.knowledge + "|" + GetDictName("xx.knowledge", q.knowledge, " "));
            }
        }

    }
}
