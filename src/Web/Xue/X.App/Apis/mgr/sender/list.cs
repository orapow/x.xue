﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.sender
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        protected override int powercode
        {
            get
            {
                return 2;
            }
        }

        protected override XResp Execute()
        {
            var r = new Resp_List();
            r.page = page;

            var q = from d in GetDictList("user.sender", "0")
                    select new
                    {
                        d.id,
                        d.name,
                        tel = d.f3,
                        isbind = string.IsNullOrEmpty(d.jp) ? "未绑定" : "已绑定",
                        d.img,
                        date = d.f4
                    };

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.name.Contains(key));

            r.items = q.Skip((page - 1) * limit).Take(limit);
            r.count = q.Count();
            return r;
        }

    }
}
