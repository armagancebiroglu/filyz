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
    public class EnjoyBL
    {
        public static bool ENJOY_GET_BY_ID(int obje_id, int sort,int Who_id,int Whom_id, int noticeTableSort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ENJOY_GET_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sort", sort);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@who_id", Who_id);
            cmd.Parameters.AddWithValue("@whom_id", Whom_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
            Enjoy enjoy = new Enjoy();
         
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {//id, who_id, whom_id, sort, obje_id
                    enjoy = new Enjoy();
                    enjoy.id = (int)dr["id"];
                    enjoy.Who_id = (int)dr["Who_id"];
                    enjoy.Whom_id = (int)dr["Whom_id"];
                    enjoy.sort = (int)dr["sort"];
                    enjoy.obje_id = (int)dr["obje_id"];                  
                }
                con.Close();

                if (enjoy.id!=0|| enjoy.sort != 0|| enjoy.Whom_id != 0|| enjoy.Who_id != 0)
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

        public static bool ENJOY_INSERT(Enjoy enjoy)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ENJOY_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id",enjoy.Who_id);
            cmd.Parameters.AddWithValue("@whom_id",enjoy.Whom_id);
            cmd.Parameters.AddWithValue("@obje_id",enjoy.obje_id);
            cmd.Parameters.AddWithValue("@sort", enjoy.sort);
            cmd.Parameters.AddWithValue("@noticeTableSort", enjoy.noticeTableSort);
            string text = "";
            switch (enjoy.noticeTableSort)
            {
                case 0:
                    {
                        switch (enjoy.sort)
                        {
                            case 0:
                                text = "fotoğraf";// Enum.GetName(typeof(sort), Convert.ToInt32(Enum.Parse(typeof(sort), sort.image.ToString())));
                                break;
                            case 1:
                                text = "panoramik fotoğraf";// Enum.GetName(typeof(sort), Convert.ToInt32(Enum.Parse(typeof(sort), sort.image180.ToString())));
                                break;
                            case 2:
                                text = "360° Panoramik fotoğraf";// Enum.GetName(typeof(sort), Convert.ToInt32(Enum.Parse(typeof(sort), sort.image360.ToString())));
                                break;
                            case 4:
                                text = "video";// Enum.GetName(typeof(sort), Convert.ToInt32(Enum.Parse(typeof(sort), sort.video.ToString())));
                                break;
                            case 8:
                                text = "ses kaydı";// Enum.GetName(typeof(sort), Convert.ToInt32(Enum.Parse(typeof(sort), sort.audio.ToString())));
                                break;
                            case 16:
                                text = "konum";// Enum.GetName(typeof(sort), Convert.ToInt32(Enum.Parse(typeof(sort), sort.location.ToString())));
                                break;
                            case 32:
                                text = "anket";// Enum.GetName(typeof(sort), Convert.ToInt32(Enum.Parse(typeof(sort), sort.survey.ToString())));
                                break;

                            default:
                                break;
                        }
                        cmd.Parameters.AddWithValue("@text", " Bir " + text + " paylaşımınızı beğendi.");
                    }break;
                case 1:
                    {
                        cmd.Parameters.AddWithValue("@text", " Albümünüzden bir paylaşımınızı beğendi.");
                    }
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
               
                if (id>0)
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

        public static bool ENJOY_DELETE(int obje_id, int sort, int Who_id, int Whom_id, int noticeTableSort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ENJOY_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sort", sort);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);
            cmd.Parameters.AddWithValue("@who_id", Who_id);
            cmd.Parameters.AddWithValue("@whom_id", Whom_id);
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort); 

            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (query>0)
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

        public static int ENJOY_MY_PROFIL_GETCOUNT_BY_ID(int obje_id, int sort, int noticeTableSort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ENJOY_MY_PROFIL_GETCOUNT_BY_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sort", sort);
            cmd.Parameters.AddWithValue("@obje_id", obje_id);            
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
          
            int heartCount;
            try
            {
                con.Open();
                heartCount = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

            }
            catch (Exception)
            {
                return 0;
            }
            return heartCount;
        }

        public static List<MemberShortCard> ENJOY_SHARE_ID(int who_id,int shareObje_id,int noticeTableSort)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = new SqlCommand("ENJOY_SHARE_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@who_id", who_id); 
            cmd.Parameters.AddWithValue("@noticeTableSort", noticeTableSort);
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
                    MemberShortCard.eventDate= (DateTime)dr["eventDate"];
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
    }
}