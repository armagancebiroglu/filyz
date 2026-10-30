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
    public class MemberProfilBL
    {
        public static int MEMBER_PROFIL_INSERT(MemberProfil MemberProfil)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_PROFIL_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nick_name", MemberProfil.nick_name);
            cmd.Parameters.AddWithValue("@sex", MemberProfil.sex);
            cmd.Parameters.AddWithValue("@matiral_status", MemberProfil.matiral_status);
            cmd.Parameters.AddWithValue("@age", MemberProfil.age);
            cmd.Parameters.AddWithValue("@educational_status", MemberProfil.educational_status);
            cmd.Parameters.AddWithValue("@city", MemberProfil.city);
            cmd.Parameters.AddWithValue("@profil_media", MemberProfil.profil_media);
            cmd.Parameters.AddWithValue("@status", MemberProfil.status);
            cmd.Parameters.AddWithValue("@payment_date", MemberProfil.payment_date);
            cmd.Parameters.AddWithValue("@password", MemberProfil.password);
            cmd.Parameters.AddWithValue("@my_story", MemberProfil.my_story);
            cmd.Parameters.AddWithValue("@country", MemberProfil.country);
            cmd.Parameters.AddWithValue("@city_wants_to_live", MemberProfil.city_wants_to_live);
            cmd.Parameters.AddWithValue("@hicap", MemberProfil.hicap);
            cmd.Parameters.AddWithValue("@height", MemberProfil.height);
            cmd.Parameters.AddWithValue("@kilo", MemberProfil.kilo);
            cmd.Parameters.AddWithValue("@hair_color", MemberProfil.hair_color);
            cmd.Parameters.AddWithValue("@eye_color", MemberProfil.eye_color);
            cmd.Parameters.AddWithValue("@children", MemberProfil.children);
            cmd.Parameters.AddWithValue("@disable", MemberProfil.disable);
            cmd.Parameters.AddWithValue("@score", MemberProfil.score);
            cmd.Parameters.AddWithValue("@e_mail", MemberProfil.e_mail);
            cmd.Parameters.AddWithValue("@is_online", MemberProfil.is_online);
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

        public static int MEMBER_PROFIL_UPDATE(MemberProfil MemberProfil)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_PROFIL_UPDATE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", MemberProfil.id);
            cmd.Parameters.AddWithValue("@matiral_status", MemberProfil.matiral_status);
            cmd.Parameters.AddWithValue("@age", MemberProfil.age);
            cmd.Parameters.AddWithValue("@educational_status", MemberProfil.educational_status);
            cmd.Parameters.AddWithValue("@city", MemberProfil.city);
            cmd.Parameters.AddWithValue("@my_story", MemberProfil.my_story);
            cmd.Parameters.AddWithValue("@country", MemberProfil.country);
            cmd.Parameters.AddWithValue("@city_wants_to_live", MemberProfil.city_wants_to_live);
            cmd.Parameters.AddWithValue("@hicap", MemberProfil.hicap);
            cmd.Parameters.AddWithValue("@height", MemberProfil.height);
            cmd.Parameters.AddWithValue("@kilo", MemberProfil.kilo);
            cmd.Parameters.AddWithValue("@hair_color", MemberProfil.hair_color);
            cmd.Parameters.AddWithValue("@eye_color", MemberProfil.eye_color);
            cmd.Parameters.AddWithValue("@children", MemberProfil.children);
            cmd.Parameters.AddWithValue("@disable", MemberProfil.disable);
            cmd.Parameters.AddWithValue("@e_mail", MemberProfil.e_mail);

            int st = 0;
            try
            {
                con.Open();
                cmd.ExecuteScalar();
                st = 1;
                con.Close();
            }
            catch (Exception)
            {
                st = 0;
            }

            return st;
        }

        public static MemberProfil MEMBER_PROFIL_GET_BY_NICK_NAME(string nick_name)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_PROFIL_GET_BY_NICK_NAME", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nick_name", nick_name);
            MemberProfil memberProfil = new MemberProfil();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    memberProfil = new MemberProfil();
                    memberProfil.id = (int)dr["id"];
                    memberProfil.nick_name = dr["nick_name"].ToString();
                    memberProfil.sex = dr["sex"].ToString();
                    memberProfil.matiral_status = dr["matiral_status"].ToString();
                    memberProfil.age = (DateTime)dr["age"];
                    memberProfil.educational_status = dr["educational_status"].ToString();
                    memberProfil.city = dr["city"].ToString();
                    memberProfil.profil_media = dr["profil_media"].ToString();
                    memberProfil.status = (int)dr["status"];
                    memberProfil.payment_date = (DateTime)dr["payment_date"];
                    memberProfil.password = dr["password"].ToString();
                    memberProfil.my_story = dr["my_story"].ToString();
                    memberProfil.country = dr["country"].ToString();
                    memberProfil.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    memberProfil.hicap = (bool)dr["hicap"];
                    memberProfil.height = (int)dr["height"];
                    memberProfil.kilo = (int)dr["kilo"];
                    memberProfil.hair_color = dr["hair_color"].ToString();
                    memberProfil.eye_color = dr["eye_color"].ToString();
                    memberProfil.children = dr["children"].ToString();
                    memberProfil.disable = dr["disable"].ToString();
                    memberProfil.score = (int)dr["score"];
                    memberProfil.e_mail = dr["e_mail"].ToString();
                    memberProfil.last_enter_date = (DateTime)dr["last_enter_date"];
                    memberProfil.is_online = (bool)dr["is_online"];
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return memberProfil;
        }

        public static MemberProfil MEMBER_PROFIL_GET_BY_ID(int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_PROFIL_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            MemberProfil memberProfil = new MemberProfil();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    memberProfil = new MemberProfil();
                    memberProfil.id = (int)dr["id"];
                    memberProfil.nick_name = dr["nick_name"].ToString();
                    memberProfil.sex = dr["sex"].ToString();
                    memberProfil.matiral_status = dr["matiral_status"].ToString();
                    memberProfil.age = (DateTime)dr["age"];
                    memberProfil.educational_status = dr["educational_status"].ToString();
                    memberProfil.city = dr["city"].ToString();
                    memberProfil.profil_media = dr["profil_media"].ToString();
                    memberProfil.status = (int)dr["status"];
                    memberProfil.payment_date = (DateTime)dr["payment_date"];
                    memberProfil.password = dr["password"].ToString();
                    memberProfil.my_story = dr["my_story"].ToString();
                    memberProfil.country = dr["country"].ToString();
                    memberProfil.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    memberProfil.hicap = (bool)dr["hicap"];
                    memberProfil.height = (int)dr["height"];
                    memberProfil.kilo = (int)dr["kilo"];
                    memberProfil.hair_color = dr["hair_color"].ToString();
                    memberProfil.eye_color = dr["eye_color"].ToString();
                    memberProfil.children = dr["children"].ToString();
                    memberProfil.disable = dr["disable"].ToString();
                    memberProfil.score = (int)dr["score"];
                    memberProfil.e_mail = dr["e_mail"].ToString();
                    memberProfil.last_enter_date = (DateTime)dr["last_enter_date"];
                    memberProfil.is_online = (bool)dr["is_online"];
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return memberProfil;
        }

        public static MemberFullCard MEMBER_FULL_SELECT(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_FULL_SELECT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            MemberFullCard MemberFullCard = new MemberFullCard();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MemberFullCard = new MemberFullCard();
                    MemberFullCard.id = (int)dr["id"];
                    MemberFullCard.nick_name = dr["nick_name"].ToString();
                    MemberFullCard.sex = dr["sex"].ToString();
                    MemberFullCard.matiral_status = dr["matiral_status"].ToString();
                    MemberFullCard.age = (DateTime)dr["age"];
                    MemberFullCard.educational_status = dr["educational_status"].ToString();
                    MemberFullCard.city = dr["city"].ToString();
                    MemberFullCard.profil_media = dr["profil_media"].ToString();
                    MemberFullCard.status = (int)dr["status"];
                    MemberFullCard.payment_date = (DateTime)dr["payment_date"];
                    MemberFullCard.password = dr["password"].ToString();
                    MemberFullCard.my_story = dr["my_story"].ToString();
                    MemberFullCard.country = dr["country"].ToString();
                    MemberFullCard.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    MemberFullCard.hicap = (bool)dr["hicap"];
                    MemberFullCard.height = (int)dr["height"];
                    MemberFullCard.kilo = (int)dr["kilo"];
                    MemberFullCard.hair_color = dr["hair_color"].ToString();
                    MemberFullCard.eye_color = dr["eye_color"].ToString();
                    MemberFullCard.children = dr["children"].ToString();
                    MemberFullCard.disable = dr["disable"].ToString();
                    MemberFullCard.score = (int)dr["score"];
                    MemberFullCard.e_mail = dr["e_mail"].ToString();
                    MemberFullCard.last_enter_date = (DateTime)dr["last_enter_date"];
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MemberFullCard;
        }

        public static bool CODE_QUERY(string code)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("CODE_QUERY", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code", code);
            bool kod = false;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    kod = false;
                }
                else
                {
                    kod = true;
                }
                con.Close();
            }
            catch (Exception)
            {
                SqlConnection.ClearAllPools();
            }
            return kod;
        }

        public static MemberLogin MEMBER_LOGIN(string nick_name)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_LOGIN", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nick_name", nick_name);
            MemberLogin memberLogin = new MemberLogin();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    memberLogin = new MemberLogin();
                    memberLogin.id = (int)dr["id"];
                    memberLogin.nick_name = dr["nick_name"].ToString();
                    memberLogin.password = dr["password"].ToString();
                    memberLogin.profil_media = dr["profil_media"].ToString();
                    memberLogin.sex = dr["sex"].ToString();
                    memberLogin.status = (int)dr["status"];
                    memberLogin.payment_date = (DateTime)dr["payment_date"];

                    if (memberLogin.last_enter_date.Year <= 2016)
                    {
                        memberLogin.last_enter_date = new DateTime(2017, 01, 01);
                    }
                    else
                    {
                        memberLogin.last_enter_date = (DateTime)dr["last_enter_date"];
                    }

                    memberLogin.is_online = (bool)dr["is_online"];
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return memberLogin;
        }

        public static bool IS_ONLINE(string nick_name, bool is_online)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("IS_ONLINE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nick_name", nick_name);
            cmd.Parameters.AddWithValue("@is_online", is_online);
            bool value;
            try
            {
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                value = true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return value;
        }

        public static MemberFullCard MEMBER_FULL_SELECT_BY_CODE(string nick_name)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_FULL_SELECT_BY_CODE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("nick_name", nick_name);
            MemberFullCard MemberFullCard = new MemberFullCard();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MemberFullCard = new MemberFullCard();
                    MemberFullCard.id = (int)dr["id"];
                    MemberFullCard.nick_name = dr["nick_name"].ToString();
                    MemberFullCard.sex = dr["sex"].ToString();
                    MemberFullCard.matiral_status = dr["matiral_status"].ToString();
                    MemberFullCard.age = (DateTime)dr["age"];
                    MemberFullCard.educational_status = dr["educational_status"].ToString();
                    MemberFullCard.city = dr["city"].ToString();
                    MemberFullCard.profil_media = dr["profil_media"].ToString();
                    MemberFullCard.status = (int)dr["status"];
                    MemberFullCard.payment_date = (DateTime)dr["payment_date"];
                    MemberFullCard.password = dr["password"].ToString();
                    MemberFullCard.my_story = dr["my_story"].ToString();
                    MemberFullCard.country = dr["country"].ToString();
                    MemberFullCard.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    MemberFullCard.hicap = (bool)dr["hicap"];
                    MemberFullCard.height = (int)dr["height"];
                    MemberFullCard.kilo = (int)dr["kilo"];
                    MemberFullCard.hair_color = dr["hair_color"].ToString();
                    MemberFullCard.eye_color = dr["eye_color"].ToString();
                    MemberFullCard.children = dr["children"].ToString();
                    MemberFullCard.disable = dr["disable"].ToString();
                    MemberFullCard.score = (int)dr["score"];
                    MemberFullCard.e_mail = dr["e_mail"].ToString();
                    MemberFullCard.last_enter_date = (DateTime)dr["last_enter_date"];
                }
                con.Close();
            }
            catch (Exception)
            {
                SqlConnection.ClearAllPools();
            }
            return MemberFullCard;
        }

        public static int MEMBER_PRFIL_IMAGE_UPDATE(int id, string profil_media)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_PRFIL_IMAGE_UPDATE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@profil_media", profil_media);
            MemberFullCard MemberFullCard = new MemberFullCard();
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

        public static int CHANGE_PASSWORD(int id, string password)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("CHANGE_PASSWORD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@password", password);
            int st = 0;
            try
            {
                con.Open();
                st = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            catch
            {
                st = 0;
            }

            return st;
        }

        public static List<MemberProfil> MEMBER_PROFIL_GET_COMMENT_LIST(int obje_id, int noticeTableSort, int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_PROFIL_GET_COMMENT_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
            cmd.Parameters.AddWithValue("@member_id", member_id);
            List<MemberProfil> MemberProfilList = new List<MemberProfil>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MemberProfil memberProfil = new MemberProfil();
                    memberProfil.id = (int)dr["id"];
                    memberProfil.nick_name = dr["nick_name"].ToString();
                    memberProfil.sex = dr["sex"].ToString();
                    memberProfil.matiral_status = dr["matiral_status"].ToString();
                    memberProfil.age = (DateTime)dr["age"];
                    memberProfil.educational_status = dr["educational_status"].ToString();
                    memberProfil.city = dr["city"].ToString();
                    memberProfil.profil_media = dr["profil_media"].ToString();
                    memberProfil.status = (int)dr["status"];
                    memberProfil.payment_date = (DateTime)dr["payment_date"];
                    memberProfil.password = dr["password"].ToString();
                    memberProfil.my_story = dr["my_story"].ToString();
                    memberProfil.country = dr["country"].ToString();
                    memberProfil.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    memberProfil.hicap = (bool)dr["hicap"];
                    memberProfil.height = (int)dr["height"];
                    memberProfil.kilo = (int)dr["kilo"];
                    memberProfil.hair_color = dr["hair_color"].ToString();
                    memberProfil.eye_color = dr["eye_color"].ToString();
                    memberProfil.children = dr["children"].ToString();
                    memberProfil.disable = dr["disable"].ToString();
                    memberProfil.score = (int)dr["score"];
                    memberProfil.e_mail = dr["e_mail"].ToString();
                    memberProfil.last_enter_date = (DateTime)dr["last_enter_date"];
                    memberProfil.is_online = (bool)dr["is_online"];
                    MemberProfilList.Add(memberProfil);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MemberProfilList;
        }

        public static MemberShortCard MEMBER_SHORT_CARD(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_SHORT_CARD", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);
            MemberShortCard member = new MemberShortCard();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//MemberProfil.id, nick_name, sex, matiral_status, age, educational_status, city, profil_media, country, last_enter_date, is_online, eventDate                    
                    member.id = (int)dr["id"];
                    member.nick_name = dr["nick_name"].ToString();
                    member.sex = dr["sex"].ToString();
                    member.matiral_status = dr["matiral_status"].ToString();
                    member.age = (DateTime)dr["age"];
                    member.educational_status = dr["educational_status"].ToString();
                    member.city = dr["city"].ToString();
                    member.profil_media = dr["profil_media"].ToString();
                    member.country = dr["country"].ToString();
                    member.last_enter_date = (DateTime)dr["last_enter_date"];
                    member.is_online = (bool)dr["is_online"];

                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return member;
        }

        public static List<MemberProfil> SEARCH_PAGE_MEMBER_PROFIL(int id,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_PAGE_MEMBER_PROFIL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@page", page);
            List<MemberProfil> MemberProfilList = new List<MemberProfil>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MemberProfil memberProfil = new MemberProfil();
                    memberProfil.id = (int)dr["id"];
                    memberProfil.nick_name = dr["nick_name"].ToString();
                    memberProfil.sex = dr["sex"].ToString();
                    memberProfil.matiral_status = dr["matiral_status"].ToString();
                    memberProfil.age = (DateTime)dr["age"];
                    memberProfil.educational_status = dr["educational_status"].ToString();
                    memberProfil.city = dr["city"].ToString();
                    memberProfil.profil_media = dr["profil_media"].ToString();
                    memberProfil.status = (int)dr["status"];
                    memberProfil.payment_date = (DateTime)dr["payment_date"];
                    memberProfil.password = dr["password"].ToString();
                    memberProfil.my_story = dr["my_story"].ToString();
                    memberProfil.country = dr["country"].ToString();
                    memberProfil.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    memberProfil.hicap = (bool)dr["hicap"];
                    memberProfil.height = (int)dr["height"];
                    memberProfil.kilo = (int)dr["kilo"];
                    memberProfil.hair_color = dr["hair_color"].ToString();
                    memberProfil.eye_color = dr["eye_color"].ToString();
                    memberProfil.children = dr["children"].ToString();
                    memberProfil.disable = dr["disable"].ToString();
                    memberProfil.score = (int)dr["score"];
                    memberProfil.e_mail = dr["e_mail"].ToString();
                    memberProfil.last_enter_date = (DateTime)dr["last_enter_date"];
                    memberProfil.is_online = (bool)dr["is_online"];
                    MemberProfilList.Add(memberProfil);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MemberProfilList;
        }

        public static List<MemberProfil> SEARCH_CUSTOM_MEMBER_PROFIL(int id, string sex, string matiral_status, int age_from, int age_to, string educational_status, string city, string country, int hicap, int height_from, int height_to, int kilo_from, int kilo_to, string hair_color, string eye_color, string disable,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_CUSTOM_MEMBER_PROFIL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@matiral_status", matiral_status);
            cmd.Parameters.AddWithValue("@age_from", age_from);
            cmd.Parameters.AddWithValue("@age_to", age_to);
            cmd.Parameters.AddWithValue("@educational_status", educational_status);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@country", country);
            cmd.Parameters.AddWithValue("@hicap", hicap);
            cmd.Parameters.AddWithValue("@height_from", height_from);
            cmd.Parameters.AddWithValue("@height_to", height_to);
            cmd.Parameters.AddWithValue("@kilo_from", kilo_from);
            cmd.Parameters.AddWithValue("@kilo_to", kilo_to);
            cmd.Parameters.AddWithValue("@hair_color", hair_color);
            cmd.Parameters.AddWithValue("@eye_color", eye_color);
            cmd.Parameters.AddWithValue("@disable", disable);
            cmd.Parameters.AddWithValue("@page", page);
            List<MemberProfil> MemberProfilList = new List<MemberProfil>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MemberProfil memberProfil = new MemberProfil();
                    memberProfil.id = (int)dr["id"];
                    memberProfil.nick_name = dr["nick_name"].ToString();
                    memberProfil.sex = dr["sex"].ToString();
                    memberProfil.matiral_status = dr["matiral_status"].ToString();
                    memberProfil.age = (DateTime)dr["age"];
                    memberProfil.educational_status = dr["educational_status"].ToString();
                    memberProfil.city = dr["city"].ToString();
                    memberProfil.profil_media = dr["profil_media"].ToString();
                    memberProfil.status = (int)dr["status"];
                    memberProfil.payment_date = (DateTime)dr["payment_date"];
                    memberProfil.password = dr["password"].ToString();
                    memberProfil.my_story = dr["my_story"].ToString();
                    memberProfil.country = dr["country"].ToString();
                    memberProfil.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    memberProfil.hicap = (bool)dr["hicap"];
                    memberProfil.height = (int)dr["height"];
                    memberProfil.kilo = (int)dr["kilo"];
                    memberProfil.hair_color = dr["hair_color"].ToString();
                    memberProfil.eye_color = dr["eye_color"].ToString();
                    memberProfil.children = dr["children"].ToString();
                    memberProfil.disable = dr["disable"].ToString();
                    memberProfil.score = (int)dr["score"];
                    memberProfil.e_mail = dr["e_mail"].ToString();
                    memberProfil.last_enter_date = (DateTime)dr["last_enter_date"];
                    memberProfil.is_online = (bool)dr["is_online"];
                    MemberProfilList.Add(memberProfil);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MemberProfilList;
        }
    
      public static List<MemberProfil> SEARCH_CUSTOM_ONLINE_MEMBER(int id,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_CUSTOM_ONLINE_MEMBER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@page", page);
            List<MemberProfil> MemberProfilList = new List<MemberProfil>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MemberProfil memberProfil = new MemberProfil();
                    memberProfil.id = (int)dr["id"];
                    memberProfil.nick_name = dr["nick_name"].ToString();
                    memberProfil.sex = dr["sex"].ToString();
                    memberProfil.matiral_status = dr["matiral_status"].ToString();
                    memberProfil.age = (DateTime)dr["age"];
                    memberProfil.educational_status = dr["educational_status"].ToString();
                    memberProfil.city = dr["city"].ToString();
                    memberProfil.profil_media = dr["profil_media"].ToString();
                    memberProfil.status = (int)dr["status"];
                    memberProfil.payment_date = (DateTime)dr["payment_date"];
                    memberProfil.password = dr["password"].ToString();
                    memberProfil.my_story = dr["my_story"].ToString();
                    memberProfil.country = dr["country"].ToString();
                    memberProfil.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    memberProfil.hicap = (bool)dr["hicap"];
                    memberProfil.height = (int)dr["height"];
                    memberProfil.kilo = (int)dr["kilo"];
                    memberProfil.hair_color = dr["hair_color"].ToString();
                    memberProfil.eye_color = dr["eye_color"].ToString();
                    memberProfil.children = dr["children"].ToString();
                    memberProfil.disable = dr["disable"].ToString();
                    memberProfil.score = (int)dr["score"];
                    memberProfil.e_mail = dr["e_mail"].ToString();
                    memberProfil.last_enter_date = (DateTime)dr["last_enter_date"];
                    memberProfil.is_online = (bool)dr["is_online"];
                    MemberProfilList.Add(memberProfil);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MemberProfilList;
        }

        public static List<MemberProfil> SEARCH_CUSTOM_NEW_MEMBER( int id,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_CUSTOM_NEW_MEMBER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@page", page);
            List<MemberProfil> MemberProfilList = new List<MemberProfil>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MemberProfil memberProfil = new MemberProfil();
                    memberProfil.id = (int)dr["id"];
                    memberProfil.nick_name = dr["nick_name"].ToString();
                    memberProfil.sex = dr["sex"].ToString();
                    memberProfil.matiral_status = dr["matiral_status"].ToString();
                    memberProfil.age = (DateTime)dr["age"];
                    memberProfil.educational_status = dr["educational_status"].ToString();
                    memberProfil.city = dr["city"].ToString();
                    memberProfil.profil_media = dr["profil_media"].ToString();
                    memberProfil.status = (int)dr["status"];
                    memberProfil.payment_date = (DateTime)dr["payment_date"];
                    memberProfil.password = dr["password"].ToString();
                    memberProfil.my_story = dr["my_story"].ToString();
                    memberProfil.country = dr["country"].ToString();
                    memberProfil.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    memberProfil.hicap = (bool)dr["hicap"];
                    memberProfil.height = (int)dr["height"];
                    memberProfil.kilo = (int)dr["kilo"];
                    memberProfil.hair_color = dr["hair_color"].ToString();
                    memberProfil.eye_color = dr["eye_color"].ToString();
                    memberProfil.children = dr["children"].ToString();
                    memberProfil.disable = dr["disable"].ToString();
                    memberProfil.score = (int)dr["score"];
                    memberProfil.e_mail = dr["e_mail"].ToString();
                    memberProfil.last_enter_date = (DateTime)dr["last_enter_date"];
                    memberProfil.is_online = (bool)dr["is_online"];
                    MemberProfilList.Add(memberProfil);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MemberProfilList;
        }
      
        public static List<MemberProfil> SEARCH_CUSTOM_SURVEY_MEMBER(int id, int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_CUSTOM_SURVEY_MEMBER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@page", page);
            List<MemberProfil> MemberProfilList = new List<MemberProfil>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MemberProfil memberProfil = new MemberProfil();
                    memberProfil.id = (int)dr["id"];
                    memberProfil.nick_name = dr["nick_name"].ToString();
                    memberProfil.sex = dr["sex"].ToString();
                    memberProfil.matiral_status = dr["matiral_status"].ToString();
                    memberProfil.age = (DateTime)dr["age"];
                    memberProfil.educational_status = dr["educational_status"].ToString();
                    memberProfil.city = dr["city"].ToString();
                    memberProfil.profil_media = dr["profil_media"].ToString();
                    memberProfil.status = (int)dr["status"];
                    memberProfil.payment_date = (DateTime)dr["payment_date"];
                    memberProfil.password = dr["password"].ToString();
                    memberProfil.my_story = dr["my_story"].ToString();
                    memberProfil.country = dr["country"].ToString();
                    memberProfil.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    memberProfil.hicap = (bool)dr["hicap"];
                    memberProfil.height = (int)dr["height"];
                    memberProfil.kilo = (int)dr["kilo"];
                    memberProfil.hair_color = dr["hair_color"].ToString();
                    memberProfil.eye_color = dr["eye_color"].ToString();
                    memberProfil.children = dr["children"].ToString();
                    memberProfil.disable = dr["disable"].ToString();
                    memberProfil.score = (int)dr["score"];
                    memberProfil.e_mail = dr["e_mail"].ToString();
                    memberProfil.last_enter_date = (DateTime)dr["last_enter_date"];
                    memberProfil.is_online = (bool)dr["is_online"];
                    MemberProfilList.Add(memberProfil);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MemberProfilList;
        }
       
        public static List<MemberProfil> SEARCH_CUSTOM_SHARE_MEMBER(int id, int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SEARCH_CUSTOM_SHARE_MEMBER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@page", page);
            List<MemberProfil> MemberProfilList = new List<MemberProfil>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MemberProfil memberProfil = new MemberProfil();
                    memberProfil.id = (int)dr["id"];
                    memberProfil.nick_name = dr["nick_name"].ToString();
                    memberProfil.sex = dr["sex"].ToString();
                    memberProfil.matiral_status = dr["matiral_status"].ToString();
                    memberProfil.age = (DateTime)dr["age"];
                    memberProfil.educational_status = dr["educational_status"].ToString();
                    memberProfil.city = dr["city"].ToString();
                    memberProfil.profil_media = dr["profil_media"].ToString();
                    memberProfil.status = (int)dr["status"];
                    memberProfil.payment_date = (DateTime)dr["payment_date"];
                    memberProfil.password = dr["password"].ToString();
                    memberProfil.my_story = dr["my_story"].ToString();
                    memberProfil.country = dr["country"].ToString();
                    memberProfil.city_wants_to_live = dr["city_wants_to_live"].ToString();
                    memberProfil.hicap = (bool)dr["hicap"];
                    memberProfil.height = (int)dr["height"];
                    memberProfil.kilo = (int)dr["kilo"];
                    memberProfil.hair_color = dr["hair_color"].ToString();
                    memberProfil.eye_color = dr["eye_color"].ToString();
                    memberProfil.children = dr["children"].ToString();
                    memberProfil.disable = dr["disable"].ToString();
                    memberProfil.score = (int)dr["score"];
                    memberProfil.e_mail = dr["e_mail"].ToString();
                    memberProfil.last_enter_date = (DateTime)dr["last_enter_date"];
                    memberProfil.is_online = (bool)dr["is_online"];
                    MemberProfilList.Add(memberProfil);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MemberProfilList;
        }

        public static int SCORE_UPDATE(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SCORE_UPDATE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            int value;
            try
            {
                con.Open();
                value=Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return value;
            }
            catch
            {
                return 0;
            }
            
        }

        public static int CHECK_SURVEY_FORCED(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SCORE_UPDATE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            int value;
            try
            {
                con.Open();
                value = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return value;
            }
            catch
            {
                return 0;
            }

        }
    }
}