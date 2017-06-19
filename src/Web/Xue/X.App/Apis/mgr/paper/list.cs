using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.paper {
    public class list : xmg {
        public int id { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        protected override XResp Execute() {
            var q = from pa in DB.x_paper
                    select pa;
            if (!string.IsNullOrEmpty(key))
                q = q.Where(o => o.topic.Contains(key));
            var list = q.OrderBy(o => o.user_id).Skip((page - 1) * limit).Take(limit);
            
            var r = new Resp_List();
            r.page = page;
            r.count = q.Count();
            r.items = list.ToList().Select(o => new {
                p_id = o.paper_id,
                subject = getSub(o.subject.Value),
                o.topic,
                o.qids,
                ctime=o.ctime.Value.ToString("yyyy-MM-dd HH:mm"),
                o.user_id,
                o.area,
                o.qcount,
                o.group,
                o.price

            });
            return r;
        }

        public string getSub(int s) {
            var sb = GetDict("xx.subject", s+"");
            return GetDictName("xx.age", sb.f3) + "->" + sb.name;
        }
    }
}
