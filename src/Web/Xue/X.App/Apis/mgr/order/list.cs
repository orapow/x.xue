using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.order {
    public class list : xmg {
        public int page { get; set; }
        public int limit { get; set; }
        public int st { get; set; }
        public string key { get; set; }
        protected override XResp Execute() {
            var r = new Resp_List();
            var q = from o in DB.x_order
                    select o;

            //if (mg.x_role.power != "###") q = q.Where(o => o.city == mg.city);

            if (st > 0) {
                if (st == 1)
                    q = q.Where(o => o.pid == null);
                else
                    q = q.Where(o => o.pid != null);
            }

            if (!string.IsNullOrEmpty(key))
                q = q.Where(o => o.no == key || o.desc.Contains(key) || o.x_user.name.Contains(key));

            r.count = q.Count();

            var sts = "|待付款|已完成|已取消".Split('|');

            r.items = q.OrderByDescending(o => o.ctime).Skip((page - 1) * limit).Take(limit).ToList().Select(o => new {
                id = o.order_id,
                platform = o.platform == 1 ? "微信" : "支付宝",
                etime = o.etime == null ? "" : o.etime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                uid = o.user_id,
                un = o.x_user.name,
                up = o.x_user.headimg,
                gs = string.Join(" ", DB.x_paper.Where(d => d.paper_id == o.pid).Select(d => d.topic)),
                o.no,
                o.status,
                o.desc,
                st_name = o.status > 0 && o.status < 7 ? sts[o.status.Value] : "未知：" + o.status,
                yf_amount = o.amount,

                ctime = o.ctime.Value.ToString("yyyy-MM-dd HH:mm:ss"),

            });

            r.page = page;

            return r;
        }

    }
}
