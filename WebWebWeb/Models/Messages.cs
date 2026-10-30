using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class Messages
    {// üyeler birbirine mesaj yazacak
        public int id { get; set; }
        public int who_id { get; set; }
        public int whom_id { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
        public int active { get; set; }
        public string nick_name { get; set; }
        public string profil_media { get; set; }
    }        
}