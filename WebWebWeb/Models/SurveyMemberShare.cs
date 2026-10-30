using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class SurveyMemberShare
    {//bir üye birinin anketini paylaştığında buraya data verilecek. anketinizi paylaştı diye bildirim gidecek.
        //aslında bu ara tablolar paylaşımları bildirmek için iyi bir yol.
        public int id { get; set; }
        public int survey_id { get; set; }
        public int member_id { get; set; }
        public DateTime date { get; set; }
    }
}