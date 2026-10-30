using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
//using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml;
using WebWebWeb.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace WebWebWeb.BL
{
    public class HELPER
    {
        public static string CREATE_KOD()
        {
            string[] code = { "A", "B", "2", "C", "D", "E", "3", "F", "G", "4", "H", "5", "J", "K", "6", "L", "M", "N", "7", "P", "R", "S", "8", "T", "U", "V", "9", "Y", "Z", "Q", "W", "X" };
            StringBuilder sb_code = new StringBuilder();
            string MemberCode = "";
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                char char_ = Convert.ToChar(code[random.Next(0, code.Length)]);
                sb_code.Append(char_);

            }
            if (MemberProfilBL.CODE_QUERY(sb_code.ToString()))
            {
                MemberCode = sb_code.ToString();
            }
            else
            { CREATE_KOD(); }

            return MemberCode;
        }

        public static bool SEND_MAIL(string e_mail, string subject, string body)
        {
           
            try
            {
                string user = ConfigurationManager.AppSettings["MailUser"];
                string pass = ConfigurationManager.AppSettings["MailPass"];
                string host = ConfigurationManager.AppSettings["MailHost"];
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]);

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("Filiz", user));
                message.To.Add(MailboxAddress.Parse(e_mail));

                message.Subject = subject;

                message.Body = new TextPart("plain")
                {
                    Text = body
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Timeout = 10000;

                    client.Connect(host, port, SecureSocketOptions.SslOnConnect);

                    client.Authenticate(user, pass);

                    client.Send(message);

                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(System.Web.HttpContext.Current.Server.MapPath("~/mail_error.log"), DateTime.Now + " - " + ex.ToString() + Environment.NewLine + Environment.NewLine);

                return false;
            }         
           
        }

        public static string PHOTO_SRC(string sort, string fileName)
        {
            /*sort: Album,Shared,Space360,ProfilCrop*/
            /*sort:Image, Image180, Image360, Video, Audio,Location*/
            string src = "";
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(fileName, settings);
            //XmlTextReader reader = new XmlTextReader(fileName);
            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == sort)
                {
                    src = reader.ReadInnerXml();
                    return src;
                }

            }
            return src;
        }

        public static bool CHECK_FORBIDDEN(String text)
        {
            /*burada birden çok metodla kontrol yapılıp sadece bir değer dönülecek
            1.yasaklı kelimeler listesiyle gelen textin kelimelerinin listesi karşılaştırılır
            2.girilen textin tüm harf dışı karakterleri temizlenip tümmetin birer harf birleştirilerek yasaklı kelimelerle kontrol edilir her harften yeni kelime başlatarak kontrol edilecek.
            3.
            */
            bool chekc = CheckText_1(text);
            return chekc;
        }

        public static bool CheckText_1(string text)
        {
            bool check = false;
            if (text != null)
            {
                string[] words = text.Split(' ');

                List<forbidden> forbidden_ = ForbiddenBL.FORBIDDEN_GET();
                foreach (var word in words)
                {
                    foreach (var forbidden_word in forbidden_)
                    {
                        if (forbidden_word.text == word)
                        {
                            check = false;
                            return check;
                        }
                        else
                        {
                            check = true;
                        }
                    }
                }
            }
            else
            {
                return true;
            }
            return check;
        }

        public Image Base64ToImage(string base64String)
        {
            //Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            //Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        public static int SETTING_DELETE(int result, int member_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;
            SqlCommand cmd = null;
            if (result == 1)
            {
                cmd = new SqlCommand("SETTING_DELETE_1", con);
            }
            else if (result == 2)
            {
                cmd = new SqlCommand("SETTING_DELETE_2", con);
            }
            else if (result == 3)
            {
                cmd = new SqlCommand("SETTING_DELETE_3", con);
            }
            else if (result == 4)
            {
                cmd = new SqlCommand("SETTING_DELETE_4", con);
            }
            else if (result == 5)
            {
                cmd = new SqlCommand("SETTING_DELETE_5", con);
            }
            else if (result == 6)
            {
                cmd = new SqlCommand("SETTING_DELETE_6", con);
            }
            else if (result == 7)
            {
                cmd = new SqlCommand("SETTING_DELETE_7", con);
            }
            else if (result == 8)
            {
                cmd = new SqlCommand("SETTING_DELETE_8", con);
            }
            else if (result == 9)
            {
                cmd = new SqlCommand("SETTING_DELETE_9", con);
            }
            else if (result == 10)
            {
                cmd = new SqlCommand("SETTING_DELETE_10", con);
            }
            else if (result == 11)
            {
                cmd = new SqlCommand("SETTING_DELETE_11", con);
            }
            else if (result == 12)
            {
                cmd = new SqlCommand("SETTING_DELETE_12", con);
            }
            else if (result == 13)
            {
                cmd = new SqlCommand("SETTING_DELETE_13", con);
            }
            else if (result == 14)
            {
                cmd = new SqlCommand("SETTING_DELETE_14", con);
            }
            else if (result == 15)
            {
                cmd = new SqlCommand("SETTING_DELETE_15", con);
            }
            else if (result == 16)
            {
                cmd = new SqlCommand("SETTING_DELETE_16", con);
            }
            else if (result == 17)
            {
                cmd = new SqlCommand("SETTING_DELETE_17", con);
            }
            else if (result == 18)
            {
                cmd = new SqlCommand("SETTING_DELETE_18", con);
            }
            else if (result == 19)
            {
                cmd = new SqlCommand("SETTING_DELETE_19", con);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", member_id);
            int query;
            try
            {
                con.Open();
                query = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return query;
            }
            catch
            {
                return 0;
            }
        }

        public static void ResizeStream(int imageSize, Stream filePath, string outputPath)
        {
            var image = Image.FromStream(filePath);
            int thumbnailSize = imageSize;
            int newWidth, newHeight;
            if (image.Width > image.Height)
            {
                newWidth = thumbnailSize;
                newHeight = image.Height * thumbnailSize / image.Width;
            }
            else
            {
                newWidth = image.Width * thumbnailSize / image.Height;
                newHeight = thumbnailSize;
            }
            var thumbnailBitmap = new Bitmap(newWidth, newHeight);
            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);

            thumbnailGraph.CompositingQuality = CompositingQuality.Default;
            thumbnailGraph.SmoothingMode = SmoothingMode.Default;
            thumbnailGraph.InterpolationMode = InterpolationMode.Low;

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(image, imageRectangle);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            System.Drawing.Imaging.ImageCodecInfo jpgEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 50L);
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = myEncoderParameter;
            //thumbnailBitmap.Save(Server.MapPath("/Media/a.jpg"), jpgEncoder, myEncoderParameters);
            thumbnailBitmap.Save(outputPath, jpgEncoder, myEncoderParameters);

            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }

    public class MediaCleanupService
    {
        private readonly string _connectionString =ConfigurationManager.ConnectionStrings["conKadir"].ConnectionString;

        private readonly string _trashRoot = HttpContext.Current.Server.MapPath("~/Trash");

        public void Run()
        {
            HashSet<string> dbFiles = GetDatabaseFileNames();

            string[] scanFolders =
            {
            HttpContext.Current.Server.MapPath("~/Media/Images"),
            HttpContext.Current.Server.MapPath("~/Photo/Image"),
            HttpContext.Current.Server.MapPath("~/Photo/Audio"),
            HttpContext.Current.Server.MapPath("~/Photo/Video"),
            HttpContext.Current.Server.MapPath("~/Photo/Location")
        };

            foreach (string folder in scanFolders)
            {
                if (!Directory.Exists(folder))
                    continue;

                foreach (string file in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
                {
                    string fileName = Path.GetFileName(file);

                    if (!dbFiles.Contains(fileName))
                    {
                        MoveToTrash(file);
                    }
                }
            }
        }

        private HashSet<string> GetDatabaseFileNames()
        {
            HashSet<string> files =
                new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            string sql = @"
            SELECT profil_media
            FROM MemberProfil
            WHERE profil_media IS NOT NULL

            UNION

            SELECT image_url
            FROM ShareObject
            WHERE image_url IS NOT NULL

            UNION

            SELECT media_url
            FROM Albums
            WHERE media_url IS NOT NULL";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string path = dr[0]?.ToString();

                        if (string.IsNullOrWhiteSpace(path))
                            continue;

                        string fileName = Path.GetFileName(path);

                        if (!string.IsNullOrWhiteSpace(fileName))
                            files.Add(fileName);
                    }
                }
            }

            return files;
        }

        private void MoveToTrash(string sourceFile)
        {
            try
            {
                string fileName = Path.GetFileName(sourceFile);

                string targetFile =
                    Path.Combine(_trashRoot, fileName);

                Directory.CreateDirectory(_trashRoot);

                if (File.Exists(targetFile))
                {
                    targetFile =
                        Path.Combine(
                            _trashRoot,
                            $"{DateTime.Now:yyyyMMddHHmmssfff}_{fileName}");
                }

                File.Move(sourceFile, targetFile);

                Log($"MOVED : {fileName}");
            }
            catch (Exception ex)
            {
                Log($"ERROR : {sourceFile} => {ex.Message}");
            }
        }

        private void Log(string message)
        {
            try
            {
                string logFile =
                    Path.Combine(_trashRoot, "cleanup-log.txt");

                Directory.CreateDirectory(_trashRoot);

                File.AppendAllText(
                    logFile,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
            }
            catch
            {
            }
        }
    }
}
