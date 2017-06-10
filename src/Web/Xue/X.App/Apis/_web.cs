using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;

namespace X.App.Apis
{
    public class _web : xapi
    {
        protected x_user cu = null;

        protected virtual bool nduser { get { return true; } }

        protected override void InitApi()
        {
            base.InitApi();

            //var uk = GetReqParms("ukey");
            //if (!string.IsNullOrEmpty(uk)) cu = CacheHelper.Get<x_user>(uk);
            cu = DB.x_user.FirstOrDefault(o => o.user_id == 1);

            if (nduser && cu == null) throw new XExcep("T用户登陆超时");

        }
    }
}
