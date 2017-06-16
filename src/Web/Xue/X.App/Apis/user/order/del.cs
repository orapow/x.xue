using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.order
{
    public class del : _web
    {
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        public int id { get; set; }
        protected override XResp Execute()
        {
            var p = cu.x_order.FirstOrDefault(o => o.order_id == id);
            if (p == null) throw new XExcep("T订单记录不存在");

            p.user_del = true;
            SubmitDBChanges();

            return new XResp();
        }
    }
}
