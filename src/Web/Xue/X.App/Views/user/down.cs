using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.user
{
    public class down : _web
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
            dict.Add("down", "using-active");

            if (page == 0) page = 1;

            var q = from p in cu.x_down
                    select p;

            dict.Add("pc", q.Count());

            dict.Add("ps", q.OrderByDescending(o => o.ctime)
                .Skip((page - 1)).Take(10)
                .ToList()
                .Select(o => new
                {
                    id = o.paper_id,
                    o.x_paper.topic,
                    date = o.ctime.Value.ToString("yyyy-MM-dd"),
                    sub = getsubject(o.x_paper.subject.Value)
                }));

        }

        string getsubject(int v)
        {
            var d = GetDict("xx.subject", v + "");
            return GetDictName("xx.age", d.f3) + d.name;
        }

    }
}
