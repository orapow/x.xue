using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.paper.question
{
    public class del : xmg
    {
        public int id { get; set; }
        public int pid { get; set; }

        protected override XResp Execute()
        {
            x_paper pap = pap = DB.x_paper.FirstOrDefault(o => o.paper_id == pid);
            if (pap == null) throw new XExcep("T试卷不存在");

            if (string.IsNullOrEmpty(pap.qids)) throw new XExcep("T试题不存在");
            var ids = Serialize.FromJson<Dictionary<string, int>>(pap.qids);
            if (ids == null) throw new XExcep("T试题不存在");

            ids.Remove(id + "");
            pap.qcount = ids.Count;
            pap.qids = Serialize.ToJson(ids);

            SubmitDBChanges();

            return new XResp();
        }
    }
}
