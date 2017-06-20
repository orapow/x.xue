using System;
using System.Collections.Generic;
using System.Linq;
using X.Core.Cache;
using X.Web.Com;

namespace X.App.Apis.mgr
{
    public class logout : xapi
    {
        protected override XResp Execute()
        {
            var k = GetReqParms("mgr_ad");
            CacheHelper.Remove("mgr." + k);
            return new XResp();
        }
    }
}
