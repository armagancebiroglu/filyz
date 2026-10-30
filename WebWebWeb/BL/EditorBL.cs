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
    public class EditorBL
    {
        public static Editor EDITOR_LOGIN(string nick_name)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_LOGIN", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nick_name", nick_name);
            Editor EditorLogin = new Editor();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EditorLogin = new Editor();
                    EditorLogin.id = (int)dr["id"];
                    EditorLogin.nick_name = dr["nick_name"].ToString();
                    EditorLogin.name_last_name = dr["name_last_name"].ToString();
                    EditorLogin.tel = dr["tel"].ToString();
                    EditorLogin.e_mail = dr["e_mail"].ToString();
                    EditorLogin.password = dr["password"].ToString();
                    EditorLogin.score = (int)dr["score"];
                    EditorLogin.role = (int)dr["role"];
                    EditorLogin.is_online = (bool)dr["is_online"];
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return EditorLogin;
        }

        public static int EDITOR_INSERT(Editor editor)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nick_name", editor.nick_name);
            cmd.Parameters.AddWithValue("@name_last_name", editor.name_last_name);
            cmd.Parameters.AddWithValue("@tel", editor.tel);
            cmd.Parameters.AddWithValue("@e_mail", editor.e_mail);
            cmd.Parameters.AddWithValue("@password", editor.password);
            cmd.Parameters.AddWithValue("@score", editor.score);
            cmd.Parameters.AddWithValue("@role", editor.role);           
            cmd.Parameters.AddWithValue("@is_online", editor.is_online);
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

        public static bool IS_ONLINE(string nick_name, bool is_online)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_IS_ONLINE", con);
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

        public static int CHANGE_PASSWORD_EDITOR(int id, string password)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("CHANGE_PASSWORD_EDITOR", con);
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


        public static List<shareEditorObje> EDITOR_SHARE_OBJE_LIST(int? editor_id, string sex, int confrim, int page, int sort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_SHARE_OBJE_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@editor_id", editor_id);
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@confrim", confrim);
            cmd.Parameters.AddWithValue("@sort", sort);
            cmd.Parameters.AddWithValue("@page", page);
            List<shareEditorObje> ShareEditorList = new List<shareEditorObje>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //
                while (dr.Read())
                {
                    shareEditorObje ShareEditorObje = new shareEditorObje();
                    ShareEditorObje.number = Convert.ToInt32(dr["number"]);
                    ShareEditorObje.id = (int)dr["id"];
                    ShareEditorObje.member_id = (int)dr["member_id"];
                    ShareEditorObje.date = (DateTime)dr["date"];
                    ShareEditorObje.text = dr["text"].ToString();
                    ShareEditorObje.image_url = dr["image_url"].ToString();
                    ShareEditorObje.sort = (int)dr["sort"];
                    ShareEditorObje.status = (int)dr["status"];
                    ShareEditorObje.confrim = (int)dr["confrim"];
                    ShareEditorObje.sex = dr["sex"].ToString();
                    ShareEditorObje.LATITUDE = dr["LATITUDE"].ToString();
                    ShareEditorObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    ShareEditorObje.active = (int)dr["active"];
                    ShareEditorObje.nick_name = dr["nick_name"].ToString();
                    ShareEditorObje.name_last_name = dr["name_last_name"].ToString();
                    ShareEditorObje.tel = dr["tel"].ToString();
                    ShareEditorObje.e_mail = dr["e_mail"].ToString();
                    ShareEditorObje.password = dr["password"].ToString();
                    ShareEditorObje.score = (int)dr["score"];
                    ShareEditorObje.role = (int)dr["role"];
                    ShareEditorObje.count_List = (int)dr["count_List"];
                    ShareEditorObje.is_online = Convert.ToInt32(dr["is_online"]);

                    ShareEditorList.Add(ShareEditorObje);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return (ShareEditorList);
        }

        public static AdminView ADMIN_VIEW_PROC()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ADMIN_VIEW_PROC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            AdminView adminView = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    adminView = new AdminView();
                    adminView.id = 1;
                    adminView.member = (int)dr["member"];
                    adminView.man_member = (int)dr["man_member"];
                    adminView.woman_member = (int)dr["woman_member"];
                    adminView.member_o = (int)dr["member_o"];
                    adminView.man_member_o = (int)dr["man_member_o"];
                    adminView.woman_member_o = (int)dr["woman_member_o"];
                    adminView.member_d = (int)dr["member_d"];
                    adminView.man_member_d = (int)dr["man_member_d"];
                    adminView.woman_member_d = (int)dr["woman_member_d"];
                    adminView.member_w = (int)dr["member_w"];
                    adminView.man_member_w = (int)dr["man_member_w"];
                    adminView.woman_member_w = (int)dr["woman_member_w"];
                    adminView.member_dp = (int)dr["member_dp"];
                    adminView.man_member_dp = (int)dr["man_member_dp"];
                    adminView.woman_member_dp = (int)dr["woman_member_dp"];
                    adminView.member_wp = (int)dr["member_wp"];
                    adminView.man_member_wp = (int)dr["man_member_wp"];
                    adminView.woman_member_wp = (int)dr["woman_member_wp"];
                    adminView.member_mp = (int)dr["member_mp"];
                    adminView.man_member_mp = (int)dr["man_member_mp"];
                    adminView.woman_member_mp = (int)dr["woman_member_mp"];
                    adminView.member_m = (int)dr["member_m"];
                    adminView.man_member_m = (int)dr["man_member_m"];
                    adminView.woman_member_m = (int)dr["woman_member_m"];
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return adminView;
        }

        public static List<EditorComplaint> COMPLAINED_SHARES(int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMPLAINED_SHARES", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@page", page);
            List<EditorComplaint> ComplaintList = new List<EditorComplaint>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EditorComplaint compObje = new EditorComplaint();
                    compObje.complaint_date = (DateTime)dr["complaint_date"];
                    compObje.complaint_id = (int)dr["complaint_id"];
                    compObje.complaint_noticeTableSort = (int)dr["complaint_noticeTableSort"];
                    compObje.complaint_text = dr["complaint_text"].ToString();
                    compObje.complaint_who_id = (int)dr["complaint_who_id"];
                    compObje.number = Convert.ToInt32(dr["number"]);
                    compObje.share_active = (int)dr["share_active"];
                    compObje.share_confrim = (int)dr["share_confrim"];
                    compObje.share_date = (DateTime)dr["share_date"];
                    compObje.share_id = (int)dr["share_id"];
                    compObje.share_imgUrl = dr["share_imgUrl"].ToString();
                    compObje.share_memberid = (int)dr["share_memberid"];
                    compObje.share_sex = dr["share_sex"].ToString();
                    compObje.share_sort = (int)dr["share_sort"];
                    compObje.share_status = (int)dr["share_status"];
                    compObje.share_text = dr["share_text"].ToString();
                    compObje.whom_nick_name = dr["whom_nick_name"].ToString();
                    compObje.whom_profil_media = dr["whom_profil_media"].ToString();
                    compObje.who_nick_name = dr["who_nick_name"].ToString();
                    compObje.who_profil_media = dr["who_profil_media"].ToString();
                    ComplaintList.Add(compObje);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ComplaintList;
        }

        public static List<SurveyQuestion> EDITOR_COMP_SURVEY_QUESTION(int survey_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_COMP_SURVEY_QUESTION", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@survey_id", survey_id);
            List<SurveyQuestion> SurveyQuestionList = new List<SurveyQuestion>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SurveyQuestion question = new SurveyQuestion();
                    question.id = (int)dr["id"];
                    question.survey_id = (int)dr["survey_id"];
                    question.questionText = dr["questionText"].ToString();
                    question.A_text = dr["A_text"].ToString();
                    question.B_text = dr["B_text"].ToString();
                    question.C_text = dr["C_text"].ToString();
                    question.D_text = dr["D_text"].ToString();
                    question.confrim = (int)dr["confrim"];
                    SurveyQuestionList.Add(question);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return (SurveyQuestionList);
        }

        public static int EDITOR_COMP_SHARE_OBJECT_DELETE(int id, int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_COMP_SHARE_OBJECT_DELETE", con);
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

        public static int EDITOR_COMP_SHARE_OBJECT_UPDATE(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMPLAINED_SHARES_UPDATE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@obje_id", id);
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

        public static int EDITOR_MEMBER_DELETE(int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_MEMBER_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            try
            {
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public static List<shareStreem> EDITOR_SHARE_PAGE(int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_SHARE_PAGE", con);
            cmd.CommandType = CommandType.StoredProcedure;
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