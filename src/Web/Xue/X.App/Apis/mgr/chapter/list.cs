using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.chapter
{
    public class list : xmg
    {
        public int sub { get; set; }//科目
        public int bk { get; set; }//教材
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }

        string code = "xx.chapter";

        protected override XResp Execute()
        {
            var r = new Resp_List();
            var tree = new XTree();
            tree.LoadList += tree_LoadList;
            tree.InitTree("", 5);

            var list = tree.OutTree();
            if (!string.IsNullOrEmpty(key)) list = list.Where(o => o.name.Contains(key)).ToList();
            r.items = list;

            return r;
        }

        List<TreeNode> tree_LoadList(object id)
        {
            var list = GetDictList(code, id + "").Where(o => o.f3.Equals(bk)).ToList();
            if (list == null) return null;
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
