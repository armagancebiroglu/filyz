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
    public class SurveyBL
    {
        public static int SURVEY_INSERT(ShareObject ShareObject)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrWhiteSpace(ShareObject.text))
            {
                cmd.Parameters.AddWithValue("@member_id", ShareObject.member_id);
                cmd.Parameters.AddWithValue("@text", ShareObject.text);
                cmd.Parameters.AddWithValue("@image_url", "yok");
                cmd.Parameters.AddWithValue("@sex", ShareObject.sex);
                cmd.Parameters.AddWithValue("@sort", ShareObject.sort);
                cmd.Parameters.AddWithValue("@date", ShareObject.date);
                cmd.Parameters.AddWithValue("@confrim", ShareObject.confrim);
                cmd.Parameters.AddWithValue("@status", ShareObject.status);
                cmd.Parameters.AddWithValue("@LATITUDE", "");
                cmd.Parameters.AddWithValue("@LONGITUDE", "");
                cmd.Parameters.AddWithValue("@active", ShareObject.active);
            }
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

        public static int SURVEY_DELETE(int obje_id, int member_id)
        { 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
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
        
        public static int EDITOR_SURVEY_DELETE(int obje_id, int memberStatus)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_SURVEY_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@memberStatus", memberStatus);

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

        public static List<shareStreem> SURVEY_LIST_MY_ALL(int member_id)
        { 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_LIST_MY_ALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
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

        public static shareStreem SURVEY_GET_BY_ID(int obje_id, int member_id) 
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@survey_id", obje_id);
            cmd.Parameters.AddWithValue("@member_id", member_id);
            shareStreem ShareStreemObje = new shareStreem();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();                
                while (dr.Read())
                {
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
                    con.Close();
                }
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return (ShareStreemObje);
        }

        public static int SURVEY_QUESTION_INSERT(SurveyQuestion question)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_QUESTION_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@survey_id", question.survey_id);
            cmd.Parameters.AddWithValue("@questionText", question.questionText);
            if (!string.IsNullOrWhiteSpace(question.A_text) && !string.IsNullOrWhiteSpace(question.B_text))
            {
                cmd.Parameters.AddWithValue("@A_text", question.A_text);
                cmd.Parameters.AddWithValue("@B_text", question.B_text);
                cmd.Parameters.AddWithValue("@C_text", question.C_text);
                cmd.Parameters.AddWithValue("@D_text", question.D_text);
                cmd.Parameters.AddWithValue("@confrim", question.confrim);
            }
           
            int result = 0;
            try
            {
                con.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return result;
        }

        public static List<SurveyQuestion> SURVEY_QUESTION(int survey_id, int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_QUESTION", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@survey_id", survey_id);
            cmd.Parameters.AddWithValue("@member_id", member_id);
            List<SurveyQuestion> SurveyQuestionList = new List<SurveyQuestion>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    SurveyQuestion question = new SurveyQuestion();
                    question.id = (int)dr["id"];
                    question.survey_id= (int)dr["survey_id"];
                    question.questionText = dr["questionText"].ToString();
                    question.A_text= dr["A_text"].ToString();
                    question.B_text= dr["B_text"].ToString();
                    question.C_text= dr["C_text"].ToString();
                    question.D_text= dr["D_text"].ToString();
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

        public static List<SurveyQuestion> EDITOR_SURVEY_QUESTION(int survey_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_SURVEY_QUESTION", con);
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
        
        public static int SURVEY_QUESTION_DELETE(int obje_id, int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_QUESTION_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
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

        public static int EDITOR_SURVEY_QUESTION_DELETE(int obje_id, int memberStatus)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_SURVEY_QUESTION_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@memberStatus", memberStatus);
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

        public static int SURVEY_SHARE(int obje_id, int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_SHARE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@member_id", member_id);
            int updated;
            try
            {
                con.Open();
                updated = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return updated;
            }
            catch
            {
                return 0;
            }
        }

        public static int EDITOR_SURVEY_SHARE(int obje_id,int memberStatus)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("EDITOR_SURVEY_SHARE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@memberStatus", memberStatus);
            int updated;
            try
            {
                con.Open();
                updated = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return updated;
            }
            catch
            {
                return 0;
            }
        }

        public static int SURVEY_ANSWER_INSERT(SurveyAnswer answer)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_ANSWER_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@survey_id", answer.survey_id);
            cmd.Parameters.AddWithValue("@question_id", answer.question_id);
            cmd.Parameters.AddWithValue("@member_id", answer.member_id);
            cmd.Parameters.AddWithValue("@ABCD_options", answer.ABCD_options);
            int insert;
            try
            {
                con.Open();
                insert = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return insert;
            }
            catch
            {
                return 0;
            }
        }

        public static List<SurveyAnswerItem> SURVEY_ANSWER(int survey_id, int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_ANSWER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@survey_id", survey_id);
            cmd.Parameters.AddWithValue("@member_id", member_id);
            List<SurveyAnswerItem> SurveyQuestionList = new List<SurveyAnswerItem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SurveyAnswerItem answer = new SurveyAnswerItem();
                    answer.id = (int)dr["id"];
                    answer.question_id= (int)dr["question_id"];
                    answer.member_id= (int)dr["member_id"];
                    answer.survey_id = (int)dr["survey_id"];
                    answer.questionText = dr["questionText"].ToString();
                    answer.A_text = dr["A_text"].ToString();
                    answer.B_text = dr["B_text"].ToString();
                    answer.C_text = dr["C_text"].ToString();
                    answer.D_text = dr["D_text"].ToString();
                    answer.ABCD_options= dr["ABCD_options"].ToString();
                    answer.confrim = (int)dr["confrim"];
                    SurveyQuestionList.Add(answer);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return (SurveyQuestionList);
        }

        //public static List<SurveyQuestion> SURVEY_QUESTION_ANSWER_OVER(int survey_id, int member_id)
        //{
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand("SURVEY_QUESTION_ANSWER_OVER", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@survey_id", survey_id);
        //    cmd.Parameters.AddWithValue("@member_id", member_id);
        //    List<SurveyQuestion> SurveyQuestionList = new List<SurveyQuestion>();
        //    try
        //    {
        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            SurveyQuestion question = new SurveyQuestion();
        //            question.id = (int)dr["id"];
        //            question.survey_id = (int)dr["survey_id"];
        //            question.questionText = dr["questionText"].ToString();
        //            question.A_text = dr["A_text"].ToString();
        //            question.B_text = dr["B_text"].ToString();
        //            question.C_text = dr["C_text"].ToString();
        //            question.D_text = dr["D_text"].ToString();
        //            question.confrim = (int)dr["confrim"];
        //            SurveyQuestionList.Add(question);
        //        }
        //        con.Close();
        //    }
        //    catch
        //    {
        //        SqlConnection.ClearAllPools();
        //    }
        //    return (SurveyQuestionList);
        //}

        public static List<SurveyQuestion> SURVEY_ANSWER_ALL_QUESTION(int survey_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_ANSWER_ALL_QUESTION", con);
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

        public static int SURVEY_ANSWER_DELETE(int answer_id, int question_id, int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_ANSWER_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@answer_id", answer_id);
            cmd.Parameters.AddWithValue("@question_id", question_id);
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

        public static int SURVEY_COUNT(int whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_COUNT", con);
            cmd.CommandType = CommandType.StoredProcedure;
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

        public static bool SURVEY_CHECK(int who_id, int obje_id) 
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_CHECK", con);
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

        public static int SURVEY_RESPONDENTS_COUNT(int Obje_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_RESPONDENTS_COUNT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Obje_id", Obje_id);

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

        public static List<MemberShortCard> SURVEY_RESPONDENTS_LIST(int Obje_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_RESPONDENTS_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Obje_id", Obje_id);

            List<MemberShortCard> SurveyRespondentsMemberList = new List<MemberShortCard>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//MemberProfil.id, nick_name, sex, matiral_status, age, educational_status, city, profil_media, country, last_enter_date, is_online, eventDate 
                    MemberShortCard MemberShortCard = new MemberShortCard();
                    MemberShortCard.id = (int)dr["id"];
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
                    SurveyRespondentsMemberList.Add(MemberShortCard);
                }
                con.Close();
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return SurveyRespondentsMemberList;
        }

        public static int SURVEY_ALL_ANSWER_DELETE(int whom_id, int survey_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_ALL_ANSWER_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@whom_id", whom_id);
            cmd.Parameters.AddWithValue("@survey_id", survey_id);

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

        public static List<shareStreem> SURVEY_LIST_PROFIL_ALL(int member_id) 
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_LIST_PROFIL_ALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
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

        public static List<SurveyQA> SURVEY_ANSWER_PORFIL_ALL(int whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_ANSWER_PORFIL_ALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@whom_id", whom_id);
            List<SurveyQA> SurveyQuestionList = new List<SurveyQA>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SurveyQA question = new SurveyQA();
                    question.answer_id = (int)dr["answer_id"];
                    question.member_id = (int)dr["member_id"];
                    question.survey_id = (int)dr["survey_id"];
                    question.question_id = (int)dr["question_id"];
                    question.ABCD_options = dr["ABCD_options"].ToString();
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
        
        public static List<shareStreem> SURVEY_LIST_PROFIL(int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_LIST_PROFIL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, [text], image_url, sex, sort, [date], confrim, [status], LATITUDE, LONGTITUDE, active, nick_name, member_sex, age, city, profil_media, member_status, last_enter_date, is_online, score
                    shareStreem ShareStreemObje = new shareStreem();
                    //ShareStreemObje.number = Convert.ToInt32(dr["number"]);
                    ShareStreemObje.id = (int)dr["survey_id"];
                    ShareStreemObje.member_id = (int)dr["member_id"];
                    ShareStreemObje.date = (DateTime)dr["date"];
                    ShareStreemObje.text = dr["text"].ToString();
                    ShareStreemObje.image_url = dr["profil_media"].ToString();
                    ShareStreemObje.sort = (int)dr["sort"];
                    ShareStreemObje.status = (int)dr["status"];
                    ShareStreemObje.confrim = (int)dr["confrim"];
                    ShareStreemObje.sex = dr["member_sex"].ToString();
                    //if (!String.IsNullOrEmpty(dr["LATITUDE"].ToString()) || !String.IsNullOrEmpty(dr["LONGTITUDE"].ToString()))
                    //{
                        ShareStreemObje.LATITUDE = dr["LATITUDE"].ToString();
                        ShareStreemObje.LONGITUDE = dr["LONGTITUDE"].ToString();
                    //}
                    //else
                    //{
                    //    ShareStreemObje.LATITUDE = 0;
                    //    ShareStreemObje.LONGITUDE = 0;
                    //}
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

        public static List<shareStreem> SURVEY_LIST_EDITOR_ALL()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_LIST_EDITOR_ALL", con);
            cmd.CommandType = CommandType.StoredProcedure;          
            List<shareStreem> ShareStreemList = new List<shareStreem>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, text, image_url, sex, sort, date, confrim, status, LATITUDE, LONGTITUDE, active
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

        public static shareStreem SURVEY_EDITOR_GET_BY_ID(int obje_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_EDITOR_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@survey_id", obje_id);
            shareStreem ShareStreemObje = new shareStreem();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
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

                    con.Close();
                }
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return (ShareStreemObje);
        }

        public static List<Survey> SURVEY_HOME_FORCED(int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_HOME_FORCED", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            List<Survey> ShareStreemList = new List<Survey>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, text, image_url, sex, sort, date, confrim, status, LATITUDE, LONGTITUDE, active
                    Survey S_Obje = new Survey();
                    S_Obje.id = (int)dr["id"];
                    S_Obje.member_id = (int)dr["member_id"];
                    S_Obje.date = (DateTime)dr["date"];
                    S_Obje.text = dr["text"].ToString();
                    S_Obje.status = (int)dr["status"];
                    S_Obje.confrim = (int)dr["confrim"];
                    S_Obje.sex = dr["sex"].ToString();
                    S_Obje.active = (int)dr["active"];
                    S_Obje.questionCount = (int)dr["questionCount"];
                    S_Obje.answerCount = (int)dr["answerCount"];

                    ShareStreemList.Add(S_Obje);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ShareStreemList;
        }

        public static List<Survey> SURVEY_HOME_SELECTIVE(int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SURVEY_HOME_SELECTIVE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            List<Survey> ShareStreemList = new List<Survey>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, member_id, text, image_url, sex, sort, date, confrim, status, LATITUDE, LONGTITUDE, active
                    Survey S_Obje = new Survey();
                    S_Obje.id = (int)dr["id"];
                    S_Obje.member_id = (int)dr["member_id"];
                    S_Obje.date = (DateTime)dr["date"];
                    S_Obje.text = dr["text"].ToString();
                    S_Obje.status = (int)dr["status"];
                    S_Obje.confrim = (int)dr["confrim"];
                    S_Obje.sex = dr["sex"].ToString();
                    S_Obje.active = (int)dr["active"];
                    S_Obje.questionCount = (int)dr["questionCount"];
                    S_Obje.answerCount = (int)dr["answerCount"];

                    ShareStreemList.Add(S_Obje);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return ShareStreemList;
        }

        public static List<MemberSurveyAnswer> MEMBER_SURVEY_ANSWER(int member_id,int survey_id,int page)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("MEMBER_SURVEY_ANSWER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            cmd.Parameters.AddWithValue("@survey_id", survey_id);
            cmd.Parameters.AddWithValue("@page", page);
            List<MemberSurveyAnswer> MemberList = new List<MemberSurveyAnswer>();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//member_id,percent, nick_name, matiral_status, age, educational_status, city, profil_media, country, hicap,score, last_enter_date, is_online, date
                    MemberSurveyAnswer member = new MemberSurveyAnswer();
                    member.percent = (int)dr["percent"];
                    member.member_id = (int)dr["member_id"];
                    member.date = (DateTime)dr["date"];
                    member.nick_name = dr["nick_name"].ToString();
                    member.matiral_status = dr["matiral_status"].ToString();
                    member.age = (DateTime)dr["age"];
                    member.educational_status = dr["educational_status"].ToString();
                    member.city = dr["matiral_status"].ToString();
                    member.profil_media = dr["profil_media"].ToString();
                    member.country = dr["country"].ToString();
                    member.hicap = Convert.ToInt32(dr["hicap"]);
                    member.score = (int)dr["score"];
                    member.last_enter_date = (DateTime)dr["last_enter_date"];
                    member.is_online = Convert.ToInt32(dr["is_online"]);

                    MemberList.Add(member);
                }

                con.Close();

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return MemberList;
        }


    }
}