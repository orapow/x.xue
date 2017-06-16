using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.App.Com;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;
using X.Web;

namespace X.App.Views
{
    public class logout : _web
    {
        protected override void InitView()
        {
            base.InitView();
            var uk = GetReqParms("ukey");
            Sdk.Logout(uk);
            Context.Response.SetCookie(new System.Web.HttpCookie("ukey", ""));
            Context.Response.Redirect("/index.html");
        }
    }
}
