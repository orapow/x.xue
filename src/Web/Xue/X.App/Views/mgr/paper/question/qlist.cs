using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr.paper.question {
    public class qlist : xmg {
        public int p_id { get; set; }
        protected override string GetParmNames {
            get {
                return "p_id";
            }
        }

        protected override void InitDict() {
            base.InitDict();
            if (p_id > 0) {
                var q = DB.x_paper.FirstOrDefault(o => o.paper_id == p_id);
                dict.Add("sub", q.subject + "|" + GetDictName("xx.subject", q.subject));
                dict.Add("item", q);
            }

        }

    }
}
