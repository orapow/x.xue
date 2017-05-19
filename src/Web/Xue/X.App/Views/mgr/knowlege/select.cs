using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Data;

namespace X.App.Views.mgr.knowledge
{
    public class select : xmg
    {
        string code = "xx.knowledge";

        public int up { get; set; }
        public int bk { get; set; }

        protected override string GetParmNames
        {
            get
            {
                return "up-bk";
            }
        }

        protected override void InitDict()
        {
            var tree = new XTree();
            tree.LoadList += tree_LoadList;
            tree.InitTree("");
            var list = tree.OutTree();
            list.Insert(0, new item() { id = "0", name = "无" });
            dict.Add("dict", list);
        }

        List<TreeNode> tree_LoadList(object id)
        {
            var list = GetDictList(code, id + "");
            if (id.Equals(0)) list = list.Where(o => o.f3.Equals(bk)).ToList();
            else if (id.Equals(up + "")) return null;
            if (up > 0) list = list.Where(o => o.value != up + "").ToList();
            return list.Select(m => new item()
            {
                name = m.name,
                id = m.upval == "0" ? m.value : m.upval + "-" + m.value,
                cid = m.dict_id,
                value = m.value,
                img = m.img,
                jp = m.jp
            }).ToList<TreeNode>();
        }

        public class item : TreeNode
        {
            public long cid { get; set; }
            public string img { get; set; }
            public string value { get; set; }
            public string jp { get; set; }
            public item() : base("") { }
        }

        public override string GetTplFile()
        {
            return "com/dict";
        }
    }
}
