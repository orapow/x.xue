using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.user.fav
{
    public class save : _web
    {
        public int cid { get; set; }
        public int tp { get; set; }
        public int sub { get; set; }
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }

        protected override XResp Execute()
        {
            var fa = DB.x_fav.FirstOrDefault(o => o.cid == cid && o.type == tp);
            if (fa == null)
                fa = new x_fav() { ctime = DateTime.Now, type = tp };

            fa.user_id = cu.user_id;
            fa.subject = sub;
            fa.cid = cid;
            fa.ctime = DateTime.Now;

            DB.x_fav.InsertOnSubmit(fa);
            SubmitDBChanges();

            return new XResp();
        }

    }
}
