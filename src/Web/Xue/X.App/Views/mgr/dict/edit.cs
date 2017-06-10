using System.Linq;
using X.Web;

namespace X.App.Views.mgr.dict {
    public class edit : xmg {
        public int id {
            get; set;
        }
        public string pid {
            get; set;
        }
        public string code {
            get; set;
        }
        protected override string GetParmNames {
            get {
                return "id-code";
            }
        }
        protected override void InitDict() {
            base.InitDict();

            if (id > 0) {
                var ent = DB.x_dict.FirstOrDefault(o => o.dict_id == id);
                if (ent == null)
                    throw new XExcep("0x0005");
                dict.Add("item", ent);
                pid = ent.upval.Split('-').Last();
            }
            var q = DB.x_dict.FirstOrDefault(o => o.code == code);
            dict.Add("coe", q);
            //var up = GetDict("question.topic", pid);
            //if (up != null)
            //    dict.Add("up", pid + "|" + up.name);

          

        }
    }
}
