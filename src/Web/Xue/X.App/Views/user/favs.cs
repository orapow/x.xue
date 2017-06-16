using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X.App.Views.user
{
    public class favs : _web
    {
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        public int tp { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "tp";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("favs" + tp, "using-active");
        }
    }
}
