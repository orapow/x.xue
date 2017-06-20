using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.paper
{
    public class save : xmg
    {
        public int id { get; set; }
        public int sub { get; set; }
        public int book { get; set; }
        public string topic { get; set; }
        public int bk { get; set; }
        public int area { get; set; }
        public int type { get; set; }
        public string group { get; set; }
        public decimal price { get; set; }

        public string stopic { get; set; }
        public string notice { get; set; }
        public int time { get; set; }
        public int score { get; set; }

        public string papercfg { get; set; }

        protected override XResp Execute()
        {
            x_paper pap = null;

            if (papercfg == null) papercfg = "";

            var pcfg = new
            {
                cfg = new Dictionary<string, bool>() {
                    { "2",papercfg.Contains("[2]")},
                    { "3",papercfg.Contains("[3]")},
                    { "4",papercfg.Contains("[4]")},
                    { "5",papercfg.Contains("[5]")},
                    { "7",papercfg.Contains("[7]")},
                    { "9",papercfg.Contains("[9]")},
                    { "10",papercfg.Contains("[10]")},
                    { "11",papercfg.Contains("[11]")},
                    { "12",papercfg.Contains("[12]")}
                },
                topic = topic,
                stopic = stopic,
                time = time,
                score = score,
                notice = notice
            };

            if (id > 0)
                pap = DB.x_paper.FirstOrDefault(o => o.paper_id == id);
            if (pap == null)
                pap = new x_paper() { ctime = DateTime.Now, qcount = 0 };

            pap.mtime = DateTime.Now;
            pap.subject = sub;
            pap.book = book;
            pap.type = type;
            pap.topic = topic;
            pap.area = area;
            pap.score = score;
            pap.group = group;
            pap.price = price;
            pap.setting = Serialize.ToJson(pcfg);

            if (id == 0)
                DB.x_paper.InsertOnSubmit(pap);

            SubmitDBChanges();

            return new XResp();
        }

    }
}
