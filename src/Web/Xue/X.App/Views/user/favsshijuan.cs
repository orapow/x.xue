using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.user {
    public class favsshijuan : _web {
        public int page { get; set; }
        protected override bool nduser {
            get {
                return true;
            }
        }
        //public int tp { get; set; }
        //protected override string GetParmNames {
        //    get {
        //        return "tp";
        //    }
        //}
        protected override void InitDict() {
            base.InitDict();
            var q = from p in cu.x_fav
                    where p.type==2
                    select p;

            dict.Add("pc", q.Count());
            
            if (1== 1) {
                dict.Add("ps", q.OrderByDescending(o => o.ctime)
                    .Skip((page - 1)).Take(10)
                    .ToList()
                    .Select(o => new {
                        id = o.fav_id,
                        title = DB.x_paper.FirstOrDefault(p => p.paper_id == o.cid).topic,
                        type = GetDictName("paper.type", DB.x_paper.FirstOrDefault(p => p.paper_id == o.cid).type),
                        date = DB.x_down.FirstOrDefault(d => d.paper_id == o.cid),
                        o.user_id,
                        o.group,
                        sub = GetDictName("xx.subject",o.subject),
                        o.cid,

                    }));
               
            }
            

         //   dict.Add("favs", "using-active");
        }
    }
}
