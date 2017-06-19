using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.paper {
    public class save : xmg {
        public int id { get; set; }
        public int user_id { get; set; }
        public int sub { get; set; }
        public string topic { get; set; }
        public int area { get; set; }
        public int group { get; set; }
        public decimal price { get;set;}

        protected override XResp Execute() {
            x_paper pap = null;
            if (id > 0)
                pap = DB.x_paper.FirstOrDefault(o => o.paper_id == id);
            if (pap == null)
                pap = new x_paper() { ctime = DateTime.Now };
            pap.mtime = DateTime.Now;
            pap.user_id = user_id;
            pap.subject = sub;
            pap.topic = topic;
            pap.area = area;
            pap.group = group;
            pap.price = price;




            if (id == 0)
                DB.x_paper.InsertOnSubmit(pap);
            SubmitDBChanges();

            return new XResp();
        }

    }
}
