using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.fav {
    public class del : _web {
        protected override bool nduser {
            get {
                return true;
            }
        }
        public int cid { get; set; }
        public int tp { get; set; }
        protected override XResp Execute() {
            var p = cu.x_fav.FirstOrDefault(o => o.cid == cid && o.type == tp);
            if (p == null)
                return new XResp();
            //throw new XExcep("T收藏记录不存在");

            DB.x_fav.DeleteOnSubmit(p);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
