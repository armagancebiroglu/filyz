using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{   
    public class MemberProfil
    {
        //id, nick_name, sex, matiral_status, age, educational_status, city, profil_image, profil_video, confrim, payment_date, password
        public int id { get; set; }

        [Required(ErrorMessage = "Üye kodunuzu giriniz.")]
        [Display(Name = "Üye Kodu")]
        public string nick_name { get; set; }

        [Required(ErrorMessage = "Cinsiyetinizi belirtiniz.")]
        [Display(Name = "Cinsiyetiniz")]
        public string sex { get; set; }

        [Required(ErrorMessage = "Tesettürlü olup olmadığınızı belirtiniz.")]
        [Display(Name = "Tesettür")]
        public bool hicap { get; set; }

        [Required(ErrorMessage = "Doğum tarihinizi belirtiniz.")]
        [Display(Name = "Yaş")]
        public DateTime age { get; set; }

        [Required(ErrorMessage = "Medeni durumunuzu belirtiniz.")]
        [Display(Name = "Medeni Hal")]
        public string matiral_status { get; set; }

        [Required(ErrorMessage = "Çocuğunuzun olup olmadığını belirtiniz.")]
        [Display(Name = "Çocuk")]
        public string children { get; set; }

        [Required(ErrorMessage = "Eğitim durumunuzu belirtiniz.")]
        [Display(Name = "Eğitim Durumu")]
        public string educational_status { get; set; }

        [Required(ErrorMessage = "Yaşadığınız şehri belirtiniz.")]
        [Display(Name = "Yaşadığı Şehir")]
        public string city { get; set; }

        [Required(ErrorMessage = "Memleketinizi belirtiniz.")]
        [Display(Name = "Memleket")]
        public string country { get; set; }

        [Required(ErrorMessage = "Öncelikle yaşamak istediğiniz şehri belirtiniz.")]
        [Display(Name = "Yaşamak İstediği Şehir")]
        public string city_wants_to_live { get; set; }

        [Required(ErrorMessage = "Boy uzunluğunuzu belirtiniz.")]
        [Display(Name = "Boy")]
        public int height { get; set; }

        [Required(ErrorMessage = "Kilonuzu belirtiniz.")]
        [Display(Name = "Kilo")]
        public int kilo { get; set; }

        [Required(ErrorMessage = "Saç renginizi belirtiniz.")]
        [Display(Name = "Saç Rengi")]
        public string hair_color { get; set; }

        [Required(ErrorMessage = "Göz renginizi belirtiniz.")]
        [Display(Name = "Göz Rengi")]
        public string eye_color { get; set; }

        [Required(ErrorMessage = "Fiziksel engelinizin olup olmadığını belirtiniz.")]
        [Display(Name = "Fiziksel Engel Durumu")]
        public string disable { get; set; }

        [Required(ErrorMessage = "300 karakterden fazla olmamak koşuluyla kendinizden biraz bahsetmelisiniz.")]
        [Display(Name = "Diyor ki")]
        [StringLength(300, MinimumLength = 0)]
        public string my_story { get; set; }

        [Required(ErrorMessage = "Şifrenizi unutmanız durumunda üyelik hesabınızı kurtarmak için email adresiniz gereklidir.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Mail adresiniz formata uymamaktadır.")]
        public string e_mail { get; set; }

        [Required(ErrorMessage = "Şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string password { get; set; }

        [Display(Name = "Profil Fotoğrafı")]
        public string profil_media { get; set; }

        public int status { get; set; }

        public DateTime payment_date { get; set; }  
        
        public int score { get; set; }        

       public DateTime last_enter_date { get; set; }

        public bool is_online { get; set; }
        
    }
           
    public class MemberLogin
    {
        public int id { get; set; }

        [Display(Name ="Üye Kodu")]
        [Required(ErrorMessage ="Üye Kodunu Giriniz.")]
        public string nick_name { get; set; }

        [Display(Name ="Şifre")]
        [Required(ErrorMessage ="Şifrenizi Giriniz")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool is_online { get; set; }

        public DateTime payment_date { get; set; }

        public DateTime last_enter_date { get; set; }

        public string profil_media { get; set; }

        public string sex { get; set; }

        public int status { get; set; }
    }

    public class MemberFullCard
    {
        public int id { get; set; }

        [Display(Name = "Üye Kodu")]
        public string nick_name { get; set; }
        public string sex { get; set; }
        [Display(Name = "Medeni Hal")]
        public string matiral_status { get; set; }
        [Display(Name = "Yaş")]
        public DateTime age { get; set; }
        [Display(Name = "Eğitim Durumu")]
        public string educational_status { get; set; }
        [Display(Name = "Yaşadığınız Şehir")]
        public string city { get; set; }
        public string profil_media { get; set; }
        public int status { get; set; }
        public DateTime payment_date { get; set; }
        public string password { get; set; }
        public int member_id { get; set; }
        [Display(Name = "Diyor ki")]
        public string my_story { get; set; }
        [Display(Name = "Memleket")]
        public string country { get; set; }
        [Display(Name = "Yaşamak İstediği Şehir")]
        public string city_wants_to_live { get; set; }
        [Display(Name = "Tesettür")]
        public bool hicap { get; set; }
        [Display(Name = "Boy")]
        public int height { get; set; }
        [Display(Name = "Kilo")]
        public int kilo { get; set; }
        [Display(Name = "Saç Rengi")]
        public string hair_color { get; set; }
        [Display(Name = "Göz Rengi")]
        public string eye_color { get; set; }
        [Display(Name = "Çocuk")]
        public string children { get; set; }
        [Display(Name = "Fiziksel Engel Durumu")]
        public string disable { get; set; }
        public int score { get; set; }
        public string e_mail { get; set; }       
        public DateTime last_enter_date { get; set; } 
    }

    public class ChangePassword
    {
        public int id { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifrede Hata.")]
        public string password { get; set; } 
    }

    public class MemberShortCard
    {//id, nick_name, sex, matiral_status, age, educational_status, city, profil_media, country, last_enter_date, is_online, eventDate
        public int id { get; set; }
        public int who_id { get; set; } 
        public string nick_name { get; set; }
        public string sex { get; set; }
        public string matiral_status { get; set; }
        public DateTime age { get; set; }
        public string educational_status { get; set; }
        public string city { get; set; }
        public string profil_media { get; set; }
        public string country { get; set; }
        public DateTime last_enter_date { get; set; }
        public bool is_online { get; set; }
        public DateTime eventDate { get; set; } 
    }

    public class MemberSurveyAnswer
    {//member_id,percent, nick_name, matiral_status, age, educational_status, city, profil_media, country, hicap,score, last_enter_date, is_online, date
        public int member_id { get; set; }
        public decimal percent { get; set; }
        public string nick_name { get; set; }
        public string matiral_status { get; set; }
        public string educational_status { get; set; }
        public DateTime age { get; set; }
        public string city { get; set; }
        public string profil_media { get; set; }
        public string country { get; set; }
        public int hicap { get; set; }
        public int score { get; set; } 
        public DateTime last_enter_date { get; set; }
        public int is_online { get; set; }
        public DateTime date { get; set; }
    }
}
 