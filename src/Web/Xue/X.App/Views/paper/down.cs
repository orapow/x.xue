using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;

namespace X.App.Views.paper
{
    public class down : _web
    {
        string[] nums = "|一|二|三|四|五|六|七|八|九|十|十一|十二|十三|十四|十五|十六|十七|十八|十九|二十".Split('|');
        x_paper p = null;
        List<que> ques = null;

        public int id { get; set; }

        protected override bool nduser
        {
            get
            {
                return true;
            }
        }

        protected override string GetParmNames
        {
            get
            {
                return "id";
            }
        }

        public override byte[] GetResponse()
        {
            InitView();
            var doc = new XWPFDocument();
            var p2 = doc.CreateParagraph();
            //XWPFRun r1 = p2.CreateRun();
            //r1.SetText("test");

            var r2 = p2.CreateRun();
            var fs = File.Open("d:\\1.jpg", FileMode.Open, FileAccess.Read);
            r2.AddPicture(fs, (int)PictureType.JPEG, "1.jpg", 400, 600);
            fs.Close();

            var dc = File.OpenWrite("d:\\1.docx");
            doc.Write(dc);
            doc.Close();

            var ms = new MemoryStream();
            doc.Write(ms);
            return ms.ToArray();
        }



        protected override void InitView()
        {
            base.InitView();
            //var down = cu.x_down.FirstOrDefault(o => o.paper_id == id);
            p = DB.x_paper.FirstOrDefault(o => o.paper_id == id);
            if (p == null) throw new XExcep("T试卷不存在");

            Context.Response.ContentType = "applicationnd/msword";
            Context.Response.AddHeader("Content-Disposition", "inline; filename=" + p.topic + " - " + cfg.name + ".doc");
        }

        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("paper", p);
            dict.Add("set", Serialize.FromJson<config>(p.setting));
            var qids = Serialize.FromJson<Dictionary<long, int>>(p.qids);
            dict.Add("qids", qids);
            ques = DB.x_question.Where(o => qids.Keys.Contains(o.question_id)).Select(o => new que
            {
                score = qids[o.question_id],
                topic = Context.Server.HtmlDecode(o.title).Replace("<p", "<span").Replace("</p>", "</span>"),
                type = o.topic.Value
            }).ToList();
            dict.Add("pts", ques.GroupBy(o => o.type)
                .Select(o =>
                    new
                    {
                        id = o.Key,
                        name = GetDictName("question.topic", o.Key),
                        score = o.Sum(t => t.score),
                        count = o.Count()
                    })
                .ToList()
            );
        }
        public object GetQues(int t)
        {
            return ques.Where(o => o.type == t).ToList();
        }
        public string GetNum(int no)
        {
            return nums[no];
        }
        class que
        {
            public string topic { get; set; }
            public int type { get; set; }
            public int score { get; set; }
        }
        class config
        {
            public Dictionary<int, bool> cfg { get; set; }
            public string topic { get; set; }
            public string stopic { get; set; }
            public string time { get; set; }
            public int score { get; set; }
            public string notice { get; set; }
        }
    }
}
