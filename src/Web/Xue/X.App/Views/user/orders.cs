using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.user
{
    public class orders : _web
    {
        public int page { get; set; }
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        protected override string GetParmNames
        {
            get
            {
                return "page";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("orders", "using-active");

            if (page == 0) page = 1;

            var q = from p in cu.x_order
                    where p.user_del != true
                    select p;

            dict.Add("pc", q.Count());

            dict.Add("ps", q.OrderByDescending(o => o.ctime)
                .Skip((page - 1)).Take(20)
                .ToList()
                .Select(o => new
                {
                    id = o.order_id,
                    o.desc,
                    o.amount,
                    pf = o.platform == 1 ? "微信" : "支付宝",
                    o.no,
                    date = o.ctime.Value.ToString("yyyy-MM-dd"),
                    o.status,
                    st = o.status == 1 ? "待付款" : o.status == 2 ? "完成" : "已取消"
                }));

        }
    }
}
