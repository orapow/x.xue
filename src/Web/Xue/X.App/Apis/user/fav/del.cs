using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.fav
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
            var p = cu.x_fav.FirstOrDefault(o => o.fav_id == id);
            if (p == null) throw new XExcep("T收藏记录不存在");

            DB.x_fav.DeleteOnSubmit(p);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
