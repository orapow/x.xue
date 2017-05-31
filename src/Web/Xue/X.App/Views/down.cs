using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace X.App.Views
{
    public class down : xview
    {
        public override byte[] GetResponse()
        {
            Context.Response.ContentType = "applicationnd/msword";
            Context.Response.AddHeader("Content-Disposition", "inline; filename=" + "down.doc");
            //Context.Server.Transfer("/tpl/paper.html");
            return File.ReadAllBytes(Context.Server.MapPath("/tpl/paper.html"));
        }
    }
}
