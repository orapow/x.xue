using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.order
{
    public class st : _web
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
            var od = cu.x_order.FirstOrDefault(o => o.order_id == id);
            if (od == null) throw new XExcep("T订单不存在");
            return new back() { status = od.status.Value };
        }
        class back : XResp
        {
            public int status { get; set; }
        }
    }
}
