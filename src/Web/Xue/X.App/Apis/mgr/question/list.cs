using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web.Com;

namespace X.App.Apis.mgr.question
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int sub { get; set; }
        public int ey { get; set; }
        public int ty { get; set; }
        public int tp { get; set; }
        public string key { get; set; }
        protected override XResp Execute()
        {
            var q = from qu in DB.x_question
                    select qu;

            if (!string.IsNullOrEmpty(key)) q = q.Where(o => o.title.Contains(key) || o.src.Contains(key));

            if (sub > 0) q = q.Where(o => o.subject == sub);
            if (ey > 0) q = q.Where(o => o.easy == ey);
            if (ty > 0) q = q.Where(o => o.type == ty);
            if (tp > 0) q = q.Where(o => o.topic == tp);

            var list = q.OrderByDescending(o => o.mtime).Skip((page - 1) * limit).Take(limit);

            var r = new Resp_List();
            r.page = page;
            r.count = q.Count();
            r.items = list.ToList().Select(o => new
            {
                id = o.question_id,
                sub = getSub(o.subject.Value),
                bk = GetDictName("xx.book", o.book),
                cp = GetDictName("xx.chapter", o.chapter),
                tp = GetDictName("question.topic", o.topic),
                ty = GetDictName("question.type", o.type),
                ey = GetDictName("question.easy", o.easy),
                title = Context.Server.HtmlDecode(o.title),
                kg = GetDictName("xx.knowledge", o.knowledge),
                ctime = o.ctime.Value.ToString("yy-MM-dd HH:mm")
            });

            return r;

        }

        string getSub(int s)
        {
            var sb = GetDict("xx.subject", s + "");
            return GetDictName("xx.age", sb.f3) + "->" + sb.name;
        }
    }
}
