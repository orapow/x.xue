using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.book
{
    public class save : xmg
    {
        public int id { get; set; }
        [ParmsAttr(req = true, name = "名称")]
        public string name { get; set; }
        public string upv { get; set; }
        public int subject { get; set; }
        public int sort { get; set; }


        protected override XResp Execute()
        {
            x_dict ent = null;
            var code = "xx.book";
            if (id > 0) ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) ent = new x_dict() { code = code };

            ent.name = name;
            ent.f3 = subject;
            ent.sort = sort;

            if (ent.id == 0)
            {
                DB.x_dict.InsertOnSubmit(ent);
                SubmitDBChanges();
            }

            if (upv == ent.value || string.IsNullOrEmpty(upv)) upv = "0";

            ent.upval = upv;
            ent.value = ent.dict_id + "";

            SubmitDBChanges();

            CacheHelper.Remove("dict." + ent.code);

            return new XResp();
        }
    }
}
