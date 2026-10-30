using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebWebWeb.Models;
namespace WebWebWeb.BL
{
    public class ForbiddenBL
    {
        public static List<forbidden> FORBIDDEN_GET()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("FORBIDDEN_GET", con);
            cmd.CommandType = CommandType.StoredProcedure;
            List<forbidden> forbiddens_ = new List<forbidden>();
            try
            {
                
                con.Open();
               SqlDataReader dr= cmd.ExecuteReader();
                while (dr.Read())
                {
                    forbidden forbidden = new forbidden();
                    forbidden.id = (int)dr["id"];
                    forbidden.text = dr["text"].ToString();
                    forbiddens_.Add(forbidden);
                }
                
            }
            catch
            {

            }
            return forbiddens_;
        }

    }
}