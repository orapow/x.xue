using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;

namespace X.App.Views.mgr.subject
{
    public class select : xmg
    {
        private List<item> data = new List<item>();
        protected override void InitDict()
        {
            var tree = new XTree();
            tree.LoadList += tree_LoadList;
            tree.InitTree("", 1);
            var list = tree.OutTree();
            list.Insert(0, new item() { id = "0", name = "无" });
            dict.Add("dict", list);
        }

        List<TreeNode> tree_LoadList(object id)
        {
            List<x_dict> list = null;
            if (id.Equals(0)) list = GetDictList("xx.age", id + "");
            else list = GetDictList("xx.subject", "0").Where(o => (o.f3 + "").Equals(id)).ToList();
            return list.Select(m => new item()
            {
                name = m.name,
                id = m.upval == "0" ? m.value : m.upval + "-" + m.value,
                cid = m.dict_id,
                value = id.Equals(0) ? "0000" : m.value,
                data = m.f3
            }).ToList<TreeNode>();
        }

        public class item : TreeNode
        {
            public long cid { get; set; }
            public int? data { get; set; }
            public string value { get; set; }
            public item() : base("") { }
        }

        public override string GetTplFile()
        {
            return "com/dict";
        }
    }
}
