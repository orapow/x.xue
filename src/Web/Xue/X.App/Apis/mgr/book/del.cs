﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.mgr.book
{
    public class del : xmg
    {
        [ParmsAttr(min = 1)]
        public int id { get; set; }

        protected override XResp Execute()
        {
            var ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
            if (ent == null) throw new XExcep("0x0013");

            var upv = "";
            if (ent.upval != "0") upv = ent.upval + "-" + ent.value;
            else upv = ent.value;

            var childs = DB.x_dict.Where(o => o.upval.StartsWith(upv));
            foreach (var e in childs.ToList())
            {
                if (e.upval == ent.value) e.upval = ent.upval;
                if (ent.upval == "0") e.upval = e.upval.Replace(ent.value + "-", "");
                else e.upval = e.upval.Replace("-" + ent.value, "");
            }

            DB.x_dict.DeleteOnSubmit(ent);

            CacheHelper.Remove("dict." + ent.code);

            SubmitDBChanges();

            return new XResp();
        }
    }
}
