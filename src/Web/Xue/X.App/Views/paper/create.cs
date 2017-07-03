using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;

namespace X.App.Views.paper
{
    public class create : _web
    {        /// <summary>
             /// 左边显示
             /// 1、章节
             /// 2、短信点
             /// </summary>
        public int lt { get; set; }
        /// <summary>
        /// 教材
        /// </summary>
        public string bk { get; set; }

        protected override string GetParmNames
        {
            get
            {
                return "lt-bk";
            }
        }

        protected override void InitDict()
        {
            base.InitDict();
            dict.Add("create", "nav-active");
            var ques = from q in DB.x_question where (q.subject + "") == sub select q;
            if (lt == 1)
            {
                var bk_list = GetDictList("xx.book", "00").Where(o => (o.f3 + "") == sub).ToList();

                var bk1 = bk_list.Where(o => o.upval == "0").ToList();
                if (bk1 == null || bk1.Count == 0) { bk = "0"; return; }

                dict.Add("bk1", bk1);

                x_dict book = null;
                if (string.IsNullOrEmpty(bk)) book = bk1.FirstOrDefault();
                else book = GetDict("xx.book", bk);

                var bk2 = bk_list.Where(o => o.upval == (book.upval == "0" ? book.value : book.upval)).ToList();
                dict.Add("bk2", bk2);

                if (book.upval == "0") book = bk2.FirstOrDefault();

                bk = book.value;

                dict.Add("b1", book.upval);
                dict.Add("b2", book.value);
                ques = ques.Where(o => (o.book + "") == bk);
            }
            dict["bk"] = bk;

            dict.Add("topics", ques.GroupBy(o => o.topic).Select(o => o.Key).ToList().ToDictionary(v => v.Value + "", n => GetDictName("question.topic", n.Value)));
        }

        public List<x_dict> getLeft(string up)
        {
            if (bk == "0") return null;

            var list = GetDictList(lt == 2 ? "xx.knowledge" : "xx.chapter", up);

            if (lt == 1) list = list.Where(o => (o.f3 + "") == bk).ToList();
            else list = list.Where(o => (o.f3 + "") == sub).ToList();

            return list;
        }
    }
}
