using Aop.Api;
using Aop.Api.Request;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using X.App.Com;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.user.order
{
    public class md : _web
    {
        /// <summary>
        /// 1、单次下载
        /// 2、包时长
        /// </summary>
        [ParmsAttr(name = "订单类型", min = 1, max = 2)]
        public int tp { get; set; }
        public int val { get; set; }//试卷id
        [ParmsAttr(name = "支付平台", min = 1, max = 2)]
        public int platform { get; set; }

        protected override bool nduser
        {
            get
            {
                return true;
            }
        }
        protected override XResp Execute()
        {
            var od = new x_order() { ctime = DateTime.Now, platform = platform, user_id = cu.user_id };
            var am = getamount();
            if (am == -1) throw new XExcep("T购买时长不正确");
            if (tp == 1)
            {
                var p = DB.x_paper.FirstOrDefault(o => o.paper_id == val && o.user_id == cu.user_id);
                if (p == null) throw new XExcep("T试卷不存在，无法下单！");
                od.desc = "下载试卷【" + p.topic + "(" + p.paper_id + ")】";
                od.pid = val;
            }
            else
            {
                var et = cu.etime == null || cu.etime < DateTime.Now ? DateTime.Now.AddMonths(val) : cu.etime.Value.AddMonths(val);
                od.desc = (val == 12 ? "包年" : "包月") + "订单，服务到期：" + et.ToString("yyyy-MM-dd HH:mm:ss");
                od.etime = et;
            }
            od.amount = am;
            od.no = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Tools.GetRandRom(5);
            od.status = 1;

            DB.x_order.InsertOnSubmit(od);

            var qr_url = "";
            if (platform == 1)
            {
                var wx = Wx.Pay.MdOrder(od.desc, od.no, (int)(od.amount * 100) + "", "http://" + cfg.domain + "/notify-1-" + od.no + ".html", cfg.wx_appid, cfg.wx_mch_id, cfg.wx_mch_key);
                if (wx.return_code == "FAIL") throw new XExcep(wx.return_msg);
                if (wx.result_code == "FAIL") throw new XExcep(wx.err_code + "," + wx.err_code_des);
                if (string.IsNullOrEmpty(wx.prepay_id)) throw new XExcep("T第三方订单号为空");
                od.app_no = wx.prepay_id;
                qr_url = wx.code_url;
            }
            else if (platform == 2)
            {
                var c = new DefaultAopClient(cfg.ap_gateway, cfg.ap_appid, cfg.ap_mch_key, false);
                var p = new
                {
                    subject = od.desc,
                    out_trade_no = od.no,
                    total_amount = od.amount
                };
                var req = new AlipayTradePrecreateRequest() { BizContent = Serialize.ToJson(p) };
                req.SetNotifyUrl("http://" + cfg.domain + "/notify-2-" + od.no + ".html");
                var rsp = c.Execute(req);
                if (rsp.IsError) throw new XExcep("T支付宝支付出错，错误信息：" + rsp.Msg);
                qr_url = rsp.QrCode;
            }

            SubmitDBChanges();

            var r = new back() { order_id = od.order_id };
            using (var ms = new MemoryStream())
            {
                var qr = new QrEncoder();
                var code = qr.Encode(qr_url);
                var rd = new GraphicsRenderer(new FixedModuleSize(15, QuietZoneModules.Two));
                rd.WriteToStream(code.Matrix, ImageFormat.Jpeg, ms);
                r.qr_code = Convert.ToBase64String(ms.GetBuffer());
            }
            return r;
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

        class back : XResp
        {
            public string qr_code { get; set; }
            public long order_id { get; set; }
        }
    }
}
