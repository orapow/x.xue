using System;
using System.Collections.Generic;
using System.Linq;
using X.App.Com;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;
using X.Web.Views;

namespace X.App.Views.mgr
{
    public class xmg : xview
    {
        protected x_mgr mg = null;

        protected virtual bool nduser { get { return true; } }

        protected override void InitDict()
        {
            base.InitDict();

            var key = GetReqParms("mgr_ad");// Context.Request.Cookies["ad"];
            var id = CacheHelper.Get<long>("mgr." + key); //CacheHelper.Get<x_mgr>("mgr." + id);

            mg = DB.x_mgr.FirstOrDefault(o => o.mgr_id == id);
            if (mg == null && nduser) throw new XExcep("0x0005");

            CacheHelper.Save("mgr." + key, id, 60 * 20);
            dict.Add("mg", mg);
        }
    }
}
