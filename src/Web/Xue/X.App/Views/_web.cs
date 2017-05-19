using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;

namespace X.App.Views
{
    public class _web : xview
    {
        protected string sub = "";
        protected override void InitView()
        {
            base.InitView();
            sub = GetReqParms("xx.sub");
        }
        protected override void InitDict()
        {
            base.InitDict();
            x_dict sb = null;
            if (!string.IsNullOrEmpty(sub)) sb = GetDict("xx.subject", sub);
            else sb = getSubject(1).FirstOrDefault();
            if (sb != null)
            {
                sub = sb.value;
                dict.Add("sb", sb.value);
                dict.Add("sbname", GetDictName("xx.age", sb.f3) + " " + sb.name);
            }
        }

        public List<x_dict> getSubject(int age)
        {
            return GetDictList("xx.subject", "00").Where(o => o.f3 == age).ToList();
        }
    }
}
