using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class Tag
    {//kim, kimi, neye etiketledi ve etiketlediği şey ne tipinde karşı taraf eklenmeyi kabul etmiş mi? etiketi kaldırmış mı?
        public int who_id { get; set; }
        public int whom_id { get; set; }
        public int shareObje_id { get; set; }
        public int noticeTableSort { get; set; }
        public DateTime date { get; set; }
    }
   
}