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
    public class FavoriteBL
    {
        public static int FAVORITE_INSERT(Favorite favorite)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("FAVORITE_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", favorite.who_id);
            cmd.Parameters.AddWithValue("@whom_id", favorite.whom_id);
            cmd.Parameters.AddWithValue("@favoriBlocked", favorite.favoriBlocked);
            cmd.Parameters.AddWithValue("@active", favorite.active);
            cmd.Parameters.AddWithValue("@sort", Convert.ToInt32(Enum.Parse(typeof(sort), sort.image.ToString())));
            cmd.Parameters.AddWithValue("@noticeTableSort", Convert.ToInt32(Enum.Parse(typeof(noticeTableSort), noticeTableSort.Favorite.ToString())));
            cmd.Parameters.AddWithValue("@notice_text", " Sizi favori listesine ekledi.");

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

        public static int FAVORITE_GET_BY_ID(int who_id, int whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("FAVORITE_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);

            Favorite favorite = new Favorite();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, who_id, whom_id, favoriBlocked, date, active
                    favorite = new Favorite();
                    favorite.id = (int)dr["id"];
                    favorite.who_id = (int)dr["who_id"];
                    favorite.whom_id = (int)dr["whom_id"];
                    favorite.favoriBlocked = (int)dr["favoriBlocked"];
                    favorite.date = (DateTime)dr["date"];
                    favorite.active = (int)dr["active"];
                }
                con.Close();

                return favorite.favoriBlocked;
            }
            catch
            {
                SqlConnection.ClearAllPools();
                return 3;
            }
        }

        public static int FAVORITE_DELETE(int who_id, int whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("FAVORITE_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);

            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (query > 0)
                {
                    return 0;
                }
                else
                {
                    return 3;
                }
            }
            catch (Exception)
            {
                return 3;

            }
        }

        public static int FAVORITE_COUNT(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("FAVORITE_COUNT", con);
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

        public static List<MemberShortCard> FAVORITE_ME_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("FAVORITE_ME_LIST", con);
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

        public static List<MemberShortCard> FAVORITE_MY_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("FAVORITE_MY_LIST", con);
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

        public static List<MemberShortCard> FAVORITE_MY_ALL_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("FAVORITE_MY_ALL_LIST", con);
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

        public static List<MemberShortCard> FAVORITE_ALL_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("FAVORITE_ALL_LIST", con);
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

        public class ChatMember
        {
            public int member_id { get; set; }
            public string nick_name { get; set; }
            public string img_url { get; set; }
            public int age { get; set; }
            public bool is_online { get; set; }
        }
        public static List<ChatMember> FAVORITE_CHAT_LIST(int who_id, string nick_name)
        {
            try
            {
                List<MemberShortCard> ChatFavoriMemberList = FAVORITE_MY_LIST(who_id);
                List<ChatMember> ChatMemberList = new List<ChatMember>();
                foreach (var member in ChatFavoriMemberList)
                {
                    ChatMember ChatMember_ = new ChatMember();
                    ChatMember_.member_id = member.id;
                    ChatMember_.nick_name = member.nick_name;
                    ChatMember_.img_url = member.profil_media;
                    ChatMember_.age = (int)(DateTime.Now.Year) - (int)((member.age).Year);
                    ChatMember_.is_online = member.is_online;
                    ChatMemberList.Add(ChatMember_);
                }
                if (!string.IsNullOrEmpty(nick_name))
                {
                    MemberProfil Member = MemberProfilBL.MEMBER_PROFIL_GET_BY_NICK_NAME(nick_name);
                    ChatMember ChatMember_ = new ChatMember();
                    ChatMember_.member_id = Member.id;
                    ChatMember_.nick_name = Member.nick_name;
                    ChatMember_.img_url = Member.profil_media;
                    ChatMember_.age = (int)(DateTime.Now.Year) - (int)((Member.age).Year);
                    ChatMember_.is_online = Member.is_online;
                    ChatMemberList.Add(ChatMember_);
                }
                return ChatMemberList;
            }
            catch
            {
                return null;
            }
        }
    }
}