using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;

namespace X.App.Views.paper
{
    public class edit : _web
    {
        Dictionary<string, long[]> qids = null;
        string[] nums = "|一|二|三|四|五|六|七|八|九|十|十一|十二|十三|十四|十五|十六|十七|十八|十九|二十".Split('|');

        protected override void InitDict()
        {
            base.InitDict();

            var json = Context.Server.UrlDecode(GetReqParms("xx.cart." + sub));
            if (string.IsNullOrEmpty(json)) { dict.Add("empty", 1); return; }

            qids = Serialize.FromJson<Dictionary<string, long[]>>(json);
            if (qids == null || qids.Keys.Count == 0) dict.Add("empty", 1);

            dict.Add("ids", qids.Keys);
            dict.Add("date", DateTime.Now.ToString("yyyy年MM月dd日"));
        }

        public object GetQues(string k)
        {
            if (qids == null || !qids.Keys.Contains(k)) return null;
            var ques = DB.x_question.Where(o => qids[k].Contains(o.question_id));
            return ques.Select(o => new
            {
                topic = Context.Server.HtmlDecode(o.title),
                id = o.question_id,
                o.easy
            }).ToList();
        }

        public string GetNum(int no)
        {
            return nums[no];
        }
    }
}
