using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class Editor
    {//id, nick_name, name_last_name, tel, e_mail, password, score
        public int id { get; set; }
        [Required(ErrorMessage = "Üye kodunuzu giriniz.")]
        [Display(Name = "Üye Kodu")]
        public string nick_name { get; set; }
        public string name_last_name { get; set; }
        public string tel { get; set; }
        public string e_mail { get; set; }

        [Required(ErrorMessage = "Şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string password { get; set; }
        public int score { get; set; }
        public int role { get; set; }
        public bool is_online { get; set; }
    }
    public class AdminView
    {
        public int id { get; set; }
        public int member { get; set; }
        public int man_member { get; set; }
        public int woman_member { get; set; }
        public int member_o { get; set; }
        public int man_member_o { get; set; }
        public int woman_member_o { get; set; }
        public int member_d { get; set; }
        public int man_member_d { get; set; }
        public int woman_member_d { get; set; }
        public int member_w { get; set; }
        public int man_member_w { get; set; }
        public int woman_member_w { get; set; }
        public int member_dp { get; set; }
        public int man_member_dp { get; set; }
        public int woman_member_dp { get; set; }
        public int member_wp { get; set; }
        public int man_member_wp { get; set; }
        public int woman_member_wp { get; set; }
        public int member_mp { get; set; }
        public int man_member_mp { get; set; }
        public int woman_member_mp { get; set; }
        public int member_m { get; set; }
        public int man_member_m { get; set; }
        public int woman_member_m { get; set; } 
    }
    public class EditorComplaint
    { 
        public int number { get; set; }
        public int share_id { get; set; }
        public string who_nick_name { get; set; }
        public int complaint_who_id { get; set; }
        public string who_profil_media { get; set; }        
        public string whom_nick_name { get; set; }
        public int share_memberid { get; set; }
        public string whom_profil_media { get; set;}        
        public string share_text { get; set; }
        public string share_imgUrl { get; set; }
        public string share_sex { get; set; }
        public int share_sort { get; set; }        
        public DateTime share_date { get; set; }
        public int share_confrim { get; set; }
        public int share_status { get; set; }
        public int share_active { get; set; }
        public int complaint_id { get; set; }
        public DateTime complaint_date { get; set; }
        public int complaint_noticeTableSort { get; set; }       
        public string complaint_text { get; set; }
    }
}