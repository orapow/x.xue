using System.Linq;
using X.Web;

namespace X.App.Views.mgr.subject
{
    public class edit : xmg
    {
        public string val { get; set; }
        [ParmsAttr(name = "年级")]
        public int gp { get; set; }

        protected override string GetParmNames
        {
            get
            {
                return "val-gp";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            if (!string.IsNullOrEmpty(val))
            {
                var ent = DB.x_dict.FirstOrDefault(o => o.code == "xx.subject" && o.value == val);
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
                //gp = ent.f3.Value;
            }
            //if (gp > 0) dict.Add("group", gp + "|" + GetDictName("xx.group", gp));
        }
    }
}
