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
    public class ShareObjectBL
    {
        public static int SHARE_OBJECT_INSERT(ShareObject ShareObject)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SHARE_OBJECT_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", ShareObject.member_id);
            cmd.Parameters.AddWithValue("@text", ShareObject.text);
            cmd.Parameters.AddWithValue("@image_url", ShareObject.image_url);
            cmd.Parameters.AddWithValue("@sex", ShareObject.sex);
            cmd.Parameters.AddWithValue("@sort", ShareObject.sort);
            cmd.Parameters.AddWithValue("@date", ShareObject.date);
            cmd.Parameters.AddWithValue("@confrim", ShareObject.confrim);
            cmd.Parameters.AddWithValue("@status", ShareObject.status);
            cmd.Parameters.AddWithValue("@LATITUDE", ShareObject.LATITUDE);
            cmd.Parameters.AddWithValue("@LONGITUDE", ShareObject.LONGITUDE);
            cmd.Parameters.AddWithValue("@active", ShareObject.active);

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

        public static int SHARE_OBJECT_DELETE(int id, int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SHARE_OBJECT_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@member_id", member_id); 
            int deleted;
            try
            {
                con.Open();
                deleted = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return deleted;
            }
            catch
            {
                return 0;
            }
        }

        public static ShareObject SHARE_OBJECT_BY_ID(int obje_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SHARE_OBJECT_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
           ShareObject ShareObje = new ShareObject();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(); 
                //id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active 
                while (dr.Read())
                {
                    ShareObje.id = (int)dr["id"];
                    ShareObje.member_id = (int)dr["member_id"];
                    ShareObje.date = (DateTime)dr["date"];
                    ShareObje.text = dr["text"].ToString();
                    ShareObje.image_url = dr["image_url"].ToString();
                    ShareObje.sort = (int)dr["sort"];
                    ShareObje.status = (int)dr["status"];
                    ShareObje.confrim = (int)dr["confrim"];
                    ShareObje.sex = dr["sex"].ToString();
                    if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    {
                        ShareObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    }
                    else
                    {
                        ShareObje.LATITUDE = "";
                        ShareObje.LONGITUDE = "";
                    }
                    ShareObje.active = (int)dr["active"];
                    
                    con.Close();
                }
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return (ShareObje);
        }

        public static int SHARE_OBJECT_UPDATE(ShareObject ShareObject)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SHARE_OBJECT_UPDATE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@text", ShareObject.text);
            cmd.Parameters.AddWithValue("@image_url", ShareObject.image_url);
            cmd.Parameters.AddWithValue("@sex", ShareObject.sex);
            cmd.Parameters.AddWithValue("@confrim", ShareObject.confrim);
            cmd.Parameters.AddWithValue("@LATITUDE", ShareObject.LATITUDE);
            cmd.Parameters.AddWithValue("@LONGITUDE", ShareObject.LONGITUDE);
            cmd.Parameters.AddWithValue("@id", ShareObject.id);
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

        public static List<ShareObject> HOME_STREEM(string sex,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("HOME_STREEM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@page", page);
            List<ShareObject> ShareObjeList = new List<ShareObject>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //
                while (dr.Read())
                {
                    ShareObject ShareObje = new ShareObject();
                    ShareObje.id = (int)dr["id"];
                    ShareObje.member_id = (int)dr["member_id"];
                    ShareObje.date = (DateTime)dr["date"];
                    ShareObje.text = dr["text"].ToString();
                    ShareObje.image_url = dr["image_url"].ToString();
                    ShareObje.sort = (int)dr["sort"];
                    ShareObje.status = (int)dr["status"];
                    ShareObje.confrim = (int)dr["confrim"];
                    ShareObje.sex = dr["sex"].ToString();
                    ShareObje.LATITUDE = dr["LATITUDE"].ToString();
                    ShareObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    ShareObje.active = (int)dr["active"];

                    ShareObjeList.Add(ShareObje);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return (ShareObjeList);
        }

        public static List<ShareObject> HOME_STREEM_SORT(string sex, int sort,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("HOME_STREEM_SORT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@sort", sort);
            cmd.Parameters.AddWithValue("@page", page);
            List<ShareObject> ShareObjeList = new List<ShareObject>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //
                while (dr.Read())
                {
                    ShareObject ShareObje = new ShareObject();
                    ShareObje.id = (int)dr["id"];
                    ShareObje.member_id = (int)dr["member_id"];
                    ShareObje.date = (DateTime)dr["date"];
                    ShareObje.text = dr["text"].ToString();
                    ShareObje.image_url = dr["image_url"].ToString();
                    ShareObje.sort = (int)dr["sort"];
                    ShareObje.status = (int)dr["status"];
                    ShareObje.confrim = (int)dr["confrim"];
                    ShareObje.sex = dr["sex"].ToString();
                    ShareObje.LATITUDE = dr["LATITUDE"].ToString();
                    ShareObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    ShareObje.active = (int)dr["active"];

                    ShareObjeList.Add(ShareObje);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return (ShareObjeList);
        }
    }
}