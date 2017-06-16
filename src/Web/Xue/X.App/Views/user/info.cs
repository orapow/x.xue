using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.user
{
    public class info : _web
    {
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("info", "using-active");
            dict.Add("exp", cu.etime < DateTime.Now);
        }
    }
}
