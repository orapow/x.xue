using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.book
{
    public class list : xmg
    {
        public int sub { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        string code = "xx.book";

        protected override int powercode
        {
            get
            {
                return 1;
            }
        }
        protected override XResp Execute()
        {
            var r = new Resp_List();
            var tree = new XTree();
            tree.LoadList += tree_LoadList;
            var dt = DateTime.Now;
            tree.InitTree("", 5);
            dt = DateTime.Now;
            r.items = tree.OutTree();

            return r;
        }

        List<TreeNode> tree_LoadList(object id)
        {
            var dt = DateTime.Now;
            var list = GetDictList(code, id + "");
            if (list == null) return null;
            if (id.Equals(0)) list = list.Where(o => o.f3.Equals(sub)).ToList();
            return list.Select(m => new item()
            {
                name = m.name,
                value = m.id,
                id = m.value,
                upv = m.upval,
                subject = GetDictName("xx.subject", m.f3),
                sort = m.sort.Value
            }).ToList<TreeNode>();
        }

        public class item : TreeNode
        {
            public string upv { get; set; }
            public long value { get; set; }
            public string subject { get; set; }
            public int sort { get; set; }
            public item() : base("") { }
        }

    }
}
