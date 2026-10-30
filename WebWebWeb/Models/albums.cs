using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class albums
    {//profilinde paylaşmak isteyeceği fotoğraflar istediği kadar olabilir. "RESİMLERİM" "VİDEOLARIM"
        //onaylananakadar önce sadece kendisi görecek aslında bu bütün konolarda böyle olacak.
        //RESMİNİ BEĞENME:
        public int id { get; set; }
        public int member_id { get; set; } 
        public int sort { get; set; }
        public string media_url { get; set; }
        public int confrim { get; set; }      
    }
   
}