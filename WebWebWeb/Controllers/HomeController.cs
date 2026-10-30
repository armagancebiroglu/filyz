using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWebWeb.BL;
using WebWebWeb.Models;

namespace WebWebWeb.Controllers
{
    public class HomeController : Controller
    {
        string url = "";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            try
            {
                //int result = MemberProfilBL.CHECK_SURVEY_FORCED(Convert.ToInt32(Session["id"]));
                //if (result > 0 && string.IsNullOrEmpty(Session["survey"].ToString()))
                //{
                //    //burada zorunlu ankete katılım kontrolü yapılacak.
                //    Session["survey"] = "ok";
                //    return RedirectToAction("Uppss", "Error", new { P = 42 });
                //}
                Session["survey"] = "ok"; //burası yukarıda yapılan kontrolü atlamak için yazdım. session nerede "ok" olacak unuttum
                return View();
            }
            catch (Exception)
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Info", "Default");

            }

        }

        public ActionResult IsOnline()
        {
            try
            {
                if (!(Request.QueryString["bool_"] == null))
                {
                    bool is_online = Convert.ToBoolean(Convert.ToInt32(Request.QueryString["bool_"]));
                    string nick_name = Session["nick_name"].ToString();
                    Session["is_online"] = is_online.ToString();
                    MemberProfilBL.IS_ONLINE(nick_name, is_online);
                    ViewBag.bool_ = is_online;
                    return PartialView(is_online);
                }
                else
                {
                    if (Session["is_online"] != null)
                    {
                        bool is_online = Convert.ToBoolean(Session["is_online"].ToString());
                        ViewBag.is_online = is_online;
                        return PartialView();
                    }
                    else
                    {
                        RedirectToAction("Info", "Default");
                    }
                }
            }
            catch
            {
                RedirectToAction("Uppss", "Error", new { P = 6 });
            }
            return RedirectToAction("Uppss", "Error", new { P = 6 });
        }

        public ActionResult ShareRouter()
        {
            try
            {
                if (string.IsNullOrEmpty(Session["id"].ToString()))
                {
                    return RedirectToAction("Info", "Default");

                }
                return View();
            }
            catch
            {
                return RedirectToAction("Info", "Default");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShareObject(ShareObject obje, HttpPostedFileBase file)
        {
            try
            {
                if (true)//DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35
                {
                    if (!string.IsNullOrEmpty(obje.text.ToString()))
                    {
                        if (file.ContentType.ToLower() == "image/jpg" ||
                                                           file.ContentType.ToLower() == "image/jpeg" ||
                                                           file.ContentType.ToLower() == "image/pjpeg" ||
                                                           file.ContentType.ToLower() == "image/gif" ||
                                                           file.ContentType.ToLower() == "image/x-png" ||
                                                           file.ContentType.ToLower() == "image/png")
                        {
                            if (file.ContentLength < Convert.ToDouble(50000000))
                            {
                                string file_name = Path.GetFileName(file.FileName);
                                string extension = Path.GetExtension(file.FileName);
                                string media_Url = "/Photo/Image/" + HELPER.PHOTO_SRC("Image", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                                url = media_Url;
                                ShareObject obje_ = new Models.ShareObject();

                                obje_.member_id = Convert.ToInt32(Session["id"]);
                                obje_.image_url = media_Url;
                                obje_.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.image.ToString()));
                                obje_.date = DateTime.Now;
                                obje_.sex = Session["sex"].ToString();
                                obje_.status = Convert.ToInt32(Enum.Parse(typeof(status), Session["status"].ToString()));
                                obje_.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                                obje_.text = obje.text;
                                obje_.confrim = Convert.ToInt32(Enum.Parse(typeof(confrim), confrim.in_review.ToString()));
                                obje_.LATITUDE = "";
                                obje_.LONGITUDE = "";

                                if (extension == ".gif")
                                {
                                    file.SaveAs(Server.MapPath(media_Url));
                                }
                                else
                                {
                                    file.SaveAs(Server.MapPath(media_Url));
                                    //HELPER.ResizeStream(600, file.InputStream, Server.MapPath(media_Url));                               
                                }

                                if (System.IO.File.Exists(Server.MapPath(media_Url)))
                                {
                                    ShareObjectBL.SHARE_OBJECT_INSERT(obje_);

                                    return RedirectToAction("Home", "Home");
                                }
                                else
                                {
                                    return RedirectToAction("Uppss", "Error", new { P = 5 });
                                }
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 7 });
                            }

                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 8 });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 21 });
                    }
                   
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 38 });
                }

            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }

        }
        public ActionResult ShareObject()
        {
            ShareObject obje = new Models.ShareObject();
            return View(obje);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShareSpace(ShareObject obje, HttpPostedFileBase file)
        {
            try
            {
                if (DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35)
                {
                    if (file.ContentType.ToLower() == "image/jpg" ||
                                    file.ContentType.ToLower() == "image/jpeg" ||
                                    file.ContentType.ToLower() == "image/pjpeg" ||
                                    file.ContentType.ToLower() == "image/gif" ||
                                    file.ContentType.ToLower() == "image/x-png" ||
                                    file.ContentType.ToLower() == "image/png")
                    {
                        if (file.ContentLength < Convert.ToDouble(50000000))
                        {
                            string file_name = Path.GetFileName(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            string media_Url = "/Photo/Image180/" + HELPER.PHOTO_SRC("Image180", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                            ShareObject obje_ = new Models.ShareObject();

                            obje_.member_id = Convert.ToInt32(Session["id"]);
                            obje_.image_url = media_Url;
                            obje_.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.image180.ToString()));
                            obje_.date = DateTime.Now;
                            obje_.sex = Session["sex"].ToString();
                            obje_.status = Convert.ToInt32(Enum.Parse(typeof(status), Session["status"].ToString()));
                            obje_.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                            obje_.text = obje.text;
                            obje_.confrim = Convert.ToInt32(Enum.Parse(typeof(confrim), confrim.in_review.ToString()));
                            obje_.LATITUDE = "";
                            obje_.LONGITUDE = "";

                            if (extension == ".gif")
                            {
                                file.SaveAs(Server.MapPath(media_Url));
                            }
                            else
                            {
                                HELPER.ResizeStream(2400, file.InputStream, Server.MapPath(media_Url));
                            }

                            if (System.IO.File.Exists(Server.MapPath(media_Url)))
                            {
                                ShareObjectBL.SHARE_OBJECT_INSERT(obje_);

                                return RedirectToAction("Home", "Home");
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 5 });
                            }
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 7 });
                        }

                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 8 });
                    }
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 38 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }

        public ActionResult ShareSpace()
        {
            ShareObject obje = new Models.ShareObject();
            return View(obje);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShareSpace360(ShareObject obje, HttpPostedFileBase file)
        {
            try
            {
                if (DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35)
                {
                    if (file.ContentType.ToLower() == "image/jpg" ||
                                    file.ContentType.ToLower() == "image/jpeg" ||
                                    file.ContentType.ToLower() == "image/pjpeg" ||
                                    file.ContentType.ToLower() == "image/gif" ||
                                    file.ContentType.ToLower() == "image/x-png" ||
                                    file.ContentType.ToLower() == "image/png")
                    {
                        if (file.ContentLength < Convert.ToDouble(50000000))
                        {
                            string file_name = Path.GetFileName(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            string media_Url = "/Photo/Image360/" + HELPER.PHOTO_SRC("Image360", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                            ShareObject obje_ = new Models.ShareObject();

                            obje_.member_id = Convert.ToInt32(Session["id"]);
                            obje_.image_url = media_Url;
                            obje_.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.image360.ToString()));
                            obje_.date = DateTime.Now;
                            obje_.sex = Session["sex"].ToString();
                            obje_.status = Convert.ToInt32(Enum.Parse(typeof(status), Session["status"].ToString()));
                            obje_.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                            obje_.text = obje.text;
                            obje_.confrim = Convert.ToInt32(Enum.Parse(typeof(confrim), confrim.in_review.ToString()));
                            obje_.LATITUDE = "";
                            obje_.LONGITUDE = "";

                            if (extension == ".gif")
                            {
                                file.SaveAs(Server.MapPath(media_Url));
                            }
                            else
                            {
                                HELPER.ResizeStream(2400, file.InputStream, Server.MapPath(media_Url));
                            }

                            if (System.IO.File.Exists(Server.MapPath(media_Url)))
                            {
                                ShareObjectBL.SHARE_OBJECT_INSERT(obje_);

                                return RedirectToAction("Home", "Home");
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 5 });
                            }
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 7 });
                        }

                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 8 });
                    }
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 38 });
                }

            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }

        public ActionResult ShareSpace360(ShareObject obje)
        {
            return View();
        }
        public ActionResult ShareLocation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShareLocation(Location location, string latitude, string longitude, string mapsImgSrc, string text)
        {

            try
            {
                if (true)//DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35
                {
                    if (latitude != "" && longitude != "" && mapsImgSrc != "")
                    {
                        location = new Location();
                        location.member_id = Convert.ToInt32(Session["id"]);
                        location.LATITUDE = latitude;
                        location.LONGITUDE = longitude;
                        location.sex = Session["sex"].ToString();
                        location.status = (int)Enum.Parse(typeof(status), Session["status"].ToString());
                        if (!string.IsNullOrEmpty(text))
                        {
                            location.text = text;
                        }
                        else
                        {
                            location.text = Session["nick_name"].ToString() + " " + DateTime.Now.Date.Day.ToString() + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month) + " günü saat " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + " da burada olduğunu bildirdi.";
                        }
                        if (!string.IsNullOrEmpty(mapsImgSrc))
                        {                          
                            string valueBase = mapsImgSrc;
                            string valueBaseString = valueBase.Replace("data:image/png;base64,", "" );

                            byte[] image64 = Convert.FromBase64String(valueBaseString);

                            string profil_media = "/Photo/Location/"+ HELPER.PHOTO_SRC("Location", Server.MapPath("../FotoFileName.xml").ToString() ) + "/"+ DateTime.Now.ToFileTime()+ ".png";

                            string fullPath = Server.MapPath(profil_media);

                            // DIREKT BYTE KAYDET

                            System.IO.File.WriteAllBytes( fullPath, image64 );

                            location.image_url = profil_media;
                        }
                        else
                        {
                            return RedirectToAction("Home", "Home");
                        }
                       
                        location.date = DateTime.Now;
                        location.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.location.ToString())); //Enum.GetName(typeof(sort), 0);
                        location.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                        location.confrim = Convert.ToInt32(Enum.Parse(typeof(confrim), confrim.in_review.ToString()));
                        int location_id = LocationBL.LOCATION_INSERT(location);
                        if (location_id != 0)
                        {
                            return RedirectToAction("Home", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 21 });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 22 });
                    }
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 38 });
                }


            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }

        [HttpGet]
        public ActionResult ShareVideo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShareVideo(ShareObject obje, HttpPostedFileBase file, string ShareObjectVideoText)
        {
            try
            {
                if (true)//DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35
                {
                    if (!string.IsNullOrEmpty(ShareObjectVideoText))
                    {
                        if (file.ContentType.ToLower() == "video/mp4" || file.ContentType.ToLower() == "video/webm" || file.ContentType.ToLower() == "video/ogg" || file.ContentType.ToLower() == "video/quicktime" && ShareObjectVideoText != "")
                        {
                            if (file.ContentLength < Convert.ToDouble(10000000))
                            {
                                string file_name = Path.GetFileName(file.FileName);
                                string extension = Path.GetExtension(file.FileName);
                                string media_Url = "/Photo/Video/" + HELPER.PHOTO_SRC("Video", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                                obje = new Models.ShareObject();

                                obje.member_id = Convert.ToInt32(Session["id"]);
                                obje.image_url = media_Url;
                                obje.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.video.ToString()));
                                obje.date = DateTime.Now;
                                obje.sex = Session["sex"].ToString();
                                obje.status = Convert.ToInt32(Enum.Parse(typeof(status), Session["status"].ToString()));
                                obje.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                                obje.text = ShareObjectVideoText.ToString();
                                obje.confrim = Convert.ToInt32(Enum.Parse(typeof(confrim), confrim.in_review.ToString()));
                                obje.LATITUDE = "";
                                obje.LONGITUDE = "";

                                file.SaveAs(Server.MapPath(media_Url));

                                if (System.IO.File.Exists(Server.MapPath(media_Url)))
                                {
                                    ShareObjectBL.SHARE_OBJECT_INSERT(obje);

                                    return RedirectToAction("Home", "Home");
                                }
                                else
                                {
                                    return RedirectToAction("Uppss", "Error", new { P = 5 });
                                }
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 7 });
                            }

                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 9 });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 21 });
                    }

                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 38 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }

        public ActionResult ShareAudio()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShareAudio(ShareObject obje, HttpPostedFileBase file, string ShareObjectAudioText)
        {
            try
            {
                if (true)//DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35
                {
                    if (!string.IsNullOrEmpty(ShareObjectAudioText))
                    {
                        if (file.ContentType.ToLower() == "audio/mpeg" || file.ContentType.ToLower() == "audio/ogg" || file.ContentType.ToLower() == "audio/mp3" || file.ContentType.ToLower() == "audio/wav")
                        {
                            if (file.ContentLength < Convert.ToDouble(5000000))
                            {
                                string file_name = Path.GetFileName(file.FileName);
                                string extension = Path.GetExtension(file.FileName);
                                string media_Url = "/Photo/Audio/" + HELPER.PHOTO_SRC("Audio", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                                obje = new Models.ShareObject();

                                obje.member_id = Convert.ToInt32(Session["id"]);
                                obje.image_url = media_Url;
                                obje.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.audio.ToString()));
                                obje.date = DateTime.Now;
                                obje.sex = Session["sex"].ToString();
                                obje.status = Convert.ToInt32(Enum.Parse(typeof(status), Session["status"].ToString()));
                                obje.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                                obje.text = ShareObjectAudioText.ToString();
                                obje.confrim = Convert.ToInt32(Enum.Parse(typeof(confrim), confrim.in_review.ToString()));
                                obje.LATITUDE = "";
                                obje.LONGITUDE = "";

                                file.SaveAs(Server.MapPath(media_Url));

                                if (System.IO.File.Exists(Server.MapPath(media_Url)))
                                {
                                    ShareObjectBL.SHARE_OBJECT_INSERT(obje);

                                    return RedirectToAction("Home", "Home");
                                }
                                else
                                {
                                    return RedirectToAction("Uppss", "Error", new { P = 5 });
                                }
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 7 });
                            }

                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 10 });
                        }

                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 21 });
                    }

                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 38 });
                }

            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }

        public ActionResult ShareStreemHome(int page)
        {
            try
            {
                int id = Convert.ToInt32(Session["id"]);
                string sex = Session["sex"].ToString();
                List<shareStreem> ShareStreemList = Share_MemberBL.SHARE_STREEM_ALL(sex, id, page);
                return PartialView(ShareStreemList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 24 });
            }
        }

        public ActionResult NoticeEventHeart(int obje_id, int sort, int whom_id, string eventAction, int noticeTableSort)
        {//int id,int sort,int whom_id
            try
            {
                if (Request.QueryString["eventAction"] == null)
                {
                    int who_id = Convert.ToInt32(Session["id"]);

                    if (obje_id != 0 || sort != 0 || whom_id != 0)
                    {
                        bool result_ = EnjoyBL.ENJOY_GET_BY_ID(obje_id, sort, who_id, whom_id, noticeTableSort);

                        ViewBag.result = result_;
                        ViewBag.obje_id = obje_id;
                        ViewBag.sort = sort;
                        ViewBag.whom_id = whom_id;
                        ViewBag.noticeTableSort = noticeTableSort;

                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }

                }
                else
                {
                    if (Request.QueryString["eventAction"] == "like")
                    {//Request.QueryString["obje_id"] != null || Request.QueryString["whom_id"] != null || Request.QueryString["sort"] != null ||                       

                        Enjoy enjoy = new Enjoy();
                        enjoy.obje_id = Convert.ToInt32(Request.QueryString["obje_id"]);
                        enjoy.sort = Convert.ToInt32(Request.QueryString["sort"]);
                        enjoy.Whom_id = Convert.ToInt32(Request.QueryString["Whom_id"]);
                        enjoy.Who_id = Convert.ToInt32(Session["id"]);
                        enjoy.noticeTableSort = noticeTableSort;
                        ViewBag.result = EnjoyBL.ENJOY_INSERT(enjoy);

                        ViewBag.obje_id = obje_id;
                        ViewBag.sort = sort;
                        ViewBag.whom_id = whom_id;
                        ViewBag.noticeTableSort = noticeTableSort;
                        return View();
                    }
                    else
                    {
                        if (Request.QueryString["eventAction"] == "dislike")
                        {
                            ViewBag.result = EnjoyBL.ENJOY_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Request.QueryString["sort"]), Convert.ToInt32(Session["id"]), Convert.ToInt32(Request.QueryString["Whom_id"]), noticeTableSort);

                            ViewBag.obje_id = obje_id;
                            ViewBag.sort = sort;
                            ViewBag.whom_id = whom_id;
                            ViewBag.noticeTableSort = noticeTableSort;
                            return View();
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 0 });
                        }
                    }
                }


            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult Complaint(int obje_id, int sort, int whom_id, string eventAction, int noticeTableSort)
        {//id, who_id, whom_id, sort, obje_id, text, date
            try
            {
                if (Request.QueryString["eventAction"] == null)
                {
                    int who_id = Convert.ToInt32(Session["id"]);

                    if (obje_id != 0 || sort != 0 || whom_id != 0)
                    {
                        bool result_ = ComplaintBL.COMPLAINT_GET_BY_ID(obje_id, sort, who_id, whom_id, noticeTableSort);

                        ViewBag.result = result_;
                        ViewBag.obje_id = obje_id;
                        ViewBag.sort = sort;
                        ViewBag.whom_id = whom_id;
                        ViewBag.noticeTableSort = noticeTableSort;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }

                }
                else
                {
                    complaint complaint_ = new complaint();
                    complaint_.obje_id = Convert.ToInt32(Request.QueryString["obje_id"]);
                    complaint_.sort = Convert.ToInt32(Request.QueryString["sort"]);
                    complaint_.whom_id = Convert.ToInt32(Request.QueryString["Whom_id"]);
                    complaint_.who_id = Convert.ToInt32(Session["id"]);
                    complaint_.noticeTableSort = noticeTableSort;
                    switch (Request.QueryString["eventAction"])
                    {
                        case "action1":
                            complaint_.text = "Dini milli duygulara hakaret";
                            ViewBag.result = ComplaintBL.COMPLAINT_INSERT(complaint_);
                            break;
                        case "action2":
                            complaint_.text = "Kişilik haklarını ihlal";
                            ViewBag.result = ComplaintBL.COMPLAINT_INSERT(complaint_);
                            break;
                        case "action3":
                            complaint_.text = "İstismar yada suistimal";
                            ViewBag.result = ComplaintBL.COMPLAINT_INSERT(complaint_);
                            break;
                        case "action4":
                            complaint_.text = "Ahlak ve terbiye dışı";
                            ViewBag.result = ComplaintBL.COMPLAINT_INSERT(complaint_);
                            break;
                        case "delete":
                            ViewBag.result = ComplaintBL.COMPLAINT_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Request.QueryString["sort"]), Convert.ToInt32(Session["id"]), Convert.ToInt32(Request.QueryString["Whom_id"]), noticeTableSort);
                            break;
                        default:

                            break;
                    }

                    ViewBag.obje_id = obje_id;
                    ViewBag.sort = sort;
                    ViewBag.whom_id = whom_id;
                    ViewBag.noticeTableSort = noticeTableSort;
                    return View();

                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult Comment(string text, int obje_id, int sort, int whom_id, int noticeTableSort)
        {
            if (Request.QueryString["eventAction"] == null)
            {

                List<Comment> commentList = CommentBL.COMMENT_GET_TOP3_BY_ID(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]), whom_id);
                shareStreem SStreem = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(Convert.ToInt32(Request.QueryString["obje_id"]));

                ViewBag.who_id = Convert.ToInt32(Session["id"]);
                ViewBag.whom_id = whom_id;
                ViewBag.obje_id = obje_id;
                ViewBag.sort = sort;
                ViewBag.noticeTableSort = noticeTableSort;

                ViewBag.ListCount = commentList.Count;
                ViewBag.commentList = commentList;
                ViewBag.currentWho_id = Convert.ToInt32(Session["id"]);
                return View(SStreem);
            }
            else
            {
                if (Request.QueryString["eventAction"] == "comment")
                {//id, sub_id, obje_id, sort, noticeTableSort, who_id, whom_id, text, date

                    if (DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35)
                    {
                        Comment comment = new Comment();
                        comment.obje_id = Convert.ToInt32(Request.QueryString["obje_id"]);

                        comment.who_id = Convert.ToInt32(Session["id"]);
                        comment.whom_id = Convert.ToInt32(Request.QueryString["whom_id"]);
                        comment.noticeTableSort = noticeTableSort;
                        comment.sort = Convert.ToInt32(Request.QueryString["sort"]);
                        comment.comment_text = text;

                        if (DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35)
                        {

                            bool result = CommentBL.COMMENT_INSERT(comment);
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 40 });
                        }


                        // List<Comment> commentList = CommentBL.COMMENT_GET_BY_ID(Convert.ToInt32(Request.QueryString["obje_id"]), noticeTableSort, Convert.ToInt32(Session["id"]), Convert.ToInt32(Request.QueryString["whom_id"]));
                        List<Comment> commentList = CommentBL.COMMENT_GET_TOP3_BY_ID(Convert.ToInt32(Request.QueryString["obje_id"]), noticeTableSort, Convert.ToInt32(Session["id"]), Convert.ToInt32(Request.QueryString["whom_id"]));

                        ViewBag.who_id = Convert.ToInt32(Session["id"]);
                        ViewBag.whom_id = whom_id;
                        ViewBag.obje_id = obje_id;
                        ViewBag.sort = sort;
                        ViewBag.noticeTableSort = noticeTableSort;

                        ViewBag.ListCount = commentList.Count;
                        ViewBag.commentList = commentList;
                        ViewBag.currentWho_id = Convert.ToInt32(Session["id"]);
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 40 });
                    }
                }
                else
                {
                    if (Request.QueryString["eventAction"] == "delete")
                    {//
                     // bool result = CommentBL.COMMENT_DELETE(Convert.ToInt32(Request.QueryString["sort"]), Convert.ToInt32(Request.QueryString["obje_id"]), noticeTableSort);//burada bir daha comment.id almamak için -Request.QueryString["sort"]- bunu kullandım

                        bool result = CommentBL.COMMENT_DELETE(Convert.ToInt32(Request.QueryString["comment_id"]), Convert.ToInt32(Request.QueryString["obje_id"]), noticeTableSort, Convert.ToInt32(Session["id"]));
                        //List<Comment> commentList = CommentBL.COMMENT_GET_BY_ID(Convert.ToInt32(Request.QueryString["obje_id"]), noticeTableSort, Convert.ToInt32(Session["id"]), Convert.ToInt32(Request.QueryString["whom_id"]));
                        List<Comment> commentList = CommentBL.COMMENT_GET_TOP3_BY_ID(Convert.ToInt32(Request.QueryString["obje_id"]), noticeTableSort, Convert.ToInt32(Session["id"]), Convert.ToInt32(Request.QueryString["whom_id"]));

                        ViewBag.who_id = Convert.ToInt32(Session["id"]);
                        ViewBag.whom_id = whom_id;
                        ViewBag.obje_id = obje_id;
                        ViewBag.sort = sort;
                        ViewBag.noticeTableSort = noticeTableSort;

                        ViewBag.ListCount = commentList.Count;
                        ViewBag.commentList = commentList;
                        ViewBag.currentWho_id = Convert.ToInt32(Session["id"]);
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }
                }
            }
        }

        public ActionResult CommentCount(int obje_id, int sort, int whom_id, int noticeTableSort)
        {
            try
            {//int obje_id, int sort, int whom_id

                List<Comment> commentList = CommentBL.COMMENT_GET_BY_ID(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]), whom_id);
                ViewBag.ListCount = commentList.Count;
                shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(obje_id);
                return View(shareObje);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult SharedObjectDetails(int obje_id, int sort, int whom_id)
        {
            try
            {
                shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(obje_id);

                List<Comment> commentList = CommentBL.COMMENT_GET_BY_ID(obje_id, Convert.ToInt32(Enum.Parse(typeof(noticeTableSort), noticeTableSort.ShareObject.ToString())), Convert.ToInt32(Session["id"]), whom_id);

                ViewBag.who_id = Convert.ToInt32(Session["id"]);
                ViewBag.whom_id = whom_id;
                ViewBag.obje_id = obje_id;
                ViewBag.sort = sort;
                ViewBag.ListCount = commentList.Count;
                ViewBag.commentList = commentList;
                ViewBag.currentWho_id = Convert.ToInt32(Session["id"]);
                return View(shareObje);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 25 });
            }

        }

        public ActionResult NoticeShortList()//int who_id
        {
            try
            {
                List<NoticeListItem> NoticeList = NoticeBL.NOTICE_SHORT_LIST(Convert.ToInt32(Session["id"]));
                int count_ = NoticeBL.NOTICE_LIST_COUNT_A(Convert.ToInt32(Session["id"]));
                ViewBag.count_ = count_;
                return View(NoticeList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 25 });
            }
        }

        public ActionResult NoticePassiveAndDistribution(int id_, int obje_id_, int who_id_, int sort_, int noticeTableSort_, bool active_)
        {
            try
            {
                if (active_)
                {
                    NoticeBL.NOTAICE_PASSIVE(id_);
                }
                if (noticeTableSort_ == 0)
                {//shareObje ile ilgili MyProfil/MySharedObjectDetails?obje_id=1179&sort=0

                    return RedirectToAction("MySharedObjectDetails", "MyProfil", new { obje_id = obje_id_, sort = sort_ });
                }
                else if (noticeTableSort_ == 1)
                {//Albüm ile ilgili   /MyProfil/MyScreen?album_id=181&sort=0
                    return RedirectToAction("MyScreen", "MyProfil", new { album_id = obje_id_, sort = sort_ });
                }
                else if (noticeTableSort_ == 2)
                {//güldü profilinize baktı vs ile ilgili bildirim şeklinde kalmış  who_id nin profiline gidilecek --/Profil/Profil?member_id=42                  
                    return RedirectToAction("Profil", "Profil", new { member_id = who_id_ });
                }
                else if (noticeTableSort_ == 4)
                {//Favori listesine eklemiş
                    return RedirectToAction("Profil", "Profil", new { member_id = who_id_ });
                }
                else if (noticeTableSort_ == 8)
                {//Mesaj yazmış Profil/ProfilMessage?whom_id=60
                    return RedirectToAction("ProfilMessage", "Profil", new { whom_id = who_id_ });
                }
                else if (noticeTableSort_ == 16)
                {//Bir gönderide etiketlemiş
                    if (sort_ == 32)
                    {//Profil/ProfilSurveyDetails?survey_id=1173&whom_id=60
                        return RedirectToAction("ProfilSurveyDetails", "Profil", new { survey_id = obje_id_, whom_id = who_id_ });
                    }
                    else
                    {//Home/SharedObjectDetails?obje_id=1173&sort=32&whom_id=60
                        return RedirectToAction("SharedObjectDetails", "Home", new { obje_id = obje_id_, sort = sort_, whom_id = who_id_ });
                    }
                }
                else if (noticeTableSort_ == 32)
                {//memberProfil tablosuyla ilgili aslında gülümseme de bununla ilgili
                    return RedirectToAction("Profil", "Profil", new { member_id = who_id_ });
                }
                else
                {//burada da hata dönecek.
                    return RedirectToAction("Uppss", "Error", new { P = 32 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 31 });
            }
        }

        public ActionResult NoticePage()
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 25 });
            }
        }

        public ActionResult NoticePageList(int page)
        {
            try
            {
                List<NoticeListItem> NoticeList = NoticeBL.NOTICE_ALL_LIST(Convert.ToInt32(Session["id"]), page);
                return View(NoticeList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 25 });
            }
        }

        public ActionResult Search()
        {
            try
            {
                ViewBag.sex = Session["sex"].ToString();
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 33 });
            }
        }

        public ActionResult SearchNickName(string nickName)
        {//, string searchEvent, string EventNickName
            try
            {
                if (!string.IsNullOrWhiteSpace(nickName))
                {
                    MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_NICK_NAME(nickName);
                    if (member != null)
                    {
                        if (member.sex != Session["sex"].ToString())
                        {
                            ViewBag.search = true;
                            return View(member);
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 34 });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 35 });
                    }
                }
                else
                {                   
                    return View();
                }

            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 34 });
            }
        }

        //public ActionResult CustomSearch(int page)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(Request.QueryString["searchEvent"]))
        //        {
        //            List<MemberProfil> memberList = MemberProfilBL.SEARCH_PAGE_MEMBER_PROFIL(Convert.ToInt32(Session["id"]), page);
        //            ViewBag.memberList = memberList;
        //            ViewBag.sex = Session["sex"].ToString();
        //            ViewBag.searchEvent = "default";
        //            ViewBag.search = false;
        //            return View(memberList);
        //            //return PartialView("CustomSearch", memberList);
        //        }
        //        else
        //        {
        //            if (Request.QueryString["searchEvent"] == "Search")
        //            {
        //                List<MemberProfil> memberList = MemberProfilBL.SEARCH_CUSTOM_MEMBER_PROFIL(Convert.ToInt32(Session["id"]), Session["sex"].ToString(),
        //                //    Request.QueryString["matiral_status"].ToString(), Convert.ToInt32(Request.QueryString["age_from"]), Convert.ToInt32(Request.QueryString["age_to"]),
        //                //    Request.QueryString["educational_status"].ToString(), Request.QueryString["city"].ToString(), Request.QueryString["country"].ToString(),
        //                //    Convert.ToInt32(Request.QueryString["hicap"]), Convert.ToInt32(Request.QueryString["height_from"]), Convert.ToInt32(Request.QueryString["height_to"]),
        //                //    Convert.ToInt32(Request.QueryString["kilo_from"]), Convert.ToInt32(Request.QueryString["kilo_to"]),
        //                //    Request.QueryString["hair_color"].ToString(), Request.QueryString["eye_color"].ToString(), Request.QueryString["disable"].ToString(), page);

        //                //ViewBag.sex = Session["sex"].ToString();
        //                //ViewBag.memberList = memberList;
        //                //ViewBag.search = true;
        //                //ViewBag.searchEvent = "Search";
        //                return View(memberList);
        //                //return PartialView("CustomSearch", memberList);
        //            }
        //            else
        //            {
        //                if (Request.QueryString["searchEvent"] == "default")
        //                {
        //                    List<MemberProfil> memberList = MemberProfilBL.SEARCH_PAGE_MEMBER_PROFIL(Convert.ToInt32(Session["id"]), page);
        //                    ViewBag.memberList = memberList;
        //                    ViewBag.sex = Session["sex"].ToString();
        //                    ViewBag.search = false;
        //                    ViewBag.searchEvent = "default";
        //                    //return View(memberList);
        //                    return PartialView("CustomSearch", memberList);
        //                }
        //                else
        //                {
        //                    return View();
        //                }
        //            }
        //        }

        //    }
        //    catch
        //    {
        //        return RedirectToAction("Uppss", "Error", new { P = 33 });
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CustomSearch(string matiral_status, string age_from, string age_to, string educational_status, string city, string country, string hicap, string height_from, string height_to, string kilo_from, string kilo_to, string hair_color, string eye_color, string disable, int? page)
        {
            try
            {
                int currentPage = page ?? 0;
                List<MemberProfil> memberList = new List<MemberProfil>();
                switch (Request.QueryString["searchEvent"])
                {
                    case "default":
                    case "":
                    case null:
                        memberList = MemberProfilBL.SEARCH_PAGE_MEMBER_PROFIL(Convert.ToInt32(Session["id"]), currentPage);
                        ViewBag.searchEvent = "default";
                        break;
                    case "Search":
                        memberList = MemberProfilBL.SEARCH_CUSTOM_MEMBER_PROFIL(Convert.ToInt32(Session["id"]), Session["sex"].ToString(), matiral_status, Convert.ToInt32(age_from), Convert.ToInt32(age_to), educational_status, city, country, Convert.ToInt32(hicap), Convert.ToInt32(height_from), Convert.ToInt32(height_to), Convert.ToInt32(kilo_from), Convert.ToInt32(kilo_to), hair_color, eye_color, disable, currentPage);
                        ViewBag.searchEvent = "Search";
                        break;
                    case "New":
                        memberList = MemberProfilBL.SEARCH_CUSTOM_NEW_MEMBER(Convert.ToInt32(Session["id"]), currentPage);
                        ViewBag.searchEvent = "New";
                        break;

                    case "Online":
                        memberList = MemberProfilBL.SEARCH_CUSTOM_ONLINE_MEMBER(Convert.ToInt32(Session["id"]), currentPage);
                        ViewBag.searchEvent = "Online";
                        break;

                    case "Survey":
                        memberList = MemberProfilBL.SEARCH_CUSTOM_SURVEY_MEMBER(Convert.ToInt32(Session["id"]), currentPage);
                        ViewBag.searchEvent = "Survey";
                        break;
                    case "Share":
                        memberList = MemberProfilBL.SEARCH_CUSTOM_SHARE_MEMBER(Convert.ToInt32(Session["id"]), currentPage);
                        ViewBag.searchEvent = "Share";
                        break;
                    default:
                        memberList = MemberProfilBL.SEARCH_PAGE_MEMBER_PROFIL(Convert.ToInt32(Session["id"]), currentPage);
                        ViewBag.searchEvent = "default";
                        break;
                }
                        ViewBag.memberList = memberList;
                        ViewBag.sex = Session["sex"].ToString();                       
                        //ViewBag.search = false;                     
                        return PartialView("CustomSearch", memberList);                
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 33 });
            }
        }

        public ActionResult CustomSearchNew(int page)
        {
            try
            {
                if (Request.QueryString["page"] != null)
                {
                    List<MemberProfil> memberList = MemberProfilBL.SEARCH_CUSTOM_NEW_MEMBER(Convert.ToInt32(Session["id"]), page);
                    ViewBag.sex = Session["sex"].ToString();
                    ViewBag.memberList = memberList;
                    ViewBag.searchEvent = "New";
                    ViewBag.search = true;
                    //return View(memberList);
                    return PartialView("CustomSearch", memberList);
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 33 });
            }
        }
        public ActionResult CustomSearchOnline(int page)
        {
            try
            {
                if (Request.QueryString["page"] != null)
                {
                    List<MemberProfil> memberList = MemberProfilBL.SEARCH_CUSTOM_ONLINE_MEMBER(Convert.ToInt32(Session["id"]), page);
                    ViewBag.sex = Session["sex"].ToString();
                    ViewBag.memberList = memberList;
                    ViewBag.searchEvent = "Online";
                    ViewBag.search = true;
                    //return View(memberList);
                    return PartialView("CustomSearch", memberList);
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 33 });
            }
        }
        public ActionResult CustomSearchCreateSurveyMembers(int page)
        {
            try
            {
                if (Request.QueryString["page"] != null)
                {
                    List<MemberProfil> memberList = MemberProfilBL.SEARCH_CUSTOM_SURVEY_MEMBER(Convert.ToInt32(Session["id"]), page);
                    ViewBag.sex = Session["sex"].ToString();
                    ViewBag.memberList = memberList;
                    ViewBag.searchEvent = "Survey";
                    ViewBag.search = true;
                    //return View(memberList);
                    return PartialView("CustomSearch", memberList);
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 33 });
            }
        }        
        public ActionResult CustomSearchCreateShareMembers(int page)
        {
            try
            {
                if (Request.QueryString["page"] != null)
                {
                    List<MemberProfil> memberList = MemberProfilBL.SEARCH_CUSTOM_SHARE_MEMBER(Convert.ToInt32(Session["id"]), page);
                    ViewBag.sex = Session["sex"].ToString();
                    ViewBag.memberList = memberList;
                    ViewBag.searchEvent = "Share";
                    ViewBag.search = true;
                    //return View(memberList);
                    return PartialView("CustomSearch", memberList);
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 33 });
            }
        }

        public ActionResult SearchLocation()
        {
            try
            {
                if (String.IsNullOrEmpty(Request.QueryString["searchLocation"]))
                {
                    ViewBag.LAT = "";
                    ViewBag.LNG = "";
                    ViewBag.searchLocation = "";
                    return View();
                }
                else
                {
                    if (Request.QueryString["searchLocation"] == "cursor")
                    {
                        ViewBag.LAT = Request.QueryString["latitude"];
                        ViewBag.LNG = Request.QueryString["longitude"];
                        ViewBag.searchLocation = "cursor";
                        return View();
                    }
                    else
                    {
                        if (Request.QueryString["searchLocation"] == "distance")
                        {
                            ViewBag.LAT = Request.QueryString["latitude"];
                            ViewBag.LNG = Request.QueryString["longitude"];
                            ViewBag.searchLocation = "distance";
                            return View();
                        }
                        else
                        {
                            if (Request.QueryString["searchLocation"] == "space")
                            {
                                ViewBag.LAT = Request.QueryString["latitude"];
                                ViewBag.LNG = Request.QueryString["longitude"];
                                ViewBag.searchLocation = "space";
                                return View();
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 36 });
                            }
                        }
                    }
                }

            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 37 });
            }
        }

        public ActionResult SearchLocationPartial(int page)
        {
            try
            {
                if (String.IsNullOrEmpty(Request.QueryString["searchLocation"]))
                {
                    List<shareStreem> LocationList = Share_MemberBL.SEARCH_LOCATION(Convert.ToInt32(Session["id"]), page);
                    ViewBag.LocationList = LocationList;
                    ViewBag.LAT = "";
                    ViewBag.LNG = "";
                    ViewBag.searchLocation = "";
                    return View();
                }
                else
                {
                    if (Request.QueryString["searchLocation"] == "cursor")
                    {
                        List<shareStreem> LocationList = Share_MemberBL.SEARCH_LOCATION_CURSOR(Convert.ToInt32(Session["id"]), Request.QueryString["latitude"], Request.QueryString["longitude"], page);
                        ViewBag.LocationList = LocationList;
                        ViewBag.LAT = Request.QueryString["latitude"];
                        ViewBag.LNG = Request.QueryString["longitude"];
                        ViewBag.searchLocation = "cursor";
                        return View();
                    }
                    else
                    {
                        if (Request.QueryString["searchLocation"] == "distance")
                        {

                            List<shareStreem> LocationList = Share_MemberBL.SEARCH_LOCATION_DISTANCE(Convert.ToInt32(Session["id"]), Request.QueryString["latitude"], Request.QueryString["longitude"], page);
                            ViewBag.LocationList = LocationList;
                            ViewBag.LAT = Request.QueryString["latitude"];
                            ViewBag.LNG = Request.QueryString["longitude"];
                            ViewBag.searchLocation = "distance";
                            return View();
                        }
                        else
                        {
                            if (Request.QueryString["searchLocation"] == "space")
                            {
                                List<shareStreem> LocationList = Share_MemberBL.SEARCH_LOCATION_SPACE(Convert.ToInt32(Session["id"]), Request.QueryString["latitude"], Request.QueryString["longitude"], page);
                                ViewBag.LocationList = LocationList;
                                ViewBag.LAT = Request.QueryString["latitude"];
                                ViewBag.LNG = Request.QueryString["longitude"];
                                ViewBag.searchLocation = "space";
                                return View();
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 36 });
                            }
                        }
                    }

                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 37 });
            }
        }

        public ActionResult svrx()
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 37 });
            }
        }

        public ActionResult SharePartial(int page)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["svrx"]))
                {
                    List<ShareObject> ShareObjeList = ShareObjectBL.HOME_STREEM(Session["sex"].ToString(), page);
                    ViewBag.ShareObjeList = ShareObjeList;
                    ViewBag.svrx = "";
                    return View(ShareObjeList);
                }
                else
                {
                    if (Request.QueryString["svrx"] == "sort")
                    {
                        List<ShareObject> ShareObjeList = ShareObjectBL.HOME_STREEM_SORT(Session["sex"].ToString(), Convert.ToInt32(Request.QueryString["sort"]), page);
                        ViewBag.ShareObjeList = ShareObjeList;
                        ViewBag.svrx = "sort";
                        ViewBag.sort = Convert.ToInt32(Request.QueryString["sort"]);
                        return View(ShareObjeList);
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 37 });
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult svrxModal(int modal_id, string Text)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["svrxModal"]))
                {
                    ShareObject obje = ShareObjectBL.SHARE_OBJECT_BY_ID(modal_id);
                    ViewBag.result = 0;
                    return View(obje);
                }
                else
                {
                    if (Request.QueryString["svrxModal"] == "modal")
                    {
                        ShareObject obje = ShareObjectBL.SHARE_OBJECT_BY_ID(Convert.ToInt32(Request.QueryString["modal_id"]));
                        ShareObject newObje = new Models.ShareObject();
                        newObje.member_id = Convert.ToInt32(Session["id"]);
                        newObje.sex = Session["sex"].ToString();
                        newObje.text = Text;
                        newObje.image_url = obje.image_url;
                        newObje.sort = obje.sort;
                        newObje.date = DateTime.Now;
                        newObje.confrim = Convert.ToInt32(Enum.Parse(typeof(confrim), confrim.ShareObject.ToString()));
                        newObje.status = Convert.ToInt32(Enum.Parse(typeof(status), Session["status"].ToString()));
                        newObje.LATITUDE = obje.LATITUDE;
                        newObje.LONGITUDE = obje.LONGITUDE;
                        newObje.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));

                        int result = ShareObjectBL.SHARE_OBJECT_INSERT(newObje);
                        ViewBag.result = result;
                        return View(obje);
                    }
                    else
                    {
                        return View();
                    }
                }

            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 37 });
            }
        }

        public ActionResult Survey()
        {
            try
            {
                List<Survey> SurveyList = SurveyBL.SURVEY_HOME_FORCED(Convert.ToInt32(Session["id"]));
                ViewBag.SurveyList = SurveyList;
                ViewBag.count = SurveyList.Count;
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 37 });
            }
        }

        public ActionResult SurveyForced()
        {
            try
            {
                List<Survey> SurveyList = SurveyBL.SURVEY_HOME_FORCED(Convert.ToInt32(Session["id"]));
                ViewBag.SurveyList = SurveyList;
                ViewBag.count = SurveyList.Count;
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 37 });
            }
        }

        public ActionResult SurveySelective()
        {
            try
            {
                List<Survey> SurveyList = SurveyBL.SURVEY_HOME_SELECTIVE(Convert.ToInt32(Session["id"]));
                ViewBag.SurveyList = SurveyList;
                ViewBag.count = SurveyList.Count;
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 37 });
            }
        }

        public ActionResult HomeSurveyDetails(int survey_id)
        {
            try
            {

                ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(survey_id);
                ViewBag.survey_id = survey_id;
                return View(shareObje);
            }
            catch
            {
                return RedirectToAction("Survey", "Home");
            }
        }

        public ActionResult HomeSurveyAnswer(int survey_id)
        {
            try
            {
                if (Request.QueryString["surveyEvent"] == null)
                {
                    List<SurveyAnswerItem> answerList = SurveyBL.SURVEY_ANSWER(survey_id, Convert.ToInt32(Session["id"]));
                    List<SurveyQuestion> SurveyQuestionList = SurveyBL.SURVEY_ANSWER_ALL_QUESTION(survey_id);
                    List<SurveyQuestion> questionList = new List<SurveyQuestion>();
                    foreach (var question in SurveyQuestionList)
                    {
                        bool check = true;
                        foreach (var answer in answerList)
                        {
                            if (question.id == answer.question_id)
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check)
                        {
                            questionList.Add(question);
                        }
                    }
                    ViewBag.answerList = answerList;
                    ViewBag.questionList = questionList;
                    ViewBag.survey_id = survey_id;
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(survey_id);
                    return View(shareObje);
                }
                else if (Request.QueryString["surveyEvent"] == "answer")
                {
                    SurveyAnswer answerItem = new SurveyAnswer();
                    answerItem.survey_id = Convert.ToInt32(Request.QueryString["survey_id"]);
                    answerItem.question_id = Convert.ToInt32(Request.QueryString["question_id"]);
                    answerItem.member_id = Convert.ToInt32(Session["id"]);
                    answerItem.ABCD_options = Request.QueryString["ABCD_options"];
                    int result_ = SurveyBL.SURVEY_ANSWER_INSERT(answerItem);


                    List<SurveyAnswerItem> answerList = SurveyBL.SURVEY_ANSWER(survey_id, Convert.ToInt32(Session["id"]));
                    List<SurveyQuestion> SurveyQuestionList = SurveyBL.SURVEY_ANSWER_ALL_QUESTION(survey_id);
                    List<SurveyQuestion> questionList = new List<SurveyQuestion>();
                    foreach (var question in SurveyQuestionList)
                    {
                        bool check = true;
                        foreach (var answer in answerList)
                        {
                            if (question.id == answer.question_id)
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check)
                        {
                            questionList.Add(question);
                        }
                    }
                    ViewBag.answerList = answerList;
                    ViewBag.questionList = questionList;
                    ViewBag.survey_id = survey_id;
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(survey_id);
                    return View(shareObje);
                }
                else if (Request.QueryString["surveyEvent"] == "delete")
                {
                    int deleted = SurveyBL.SURVEY_ANSWER_DELETE(Convert.ToInt32(Request.QueryString["answer_id"]), Convert.ToInt32(Request.QueryString["question_id"]), Convert.ToInt32(Session["id"]));
                    if (deleted > 0)
                    {
                        List<SurveyAnswerItem> answerList = SurveyBL.SURVEY_ANSWER(survey_id, Convert.ToInt32(Session["id"]));
                        List<SurveyQuestion> SurveyQuestionList = SurveyBL.SURVEY_ANSWER_ALL_QUESTION(survey_id);
                        List<SurveyQuestion> questionList = new List<SurveyQuestion>();
                        foreach (var question in SurveyQuestionList)
                        {
                            bool check = true;
                            foreach (var answer in answerList)
                            {
                                if (question.id == answer.question_id)
                                {
                                    check = false;
                                    break;
                                }
                            }
                            if (check)
                            {
                                questionList.Add(question);
                            }
                        }
                        ViewBag.answerList = answerList;
                        ViewBag.questionList = questionList;
                        ViewBag.survey_id = survey_id;
                        ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(survey_id);
                        return View(shareObje);
                    }

                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 28 });
                    }
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 0 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 37 });
            }
        }

        public ActionResult HomeSurveyMember(int survey_id, int page)
        {
            try
            {
                if (!string.IsNullOrEmpty("survey_id"))
                {
                    List<MemberSurveyAnswer> memberList = SurveyBL.MEMBER_SURVEY_ANSWER(Convert.ToInt32(Session["id"]), survey_id, page);
                    ViewBag.memberList = memberList;
                    ViewBag.survey_id = survey_id;
                    return View(memberList);
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 28 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 37 });
            }
        }

        public ActionResult Chat()
        {
            try
            {
                if (true)//DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35
                {
                    if (Request.QueryString["chat"] == null)
                    {
                        ViewBag.nick = Session["nick_name"].ToString();
                        ViewBag.MyImg = Session["profil_media"].ToString();
                        return View();
                    }
                    else
                    {
                        ViewBag.result = Request.QueryString["chat"].ToString();
                        ViewBag.nick = Session["nick_name"].ToString();
                        ViewBag.id = Convert.ToInt32(Session["id"]);
                        ViewBag.MyImg = Session["profil_media"].ToString();
                        return View();
                    }

                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 41 });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ChatList()
        {
            try
            {
                List<MemberShortCard> FavoriteMyList_ = FavoriteBL.FAVORITE_MY_LIST(Convert.ToInt32(Session["id"]));
                ViewBag.FavoriteMyList_ = FavoriteMyList_;
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }
        public ActionResult ChatNickName()
        {
            if (Request.QueryString["chat"] != null)
            {
                try
                {
                    MemberProfil Member = MemberProfilBL.MEMBER_PROFIL_GET_BY_NICK_NAME(Request.QueryString["chat"].ToString());

                    if (Member.sex != Session["sex"].ToString() && Member.is_online == true)
                    {
                        ViewBag.member_id = Member.id;
                        ViewBag.nick_name = Member.nick_name;
                        ViewBag.img_url = Member.profil_media;
                        ViewBag.age = (int)(DateTime.Now.Year) - (int)((Member.age).Year);
                        ViewBag.sex = "yes";
                    }
                    else
                    {
                        ViewBag.sex = "no";
                    }

                    return View();
                }
                catch
                {
                    return RedirectToAction("Uppss", "Error", new { P = 20 });
                }
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChatNickName(string nickName)
        {
            try
            {
                MemberProfil Member = MemberProfilBL.MEMBER_PROFIL_GET_BY_NICK_NAME(nickName);

                if (Member.sex != Session["sex"].ToString() && Member.is_online == true)
                {
                    ViewBag.member_id = Member.id;
                    ViewBag.nick_name = Member.nick_name;
                    ViewBag.img_url = Member.profil_media;
                    ViewBag.age = (int)(DateTime.Now.Year) - (int)((Member.age).Year);
                    ViewBag.sex = "yes";
                }
                else
                {
                    ViewBag.sex = "no";
                }

                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult ChatNot()
        {
            try
            {
                ViewBag.nick = Session["nick_name"].ToString();
                ViewBag.MyImg = Session["profil_media"].ToString();
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Settings()
        {
            try
            {
                if (Request.QueryString["query"] == null)
                {
                    return View();
                }
                else if (Convert.ToInt32(Request.QueryString["query"]) > 0)
                {
                    int query = HELPER.SETTING_DELETE(Convert.ToInt32(Request.QueryString["query"]), Convert.ToInt32(Session["id"]));
                    ViewBag.query = "ok";
                }

                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 43 });
            }
        }

        public ActionResult Payment()
        {
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Mail()
        {
            try
            {
                MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_NICK_NAME(Session["nick_name"].ToString());
                ViewBag.Mail = member.e_mail;
                ViewBag.send = "";
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 44 });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Mail(string text, string e_mail)
        {
            try
            {
                MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_NICK_NAME(Session["nick_name"].ToString());
                string body = text + Environment.NewLine + " E-mail adresi: " + e_mail;
                string user = ConfigurationManager.AppSettings["MailUser"];              
              
                bool value = HELPER.SEND_MAIL(user, member.nick_name + " ( " + member.sex + " ) ÜYEDEN MESAJ VAR", body);

                if (value)
                {
                    ViewBag.send = "send";
                }
                
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 44 });
            }
        }

        public ActionResult How()
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction("Info", "Default");
            }
        }
    }
}