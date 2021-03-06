﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.App.Apis;
using X.Data;

namespace X.App.Views {
    public class question : _web {
        /// <summary>
        /// 左边显示
        /// 1、章节
        /// 2、知识点
        /// </summary>
        public int lt { get; set; }
        /// <summary>
        /// 教材
        /// </summary>
        public string bk { get; set; }

        protected override string GetParmNames {
            get {
                return "lt-bk";
            }
        }

        protected override void InitDict() {
            base.InitDict();
            dict.Add("question", "nav-active");
            var ques = from q in DB.x_question where (q.subject + "") == sub select q;
            if (lt == 1) {
                var bk_list = GetDictList("xx.book", "00").Where(o => (o.f3 + "") == sub).ToList();

                var bk1 = bk_list.Where(o => o.upval == "0").ToList();
                if (bk1 == null || bk1.Count == 0) { bk = "0"; return; }

                dict.Add("bk1", bk1);

                x_dict book = null;
                if (string.IsNullOrEmpty(bk))
                    book = bk1.FirstOrDefault();
                else
                    book = GetDict("xx.book", bk);

                var bk2 = bk_list.Where(o => o.upval == (book.upval == "0" ? book.value : book.upval)).ToList();
                dict.Add("bk2", bk2);

                if (book.upval == "0")
                    book = bk2.FirstOrDefault();

                bk = book.value;

                dict.Add("b1", book.upval);
                dict.Add("b2", book.value);
                ques = ques.Where(o => (o.book + "") == bk);
            }
            dict["bk"] = bk;

            dict.Add("topics", ques.GroupBy(o => o.topic).Select(o => o.Key).ToList().ToDictionary(v => v.Value + "", n => GetDictName("question.topic", n.Value)));
            dict.Add("ques", ques.OrderByDescending(o => o.mtime).Take(15).ToList().Select(o => new {
                tp = GetDictName("question.topic", o.topic),
                ty = GetDictName("question.type", o.type),
                ey = GetDictName("question.easy", o.easy),
                kgs = GetDictName("xx.knowledge", o.knowledge),
                content = Context.Server.HtmlDecode(o.title),
                o.hits,
                id = o.question_id,
                fid = cu == null || cu.x_fav.Count(f => f.cid == o.question_id) == 0 ? 0 : 1
                //判断试题是否被收藏
                //if(DB.x_fav.FirstOrDefault(f => f.cid == o.question_id) == null ? null)
                //fid = DB.x_fav.FirstOrDefault(f => f.cid == o.question_id) == null ? 0 : (DB.x_fav.FirstOrDefault(f => f.cid == o.question_id).fav_id),
            }).ToList());
            dict.Add("count", ques.Count());

        }

        public List<x_dict> getLeft(string up) {
            if (bk == "0")
                return null;

            var list = GetDictList(lt == 2 ? "xx.knowledge" : "xx.chapter", up);

            if (lt == 1)
                list = list.Where(o => (o.f3 + "") == bk).ToList();
            else
                list = list.Where(o => (o.f3 + "") == sub).ToList();

            return list;
        }

    }
}
