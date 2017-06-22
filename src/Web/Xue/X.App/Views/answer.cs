using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace X.App.Views {
    public class answer : _web {
        public int id { get; set; }
        protected override string GetParmNames {
            get {
                return "id";
            }
        }

        protected override void InitDict() {
            base.InitDict();
            var ques = from q in DB.x_question where q.question_id  == id select q;
            // var ques = DB.x_question.FirstOrDefault(o => o.question_id == id);
            //dict.Add("ques", ques);
            dict.Add("ques", ques.ToList().Select(o => new {
                tp = GetDictName("question.topic", o.topic),
                ty = GetDictName("question.type", o.type),
                ey = GetDictName("question.easy", o.easy),
                kgs = GetDictName("xx.knowledge", o.knowledge),
                content = Context.Server.HtmlDecode(o.title),
                o.hits,
               // knowledge= GetDictName("xx",o.knowledge),
                id = o.question_id,
                fid = cu == null || cu.x_fav.Count(f => f.cid == o.question_id) == 0 ? 0 : 1
                //判断试题是否被收藏
                //if(DB.x_fav.FirstOrDefault(f => f.cid == o.question_id) == null ? null)
                //fid = DB.x_fav.FirstOrDefault(f => f.cid == o.question_id) == null ? 0 : (DB.x_fav.FirstOrDefault(f => f.cid == o.question_id).fav_id),
            }).ToList());

            

            var ans = from a in DB.x_answer
                        where a.question_id == id
                        select a;
            dict.Add("count", ans.Count());
            dict.Add("ans", ans.ToList().Select(o => new {
                result = Context.Server.HtmlDecode(o.result),
                remark = Context.Server.HtmlDecode(o.remark)
            }).ToList());

        }
    }
}
