using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.order {
    public class del:xmg {
        public int id { get; set;}
        protected override XResp Execute() {
            var ord = DB.x_order.FirstOrDefault(o => o.order_id == id);
            if (ord == null)
                throw new XExcep("0x0012");
            DB.x_order.DeleteOnSubmit(ord);
            SubmitDBChanges();
            return new XResp();
        }
       
    }
}
