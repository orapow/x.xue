using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace X.App.Views
{
    public class paper : xview
    {
        public int id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            
            


        }
    }
}
