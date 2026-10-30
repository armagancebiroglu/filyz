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
    public class Share_MemberBL
    {
        public static List<shareStreem> SHARE_STREEM_ALL(string sex, int member_id,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SHARE_STREEM_ALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@member_id", member_id);
            cmd.Parameters.AddWithValue("@page", page);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    ShareStreemObje.number = Convert.ToInt32(dr["number"]);
                    ShareStreemObje.id = (int)dr["id"];
                    ShareStreemObje.member_id = (int)dr["member_id"];
                    ShareStreemObje.date = (DateTime)dr["date"];
                    ShareStreemObje.text = dr["text"].ToString();
                    ShareStreemObje.image_url = dr["image_url"].ToString();
                    ShareStreemObje.sort = (int)dr["sort"];
                    ShareStreemObje.status = (int)dr["status"];
                    ShareStreemObje.confrim = (int)dr["confrim"];
                    ShareStreemObje.sex = dr["sex"].ToString();
                    if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    {
                        ShareStreemObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareStreemObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    }
                    else
                    {
                        ShareStreemObje.LATITUDE = "";
                        ShareStreemObje.LONGITUDE = "";
                    }
                    ShareStreemObje.active = (int)dr["active"];

                    ShareStreemObje.member_nick_name = dr["nick_name"].ToString();
                    ShareStreemObje.member_sex = dr["member_sex"].ToString();
                    ShareStreemObje.member_age = (DateTime)dr["age"];
                    ShareStreemObje.member_city = dr["city"].ToString();
                    ShareStreemObje.member_profil_media = dr["profil_media"].ToString();
                    ShareStreemObje.member_status = (int)dr["member_status"];
                    ShareStreemObje.member_last_enter_date = (DateTime)dr["last_enter_date"];
                    ShareStreemObje.member_isonline = Convert.ToInt32(dr["is_online"]);
                    ShareStreemObje.member_score = (int)dr["score"];

                    ShareStreemList.Add(ShareStreemObje);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ShareStreemList;
        }

        public static List<shareStreem> SHARE_STREEM_MY_ALL(int member_id,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SHARE_STREEM_MY_ALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            cmd.Parameters.AddWithValue("@page", page);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    ShareStreemObje.number = Convert.ToInt32(dr["number"]);
                    ShareStreemObje.id = (int)dr["id"];
                    ShareStreemObje.member_id = (int)dr["member_id"];
                    ShareStreemObje.date = (DateTime)dr["date"];
                    ShareStreemObje.text = dr["text"].ToString();
                    ShareStreemObje.image_url = dr["image_url"].ToString();
                    ShareStreemObje.sort = (int)dr["sort"];
                    ShareStreemObje.status = (int)dr["status"];
                    ShareStreemObje.confrim = (int)dr["confrim"];
                    ShareStreemObje.sex = dr["sex"].ToString();
                    if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    {
                        ShareStreemObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareStreemObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    }
                    else
                    {
                        ShareStreemObje.LATITUDE = "";
                        ShareStreemObje.LONGITUDE = "";
                    }
                    ShareStreemObje.active = (int)dr["active"];

                    ShareStreemObje.member_nick_name = dr["nick_name"].ToString();
                    ShareStreemObje.member_sex = dr["member_sex"].ToString();
                    ShareStreemObje.member_age = (DateTime)dr["age"];
                    ShareStreemObje.member_city = dr["city"].ToString();
                    ShareStreemObje.member_profil_media = dr["profil_media"].ToString();
                    ShareStreemObje.member_status = (int)dr["member_status"];
                    ShareStreemObje.member_last_enter_date = (DateTime)dr["last_enter_date"];
                    ShareStreemObje.member_isonline = Convert.ToInt32(dr["is_online"]);
                    ShareStreemObje.member_score = (int)dr["score"];

                    ShareStreemList.Add(ShareStreemObje);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ShareStreemList;
        }

        public static List<shareStreem> SHARE_STREEM_MEMBER_ALL(int member_id, int who_id, int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SHARE_STREEM_MEMBER_ALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@page", page);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    ShareStreemObje.number = Convert.ToInt32(dr["number"]);
                    ShareStreemObje.id = (int)dr["id"];
                    ShareStreemObje.member_id = (int)dr["member_id"];
                    ShareStreemObje.date = (DateTime)dr["date"];
                    ShareStreemObje.text = dr["text"].ToString();
                    ShareStreemObje.image_url = dr["image_url"].ToString();
                    ShareStreemObje.sort = (int)dr["sort"];
                    ShareStreemObje.status = (int)dr["status"];
                    ShareStreemObje.confrim = (int)dr["confrim"];
                    ShareStreemObje.sex = dr["sex"].ToString();
                    if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    {
                        ShareStreemObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareStreemObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    }
                    else
                    {
                        ShareStreemObje.LATITUDE = "";
                        ShareStreemObje.LONGITUDE = "";
                    }
                    ShareStreemObje.active = (int)dr["active"];

                    ShareStreemObje.member_nick_name = dr["nick_name"].ToString();
                    ShareStreemObje.member_sex = dr["member_sex"].ToString();
                    ShareStreemObje.member_age = (DateTime)dr["age"];
                    ShareStreemObje.member_city = dr["city"].ToString();
                    ShareStreemObje.member_profil_media = dr["profil_media"].ToString();
                    ShareStreemObje.member_status = (int)dr["member_status"];
                    ShareStreemObje.member_last_enter_date = (DateTime)dr["last_enter_date"];
                    ShareStreemObje.member_isonline = Convert.ToInt32(dr["is_online"]);
                    ShareStreemObje.member_score = (int)dr["score"];

                    ShareStreemList.Add(ShareStreemObje);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ShareStreemList;
        }

        public static shareStreem SHARE_OBJECT_GET_BY_ID(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SHARE_OBJECT_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            shareStreem ShareStreemObje = new shareStreem();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score  
                while (dr.Read())
                {
                    ShareStreemObje.number = Convert.ToInt32(dr["number"]);
                    ShareStreemObje.id = (int)dr["id"];
                    ShareStreemObje.member_id = (int)dr["member_id"];
                    ShareStreemObje.date = (DateTime)dr["date"];
                    ShareStreemObje.text = dr["text"].ToString();
                    ShareStreemObje.image_url = dr["image_url"].ToString();
                    ShareStreemObje.sort = (int)dr["sort"];
                    ShareStreemObje.status = (int)dr["status"];
                    ShareStreemObje.confrim = (int)dr["confrim"];
                    ShareStreemObje.sex = dr["sex"].ToString();
                    if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    {
                        ShareStreemObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareStreemObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    }
                    else
                    {
                        ShareStreemObje.LATITUDE = "";
                        ShareStreemObje.LONGITUDE = "";
                    }
                    ShareStreemObje.active = (int)dr["active"];

                    ShareStreemObje.member_nick_name = dr["nick_name"].ToString();
                    ShareStreemObje.member_sex = dr["member_sex"].ToString();
                    ShareStreemObje.member_age = (DateTime)dr["age"];
                    ShareStreemObje.member_city = dr["city"].ToString();
                    ShareStreemObje.member_profil_media = dr["profil_media"].ToString();
                    ShareStreemObje.member_status = (int)dr["member_status"];
                    ShareStreemObje.member_last_enter_date = (DateTime)dr["last_enter_date"];
                    ShareStreemObje.member_isonline = Convert.ToInt32(dr["is_online"]);
                    ShareStreemObje.member_score = (int)dr["score"];
                    con.Close();
                }
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return (ShareStreemObje);
        }
       
        public static List<shareStreem> SEARCH_LOCATION(int member_id,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_LOCATION", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", member_id);
            cmd.Parameters.AddWithValue("@page", page);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    ShareStreemObje.number = Convert.ToInt32(dr["number"]);
                    ShareStreemObje.id = (int)dr["id"];
                    ShareStreemObje.member_id = (int)dr["member_id"];
                    ShareStreemObje.date = (DateTime)dr["date"];
                    ShareStreemObje.text = dr["text"].ToString();
                    ShareStreemObje.image_url = dr["image_url"].ToString();
                    ShareStreemObje.sort = (int)dr["sort"];
                    ShareStreemObje.status = (int)dr["status"];
                    ShareStreemObje.confrim = (int)dr["confrim"];
                    ShareStreemObje.sex = dr["sex"].ToString();
                    if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    {
                        ShareStreemObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareStreemObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    }
                    else
                    {
                        ShareStreemObje.LATITUDE = "";
                        ShareStreemObje.LONGITUDE = "";
                    }
                    ShareStreemObje.active = (int)dr["active"];

                    ShareStreemObje.member_nick_name = dr["nick_name"].ToString();
                    ShareStreemObje.member_sex = dr["member_sex"].ToString();
                    ShareStreemObje.member_age = (DateTime)dr["age"];
                    ShareStreemObje.member_city = dr["city"].ToString();
                    ShareStreemObje.member_profil_media = dr["profil_media"].ToString();
                    ShareStreemObje.member_status = (int)dr["member_status"];
                    ShareStreemObje.member_last_enter_date = (DateTime)dr["last_enter_date"];
                    ShareStreemObje.member_isonline = Convert.ToInt32(dr["is_online"]);
                    ShareStreemObje.member_score = (int)dr["score"];

                    ShareStreemList.Add(ShareStreemObje);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ShareStreemList;
        }

        public static List<shareStreem> SEARCH_LOCATION_CURSOR(int member_id, string LATITUDE, string LONGTITUDE,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_LOCATION_CURSOR", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", member_id);
            cmd.Parameters.AddWithValue("@LATITUDE", LATITUDE);
            cmd.Parameters.AddWithValue("@LONGTITUDE", LONGTITUDE);
            cmd.Parameters.AddWithValue("@page", page);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    ShareStreemObje.number = Convert.ToInt32(dr["number"]);
                    ShareStreemObje.id = (int)dr["id"];
                    ShareStreemObje.member_id = (int)dr["member_id"];
                    ShareStreemObje.date = (DateTime)dr["date"];
                    ShareStreemObje.text = dr["text"].ToString();
                    ShareStreemObje.image_url = dr["image_url"].ToString();
                    ShareStreemObje.sort = (int)dr["sort"];
                    ShareStreemObje.status = (int)dr["status"];
                    ShareStreemObje.confrim = (int)dr["confrim"];
                    ShareStreemObje.sex = dr["sex"].ToString();
                    if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    {
                        ShareStreemObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareStreemObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    }
                    else
                    {
                        ShareStreemObje.LATITUDE = "";
                        ShareStreemObje.LONGITUDE = "";
                    }
                    ShareStreemObje.active = (int)dr["active"];

                    ShareStreemObje.member_nick_name = dr["nick_name"].ToString();
                    ShareStreemObje.member_sex = dr["member_sex"].ToString();
                    ShareStreemObje.member_age = (DateTime)dr["age"];
                    ShareStreemObje.member_city = dr["city"].ToString();
                    ShareStreemObje.member_profil_media = dr["profil_media"].ToString();
                    ShareStreemObje.member_status = (int)dr["member_status"];
                    ShareStreemObje.member_last_enter_date = (DateTime)dr["last_enter_date"];
                    ShareStreemObje.member_isonline = Convert.ToInt32(dr["is_online"]);
                    ShareStreemObje.member_score = (int)dr["score"];

                    ShareStreemList.Add(ShareStreemObje);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ShareStreemList;
        }

        public static List<shareStreem> SEARCH_LOCATION_DISTANCE(int member_id, string LATITUDE, string LONGTITUDE,int page)
        { 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_LOCATION_DISTANCE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", member_id);
            cmd.Parameters.AddWithValue("@LATITUDE", LATITUDE);
            cmd.Parameters.AddWithValue("@LONGTITUDE", LONGTITUDE);
            cmd.Parameters.AddWithValue("@page", page);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    ShareStreemObje.number = Convert.ToInt32(dr["number"]);
                    ShareStreemObje.id = (int)dr["id"];
                    ShareStreemObje.member_id = (int)dr["member_id"];
                    ShareStreemObje.date = (DateTime)dr["date"];
                    ShareStreemObje.text = dr["text"].ToString();
                    ShareStreemObje.image_url = dr["image_url"].ToString();
                    ShareStreemObje.sort = (int)dr["sort"];
                    ShareStreemObje.status = (int)dr["status"];
                    ShareStreemObje.confrim = (int)dr["confrim"];
                    ShareStreemObje.sex = dr["sex"].ToString();
                    if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    {
                        ShareStreemObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareStreemObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    }
                    else
                    {
                        ShareStreemObje.LATITUDE = "";
                        ShareStreemObje.LONGITUDE = "";
                    }
                    ShareStreemObje.active = (int)dr["active"];

                    ShareStreemObje.member_nick_name = dr["nick_name"].ToString();
                    ShareStreemObje.member_sex = dr["member_sex"].ToString();
                    ShareStreemObje.member_age = (DateTime)dr["age"];
                    ShareStreemObje.member_city = dr["city"].ToString();
                    ShareStreemObje.member_profil_media = dr["profil_media"].ToString();
                    ShareStreemObje.member_status = (int)dr["member_status"];
                    ShareStreemObje.member_last_enter_date = (DateTime)dr["last_enter_date"];
                    ShareStreemObje.member_isonline = Convert.ToInt32(dr["is_online"]);
                    ShareStreemObje.member_score = (int)dr["score"];

                    ShareStreemList.Add(ShareStreemObje);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ShareStreemList;
        }

        public static List<shareStreem> SEARCH_LOCATION_SPACE(int member_id, string LATITUDE, string LONGTITUDE,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_LOCATION_SPACE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", member_id);
            cmd.Parameters.AddWithValue("@LATITUDE", LATITUDE);
            cmd.Parameters.AddWithValue("@LONGTITUDE", LONGTITUDE);
            cmd.Parameters.AddWithValue("@page", page);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    ShareStreemObje.number = Convert.ToInt32(dr["number"]);
                    ShareStreemObje.id = (int)dr["id"];
                    ShareStreemObje.member_id = (int)dr["member_id"];
                    ShareStreemObje.date = (DateTime)dr["date"];
                    ShareStreemObje.text = dr["text"].ToString();
                    ShareStreemObje.image_url = dr["image_url"].ToString();
                    ShareStreemObje.sort = (int)dr["sort"];
                    ShareStreemObje.status = (int)dr["status"];
                    ShareStreemObje.confrim = (int)dr["confrim"];
                    ShareStreemObje.sex = dr["sex"].ToString();
                    if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    {
                        ShareStreemObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareStreemObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    }
                    else
                    {
                        ShareStreemObje.LATITUDE = "";
                        ShareStreemObje.LONGITUDE = "";
                    }
                    ShareStreemObje.active = (int)dr["active"];

                    ShareStreemObje.member_nick_name = dr["nick_name"].ToString();
                    ShareStreemObje.member_sex = dr["member_sex"].ToString();
                    ShareStreemObje.member_age = (DateTime)dr["age"];
                    ShareStreemObje.member_city = dr["city"].ToString();
                    ShareStreemObje.member_profil_media = dr["profil_media"].ToString();
                    ShareStreemObje.member_status = (int)dr["member_status"];
                    ShareStreemObje.member_last_enter_date = (DateTime)dr["last_enter_date"];
                    ShareStreemObje.member_isonline = Convert.ToInt32(dr["is_online"]);
                    ShareStreemObje.member_score = (int)dr["score"];

                    ShareStreemList.Add(ShareStreemObje);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ShareStreemList;
        }
                
    }
}