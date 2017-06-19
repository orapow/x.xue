using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Newtonsoft.Json;

namespace X.App.Views.mgr.paper {
    public class edit : xmg {
        public int p_id { get; set; }

        protected override string GetParmNames {
            get {
                return "p_id";
            }
        }

        protected override void InitDict() {
            base.InitDict();
            if (p_id > 0) {
                var q = DB.x_paper.FirstOrDefault(o => o.paper_id == p_id);
                dict.Add("sub", q.subject + "|" + GetDictName("xx.subject", q.subject));
                dict.Add("item", q);
            }

        }

        //protected override void InitDict() {
        //    base.InitDict();

        //    if (id > 0) {
        //        var q = DB.x_paper.FirstOrDefault(o => o.paper_id == id);
        //        string jsonString = q.qids;
        //        string sArras=jsonString.Replace('\"', ' ').Replace('{', ' ').Replace('}',' ');
        //        string[] sArray = sArras.Split(',');

        //        object[] st = new object[sArray.Length]; 

        //        //获取试卷的每个试题的ID并用数组st[]保存试题
        //        for (int i = 0; i < sArray.Length; i++) {
        //            string s = sArray[i].Substring(1, sArray[i].Length - 3);
        //            // "}"替换为" "后也占一个length
        //            if (i==sArray.Length-1)
        //                s = sArray[i].Substring(1, sArray[i].Length - 4);
        //            st[i] = Context.Server.HtmlDecode(DB.x_question.FirstOrDefault(o => o.question_id == Int32.Parse(s)).title);
        //        }

        //        dict.Add("st", st);
        //        dict.Add("item", q);
        //        dict.Add("sub", q.subject + "|" + GetDictName("xx.subject", q.subject));

        //    }
        //}
    }



    //public class JsonHelper {
    //    ///<summary>
    //    ///Json序列化
    //    /// </summary>

    //    public static string Serializer(object obj) {
    //        return JsonConvert.SerializeObject(obj);
    //    }
    //    ///<summary>
    //    ///Json反序列化
    //    /// </summary>
    //    public static T Deserializer<T>(string str) {
    //        return JsonConvert.DeserializeObject<T>(str);
    //    }
    //}

}
