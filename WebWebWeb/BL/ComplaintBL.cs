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
    public class ComplaintBL
    {
        public static bool COMPLAINT_GET_BY_ID(int obje_id, int sort, int who_id, int whom_id, int noticeTableSort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMPLAINT_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sort", sort);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
            complaint complaint = new complaint();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, who_id, whom_id, sort, obje_id
                    complaint = new complaint();
                    complaint.id = (int)dr["id"];
                    complaint.who_id = (int)dr["Who_id"];
                    complaint.whom_id = (int)dr["Whom_id"];
                    complaint.sort = (int)dr["sort"];
                    complaint.obje_id = (int)dr["obje_id"];
                }
                con.Close();

                if (complaint.id != 0 || complaint.sort != 0 || complaint.whom_id != 0 || complaint.who_id != 0)
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

        public static bool COMPLAINT_INSERT(complaint complaint_)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMPLAINT_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", complaint_.who_id);
            cmd.Parameters.AddWithValue("@whom_id", complaint_.whom_id);
            cmd.Parameters.AddWithValue("@obje_id", complaint_.obje_id);
            cmd.Parameters.AddWithValue("@sort", complaint_.sort);
            cmd.Parameters.AddWithValue("@text", complaint_.text);
            cmd.Parameters.AddWithValue("@noticeTableSort", complaint_.noticeTableSort);
            int id;
            try
            {
                con.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                return true;

            }
            catch 
            {
                return false;
            }
        }

        public static bool COMPLAINT_DELETE(int obje_id, int sort, int who_id, int whom_id, int noticeTableSort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("COMPLAINT_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sort", sort);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@who_id", who_id);
            cmd.Parameters.AddWithValue("@whom_id", whom_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return false;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}