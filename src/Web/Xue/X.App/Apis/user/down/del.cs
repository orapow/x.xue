using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.down
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
            var p = cu.x_down.FirstOrDefault(o => o.down_id == id);
            if (p == null) throw new XExcep("T下载记录不存在");

            DB.x_down.DeleteOnSubmit(p);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
