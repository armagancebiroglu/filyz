using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class Share_Member
    {//
        public int id { get; set; }
        public int shareObject_id { get; set; }
        public int member_id { get; set; }
        public DateTime date { get; set; }
    }
    
    public class shareStreem
    {
        public int number { get; set; }
        public int id { get; set; }
        public int member_id { get; set; }
        public string text { get; set; }
        public string image_url { get; set; }
        public string sex { get; set; }
        public int sort { get; set; }
        public DateTime date { get; set; }
        public int confrim { get; set; }
        public int status { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public int active { get; set; }

        public string member_nick_name { get; set; }
        public string member_sex { get; set; }
        public DateTime member_age { get; set; }
        public string member_city { get; set; } 
        public string member_profil_media { get; set; }
        public int member_status { get; set; }
        public DateTime member_last_enter_date { get; set; }
        public int member_isonline { get; set; }
        public int member_score { get; set; } 
    }

    public class shareEditorObje
    {
        public int number { get; set; }
        public int id { get; set; }
        public int member_id { get; set; }
        public string text { get; set; }
        public string image_url { get; set; }
        public string sex { get; set; }
        public int sort { get; set; }
        public DateTime date { get; set; }
        public int confrim { get; set; }
        public int status { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public int active { get; set; }
        public string nick_name { get; set; }
        public string name_last_name { get; set; }
        public string tel { get; set; }
        public string e_mail { get; set; }
        public string password { get; set; }
        public int score { get; set; }
        public int role { get; set; }
        public int is_online { get; set; }
        public int count_List { get; set; }
    }
}