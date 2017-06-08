using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.answer {
    public  class save:xmg {
        public int qid {get;set; }
        public int aid { get; set; }
        public string result {get;set; }
        public string remark { get; set; }
        public string no {get;set;}

        protected override XResp Execute() {
            x_answer ans = null;
            if (aid > 0)
                ans = DB.x_answer.FirstOrDefault(o => o.answer_id == aid);
            if (ans == null)
                ans = new x_answer();
            ans.question_id = qid;
            ans.result = result;
            ans.remark = remark;
            ans.no = no;
            if (aid == 0)
                DB.x_answer.InsertOnSubmit(ans);
            SubmitDBChanges();

            return new XResp() { msg = ans.answer_id + "" };
        }
    }
}
