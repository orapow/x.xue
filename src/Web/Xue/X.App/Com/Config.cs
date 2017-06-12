using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.Core.Utility;

namespace X.App.Com
{
    /// <summary>
    /// 常规配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string domain { get; set; }//
        /// <summary>
        /// 系统名称
        /// </summary>
        public string name { get; set; }//网站名
        /// <summary>
        /// 缓存设置
        /// 1、memcached
        /// 2、WebCached
        /// </summary>
        public int cache { get; set; }
        /// <summary>
        /// 货到付款
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

        public string gateway { get; set; }

        private static string file = HttpContext.Current.Server.MapPath("/dat/cfg.x");//来自服务器的文件
        private static Config cfg = null;
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public static Config LoadConfig()
        {
            if (cfg == null)
            {
                var json = Tools.ReadFile(file);
                if (string.IsNullOrEmpty(json)) return new Config();
                cfg = Serialize.FromJson<Config>(json);
            }
            return cfg;
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="cfg"></param>
        public static void SaveConfig(Config cfg)
        {
            Tools.SaveFile(HttpContext.Current.Server.MapPath("/dat/cfg.x"), Serialize.ToJson(cfg));
        }

    }
}
