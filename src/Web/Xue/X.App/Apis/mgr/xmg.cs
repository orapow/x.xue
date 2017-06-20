using System;
using System.Collections.Generic;
using System.Linq;
using X.Core.Cache;
using X.Data;
using X.Web;

namespace X.App.Apis.mgr
{
    public class xmg : xapi
    {
        protected x_mgr mg = null;

        protected override void InitApi()
        {
            base.InitApi();

            var key = GetReqParms("mgr_ad");
            var id = CacheHelper.Get<long>("mgr." + key);

            mg = DB.x_mgr.FirstOrDefault(o => o.mgr_id == id);
            if (mg == null) throw new XExcep("0x0005");

            CacheHelper.Save("mgr." + key, id, 60 * 20);
        }

    }
}
