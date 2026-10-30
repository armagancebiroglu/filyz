using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class SurveyAnswer
    {//bir anketin tüm sorularına cevap vermek zorunda değilsiniz? "şu anketinizin bazı sorularına cevap verdi!" gibi bir bildirimle tıklayınca de kaç sorusuna cevap verdiyle alt alta soru cevap listelenecek.
        public int id { get; set; }
        public int survey_id { get; set; }
        public int question_id { get; set; }
        public int member_id { get; set; }
        public string ABCD_options { get; set; }       
    }
    public class SurveyAnswerItem
    {
        public int id { get; set; }
        public int question_id { get; set; }
        public int member_id { get; set; }
        public int survey_id { get; set; }
        public string questionText { get; set; }
        public string A_text { get; set; }
        public string B_text { get; set; }
        public string C_text { get; set; }
        public string D_text { get; set; }
        public string ABCD_options { get; set; }
        public int confrim { get; set; } 

    }
}