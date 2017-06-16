using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Cache;
using X.Core.Utility;
using X.Data;
using X.Web;

namespace X.App.Views
{
    public class login : _web
    {
        public string back { get; set; }
        public string ukey { get; set; }
        protected override string GetParmNames
        {
            get
            {
                return "ukey-back";
            }
        }
        protected override void InitView()
        {
            base.InitView();

            if (cu != null) Context.Response.Redirect(back);
            if (string.IsNullOrEmpty(ukey)) Context.Response.Redirect("http://www.xinxuezaixian.com/index.php?m=Jshizhuc&a=userLogin&back_url=http://" + cfg.domain + "/login-{uk}-" + back + ".html");

            var usp = Com.Sdk.GetUser(ukey);
            if (usp.user_info == null || usp.user_info.id == 0) throw new XExcep("T用户登陆超时");

            var u = usp.user_info;
            cu = DB.x_user.FirstOrDefault(o => o.tel == u.tel);
            if (cu == null)
            {
                cu = new Data.x_user()
                {
                    balance = 0,
                    ctime = DateTime.Now,
                    tel = u.tel,
                    uid = u.uin
                };
                DB.x_user.InsertOnSubmit(cu);
            }
            cu.headimg = u.img;
            cu.name = u.realname;
            cu.ukey = ukey;

            SubmitDBChanges();

            Context.Response.SetCookie(new System.Web.HttpCookie("ukey", ukey));

            if (!string.IsNullOrEmpty(back)) Context.Response.Redirect(Secret.FormBase64(back));
            else Context.Response.Redirect("/index.html");

        }
    }
}
