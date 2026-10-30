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
    public class NoticeBL
    {
        public static int NOTICE_INSERT(Notice notice)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("NOTICE_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", notice.who_id);
            cmd.Parameters.AddWithValue("@whom_id", notice.whom_id);
            cmd.Parameters.AddWithValue("@obje_id", notice.obje_id);
            cmd.Parameters.AddWithValue("@text", notice.text);
            cmd.Parameters.AddWithValue("@noticeTableSort", notice.noticeTableSort);
            cmd.Parameters.AddWithValue("@active", notice.active);
            cmd.Parameters.AddWithValue("@sort", notice.sort);
            int id;
            try
            {              
                con.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return id;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public static int NOTAICE_PASSIVE(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("NOTAICE_PASSIVE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            int count;
            try
            {
                con.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public static bool NOTICE_DELETE(int who_id, int whom_id, int obje_id, int noticeTableSort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("NOTICE_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id",who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
           
            try
            {
                con.Open();
                int rtn = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (rtn>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch
            {
                return false;
            }
        }

        public static List<NoticeListItem> NOTICE_SHORT_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("NOTICE_SHORT_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<NoticeListItem> NoticeListItemList = new List<NoticeListItem>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, who_id, whom_id, sort, obje_id
                    NoticeListItem NoticeListItem = new NoticeListItem();

                    NoticeListItem.id = (int)dr["id"];
                    NoticeListItem.who_id = (int)dr["who_id"]; 
                    NoticeListItem.nick_name = dr["nick_name"].ToString();
                    NoticeListItem.profil_media = dr["profil_media"].ToString();
                    NoticeListItem.is_online = (bool)dr["is_online"];
                    NoticeListItem.obje_id = (int)dr["obje_id"];
                    NoticeListItem.noticeTableSort = (int)dr["noticeTableSort"];
                    NoticeListItem.sort = (int)dr["sort"];
                    NoticeListItem.text = dr["text"].ToString();                    
                    NoticeListItem.date = (DateTime)dr["date"];
                    if ((int)dr["active"]==0)
                    {
                        NoticeListItem.active = false;
                    }
                    else
                    {
                        NoticeListItem.active = true;
                    }             
                   
                    NoticeListItemList.Add(NoticeListItem);
                }
                con.Close();        

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return NoticeListItemList;
        }

        public static List<NoticeListItem> NOTICE_ALL_LIST(int who_id, int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("NOTICE_ALL_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@page", page);
            
            List<NoticeListItem> NoticeListItemList = new List<NoticeListItem>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, who_id, whom_id, sort, obje_id
                    NoticeListItem NoticeListItem = new NoticeListItem();

                    NoticeListItem.id = (int)dr["id"];
                    NoticeListItem.who_id = (int)dr["who_id"];
                    NoticeListItem.nick_name = dr["nick_name"].ToString();
                    NoticeListItem.profil_media = dr["profil_media"].ToString();
                    NoticeListItem.is_online = (bool)dr["is_online"];
                    NoticeListItem.obje_id = (int)dr["obje_id"];
                    NoticeListItem.noticeTableSort = (int)dr["noticeTableSort"];
                    NoticeListItem.sort = (int)dr["sort"];
                    NoticeListItem.text = dr["text"].ToString();
                    NoticeListItem.date = (DateTime)dr["date"];
                    if ((int)dr["active"] == 0)
                    {
                        NoticeListItem.active = false;
                    }
                    else
                    {
                        NoticeListItem.active = true;
                    }

                    NoticeListItemList.Add(NoticeListItem);
                }
                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return NoticeListItemList;
        }

        public static int NOTICE_LIST_COUNT_A(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("NOTICE_LIST_COUNT_A", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            int count_ = 0;
            try
            {
                con.Open();
                count_ = Convert.ToInt32( cmd.ExecuteScalar());               
                con.Close();
                return count_;
            }
            catch
            {
                SqlConnection.ClearAllPools();
                return count_;
            }
           
        }

        public static int SMILE_COUNT(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SMILE_COUNT", con);
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

        public static bool SMILE_CHECK(int who_id, int whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SMILE_CHECK", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);

            bool result;
            int query;
            try
            {
                con.Open();
                query =Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
               
                if (query>0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public static List<MemberShortCard> SMILE_ME_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SMILE_ME_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<MemberShortCard> NoticeListItemList = new List<MemberShortCard>();

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
                    NoticeListItemList.Add(MemberShortCard);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return NoticeListItemList;
        }

        public static List<MemberShortCard> SMILE_MY_ALL_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SMILE_MY_ALL_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<MemberShortCard> NoticeListItemList = new List<MemberShortCard>();

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
                    NoticeListItemList.Add(MemberShortCard);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return NoticeListItemList;
        }

        public static List<MemberShortCard> SMILE_ALL_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SMILE_ALL_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<MemberShortCard> NoticeListItemList = new List<MemberShortCard>();

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
                    NoticeListItemList.Add(MemberShortCard);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return NoticeListItemList;
        }
        
        public static int PROFIL_VIEW_INSERT(Notice notice)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("PROFIL_VIEW_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", notice.who_id);
            cmd.Parameters.AddWithValue("@whom_id", notice.whom_id);
            cmd.Parameters.AddWithValue("@obje_id", notice.obje_id);
            cmd.Parameters.AddWithValue("@text", notice.text);
            cmd.Parameters.AddWithValue("@noticeTableSort", notice.noticeTableSort);
            cmd.Parameters.AddWithValue("@active", notice.active);
            cmd.Parameters.AddWithValue("@sort", notice.sort);
            int id;
            try
            {
                con.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return id;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public static int PROFIL_COUNT(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("PROFIL_COUNT", con);
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

        public static bool PROFIL_CHECK(int who_id, int whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("PROFIL_CHECK", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);

            bool result;
            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (query > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<MemberShortCard> PROFIL_ME_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("PROFIL_ME_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<MemberShortCard> NoticeListItemList = new List<MemberShortCard>();

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
                    NoticeListItemList.Add(MemberShortCard);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return NoticeListItemList;
        }

        public static List<MemberShortCard> PROFIL_MY_ALL_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("PROFIL_MY_ALL_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<MemberShortCard> NoticeListItemList = new List<MemberShortCard>();

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
                    NoticeListItemList.Add(MemberShortCard);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return NoticeListItemList;
        }

        public static List<MemberShortCard> PROFIL_ALL_LIST(int who_id, int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("PROFIL_ALL_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@page", page);
            List<MemberShortCard> NoticeListItemList = new List<MemberShortCard>();

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
                    NoticeListItemList.Add(MemberShortCard);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return NoticeListItemList;
        }

    }
}