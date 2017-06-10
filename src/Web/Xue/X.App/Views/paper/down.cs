using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using X.Core.Utility;
using X.Data;
using X.Web;

namespace X.App.Views.paper
{
    public class down : _web
    {
        string[] nums = "|一|二|三|四|五|六|七|八|九|十|十一|十二|十三|十四|十五|十六|十七|十八|十九|二十".Split('|');
        x_paper pg = null;
        List<que> ques = null;
        Dictionary<string, string> imgs = new Dictionary<string, string>();
        config set = null;

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

        //public override byte[] GetResponse()
        //{
        //    InitView();
        //    var doc = new XWPFDocument();
        //    var p2 = doc.CreateParagraph();
        //    //XWPFRun r1 = p2.CreateRun();
        //    //r1.SetText("test");

        //    var r2 = p2.CreateRun();
        //    var fs = File.Open("d:\\1.jpg", FileMode.Open, FileAccess.Read);
        //    r2.AddPicture(fs, (int)PictureType.JPEG, "1.jpg", 400, 600);
        //    fs.Close();

        //    var dc = File.OpenWrite("d:\\1.docx");
        //    doc.Write(dc);
        //    doc.Close();

        //    var ms = new MemoryStream();
        //    doc.Write(ms);
        //    return ms.ToArray();
        //}

        protected override void InitView()
        {
            base.InitView();
            //var down = cu.x_down.FirstOrDefault(o => o.paper_id == id);
            pg = DB.x_paper.FirstOrDefault(o => o.paper_id == id);
            if (pg == null) throw new XExcep("T试卷不存在");

            Context.Response.ContentType = "applicationnd/msword";
            Context.Response.AddHeader("Content-Disposition", "inline; filename=" + pg.topic + " - " + cfg.name + ".doc");
        }

        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("paper", pg);
            var set = Serialize.FromJson<config>(pg.setting);
            dict.Add("set", set);
            var qids = Serialize.FromJson<Dictionary<long, int>>(pg.qids);
            dict.Add("qids", qids);
            ques = DB.x_question.Where(o => qids.Keys.Contains(o.question_id)).Select(o => new que
            {
                score = qids[o.question_id],
                topic = Context.Server.HtmlDecode(o.title),//.Replace("<p", "<span").Replace("</p>", "</span>"),
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
            dict.Add("imgs", imgs);
        }

        public List<p> GetQue(string qu)
        {
            var ps = new List<p>();
            var reg_p = new Regex("<p>(.*?)</p>");
            var reg_sp = new Regex("(<img[^>]+>)");
            var reg_w = new Regex("width=\"([^\"]+)\"");
            var reg_h = new Regex("height=\"([^\"]+)\"");
            var reg_v = new Regex("src=\"([^\"]+)\"");
            foreach (Match m in reg_p.Matches(qu))
            {
                if (m.Groups[1].Value == "<br>" || m.Groups[1].Value == "<br/>" || m.Groups[1].Value == "<br />") continue;
                var pgs = new Regex("(<img[^>]+>)").Split(m.Groups[1].Value);
                var gs = new List<p.g>();
                foreach (var s in pgs)
                {
                    var pg = new down.p.g();
                    var w = reg_w.Match(s);
                    if (w.Success)
                    {
                        pg.t = 2;
                        pg.w = int.Parse(w.Groups[1].Value) * 3 / 4.0;
                        var v = reg_v.Match(s);
                        if (v.Success)
                        {
                            var id = imgs.Count + 100;
                            imgs.Add(id + "", v.Groups[1].Value.Replace("data:image/png;base64,", ""));
                            pg.v = id + "";
                        }
                        var h = reg_h.Match(s);
                        if (h.Success) pg.h = int.Parse(h.Groups[1].Value) * 3 / 4.0;
                    }
                    else
                    {
                        pg.t = 1;
                        pg.v = s.Replace("&nbsp;", " ");
                    }
                    gs.Add(pg);
                }
                ps.Add(new down.p() { gs = gs });
            }
            dict["imgs"] = imgs.Select(o => new { key = o.Key, val = o.Value });
            return ps;
        }

        public bool getcfg(int k)
        {
            if (set == null) return false;
            if (!set.cfg.ContainsKey(k)) return false;
            return set.cfg[k];
        }

        public object GetQues(int t)
        {
            return ques.Where(o => o.type == t).ToList();
        }
        public string GetNum(int no)
        {
            return nums[no];
        }
        public class p
        {
            public List<g> gs { get; set; }
            public class g
            {
                public int t { get; set; }
                public double w { get; set; }
                public double h { get; set; }
                public string v { get; set; }
            }
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
