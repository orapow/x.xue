using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.paper
{
    public class create : _web
    {
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        [ParmsAttr(name = "筛选条件", req = true)]
        public string pids { get; set; }
        [ParmsAttr(name = "出题选项", req = true)]
        public string tids { get; set; }
        [ParmsAttr(name = "组卷方式", min = 1, max = 2)]
        public int tp { get; set; }
        public int bk { get; set; }

        protected override XResp Execute()
        {
            var tds = Serialize.FromJson<Dictionary<int, int>>(Context.Server.HtmlDecode(tids));
            if (tds == null || tds.Count == 0) throw new XExcep("T出题选项不能为空");

            var qus = from q in DB.x_question
                      select q;
            if (tp == 1)
            {
                qus = qus.Where(o => pids.IndexOf("[" + o.chapter + "]") >= 0 && o.book == bk);
            }
            else
            {
                var ids = pids.Substring(1, pids.Length - 2).Replace("][", ",").Split(',');
                foreach (var id in ids)
                    qus = qus.Union(qus.Where(o => o.knowledge.Contains("[" + id + "]")));
            }

            var r = new back() { items = new Dictionary<string, long[]>() };

            foreach (var t in tds)
            {
                r.items.Add(
                    GetDictName("question.topic", t.Key),
                    qus.Where(o => o.topic == t.Key)
                        .OrderBy(o => Guid.NewGuid())
                        .Take(t.Value)
                        .Select(o => o.question_id)
                        .ToArray()
                    );
            }

            return r;

        }

        class back : XResp
        {
            public Dictionary<string, long[]> items { get; set; }
        }
    }
}
