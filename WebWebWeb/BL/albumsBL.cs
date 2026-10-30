using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebWebWeb.Models;//hepsine bu sekilde referans verilecek
namespace WebWebWeb.BL
{
    public class albumsBL
    { 
      public static int ALBUM_INSERT(albums album)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ALBUM_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", album.member_id);
            cmd.Parameters.AddWithValue("@sort", album.sort);
            cmd.Parameters.AddWithValue("@media_url", album.media_url);
            cmd.Parameters.AddWithValue("@confrim", album.confrim);
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
      public static void ALBUM_DELETE(int id,int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ALBUM_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters.AddWithValue("@member_id", member_id); 
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
        }
      public static albums ALBUM_GET_BY_ID(int id,int member_id, int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ALBUM_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);  
            cmd.Parameters.AddWithValue("@member_id", member_id);
            cmd.Parameters.AddWithValue("@who_id", who_id);
            albums album = new albums();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, sort, media_url, confrim
                    album = new albums();
                    album.id = (int)dr["id"];
                    album.member_id = (int)dr["member_id"];
                    album.sort = (int)dr["sort"];
                    album.media_url = dr["media_url"].ToString();
                    album.confrim = (int)dr["confrim"];
                }
                con.Close();
                return album;
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return album;
        }
      public static List<albums> ALBUM_GET_ALL(int member_id, int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ALBUM_GET_ALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<albums> albus_ = new List<albums>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, sort, media_url, confrim
                    albums album = new albums();
                    album = new albums();
                    album.id = (int)dr["id"];
                    album.member_id = (int)dr["member_id"];
                    album.sort = (int)dr["sort"];
                    album.media_url = dr["media_url"].ToString();
                    album.confrim = (int)dr["confrim"];
                    albus_.Add(album);
                }
               
                con.Close();
                
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return albus_;
        }
      public static int ALBUM_COUNT_GET_BY_MEMBER_ID(int member_id) 
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ALBUM_COUNT_GET_BY_MEMBER_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            int count;
            try
            {
                con.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count;
            }
            catch
            {
               return 6;
            }
        }
    }
}