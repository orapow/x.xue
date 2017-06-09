using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.dict {
    public class list : xmg {
        public int id {
            get; set;
        }
        public int page {
            get; set;
        }
        public int limit {
            get; set;
        }
        public string key {
            get; set;
        }
        public string code {
            get;
            set;
        }
        protected override XResp Execute() {
            var q = GetDictList(code, "0").OrderBy(o => o.value).Skip((page - 1) * limit).Take(limit);
            if (!string.IsNullOrEmpty(key))
                q = q.Where(o => o.name.Contains(key));
            var r = new Resp_List();
            r.page = page;
            r.count = q.Count();
            r.items = q.Select(o => new {
                id = o.dict_id,
                name = o.name,
                value = o.value
            });
            return r;
        }
    }
}
