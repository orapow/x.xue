using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Core.Utility;
using X.Data;
using X.Web;
using X.Web.Com;

namespace X.App.Apis.paper
{
    public class save : _web
    {
        public int pid { get; set; }
        public int sub { get; set; }
        public string qids { get; set; }
        public string setting { get; set; }

        protected override XResp Execute()
        {
            config c = null;
            if (!string.IsNullOrEmpty(setting)) c = Serialize.FromJson<config>(Context.Server.HtmlDecode(setting));
            if (c == null) throw new XExcep("T试卷配置参数丢失");

            if (string.IsNullOrEmpty(qids)) throw new XExcep("T未选择试题");

            var ids = Serialize.FromJson<Dictionary<long, int>>(Context.Server.HtmlDecode(qids));
            if (ids == null || ids.Count() == 0) throw new XExcep("T试题参数错误");
            if (ids.Count(o => o.Value == 0) > 0) throw new XExcep("T有试题未设置分数");
            var limit = 0;
            if (cu.etime > DateTime.Now) limit = cfg.vip_ques_count;
            else limit = cfg.unvip_ques_count;

            if (DB.x_paper.Count(o => o.user_id == cu.user_id && o.ctime.Value.Date == DateTime.Now.Date) >= limit) throw new XExcep("T今日组卷数量已达上限，请明日再来组卷。");
            //这里要限制组卷次数 cfg.vip_ques_count;cfg.unvip_ques_count ↑

            x_paper pg = null;
            if (pid > 0) pg = DB.x_paper.FirstOrDefault(o => o.paper_id == pid && o.user_id == cu.user_id);
            if (pg == null) pg = new x_paper() { ctime = DateTime.Now, user_id = cu.user_id };

            pg.mtime = DateTime.Now;
            pg.qcount = ids.Count();
            pg.qids = Serialize.ToJson(ids);
            pg.score = c.score;
            pg.setting = Serialize.ToJson(c);
            pg.subject = sub;
            pg.topic = c.topic;

            if (pid == 0) DB.x_paper.InsertOnSubmit(pg);
            SubmitDBChanges();

            return new XResp() { msg = pg.paper_id + "" };

        }

        class config
        {
            public Dictionary<int, bool> cfg { get; set; }
            public string topic { get; set; }
            public string stopic { get; set; }
            public string time { get; set; }
            public int score { get; set; }
            public string notice { get; set; }
        }
    }
}
