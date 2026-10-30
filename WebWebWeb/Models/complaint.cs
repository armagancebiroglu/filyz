using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class complaint
    {//paylaşım, profil, albüm fotoğrafları, mekan, location,anket açıklaması ve soruları vs her şey şikayete konu olabilir. 
        //şikayetçi olamak için "ben robot değilim" diye denetim yapılacak. ayrıca bir kişi bir haftada en fazla 10 şikayet yapabilir koşulu konulacak. şikayet edilen tarafla inceleme tamamlanana kadar veya asla bir daha hiç bir paylaşımlarını ve profilini görmeyecek. 
        public int id { get; set; }
        public int who_id { get; set; }
        public int whom_id { get; set; }
        public int sort { get; set; }
        public int obje_id { get; set; }
        public string text { get; set; } 
        public DateTime date { get; set; }
        public int noticeTableSort { get; set; }

    }   
}