using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.user.fav {
    public class save : _web {
        public int id { get; set; }
        public int type { get; set; }
        public int sub { get; set; }
        protected override bool nduser {
            get {
                return true;
            }
        }

        protected override XResp Execute() {
            // cu.x_fav.Add = null;
            x_fav fa = null;
            if ((DB.x_fav.FirstOrDefault(o => o.cid == id)==null?true:false))
                fa = new x_fav();
            if (type == 1) {
                fa.type = type;
                fa.group = null;
            }
            else if (type == 2) {
                fa.type = type;

            }
            fa.user_id = cu.user_id;
            fa.subject = sub;
            fa.cid = id;
            fa.ctime = DateTime.Now;

            DB.x_fav.InsertOnSubmit(fa);
            SubmitDBChanges();

            return new XResp();
        }

    }
}
