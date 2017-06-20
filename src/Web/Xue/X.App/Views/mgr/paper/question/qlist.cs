using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;

namespace X.App.Views.mgr.paper.question
{
    public class qlist : xmg
    {
        public int p_id { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "p_id";
            }
        }

        protected override void InitDict()
        {
            base.InitDict();
            if (p_id == 0) throw new XExcep("试卷参数丢失，请从试卷管理进入");

            var q = DB.x_paper.FirstOrDefault(o => o.paper_id == p_id);
            if (q == null) throw new XExcep("T试卷不存在");
            dict.Add("paper", q);

        }

    }
}
