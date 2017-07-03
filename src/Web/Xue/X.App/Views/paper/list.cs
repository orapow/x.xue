using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;

namespace X.App.Views.paper
{
    public class list : _web
    {
        public int bk { get; set; }
        public int gp { get; set; }
        public int tp { get; set; }
        public int ar { get; set; }
        public int pg { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "bk-gp-tp-ar-pg";
            }
        }

        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("papers", "nav-active");

            var q = from p in DB.x_paper
                    where (p.subject + "") == sub && (p.user_id == null || p.user_id == 0)
                    select p;

            if (bk > 0) q = q.Where(o => o.book == bk);
            if (gp > 0) q = q.Where(o => o.group.Contains("[" + gp + "]"));
            if (tp > 0) q = q.Where(o => o.type == tp);
            if (ar > 0) q = q.Where(o => o.area == ar);

            if (pg == 0) pg = 1;

            dict.Add("list", q.Skip((pg - 1) * 20).Take(20).ToList().Select(o => new
            {
                o.topic,
                mt = o.mtime.Value.ToString("yyyy-MM-dd"),
                dc = o.x_down.Count(),
                tp = GetDictName("paper.type", o.type),
                id = o.paper_id,
                fav = cu == null || cu.x_fav.Count(f => f.cid == o.paper_id && f.type == 2) == 0 ? 0 : 1
            }));
            dict.Add("pc", q.Count());

            var sb = GetDict("xx.subject", sub);
            dict.Add("bks", GetDictList("xx.book", "0").Where(o => (o.f3 + "") == sub).ToList());
            dict.Add("gps", GetDictList("xx.group", "0").Where(o => o.f3 == sb.f3).ToList());
            dict.Add("tps", GetDictList("paper.type", "0"));
            dict.Add("ars", GetDictList("xx.area", "0"));

            if (cu != null) dict.Add("vip", cu.etime > DateTime.Now);

        }

    }
}
