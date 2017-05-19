using System.Linq;
using X.Web;

namespace X.App.Views.mgr.book
{
    public class edit : xmg
    {
        public int id { get; set; }
        public string pid { get; set; }
        public string sb { get; set; }

        protected override string GetParmNames
        {
            get
            {
                return "id-pid-sb";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();

            var up = pid;

            if (id > 0)
            {
                var ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
                sb = ent.f3 + "";
                up = ent.upval.Split('-').Last();
            }

            dict.Add("up", up);

            var sbv = GetDict("xx.subject", sb);
            if (sbv != null) dict.Add("sub", sb + "|" + GetDictName("xx.age", sbv.f3) + "-" + sbv.name);

        }
    }
}
