using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class Notice
    {//gönderini beğendi, gülümsedi,ankete mekana davet etti, etiketledi, favoriye ekledi gibi bilgileri bildirecek ve bu olaya link ile yol verecek.
        public int id { get; set; }
        public int who_id { get; set; }
        public int whom_id { get; set; }
        public int obje_id { get; set; } 
        public string text { get; set; }
        public int noticeTableSort { get; set; }
        public DateTime date { get; set; }
        public bool active { get; set; }
        public int sort { get; set; } 
    }
    public class NoticeListItem
    {
        //id, obje_id, who_id, whom_id, sort, noticeTableSort, text, date, active
        //id, nick_name, sex, matiral_status, age, educational_status, city, profil_media, status, payment_date, password, my_story, country, city_wants_to_live, hicap, height, kilo, hair_color, eye_color, children, disable, score, e_mail, last_enter_date, is_online
        //id,nick_name,profil_media,is_online
        public int id { get; set; }//bildirimin id si
        public int  who_id{ get; set; }//üyenin id si 
        public string nick_name { get; set; }
        public string profil_media { get; set; }
        public bool is_online { get; set; }
        public int obje_id { get; set; }
        public int noticeTableSort { get; set; }
        public int sort { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
        public bool active { get; set; }

    }
}