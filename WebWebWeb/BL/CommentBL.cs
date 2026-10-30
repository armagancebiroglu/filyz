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
    public class CommentBL
    {
        public static List<Comment> COMMENT_GET_BY_ID(int obje_id, int noticeTableSort, int Who_id, int Whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMMENT_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
                        
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@who_id", Who_id);
            cmd.Parameters.AddWithValue("@whom_id", Whom_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
            List<Comment> commentList = new List<Comment>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, who_id, whom_id, sort, obje_id
                    Comment comment = new Comment();
                    comment = new Comment();
                    comment.id = (int)dr["id"];
                    comment.obje_id = (int)dr["obje_id"];
                    comment.sort = (int)dr["sort"];
                    comment.noticeTableSort = (int)dr["noticeTableSort"];
                    comment.who_id = (int)dr["who_id"];
                    comment.whom_id = (int)dr["whom_id"];
                    comment.comment_text = dr["text"].ToString();
                    comment.date = (DateTime)dr["date"];
                    comment.nick_name = dr["nick_name"].ToString();
                    comment.profil_media = dr["profil_media"].ToString();
                    commentList.Add(comment);
                }
                con.Close();
                //id, sub_id, obje_id, sort, noticeTableSort, who_id, whom_id, text, date              
               
            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return commentList;
        }

        //id, sub_id, obje_id, sort, noticeTableSort, who_id, whom_id, text, date
        public static bool COMMENT_INSERT(Comment comment)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMMENT_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;        
            cmd.Parameters.AddWithValue("@obje_id", comment.obje_id);
            cmd.Parameters.AddWithValue("@sort", comment.sort);
            cmd.Parameters.AddWithValue("@noticeTableSort", comment.noticeTableSort);
            cmd.Parameters.AddWithValue("@who_id", comment.who_id);
            cmd.Parameters.AddWithValue("@whom_id", comment.whom_id);
            cmd.Parameters.AddWithValue("@comment_text", comment.comment_text);
           
            switch (comment.noticeTableSort)
            {
                case 0:
                    cmd.Parameters.AddWithValue("@notice_text", " bir paylaşımınıza yorumda bulundu");
                    break;
                case 1:
                    cmd.Parameters.AddWithValue("@notice_text", " albümünüzden bir paylaşımınıza yorumda bulundu");
                    break;
                default:
                    break;
            }

            int id;
            try
            {
                con.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (id > 0)
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

        public static bool COMMENT_DELETE(int id, int obje_id, int noticeTableSort,int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMMENT_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
            cmd.Parameters.AddWithValue("@who_id", who_id);

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

        public static bool COMMENT_DELETE_ALL_BY_MEMBER(int who_id,int whom_id, int obje_id, int noticeTableSort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMMENT_DELETE_ALL_BY_MEMBER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);

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

        public static List<Comment> COMMENT_MY_PROFIL_GET_BY_ID(int obje_id, int noticeTableSort, int who_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMMENT_MY_PROFIL_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@obje_id", obje_id);           
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
            List<Comment> commentList = new List<Comment>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, who_id, whom_id, sort, obje_id
                    Comment comment = new Comment();
                    comment = new Comment();
                    comment.id = (int)dr["id"];                   
                    comment.obje_id = (int)dr["obje_id"];
                    comment.sort = (int)dr["sort"];
                    comment.noticeTableSort = (int)dr["noticeTableSort"];
                    comment.who_id = (int)dr["who_id"];
                    comment.whom_id = (int)dr["whom_id"];
                    comment.comment_text = dr["text"].ToString();
                    comment.date = (DateTime)dr["date"];
                    comment.nick_name = dr["nick_name"].ToString();
                    comment.profil_media = dr["profil_media"].ToString();
                    commentList.Add(comment);
                }
                con.Close();
                //id, sub_id, obje_id, sort, noticeTableSort, who_id, whom_id, text, date              

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return commentList;
        }

        //burası sadece sharehome akıs sayfasında ilk 3 yorumu getirmesi için düzenlendi
        public static List<Comment> COMMENT_GET_TOP3_BY_ID(int obje_id, int noticeTableSort, int Who_id, int Whom_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMMENT_GET_TOP3_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@who_id", Who_id);
            cmd.Parameters.AddWithValue("@whom_id", Whom_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
            List<Comment> commentList = new List<Comment>();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, who_id, whom_id, sort, obje_id
                    Comment comment = new Comment();
                    comment = new Comment();
                    comment.id = (int)dr["id"];
                    comment.obje_id = (int)dr["obje_id"];
                    comment.sort = (int)dr["sort"];
                    comment.noticeTableSort = (int)dr["noticeTableSort"];
                    comment.who_id = (int)dr["who_id"];
                    comment.whom_id = (int)dr["whom_id"];
                    comment.comment_text = dr["text"].ToString();
                    comment.date = (DateTime)dr["date"];
                    comment.nick_name = dr["nick_name"].ToString();
                    comment.profil_media = dr["profil_media"].ToString();
                    commentList.Add(comment);
                }
                con.Close();
                //id, sub_id, obje_id, sort, noticeTableSort, who_id, whom_id, text, date              

            }
            catch
            {
                SqlConnection.ClearAllPools();
            }
            return commentList;
        }
    }
}