using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class SurveyQuestion
    {//anketin soruları: her soru şıkları ile yazılzcak. bunlar tek tek şikayet edilebilecek.
        //her sorunun en az ve en çok 4 seçeneği olacak. boş bırakılırsa da çıkmayacak.
        public int id { get; set; }
        public int survey_id { get; set; }
        public string questionText { get; set; }
        public string A_text { get; set; }
        public string B_text { get; set; }
        public string C_text { get; set; }
        public string D_text { get; set; }
        public int confrim { get; set; }
    }
}