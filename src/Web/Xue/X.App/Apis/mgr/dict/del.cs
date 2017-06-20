using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.App.Com;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.dict {
    public class del : xmg {
        public int id { get; set;}

        protected override XResp Execute() {
            var dic = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (dic == null)
                throw new XExcep("0x0012");
            DB.x_dict.DeleteOnSubmit(dic);

            CacheHelper.Remove("dict." + dic.code);
            SubmitDBChanges();

            return new XResp();
        }
    }
}
