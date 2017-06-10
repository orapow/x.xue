using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;

namespace X.App.Com
{
    public class Sdk
    {
        public static UserRsp GetUser(string key)
        {
            try
            {
                var json = Tools.GetHttpData("http://www.xinxuezaixian.com/index.php?m=Jshizhuc&a=getUser&ukey=" + key);
                if (string.IsNullOrEmpty(json)) throw new Exception("服务器返回空");
                return Serialize.FromJson<UserRsp>(json);
            }
            catch (Exception e)
            {
                return new UserRsp()
                {
                    result = new Rsp() { resultCode = "-1", resultMsg = e.Message }
                };
            }
        }

        //public static Rsp Logout(string key)
        //{
        //    try
        //    {
        //        var json = Tools.PostHttpData(gateway + "/Logout", "ukey=" + key);
        //        if (string.IsNullOrEmpty(json)) throw new Exception("服务器返回空");
        //        return new Rsp();
        //    }
        //    catch (Exception e)
        //    {
        //        return new Rsp()
        //        {
        //            msg = e.Message,
        //            err = true
        //        };
        //    }
        //}
    }

    public class UserRsp
    {
        public User user_info { get; set; }
        public Rsp result { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string realname { get; set; }
        public string img { get; set; }
        public string tel { get; set; }
        public string uin { get; set; }
    }
    public class Rsp
    {
        public string resultCode { get; set; }
        public string resultMsg { get; set; }
    }
}
