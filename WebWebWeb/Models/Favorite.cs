using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class Favorite
    {//karşı tarafı favori listesine alma; favori listesinde bulunanlara ancak davet gönderilir anket doldurması istenir,
        // albüm resmine bakması istenir, profiline bakması istenir vs.
        //id, who_id, whom_id, favoriBlocked, date, active
        public int id { get; set; }
        public int who_id { get; set; }
        public int whom_id { get; set; }
        public int favoriBlocked { get; set; }//
        public DateTime date { get; set; }
        public int active { get; set; }//beni favori listene ekleme gibi bir tepkiye kadar listede duracak. böyle tepki verende silecek tabii. listende ama o onaylamıyor. onaylamayan kişi de beni fovorilerine ekleyeler arasında görmeyecek.      
    }   
}