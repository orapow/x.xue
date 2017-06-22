using System.Collections.Generic;
using System.Linq;
using X.Web.Com;

namespace X.App.Apis.question {
    public class search : _web {
        public int sub { get; set; }
        public int bk { get; set; }
        public int tp { get; set; }
        public int ey { get; set; }
        public int ty { get; set; }
        public int kgc { get; set; }
        public int kg { get; set; }
        public int cp { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public int sort { get; set; }

        protected override XResp Execute() {
            var r = new Resp_List();
            r.page = page;

            var q = from qs in DB.x_question
                    where qs.subject == sub
                    select qs;

            if (page == 0)
                page = 1;
            if (limit == 0 || limit > 50)
                limit = 10;

            if (bk > 0)
                q = q.Where(o => o.book == bk);
            if (cp > 0) {
                var cps = GetDictList("xx.chapter", "00").Where(o => o.upval != null && (o.upval == (cp + "") || o.upval.StartsWith(cp + "-"))).Select(o => o.value);
                q = q.Where(o => cps.Contains(o.chapter + "") || o.chapter == cp);
            }
            if (tp > 0)
                q = q.Where(o => o.topic == tp);
            if (ey > 0)
                q = q.Where(o => o.easy == ey);
            if (ty > 0)
                q = q.Where(o => o.type == ty);
            if (kg > 0)
                q = q.Where(o => o.knowledge.Contains("[" + kg + "]"));
            if (kgc >= 3)
                q = q.Where(o => o.knowledgecount >= 3);
            else if (kgc > 0)
                q = q.Where(o => o.knowledgecount == kgc);

            switch (sort) {
                case 11:
                    q = q.OrderBy(o => o.mtime);
                    break;
                case 12:
                    q = q.OrderByDescending(o => o.mtime);
                    break;
                case 21:
                    q = q.OrderBy(o => o.hits);
                    break;
                case 22:
                    q = q.OrderByDescending(o => o.hits);
                    break;
            }

            r.items = q.Skip((page - 1) * limit).Take(limit).ToList().Select(o => new {
                tp = GetDictName("question.topic", o.topic),
                ty = GetDictName("question.type", o.type),
                ey = GetDictName("question.easy", o.easy),
                kgs = GetDictName("xx.knowledge", o.knowledge),
                content = Context.Server.HtmlDecode(o.title),
                o.hits,
                id = o.question_id,
                fid = cu == null || cu.x_fav.Count(f => f.cid == o.question_id) == 0 ? 0 : 1
            }).ToList();

            r.count = q.Count();
            return r;
        }

        //int getfav(int qid) {
        //    if (cu == null) return 0;
        //    var f=cu.x_fav.FirstOrDefault()
        //    cu == null || cu.x_fav.Count(f => f.cid == o.question_id) == 0 ? 0 : 1
        //}
    }
}
