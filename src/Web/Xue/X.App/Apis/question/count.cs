using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.question
{
    public class count : xapi
    {
        public int sub { get; set; }
        public int bk { get; set; }
        public string tids { get; set; }
        public string pids { get; set; }
        /// <summary>
        /// 1、章节
        /// 2、知识点
        /// </summary>
        public int tp { get; set; }

        protected override XResp Execute()
        {
            var r = new back();

            var qr = from q in DB.x_question
                     select q;

            if (tp == 1)
            {
                qr = qr.Where(o => pids.IndexOf("[" + o.chapter + "]") >= 0 && o.book == bk);
            }
            else
            {
                if (string.IsNullOrEmpty(pids)) pids = "[]";

                var ids = pids.Substring(1, pids.Length - 2).Replace("][", ",").Split(',');

                var exp = DynamicLinqExpressions.False<x_question>();
                foreach (var id in ids)
                    exp = exp.Or(p => p.knowledge.Contains("[" + id + "]"));

                qr = qr.Where(exp);

            }
            r.items = new Dictionary<string, string>();
            foreach (var t in tids.TrimEnd(',').Split(','))
                r.items.Add(t, qr.Count(o => (o.topic + "") == t) + "");

            return r;
        }
        class back : XResp
        {
            public Dictionary<string, string> items { get; set; }
        }
    }
}
