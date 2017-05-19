using System.Linq;
using X.Web;

namespace X.App.Views.mgr.chapter
{
    public class edit : xmg
    {
        public int id { get; set; }
        public string pid { get; set; }
        public string bk { get; set; }

        protected override string GetParmNames
        {
            get
            {
                return "id-pid-bk";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();

            if (id > 0)
            {
                var ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
                bk = ent.f3 + "";
                pid = ent.upval.Split('-').Last();
            }

            var up = GetDict("xx.chapter", pid);
            if (up != null) dict.Add("up", pid + "|" + up.name);

            var bkv = GetDict("xx.book", bk);
            if (bkv != null)
            {
                dict.Add("bkv", bk + "|" + GetDictName("xx.book", bkv.upval) + "-" + bkv.name);
                var sub = GetDict("xx.subject", bkv.f3 + "");
                if (sub != null) dict.Add("sub", bkv.f3 + "|" + GetDictName("xx.age", sub.f3) + "-" + sub.name);
            }

        }
    }
}
