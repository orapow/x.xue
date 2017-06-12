using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.Core.Utility;
using X.App;
using X.App.Com;
using X.Web.Com;
using X.Web;

namespace X.App.Apis.mgr
{
    /// <summary>
    /// 常规配置
    /// </summary>
    public class setup : xmg
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string domain { get; set; }//


        /// <summary>
        /// 系统名称
        /// </summary>
        public string name { get; set; }//网站名
        public string gateway { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string pay_ways { get; set; }//

        public string wx_appid { get; set; }//
        public string wx_scr { get; set; }//
        public string wx_mch_id { get; set; }//

        public string ap_appid { get; set; }//
        public string ap_scr { get; set; }//
        public string ap_mch_id { get; set; }//

        /// <summary>
        /// 包月价格
        /// </summary>
        public decimal month_price { get; set; }
        /// <summary>
        /// 半年价格
        /// </summary>
        public decimal half_price { get; set; }
        /// <summary>
        /// 包年价格
        /// </summary>
        public decimal year_price { get; set; }
        /// <summary>
        /// 单次价格
        /// </summary>
        public decimal down_price { get; set; }
        /// <summary>
        /// vip组卷试题限额
        /// </summary>
        public int vip_ques_count { get; set; }
        /// <summary>
        /// 非vip组卷试题限额
        /// </summary>
        public int unvip_ques_count { get; set; }

        protected override Web.Com.XResp Execute()
        {
            cfg = Config.LoadConfig();
            cfg.domain = domain;
            cfg.name = name;

            cfg.ap_appid = ap_appid;
            cfg.ap_mch_id = ap_mch_id;
            cfg.ap_scr = ap_scr;
            cfg.domain = domain;
            cfg.down_price = down_price;
            cfg.half_price = half_price;
            cfg.month_price = month_price;
            cfg.name = name;
            cfg.pay_ways = pay_ways;
            cfg.unvip_ques_count = unvip_ques_count;
            cfg.vip_ques_count = vip_ques_count;
            cfg.wx_appid = wx_appid;
            cfg.wx_mch_id = wx_mch_id;
            cfg.wx_scr = wx_scr;
            cfg.year_price = year_price;
            cfg.gateway = gateway;

            Config.SaveConfig(cfg);
            return new XResp();
        }
    }
}
