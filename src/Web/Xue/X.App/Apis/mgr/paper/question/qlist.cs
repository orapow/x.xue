using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.paper.question {
    public class qlist:xmg {
        public int id { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string topic { get; set; }
        public string qids { get; set; }
        public string qtype { get; set; }

        protected override XResp Execute() {

            var p = DB.x_paper.FirstOrDefault(o => o.paper_id == id);

            var ids = X.Core.Utility.Serialize.FromJson<Dictionary<string, int>>(p.qids);

            var r = new Resp_List();

            r.items = DB.x_question.Where(o => ids.Keys.Contains(o.question_id + "")).Select(o => new {
                qid = o.question_id,
                cot = Context.Server.HtmlDecode(o.title),
                score = o.score,
                type = GetDictName("question.topic", o.topic),
                pid = p.paper_id,
                ctime = o.ctime
            });


            //var q = from pa in DB.x_paper
            //        where pa.paper_id == id
            //        select pa;

            //string jsonString = q.FirstOrDefault().qids;

            //string sArras = jsonString.Replace('\"', ' ').Replace('{', ' ').Replace('}', ' ');
            //string[] sArray = sArras.Split(',');
            //string qi = "";

            //object[] qids = new object[sArray.Length];

            //var r = new Resp_List();

            //for (int i = 0; i < sArray.Length; i++) {
            //    //获取试卷的每个试题的id
            //    //LastIndexOf 以特定字符截取
            //    qi = sArray[i].Substring(1, sArray[i].LastIndexOf(":") - 1);
            //    qids[i] = DB.x_question.FirstOrDefault(o => o.question_id == Int32.Parse(qi)).question_id;
            //}

            //r.items = DB.x_question.Where(o => qids.Contains(o.question_id + "")).Select(o => new {
            //    qid=qids,
            //    cot = Context.Server.HtmlDecode(o.title),
            //    score = o.score,
            //    type = GetDictName("question.topic", o.topic)

            //});

            return r;

        }
    }
}
