using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.answer
{
    public class del : xmg
    {
        public int id { get; set; }

        protected override XResp Execute()
        {
            var ans = DB.x_answer.FirstOrDefault(o => o.answer_id == id);
            if (ans == null)
                throw new XExcep("0x0012");

            DB.x_answer.DeleteOnSubmit(ans);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
