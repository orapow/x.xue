using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Web;

namespace X.App.Views.user
{
    public class pay : _web
    {
        /// <summary>
        /// 1、单次下载
        /// 2、包时长
        /// </summary>
        [ParmsAttr(name = "订单类型", min = 1, max = 2)]
        public int tp { get; set; }
        public int val { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "tp-val";
            }
        }
        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            decimal am = 0;
            if (tp == 1)
            {
                var p = DB.x_paper.FirstOrDefault(o => o.paper_id == val);
                if (p == null) throw new XExcep("T试卷不存在");
                dict.Add("desc", "下载试卷【" + p.topic + "（" + p.paper_id + "）】");
                am = p.user_id == null && p.price > 0 ? p.price.Value : cfg.down_price;
            }
            else
            {
                am = getamount();
                dict.Add("desc", "Vip服务续费到：" + ((cu.etime == null || cu.etime < DateTime.Now) ? DateTime.Now.AddMonths(val).ToString("yyyy-MM-dd HH:mm:ss") : cu.etime.Value.AddMonths(val).ToString("yyyy-MM-dd HH:mm:ss")));
            }
            dict.Add("am", am);
        }
        decimal getamount()
        {
            if (tp == 1) return cfg.down_price;
            else if (tp == 2)
            {
                if (val < 6) return cfg.month_price * val;
                else if (val == 6) return cfg.half_price;
                else if (val == 12) return cfg.year_price;
            }
            return -1;
        }
    }
}
