using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class Location
    {//id, member_id, text, image_url, sex, sort, date, confrim, status, LATITUDE, LONGITUDE, activ
        public int id { get; set; }
        public int member_id { get; set; }
        public string text { get; set; }
        public string image_url { get; set; }
        public string sex { get; set; }
        public int sort { get; set; }
        public DateTime date { get; set; }
        public int confrim { get; set; }
        public int status { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public int active { get; set; } 
    }
}