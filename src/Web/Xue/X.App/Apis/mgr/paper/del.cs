using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.paper
{
    public class del : xmg
    {
        public int id { get; set; }

        protected override XResp Execute()
        {
            var pap = DB.x_paper.FirstOrDefault(o => o.paper_id == id);
            if (pap == null) throw new XExcep("0x0012");

            DB.x_paper.DeleteOnSubmit(pap);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
