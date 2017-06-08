using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.answer {
    public class list : xmg {
        public int id {get;set; }
        public int page {get;set; }
        public int limit {get;set; }
        public string key {get; set; }
        protected override XResp Execute() {

            var q = from an in DB.x_answer
                    where an.question_id==id
                    select an;
            if (!string.IsNullOrEmpty(key))
                q = q.Where(o => o.result.Contains(key) || o.remark.Contains(key));
            var list = q.OrderBy(o => o.no).Skip((page - 1) * limit).Take(limit);

            var r = new Resp_List();
            r.page = page;
            r.count = q.Count();
            r.items = list.ToList().Select(o => new {
                qid = o.question_id,
                aid =o.answer_id,
                result = o.result,
                remark = Context.Server.HtmlDecode(o.remark),
                no = o.no
            });
            return r;
        }
    }
}
