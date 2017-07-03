using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Views.paper
{
    public class view : _web
    {
        Dictionary<string, int> ids = null;
        public int id { get; set; }
        string[] nums = "|一|二|三|四|五|六|七|八|九|十|十一|十二|十三|十四|十五|十六|十七|十八|十九|二十".Split('|');

        protected override string GetParmNames
        {
            get
            {
                return "id";

            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            var paper = DB.x_paper.FirstOrDefault(o => o.paper_id == id);

            if (paper == null)
                throw new XExcep("T试卷不存在");

            dict.Add("paper", DB.x_paper.Where(o => o.paper_id == id).ToList().Select(o => new
            {
                o.topic,
                ctime = o.ctime.Value.ToString("yyyy-MM-dd"),
                dc = o.x_down.Count(),
                tp = GetDictName("paper.type", o.type),
                area = GetDictName("xx.area", o.area)
            }).FirstOrDefault());

            ids = Serialize.FromJson<Dictionary<string, int>>(paper.qids);

            var ques = DB.x_question.Where(o => ids.Keys.Contains(o.question_id + ""));

            var big = ques.GroupBy(o => o.topic).Select(o => new
            {
                name = GetDictName("question.topic", o.Key),
                o.Key,
                count = o.Count(),
                score = o.Sum(q => q.score),
            });

            dict.Add("bigs", big);

            //获取相关试卷

            var paps = DB.x_paper.Where(o => o.book == paper.book && o.user_id == null && o.paper_id != id).OrderByDescending(o => o.mtime).Take(10);
            dict.Add("paps", paps.ToList());

            //var json = Context.Server.UrlDecode(GetReqParms("xx.cart." + sub));
            //qids = Serialize.FromJson<Dictionary<string, long[]>>(json);
            //if (qids == null || qids.Keys.Count == 0)
            //    dict.Add("empty", 1);

            //dict.Add("idss", qids.Keys);



            //if (cu != null)
            //    dict.Add("vip", cu.etime > DateTime.Now);


            //// var paper = DB.x_paper.FirstOrDefault(o => o.paper_id == id);

            ////dict.Add("paper", paper);
            //dict.Add("paper", DB.x_paper.Where(o => o.paper_id == id).OrderBy(o => o.ctime).ToList().Select(o => new {
            //    o.topic,
            //    ctime = o.ctime.Value.ToString("yyyy-MM-dd"),
            //    dc = o.x_down.Count(),
            //    tp = GetDictName("paper.type", o.type),
            //}));


        }

        public object GetQues(int k)
        {

            var ques = DB.x_question.Where(o => o.topic == k && ids.Keys.Contains(o.question_id + ""));
            return ques.Select(o => new
            {
                tp = GetDictName("question.topic", o.topic),
                id = o.question_id,
                content = Context.Server.HtmlDecode(o.title),
                score = o.score == null ? 0 : o.score,
                count = ques.Count(),
                o.subject,
                fid = DB.x_fav.Count(f => f.cid == o.question_id) == 0 ? 0 : 1,
            }).ToList();

            //var paper = DB.x_paper.FirstOrDefault(o => o.paper_id == id);
            //if (paper == null)
            //    throw new XExcep("T试卷不存在");
            //// var ids = Serialize.FromJson<Dictionary<string, long>>(paper.qids);

            ////试卷所有的试题 题号数组
            //var ids = Serialize.FromJson<Dictionary<string, long>>(paper.qids);
            ////题型
            ////  var json1 = Context.Server.UrlDecode();
            ////要的数据格式 {\"题型\"：[\"题号数组\"],\"题型\"：[\"题号数组\"]}
            ////例： "{\"单选题\":[\"52\",\"51\",\"49\"],\"多选题\":[\"50\"]}"

            //if (k == "单选题") {
            //    var que = DB.x_question.Where(o => ids.Keys.Contains(o.question_id + "") && o.topic == 1);
            //    dict.Add("dco", que.Count());
            //    return que.OrderBy(o => o.ctime)
            //        .ToList()
            //        .Select(o => new {
            //            tp = GetDictName("question.topic", o.topic),
            //            id = o.question_id,
            //            content = Context.Server.HtmlDecode(o.title),
            //            o.score,
            //            count = que.Count(),
            //            o.subject
            //        });
            //}
            //else
            //    if (k == "多选题") {
            //    var que = DB.x_question.Where(o => ids.Keys.Contains(o.question_id + "") && o.topic == 2);
            //    return que.OrderBy(o => o.ctime)
            //        .ToList()
            //        .Select(o => new {
            //            tp = GetDictName("question.topic", o.topic),
            //            id = o.question_id,
            //            content = Context.Server.HtmlDecode(o.title),
            //            o.score,
            //            count = que.Count(),
            //            o.subject
            //        });
            //}
            //else
            //    if (k == "填空题") {
            //    var que = DB.x_question.Where(o => ids.Keys.Contains(o.question_id + "") && o.topic == 3);
            //    return que.OrderBy(o => o.ctime)
            //        .ToList()
            //        .Select(o => new {
            //            tp = GetDictName("question.topic", o.topic),
            //            id = o.question_id,
            //            content = Context.Server.HtmlDecode(o.title),
            //            o.score,
            //            count = que.Count(),
            //            o.subject
            //        });
            //}






            //// dict.Add("ids", ids.Keys);
            ////if (ids != null)
            ////var ques = DB.x_question.Where(o =>ids.Keys.Contains(o.question_id+""));
            ////var ques = DB.x_question.Where(o => qids[k].Contains(o.question_id));
            ////// dict.Add("dc", ques.Count());
            ////return ques.OrderBy(o => o.question_id)
            ////     .ToList()
            ////     .Select(o => new {
            ////         tp=GetDictName("question.topic", o.topic),
            ////         id = o.question_id,
            ////         content = Context.Server.HtmlDecode(o.title),
            ////         o.score,
            ////         count = ques.Count(),
            ////         o.subject
            ////     });
            //return null;


        }




        public string GetNum(int no)
        {
            return nums[no];
        }
    }
}
