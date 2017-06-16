using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;

namespace X.App.Views
{
    public class _web : xview
    {
        protected string sub = "";
        protected x_user cu { get; set; }

        protected virtual bool nduser { get { return false; } }

        protected override void InitView()
        {
            base.InitView();
            sub = GetReqParms("xx.sub");

            x_dict sb = null;
            if (!string.IsNullOrEmpty(sub)) sb = GetDict("xx.subject", sub);
            else sb = getSubject(1).FirstOrDefault();
            if (sb != null) sub = sb.value;

            dict.Add("sbname", GetDictName("xx.age", sb.f3) + " " + sb.name);

            var uk = GetReqParms("ukey");
            if (!string.IsNullOrEmpty(uk)) cu = DB.x_user.FirstOrDefault(o => o.ukey == uk);
            //cu = DB.x_user.FirstOrDefault(o => o.user_id == 1);

            if (nduser && cu == null) Context.Response.Redirect("/login-" + uk + "-" + Secret.ToBase64(Context.Request.RawUrl) + ".html");

            Context.Response.SetCookie(new System.Web.HttpCookie("xx.sub", sub));

        }
        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("sb", sub);
            if (cu != null) dict.Add("cu", cu);
        }

        public List<x_dict> getSubject(int age)
        {
            return GetDictList("xx.subject", "00").Where(o => o.f3 == age).ToList();
        }
    }
}
