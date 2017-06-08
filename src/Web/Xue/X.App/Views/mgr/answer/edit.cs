using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.answer {
    public class edit : xmg {

        public int qid {
            get; set;
        }
        public int aid {
            get; set;
        }
        protected override string GetParmNames {
            get {
                return "qid-aid";
            }
        }

        protected override void InitDict() {
            base.InitDict();
            //试题有答案
            if (aid > 0) {
                var a = DB.x_answer.FirstOrDefault(o => o.answer_id == aid);
                var w = a.question_id;
                dict.Add("items", a);
                
            }
            //无答案显示试题
            var q = DB.x_question.FirstOrDefault(o => o.question_id == qid);
            var s = q.question_id;
            dict.Add("que", q);
            if(q != null){

                dict.Add("shiti",Context.Server.HtmlDecode(q.title));

            }

        }
    }
}
