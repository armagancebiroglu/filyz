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
    public class MessagesBL
    {
        public static int MESSAGE_MY_COUNT(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_MY_COUNT", con);
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

        public static int MESSAGE_COUNT(int who_id, int whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_COUNT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);

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

        public static bool MESSAGE_INSERT(Messages messages)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", messages.who_id);
            cmd.Parameters.AddWithValue("@whom_id", messages.whom_id);
            cmd.Parameters.AddWithValue("@text", messages.text);        
            cmd.Parameters.AddWithValue("@notice_text", " size bir mesaj gönderdi.");
            cmd.Parameters.AddWithValue("@noticeTableSort",Convert.ToInt32(Enum.Parse(typeof(noticeTableSort),noticeTableSort.message.ToString())));

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

        public static bool MESSAGE_DELETE_ID(int id,int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_DELETE_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id",id);
            cmd.Parameters.AddWithValue("@member_id", member_id);
            
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

        public static bool MESSAGE_DELETE(int who_id, int whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_DELETE", con);
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

        public static bool MESSAGE_DELETE_ALL(int who_id, int whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_DELETE_ALL", con);
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

        public static List<Messages> MESSAGE_LIST(int who_id,int whom_id)
        {//id, who_id, whom_id, text, date, active
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);
            List<Messages> messageList = new List<Messages>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//
                    Messages message = new Messages();
                    message.id = (int)dr["id"];
                    message.who_id = (int)dr["who_id"];
                    message.whom_id = (int)dr["whom_id"];
                    message.text = dr["text"].ToString();
                    message.nick_name = dr["nick_name"].ToString();
                    message.profil_media = dr["profil_media"].ToString();
                    message.date = (DateTime)dr["date"];
                    message.active = (int)dr["active"];
                    messageList.Add(message);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return messageList;
        }

        public static List<Messages> MESSAGE_MY_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_MY_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<Messages> messageList = new List<Messages>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //MemberProfil.id, nick_name,id, who_id, whom_id, text, date, active  
                    Messages message = new Messages();
                    message.id = (int)dr["id"];
                    message.who_id = (int)dr["who_id"];
                    message.whom_id = (int)dr["whom_id"];
                    message.text = dr["text"].ToString();
                    message.nick_name = dr["nick_name"].ToString();
                    message.profil_media = dr["profil_media"].ToString();
                    message.date = (DateTime)dr["date"];
                    message.active =(int) dr["active"];
                    messageList.Add(message);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return messageList;
        }

        public static List<MemberShortCard> MESSAGE_MY_MEMBER_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_MY_MEMBER_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<MemberShortCard> MessageMemberList = new List<MemberShortCard>();
            
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//MemberProfil.id, nick_name, sex, matiral_status, age, educational_status, city, profil_media, country, last_enter_date, is_online, eventDate 
                    MemberShortCard MessageMember = new MemberShortCard();
                    MessageMember.id = (int)dr["id"];
                    MessageMember.nick_name = dr["nick_name"].ToString();
                    MessageMember.sex = dr["sex"].ToString();
                    MessageMember.matiral_status = dr["matiral_status"].ToString();
                    MessageMember.age = (DateTime)dr["age"];
                    MessageMember.educational_status = dr["educational_status"].ToString();
                    MessageMember.city = dr["city"].ToString();
                    MessageMember.profil_media = dr["profil_media"].ToString();
                    MessageMember.country = dr["country"].ToString();
                    MessageMember.last_enter_date = (DateTime)dr["last_enter_date"];
                    MessageMember.is_online = (bool)dr["is_online"];                   
                    MessageMemberList.Add(MessageMember);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MessageMemberList;
        }

        public static List<Messages> MESSAGE_MY_ALL_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_MY_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<Messages> messageList = new List<Messages>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //MemberProfil.id, nick_name,id, who_id, whom_id, text, date, active  
                    Messages message = new Messages();
                    message.id = (int)dr["id"];
                    message.who_id = (int)dr["who_id"];
                    message.whom_id = (int)dr["whom_id"];
                    message.text = dr["text"].ToString();
                    message.nick_name = dr["nick_name"].ToString();
                    message.profil_media = dr["profil_media"].ToString();
                    message.date = (DateTime)dr["date"];
                    message.active = (int)dr["active"];
                    messageList.Add(message);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return messageList;
        }
        public static List<MemberShortCard> MESSAGE_MY_ALL_MEMBER_LIST(int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MESSAGE_MY_ALL_MEMBER_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@who_id", who_id);
            List<MemberShortCard> MessageMemberList = new List<MemberShortCard>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//MemberProfil.id, nick_name, sex, matiral_status, age, educational_status, city, profil_media, country, last_enter_date, is_online, eventDate 
                    MemberShortCard MessageMember = new MemberShortCard();
                    MessageMember.id = (int)dr["id"];
                    MessageMember.nick_name = dr["nick_name"].ToString();
                    MessageMember.sex = dr["sex"].ToString();
                    MessageMember.matiral_status = dr["matiral_status"].ToString();
                    MessageMember.age = (DateTime)dr["age"];
                    MessageMember.educational_status = dr["educational_status"].ToString();
                    MessageMember.city = dr["city"].ToString();
                    MessageMember.profil_media = dr["profil_media"].ToString();
                    MessageMember.country = dr["country"].ToString();
                    MessageMember.last_enter_date = (DateTime)dr["last_enter_date"];
                    MessageMember.is_online = (bool)dr["is_online"];
                    MessageMemberList.Add(MessageMember);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MessageMemberList;
        }
    }
}