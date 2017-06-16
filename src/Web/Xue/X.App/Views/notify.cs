using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using X.App.Com;
using X.Core.Plugin;
using X.Core.Utility;
using X.Data;

namespace X.App.Views
{
    public class notify : xview
    {
        public string no { get; set; }
        public int tp { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "tp-no";
            }
        }

        public override byte[] GetResponse()
        {
            GetPageParms();
            cfg = Config.LoadConfig();
            DB = new DataClassesDataContext() { DeferredLoadingEnabled = true };

            string back = "";
            string n = "";
            if (tp == 1) back = VaildWx(ref n);
            else if (tp == 2) back = ValidAli(ref n);

            if (string.IsNullOrEmpty(back)) return null;
            if (string.IsNullOrEmpty(n)) return Encoding.UTF8.GetBytes(back);
            var od = DB.x_order.FirstOrDefault(o => o.no == no);
            if (od == null || od.status != 1) return Encoding.UTF8.GetBytes(back);

            od.status = 2;
            od.app_no = n;

            if (od.pid > 0)
            {
                var down = new x_down() { ctime = DateTime.Now, paper_id = od.pid };
                od.x_user.x_down.Add(down);
            }
            else if (od.etime != null)
            {
                od.x_user.etime = od.etime;
            }
            SubmitDBChanges();
            return Encoding.UTF8.GetBytes(back);
        }

        string VaildWx(ref string n)
        {
            var ntxml = string.Empty;
            using (var sr = new StreamReader(Context.Request.InputStream)) ntxml = sr.ReadToEnd();

            if (string.IsNullOrEmpty(ntxml)) return "";
            ntxml = ntxml.Replace("xml", "Ntxml");
            var nt = Serialize.FormXml<Wx.Pay.Ntxml>(ntxml);

            if (!Wx.Pay.ValidNotify(nt, cfg.wx_mch_id, cfg.wx_appid, cfg.wx_mch_key))
            {
                Loger.Info("回调验证失败，地址：" + Context.Request.RawUrl + "，参数：" + ntxml);
                return "";
            }
            n = nt.transaction_id;
            return @"<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg></xml>";
        }
        string ValidAli(ref string n)
        {
            var ps = new Dictionary<string, string>();
            foreach (string p in Context.Request.Form.Keys)
                ps.Add(p, Context.Request.Form[p]);
            if (ps["trade_status"] == "TRADE_SUCCESS")
                n = ps["trade_no"];
            Debug.WriteLine(Serialize.ToJson(ps));
            var pass = Aop.Api.Util.AlipaySignature.RSACheckV1(ps, cfg.ap_ali_key, ps["charset"], ps["sign_type"], false);
            return pass ? "success" : "";
        }

        //private void ValidCharge()
        //{
        //    var chg = DB.x_charge.SingleOrDefault(o => o.charge_id == id);
        //    if (chg == null) Loger.Info("充值记录不存在，订单号：" + id);
        //    else if (!string.IsNullOrEmpty(chg.result)) return;
        //    else
        //    {
        //        if (cfg.chg_audit == 2)
        //        {
        //            chg.x_user.balance += chg.amount;
        //            chg.audit_status = 2;
        //            chg.audit_time = DateTime.Now;
        //        }
        //        chg.result = "成功";
        //        chg.audit_status = 1;
        //    }
        //}

        //private void ValidOrder(string wx_no)
        //{
        //    var order = DB.x_order.SingleOrDefault(o => o.order_id == id);
        //    if (order == null) Loger.Info("订单不存在，订单ID：" + id);
        //    if (order.status != 1 || order.pay_way != 1) Loger.Info("订单确认失败，订单号：" + id);
        //    else
        //    {
        //        order.wx_no = wx_no;
        //        order.pay_time = DateTime.Now;
        //        order.pay_amount = order.yf_amount;
        //        order.status = 2;
        //    }
        //}
    }
}
