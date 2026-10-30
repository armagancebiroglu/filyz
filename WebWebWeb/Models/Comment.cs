using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class Comment
    {////id, sub_id, obje_id, sort, noticeTableSort, who_id, whom_id, text, date

        public int id { get; set; }
        public int obje_id { get; set; }
        public int sort { get; set; }
        public int noticeTableSort { get; set; }
        public int who_id { get; set; }
        public int whom_id { get; set; }
        public string notice_text { get; set; }
        public string comment_text { get; set; }
        public DateTime date { get; set; }
        public string nick_name { get; set; }
        public string profil_media { get; set; }
        
    }
}