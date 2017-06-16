using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.paper
{
    public class del : _web
    {
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        public int id { get; set; }
        protected override XResp Execute()
        {
            var p = DB.x_paper.FirstOrDefault(o => o.user_id == cu.user_id && o.paper_id == id);
            if (p == null) throw new XExcep("T试卷不存在");
            if (p.x_down.Count() > 0) throw new XExcep("T当前试卷已经下载，不能删除，删除下载记录后方可删除试卷！");

            DB.x_paper.DeleteOnSubmit(p);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
