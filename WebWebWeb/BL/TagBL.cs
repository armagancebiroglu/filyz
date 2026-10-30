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
    public class TagBL
    {
        public static bool TAG_INSERT(Tag tag, int sort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TAG_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", tag.who_id);
            cmd.Parameters.AddWithValue("@whom_id", tag.whom_id);
            cmd.Parameters.AddWithValue("@shareObje_id", tag.shareObje_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", tag.noticeTableSort);
            cmd.Parameters.AddWithValue("@sort",sort);
            cmd.Parameters.AddWithValue("@notice_text", "Sizi bir gönderisinde etiketledi.");

            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (query > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }

        public static bool TAG_DELETE(int who_id, int whom_id,int shareObje_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TAG_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);
            cmd.Parameters.AddWithValue("@shareObje_id",shareObje_id);

            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (query > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static int TAG_COUNT(int who_id, int shareObje_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TAG_COUNT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@shareObje_id", shareObje_id);

            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return query;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        public static int TAG_MY_COUNT(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TAG_MY_COUNT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return query;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static List<MemberShortCard> TAG_MEMBER_LIST(int who_id, int shareObje_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TAG_MEMBER_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@shareObje_id", shareObje_id);
            
            List<MemberShortCard> TagListItemList = new List<MemberShortCard>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//MemberProfil.id, nick_name, sex, matiral_status, age, educational_status, city, profil_media, country, last_enter_date, is_online, eventDate 
                    MemberShortCard MemberShortCard = new MemberShortCard();
                    MemberShortCard.id = (int)dr["id"];
                    MemberShortCard.who_id = (int)dr["who_id"];
                    MemberShortCard.nick_name = dr["nick_name"].ToString();
                    MemberShortCard.sex = dr["sex"].ToString();
                    MemberShortCard.matiral_status = dr["matiral_status"].ToString();
                    MemberShortCard.age = (DateTime)dr["age"];
                    MemberShortCard.educational_status = dr["educational_status"].ToString();
                    MemberShortCard.city = dr["city"].ToString();
                    MemberShortCard.profil_media = dr["profil_media"].ToString();
                    MemberShortCard.country = dr["country"].ToString();
                    MemberShortCard.last_enter_date = (DateTime)dr["last_enter_date"];
                    MemberShortCard.is_online = (bool)dr["is_online"];
                    MemberShortCard.eventDate = (DateTime)dr["eventDate"];
                    TagListItemList.Add(MemberShortCard);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return TagListItemList;
        }

        #region TAG_FAVORITE_LIST   
        //public static List<MemberShortCard> TAG_FAVORITE_LIST(int who_id, int shareObje_id)
        //{
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand("TAG_FAVORITE_LIST", con);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@who_id", who_id);
        //    cmd.Parameters.AddWithValue("@shareObje_id", shareObje_id);
        //    List<MemberShortCard> NoticeListItemList = new List<MemberShortCard>();

        //    try
        //    {
        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {//MemberProfil.id, nick_name, sex, matiral_status, age, educational_status, city, profil_media, country, last_enter_date, is_online, eventDate 
        //            MemberShortCard MemberShortCard = new MemberShortCard();
        //            MemberShortCard.id = (int)dr["id"];
        //            MemberShortCard.who_id = (int)dr["who_id"];
        //            MemberShortCard.nick_name = dr["nick_name"].ToString();
        //            MemberShortCard.sex = dr["sex"].ToString();
        //            MemberShortCard.matiral_status = dr["matiral_status"].ToString();
        //            MemberShortCard.age = (DateTime)dr["age"];
        //            MemberShortCard.educational_status = dr["educational_status"].ToString();
        //            MemberShortCard.city = dr["city"].ToString();
        //            MemberShortCard.profil_media = dr["profil_media"].ToString();
        //            MemberShortCard.country = dr["country"].ToString();
        //            MemberShortCard.last_enter_date = (DateTime)dr["last_enter_date"];
        //            MemberShortCard.is_online = (bool)dr["is_online"];
        //            MemberShortCard.eventDate = (DateTime)dr["eventDate"];
        //            NoticeListItemList.Add(MemberShortCard);
        //        }
        //        con.Close();
        //    }
        //    catch
        //    {
        //        SqlConnection.ClearAllPools();
        //    }
        //    return NoticeListItemList;
        //}
        #endregion

        public static bool TAG_CHECK(int who_id,int whom_id,int obje_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TAG_CHECK", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);

            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (query > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        //
        public static bool TAG_MY_CHECK(int who_id, int obje_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TAG_MY_CHECK", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);

            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (query > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        public static List<shareStreem> TAG_MY_SHARED(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TAG_MY_SHARED", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    //ShareStreemObje.number = Convert.ToInt32(dr["number"]);
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
        //TAG_PROFIL_SHARED
        public static List<shareStreem> TAG_PROFIL_SHARED(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TAG_PROFIL_SHARED", con); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    //ShareStreemObje.number = Convert.ToInt32(dr["number"]);
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