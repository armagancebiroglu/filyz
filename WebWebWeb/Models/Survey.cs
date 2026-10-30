using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class Survey
    {//anket açıklaması filan. kim tarafından düzenlendi bu kişi admin mi? statustan bakılacak. zaruri anketler olması için de active konuldu
        //id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active,questionCount,answerCount
        //, , [], image_url, , sort, [], , [], LATITUDE, LONGTITUDE, ,,
        public int id { get; set; }//public string survey_name { get; set; }
        public string text { get; set; }
        public int member_id { get; set; }
        public int status { get; set; }//bu anketi düzenleyenin vasfı nedir belirtmek için.
        public DateTime date { get; set; }
        public int confrim { get; set; }//bu anket cevaplanması mecburi olan anketlerden mi? olduğu belirtilecek.
        public int active { get; set; } // yayınlandı mı?
        public string sex { get; set; } //anket kimin için düzenlendi anlamak için
        public int questionCount { get; set; }//kaç sorusu var
        public int answerCount { get; set; } //üye tarafından kaçı cevaplandı
        //bir anket yönetici tarafından üyelerin anketide olsa kopyalanabilmeli ve sitenin kalıcı anketi olabilmeli. bunun için yönetim tarafında bir buton olmalı.
    }
    public class SurveyQA
    {
        public int answer_id { get; set; }
        public int survey_id { get; set; }
        public int question_id { get; set; }
        public int member_id { get; set; }
        public string ABCD_options { get; set; }
        public string questionText { get; set; }
        public string A_text { get; set; }
        public string B_text { get; set; }
        public string C_text { get; set; }
        public string D_text { get; set; }
        public int confrim { get; set; }
        
    }
}