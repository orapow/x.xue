using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;
using X.Web.Com;

namespace X.App.Apis.mgr.subject
{
    public class list : xmg
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string key { get; set; }
        public int age { get; set; }

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
            List<x_dict> list = null;
            if (id.Equals(0))
            {
                list = GetDictList("xx.age", id + "");
                if (age > 0) list = list.Where(o => o.value == age + "").ToList();
            }
            else
            {
                list = GetDictList("xx.subject", "0").Where(o => (o.f3 + "").Equals(id)).ToList();
                if (!string.IsNullOrEmpty(key)) list = list.Where(o => o.name.Contains(key)).ToList();
            }
            return list.Select(m => new item()
            {
                name = m.name,
                value = m.id,
                id = m.value,
                upv = m.f3 + "",
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
