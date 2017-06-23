using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.mgr
{
    public class main : xmg
    {

        public int st { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "st";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();

            dict.Add("tqc", DB.x_question.Count(o => o.ctime.Value.Date == DateTime.Now.Date));
            dict.Add("tpc", DB.x_paper.Count(o => (o.user_id == 0 || o.user_id == null) && o.ctime.Value.Date == DateTime.Now.Date));
            dict.Add("tdc", DB.x_down.Count(o => o.ctime.Value.Date == DateTime.Now.Date));
            dict.Add("tcc", DB.x_order.Where(o => o.status == 2 && o.etime != null && o.ctime.Value.Date == DateTime.Now.Date).Sum(o => o.amount) ?? 0);
            dict.Add("tsc", DB.x_paper.Count(o => o.user_id > 0 && o.ctime.Value.Date == DateTime.Now.Date));

            dict.Add("qc", DB.x_question.Count());
            dict.Add("pc", DB.x_paper.Count(o => o.user_id == 0 || o.user_id == null));
            dict.Add("dc", DB.x_down.Count());
            dict.Add("cc", DB.x_order.Where(o => o.status == 2 && o.etime != null).Sum(o => o.amount) ?? 0);
            dict.Add("sc", DB.x_paper.Count(o => o.user_id > 0));

        }
    }
}
