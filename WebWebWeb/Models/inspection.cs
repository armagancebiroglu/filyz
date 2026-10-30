using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class inspection
    {//editör incelemesini yapacak ilk giriş yapıldığında ve şikayet üzere inceleme yapılacak.inceleme sonucu ilk giriş yapan üyeye ve şikayet edene bildirim metni ile bildirilecek.
        public int id { get; set; }
        public int editör_id { get; set; }
        public int sort_object { get; set; }
        public int sort_object_id { get; set; }
        public string details_link { get; set; }
        public DateTime date { get; set; }
        public int confrim { get; set; }
    }
}