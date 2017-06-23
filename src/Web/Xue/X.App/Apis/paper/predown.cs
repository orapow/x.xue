using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.paper
{
    public class predown : _web
    {
        [ParmsAttr(name = "试卷ID")]
        public int pid { get; set; }
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        protected override XResp Execute()
        {
            var p = DB.x_paper.FirstOrDefault(o => o.paper_id == pid);
            if (p == null) throw new XExcep("T试卷不存在");

            var d = p.x_down.OrderByDescending(o => o.ctime).FirstOrDefault();

            if (d == null)
            {
                d = new x_down() { ctime = DateTime.Now, paper_id = p.paper_id, user_id = cu.user_id, dkey = Secret.MD5(Guid.NewGuid().ToString()) };
                p.x_down.Add(d);
            }
            else if (cfg.down_repeat <= 0) return new XResp();//不许重复下载

            if (cu.etime == null || cu.etime < DateTime.Now || d.ctime.Value < DateTime.Now.AddDays(-(int)cfg.down_repeat))
                return new XResp();

            SubmitDBChanges();
            return new XResp() { msg = d.dkey };
        }
    }
}
