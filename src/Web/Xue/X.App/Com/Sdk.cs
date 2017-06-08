using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;

namespace X.App.Com
{
    public class Sdk
    {
        static string gateway = "";
        public static UserRsp GetUser(string key)
        {
            try
            {
                var json = Tools.PostHttpData(gateway + "/getUser", "ukey=" + key);
                if (string.IsNullOrEmpty(json)) throw new Exception("服务器返回空");
                return Serialize.FromJson<UserRsp>(json);
            }
            catch (Exception e)
            {
                return new Rsp()
                {
                    msg = e.Message,
                    err = true
                } as UserRsp;
            }
        }

        public static Rsp Logout(string key)
        {
            try
            {
                var json = Tools.PostHttpData(gateway + "/Logout", "ukey=" + key);
                if (string.IsNullOrEmpty(json)) throw new Exception("服务器返回空");
                return new Rsp();
            }
            catch (Exception e)
            {
                return new Rsp()
                {
                    msg = e.Message,
                    err = true
                };
            }
        }
    }

    public class UserRsp : Rsp
    {
        public int id { get; set; }
        public string name { get; set; }
        public string headimg { get; set; }
        public string tel { get; set; }
        public string uin { get; set; }
    }

    public class Rsp
    {
        public bool err { get; set; }
        public string msg { get; set; }
    }
}
