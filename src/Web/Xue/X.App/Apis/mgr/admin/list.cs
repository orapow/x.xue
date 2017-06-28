using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.admin {
    public class list : xmg {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        protected override Web.Com.XResp Execute() {
            var r = new Resp_List();
            r.page = page;

            var q = from ad in DB.x_mgr
                        //orderby ad.ctime descending
                    select ad;

            if (!string.IsNullOrEmpty(key)) {
                q = q.Where(o => o.tel.Contains(key) || o.name.Contains(key) || o.uin.Contains(key));
            }

            r.items = q.Skip((page - 1) * limit).Take(limit).ToList().Select(o => new {
                o.mgr_id,
                o.uin,
                o.name,
                o.tel,
                //role = (ad.role_id == 3) ? "总管理员" : (ad.role_id == 2 ? "财务" : "客服"),
                o.email,
                ctime = (o.ctime) == null ? (" ") : (o.ctime.Value.ToString("yyyy-MM-dd HH:mm"))
                //ad.pwd
            });
            r.count = q.Count();
            return r;
        }

    }
}
