using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.user
{
    public class papers : _web
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
            dict.Add("papers", "using-active");

            if (page == 0) page = 1;

            var q = from p in DB.x_paper
                    where p.user_id == cu.user_id
                    select p;

            dict.Add("pc", q.Count());

            dict.Add("ps", q.OrderByDescending(o => o.ctime)
                .Skip((page - 1)).Take(10)
                .ToList()
                .Select(o => new
                {
                    id = o.paper_id,
                    o.topic,
                    dc = o.x_down.Count(),
                    date = o.ctime.Value.ToString("yyyy-MM-dd"),
                    sub = getsubject(o.subject.Value)
                }));

        }

        string getsubject(int v)
        {
            var d = GetDict("xx.subject", v + "");
            return GetDictName("xx.age", d.f3) + d.name;
        }
    }
}
