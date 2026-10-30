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
    public class LocationBL
    {
        public static int LOCATION_INSERT(Location location)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("LOCATION_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", location.member_id);
            cmd.Parameters.AddWithValue("@text", location.text);
            cmd.Parameters.AddWithValue("@image_url", location.image_url);
            cmd.Parameters.AddWithValue("@sex", location.sex);
            cmd.Parameters.AddWithValue("@sort", location.sort);
            cmd.Parameters.AddWithValue("@date", location.date);
            cmd.Parameters.AddWithValue("@confrim", location.confrim);
            cmd.Parameters.AddWithValue("@status", location.status);          
            cmd.Parameters.AddWithValue("@LATITUDE", location.LATITUDE);
            cmd.Parameters.AddWithValue("@LONGITUDE", location.LONGITUDE);
            cmd.Parameters.AddWithValue("@activ", location.active);
            int id;
            try
            {
                con.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return id;
            }
            catch
            {
                return 0;
            }

        }
    }
}