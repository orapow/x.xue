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
    public class login : xview
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
            if (string.IsNullOrEmpty(ukey)) throw new XExcep("T用户Key为空");

            var u = Com.Sdk.GetUser(ukey);
            if (u == null || u.id == 0) throw new XExcep("T用户登陆超时");

            var cu = DB.x_user.FirstOrDefault(o => o.uid == u.uin);
            if (cu == null)
            {
                cu = new Data.x_user()
                {
                    balance = 0,
                    ctime = DateTime.Now,
                    headimg = u.headimg,
                    name = u.name,
                    tel = u.tel,
                    uid = u.uin
                };
                DB.x_user.InsertOnSubmit(cu);
                SubmitDBChanges();
            }

            CacheHelper.Save(ukey, cu);

            Context.Response.SetCookie(new System.Web.HttpCookie("ukey", ukey));

            if (!string.IsNullOrEmpty(back)) Context.Response.Redirect(Secret.FormBase64(back));

        }
    }
}
