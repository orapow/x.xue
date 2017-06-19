using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.paper.question {
    public class del : xmg {
        public int id { get; set; }
        public int pid { get; set; }
        protected override int powercode {
            get {
                return 1;
            }
        }

        protected override XResp Execute() {
            x_paper pap = null;
            if (pid > 0)
                pap = DB.x_paper.FirstOrDefault(o => o.paper_id == pid);
            if (pap == null)
                pap = new x_paper();
            var ids = X.Core.Utility.Serialize.FromJson<Dictionary<string, int>>(pap.qids);
            ids.Remove(id + "");
            
            pap.qcount = ids.Count;
            string s = X.Core.Utility.Serialize.ToJson(ids);
            pap.qids = s;
            

            if (pid == 0)
                DB.x_paper.InsertOnSubmit(pap);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
