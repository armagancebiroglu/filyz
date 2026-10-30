using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWebWeb.BL;
using WebWebWeb.Models;

namespace WebWebWeb.Controllers
{
    public class xzgqhjkController : Controller
    {
        public ActionResult Index()
        {//şifre ile admin giriş sayfası
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection Form, Editor model)
        {//şifre ile admin giriş sayfası
            try
            {
                if (!string.IsNullOrEmpty(model.nick_name) || !string.IsNullOrEmpty(model.password))
                {
                    Editor editor = EditorBL.EDITOR_LOGIN(model.nick_name);
                    if (editor == null)
                    {
                        ModelState.AddModelError("nick_name", "Üye Kodu Bulunamadı");
                        return View();

                    }
                    else
                    {
                        if ((editor.nick_name.Trim().ToUpper()) == (model.nick_name.Trim().ToUpper()))
                        {
                            if (editor.password == model.password)
                            {
                                Session["nick_name"] = editor.nick_name;// session
                                Session["id"] = editor.id;
                                Session["is_online"] = "true";
                                Session["role"] = editor.role;
                                Session["password"] = editor.password;
                                Session["profil_media"] = "null";

                                EditorBL.IS_ONLINE(model.nick_name, true);//is_online
                                string[] values = Request.Form.GetValues("remember_me");
                                if (bool.Parse(values[0]))
                                {
                                    HttpCookie EditorLogin = Request.Cookies.Get("EditorLogin");//httpcookies
                                    if (EditorLogin == null)
                                    {

                                        EditorLogin = new HttpCookie("Login");
                                        EditorLogin.Value = model.nick_name;
                                        EditorLogin.Expires = DateTime.Now.AddDays(15);
                                        Response.Cookies.Add(EditorLogin);
                                    }

                                }

                                if (editor.role == 1)
                                {
                                    return RedirectToAction("Admin", "xzgqhjk");
                                }
                                else
                                {
                                    return RedirectToAction("Editor", "xzgqhjk");
                                }

                            }
                            else
                            {
                                ModelState["password"].Errors.Clear();
                                ModelState["nick_name"].Errors.Clear();
                                ModelState.AddModelError("password", "Şifre Yanlış. Tekrar Deneyin");
                                return View();
                            }
                        }
                        else
                        {
                            ModelState["nick_name"].Errors.Clear();
                            ModelState["password"].Errors.Clear();
                            ModelState.AddModelError("nick_name", "Üye Kodu Bulunamadı");
                            return View();
                        }

                    }

                }
                else
                {
                    ModelState["nick_name"].Errors.Clear();
                    ModelState["password"].Errors.Clear();
                    ModelState.AddModelError("nick_name", "Üye Kodu ve Şifre Giriniz.");
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 3 });// Şifre hatası
            }
        }

        public ActionResult Admin()
        {
            try
            {
                if (Convert.ToInt32(Session["role"]) == 1)
                {//admin
                    return View();
                }
                else
                {
                    return RedirectToAction("Editor", "xzgqhjk");
                }
            }
            catch
            {
                return RedirectToAction("Index", "xzgqhjk");
            }
        }

        public ActionResult Editor()
        {
            return View();
        }

        public ActionResult ChangePasswordEditor()
        {
            return View();
        }
        public ActionResult ChangePasswordAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordEditor(ChangePassword ChangePassword)
        {
            try
            {
                if (!(Request.Form["New_password"] == Request.Form["confrim_password"]))
                {
                    string errorMessage = "Yeni Şifre Tekrarı Hatalı.";
                    ModelState["password"].Errors.Clear();
                    ModelState.AddModelError("password", errorMessage);
                    return View();
                }
                if (!(Request.Form["Old_password"] == Session["password"].ToString()))
                {
                    string errorMessage = "Şifre Yanlış.";
                    ModelState["password"].Errors.Clear();
                    ModelState.AddModelError("password", errorMessage);
                    return View();
                }

                int id = EditorBL.CHANGE_PASSWORD_EDITOR(Convert.ToInt32(Session["id"]), Request.Form["New_password"]);
                string NoticeMessage = "Şifre Değiştirildi";
                Session["password"] = Request.Form["New_password"];
                ModelState["password"].Errors.Clear();
                ModelState.AddModelError("password", NoticeMessage);

                return View(ChangePassword);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 23 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordAdmin(ChangePassword ChangePassword)
        {
            try
            {
                if (!(Request.Form["New_password"] == Request.Form["confrim_password"]))
                {
                    string errorMessage = "Yeni Şifre Tekrarı Hatalı.";
                    ModelState["password"].Errors.Clear();
                    ModelState.AddModelError("password", errorMessage);
                    return View();
                }
                if (!(Request.Form["Old_password"] == Session["password"].ToString()))
                {
                    string errorMessage = "Şifre Yanlış.";
                    ModelState["password"].Errors.Clear();
                    ModelState.AddModelError("password", errorMessage);
                    return View();
                }

                int id = EditorBL.CHANGE_PASSWORD_EDITOR(Convert.ToInt32(Session["id"]), Request.Form["New_password"]);
                string NoticeMessage = "Şifre Değiştirildi";
                Session["password"] = Request.Form["New_password"];
                ModelState["password"].Errors.Clear();
                ModelState.AddModelError("password", NoticeMessage);

                return View(ChangePassword);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 23 });
            }
        }

        public ActionResult imageEditor()
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult imageEditor(ShareObject obje, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentType.ToLower() == "image/jpg" || file.ContentType.ToLower() == "image/jpeg" || file.ContentType.ToLower() == "image/pjpeg" || file.ContentType.ToLower() == "image/gif" || file.ContentType.ToLower() == "image/x-png" || file.ContentType.ToLower() == "image/png")
                {
                    if (file.ContentLength < Convert.ToDouble(50000000))
                    {
                        string file_name = Path.GetFileName(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        string media_Url = "/Photo/Image/" + HELPER.PHOTO_SRC("Image", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                        ShareObject obje_ = new Models.ShareObject();

                        obje_.member_id = Convert.ToInt32(Session["id"]);
                        obje_.image_url = media_Url;
                        obje_.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.image.ToString()));
                        obje_.date = DateTime.Now;
                        obje_.sex = obje.sex;
                        obje_.status = Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString()));
                        obje_.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                        obje_.text = obje.text;
                        obje_.confrim = obje.confrim;
                        obje_.LATITUDE = "";
                        obje_.LONGITUDE = "";

                        if (extension == ".gif")
                        {
                            file.SaveAs(Server.MapPath(media_Url));
                        }
                        else
                        {
                            HELPER.ResizeStream(900, file.InputStream, Server.MapPath(media_Url));
                        }

                        if (System.IO.File.Exists(Server.MapPath(media_Url)))
                        {
                            ShareObjectBL.SHARE_OBJECT_INSERT(obje_);

                            return RedirectToAction("imageEditor", "xzgqhjk");
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
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }

        }
        public ActionResult imagePanoramicEditor()
        {
            ShareObject obje = new Models.ShareObject();
            return View(obje);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult İmagePanoramicEditor(ShareObject obje, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentType.ToLower() == "image/jpg" || file.ContentType.ToLower() == "image/jpeg" || file.ContentType.ToLower() == "image/pjpeg" || file.ContentType.ToLower() == "image/gif" || file.ContentType.ToLower() == "image/x-png" || file.ContentType.ToLower() == "image/png")
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
                        obje_.sex = obje.sex;
                        obje_.status = Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString()));
                        obje_.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                        obje_.text = obje.text;
                        obje_.confrim = obje.confrim;
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

                            return RedirectToAction("imagePanoramicEditor", "xzgqhjk");
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
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }
        //
        public ActionResult imagePanoramic360Editor(ShareObject obje)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult imagePanoramic360Editor(ShareObject obje, HttpPostedFileBase file)
        {
            try
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
                        obje_.sex = obje.sex;
                        obje_.status = Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString()));
                        obje_.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                        obje_.text = obje.text;
                        obje_.confrim = obje.confrim;
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

                            return RedirectToAction("imagePanoramic360Editor", "xzgqhjk");
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
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }

        public ActionResult videoEditor()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult videoEditor(ShareObject obje, HttpPostedFileBase file, string ShareObjectVideoText, string sex, string confrim)
        {
            try
            {
                if (file.ContentType.ToLower() == "video/mp4" || file.ContentType.ToLower() == "video/webm" || file.ContentType.ToLower() == "video/ogg" || file.ContentType.ToLower() == "video/quicktime")
                {
                    if (file.ContentLength < Convert.ToDouble(50000000))
                    {
                        string file_name = Path.GetFileName(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        string media_Url = "/Photo/Video/" + HELPER.PHOTO_SRC("Video", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                        obje = new Models.ShareObject();

                        obje.member_id = Convert.ToInt32(Session["id"]);
                        obje.image_url = media_Url;
                        obje.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.video.ToString()));
                        obje.date = DateTime.Now;
                        obje.sex = sex;
                        obje.status = Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString()));
                        obje.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                        obje.text = ShareObjectVideoText.ToString();
                        obje.confrim = Convert.ToInt32(confrim);
                        obje.LATITUDE = "";
                        obje.LONGITUDE = "";

                        file.SaveAs(Server.MapPath(media_Url));

                        if (System.IO.File.Exists(Server.MapPath(media_Url)))
                        {
                            ShareObjectBL.SHARE_OBJECT_INSERT(obje);

                            return RedirectToAction("videoEditor", "xzgqhjk");
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
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }

        public ActionResult audioEditor()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult audioEditor(ShareObject obje, HttpPostedFileBase file, string ShareObjectAudioText, string sex, string confrim)
        {
            try
            {
                if (file.ContentType.ToLower() == "audio/mpeg" || file.ContentType.ToLower() == "audio/ogg" || file.ContentType.ToLower() == "audio/mp3" || file.ContentType.ToLower() == "audio/wav")
                {
                    if (file.ContentLength < Convert.ToDouble(50000000))
                    {
                        string file_name = Path.GetFileName(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        string media_Url = "/Photo/Audio/" + HELPER.PHOTO_SRC("Audio", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                        obje = new Models.ShareObject();

                        obje.member_id = Convert.ToInt32(Session["id"]);
                        obje.image_url = media_Url;
                        obje.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.audio.ToString()));
                        obje.date = DateTime.Now;
                        obje.sex = sex;
                        obje.status = Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString()));
                        obje.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                        obje.text = ShareObjectAudioText.ToString();
                        obje.confrim = Convert.ToInt32(confrim);
                        obje.LATITUDE = "";
                        obje.LONGITUDE = "";


                        file.SaveAs(Server.MapPath(media_Url));

                        if (System.IO.File.Exists(Server.MapPath(media_Url)))
                        {
                            ShareObjectBL.SHARE_OBJECT_INSERT(obje);

                            return RedirectToAction("audioEditor", "xzgqhjk");
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
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }

        public ActionResult LocationEditor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocationEditor(Location location, string latitude, string longitude, string mapsImgSrc, string text, string sex, string confrim)
        {

            try
            {
                if (latitude != "" || longitude != "" || mapsImgSrc != "")
                {
                    location = new Location();
                    location.member_id = Convert.ToInt32(Session["id"]);
                    location.LATITUDE = latitude;
                    location.LONGITUDE = longitude;
                    location.sex = sex;
                    location.status = (int)Enum.Parse(typeof(status), status.editor.ToString());
                    location.text = text;

                    if (!string.IsNullOrEmpty(mapsImgSrc))
                    {
                        string valueBase = mapsImgSrc;
                        string valueBaseString = valueBase.Replace("data:image/png;base64,", "");

                        byte[] image64 = Convert.FromBase64String(valueBaseString);

                        string profil_media = "/Photo/Location/" + HELPER.PHOTO_SRC("Location", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + ".png";

                        string fullPath = Server.MapPath(profil_media);

                        // DIREKT BYTE KAYDET

                        System.IO.File.WriteAllBytes(fullPath, image64);

                        location.image_url = profil_media;
                    }
                    else
                    {
                        return RedirectToAction("Index", "xzgqhjk");
                    }


                    location.date = DateTime.Now;
                    location.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.location.ToString())); //Enum.GetName(typeof(sort), 0);
                    location.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                    location.confrim = Convert.ToInt32(confrim);
                    int location_id = LocationBL.LOCATION_INSERT(location);
                    if (location_id != 0)
                    {
                        return RedirectToAction("LocationEditor", "xzgqhjk");
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
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 21 });
            }
        }

        public ActionResult SurveyEditor()
        {
            try
            {
                if (Request.QueryString["surveyEvent"] == null)
                {
                    List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_EDITOR_ALL();
                    return View(ShareStreemList);
                    //return View(); 
                }
                else
                {
                    if (Request.QueryString["surveyEvent"] == "delete")
                    {
                        int result_ = SurveyBL.SURVEY_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                        List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_EDITOR_ALL();
                        return View(ShareStreemList);
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SurveyEditor(string SurveyText, string sex, string confrim)
        {
            try
            {
                if (Request.QueryString["surveyEvent"] == null)
                {
                    List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_EDITOR_ALL();
                    return View(ShareStreemList);
                }
                else
                {
                    if (Request.QueryString["surveyEvent"] == "SurveyInsert")
                    {
                        if (!string.IsNullOrEmpty(SurveyText))
                        {
                            ShareObject obje_ = new Models.ShareObject();

                            obje_.member_id = Convert.ToInt32(Session["id"]);
                            obje_.text = SurveyText;
                            obje_.image_url = "";
                            obje_.sex = sex;
                            obje_.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.survey.ToString()));
                            obje_.date = DateTime.Now;
                            obje_.confrim = Convert.ToInt32(confrim);
                            obje_.status = Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString()));
                            obje_.active = Convert.ToInt32(Enum.Parse(typeof(active), active.passive.ToString()));

                            SurveyBL.SURVEY_INSERT(obje_);
                            List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_EDITOR_ALL();
                            return View(ShareStreemList);
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 26 });
                        }
                    }
                    else if (Request.QueryString["eventAction"] == "delete")
                    {
                        int result_ = SurveyBL.SURVEY_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                        List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_EDITOR_ALL();
                        return View(ShareStreemList);
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }

                }


            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult EditorSurveyDetails(int obje_id)
        {
            try
            {

                if (Request.QueryString["surveyEvent"] == null)
                {
                    shareStreem survey = SurveyBL.SURVEY_EDITOR_GET_BY_ID(obje_id);
                    ViewBag.survey = survey;
                    return View(survey);
                }
                else
                {
                    if (Request.QueryString["surveyEvent"] == "update")
                    {
                        int result = SurveyBL.EDITOR_SURVEY_SHARE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString())));
                        if (result != 0)
                        {
                            shareStreem survey = SurveyBL.SURVEY_EDITOR_GET_BY_ID(obje_id);
                            ViewBag.survey = survey;
                            ViewBag.result = result;
                            ViewBag.obje_id = survey.id;
                            return View(survey);
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 27 });
                        }
                    }
                    else
                    {
                        if (Request.QueryString["surveyEvent"] == "delete")
                        {
                            int result_ = SurveyBL.EDITOR_SURVEY_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString())));
                            return RedirectToAction("SurveyEditor", "xzgqhjk");
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

        public ActionResult EditorSurveyEditQeustion(int obje_id)
        {
            try
            {
                if (Request.QueryString["surveyQuestionEvent"] == null)
                {
                    List<SurveyQuestion> questionList = SurveyBL.EDITOR_SURVEY_QUESTION(obje_id);
                    ViewBag.obje_id = obje_id;
                    return View(questionList);
                }
                else if (Request.QueryString["surveyQuestionEvent"] == "delete")
                {
                    int result = SurveyBL.EDITOR_SURVEY_QUESTION_DELETE(Convert.ToInt32(Request.QueryString["question_id"]), Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString())));
                    List<SurveyQuestion> questionList = SurveyBL.EDITOR_SURVEY_QUESTION(obje_id);
                    ViewBag.obje_id = obje_id;
                    return View(questionList);
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 0 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditorSurveyEditQeustion(int obje_id, string eventAction, string Question, string A_option, string B_option, string C_option, string D_option)
        {
            try
            {
                if (Request.QueryString["surveyQuestionEvent"] == "add")
                {
                    SurveyQuestion question = new SurveyQuestion();
                    question.questionText = Question;
                    //string[] liste= new string[4];
                    ArrayList liste = new ArrayList();
                    liste.Add(A_option);
                    liste.Add(B_option);
                    liste.Add(C_option);
                    liste.Add(D_option);
                    for (int i = 0; i < liste.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                question.A_text = A_option;
                                break;
                            case 1:
                                question.B_text = B_option;
                                break;
                            case 2:
                                question.C_text = C_option;
                                break;
                            case 3:
                                question.D_text = D_option;
                                break;
                            default:
                                break;
                        }
                    }
                    question.confrim = 16;
                    question.survey_id = obje_id;
                    SurveyBL.SURVEY_QUESTION_INSERT(question);

                    List<SurveyQuestion> questionList = SurveyBL.EDITOR_SURVEY_QUESTION(obje_id);
                    ViewBag.obje_id = obje_id;
                    return View(questionList);
                }
                else
                {
                    List<SurveyQuestion> questionList = SurveyBL.EDITOR_SURVEY_QUESTION(obje_id);
                    ViewBag.obje_id = obje_id;
                    return View(questionList);
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }


        public ActionResult EditorShareObjeList(int sort)
        {
            try
            {
                List<shareEditorObje> EditorShareObjeList = EditorBL.EDITOR_SHARE_OBJE_LIST(0, "", 0, 0, sort);
                ViewBag.sex = "";
                ViewBag.editor_id = 0;
                ViewBag.confrim = 0;
                ViewBag.page = 0;
                ViewBag.sort = sort;
                if (EditorShareObjeList.Count > 0)
                {
                    ViewBag.EditorShareObjeList = EditorShareObjeList;
                    shareEditorObje obje = EditorShareObjeList[0];
                    ViewBag.count = obje.count_List;
                    ViewBag.role = obje.role;
                    ViewBag.Use_id = Convert.ToInt32(Session["id"]);
                    ViewBag.EditorShareObjeList = EditorShareObjeList;
                    return View(EditorShareObjeList);
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditorShareObjeList(string editor_id, string sex, int confrim, int page, int sort)
        {
            try
            {
                if (Request.QueryString["EditorEvent"] == "selection")
                {
                    List<shareEditorObje> EditorShareObjeList = EditorBL.EDITOR_SHARE_OBJE_LIST(Convert.ToInt32(editor_id), sex, confrim, page, sort);
                    ViewBag.sex = sex;
                    ViewBag.editor_id = editor_id;
                    ViewBag.confrim = confrim;
                    ViewBag.page = page;
                    ViewBag.sort = sort;
                    if (EditorShareObjeList.Count > 0)
                    {
                        shareEditorObje obje = EditorShareObjeList[0];
                        ViewBag.count = obje.count_List;
                        ViewBag.EditorShareObjeList = EditorShareObjeList;
                        return View(EditorShareObjeList);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 29 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        public ActionResult imageEditorDetails(int obje_id)
        {
            try
            {

                if (Request.QueryString["EditorEvent"] == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje_id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 30 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult imageEditorDetails(ShareObject obje, string EditorEvent, int obje_id, HttpPostedFileBase file)
        {
            try
            {
                if (EditorEvent == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje.id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    if (EditorEvent == "Delete")
                    {
                        if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                        {
                            int deleted_id = ShareObjectBL.SHARE_OBJECT_DELETE(obje.id, Convert.ToInt32(Session["id"]));
                            return RedirectToAction("imageEditor", "xzgqhjk", new { obje_id = obje.id });
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }

                    }
                    else
                    {
                        if (EditorEvent == "Update")
                        {
                            int sa = Convert.ToInt32(Session["role"]);
                            int sb = Convert.ToInt32(Session["id"]);
                            int sc = obje.member_id;
                            if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                            {
                                if (file != null)
                                {
                                    if (file.ContentType.ToLower() == "image/jpg" || file.ContentType.ToLower() == "image/jpeg" || file.ContentType.ToLower() == "image/pjpeg" || file.ContentType.ToLower() == "image/gif" || file.ContentType.ToLower() == "image/x-png" || file.ContentType.ToLower() == "image/png")
                                    {
                                        if (file.ContentLength < Convert.ToDouble(50000000))
                                        {
                                            string file_name = Path.GetFileName(file.FileName);
                                            string extension = Path.GetExtension(file.FileName);
                                            string media_Url = "/Photo/Image/" + HELPER.PHOTO_SRC("Image", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;

                                            file.SaveAs(Server.MapPath(media_Url));

                                            if (System.IO.File.Exists(Server.MapPath(media_Url)))
                                            {
                                                obje.image_url = media_Url;
                                            }
                                            else
                                            {
                                                return RedirectToAction("Uppss", "Error", new { P = 5 });
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    obje.image_url = obje.image_url;
                                }
                                string objeurl = obje.image_url;

                                ShareObjectBL.SHARE_OBJECT_UPDATE(obje);
                                return View(obje);
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 30 });
                            }

                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        public ActionResult imagePanoramicEditorDetails(int obje_id)
        {
            try
            {

                if (Request.QueryString["EditorEvent"] == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje_id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 30 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult imagePanoramicEditorDetails(ShareObject obje, string EditorEvent, int obje_id, HttpPostedFileBase file)
        {
            try
            {
                if (EditorEvent == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje.id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    if (EditorEvent == "Delete")
                    {
                        if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                        {
                            int deleted_id = ShareObjectBL.SHARE_OBJECT_DELETE(obje.id, Convert.ToInt32(Session["id"]));
                            return RedirectToAction("imagePanoramicEditorDetails", "xzgqhjk", new { obje_id = obje.id });
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }

                    }
                    else
                    {
                        if (EditorEvent == "Update")
                        {
                            int sa = Convert.ToInt32(Session["role"]);
                            int sb = Convert.ToInt32(Session["id"]);
                            int sc = obje.member_id;
                            int sd = obje_id;
                            if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                            {
                                if (file != null)
                                {
                                    if (file.ContentType.ToLower() == "image/jpg" || file.ContentType.ToLower() == "image/jpeg" || file.ContentType.ToLower() == "image/pjpeg" || file.ContentType.ToLower() == "image/gif" || file.ContentType.ToLower() == "image/x-png" || file.ContentType.ToLower() == "image/png")
                                    {
                                        if (file.ContentLength < Convert.ToDouble(50000000))
                                        {
                                            string file_name = Path.GetFileName(file.FileName);
                                            string extension = Path.GetExtension(file.FileName);
                                            string media_Url = "/Photo/Image180/" + HELPER.PHOTO_SRC("Image180", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;

                                            file.SaveAs(Server.MapPath(media_Url));

                                            if (System.IO.File.Exists(Server.MapPath(media_Url)))
                                            {
                                                obje.image_url = media_Url;
                                            }
                                            else
                                            {
                                                return RedirectToAction("Uppss", "Error", new { P = 5 });
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    obje.image_url = obje.image_url;
                                }
                                string objeurl = obje.image_url;

                                ShareObjectBL.SHARE_OBJECT_UPDATE(obje);
                                return View(obje);
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 30 });
                            }

                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        public ActionResult imagePanoramic360EditorDetails(int obje_id)
        {
            try
            {

                if (Request.QueryString["EditorEvent"] == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje_id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 30 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult imagePanoramic360EditorDetails(ShareObject obje, string EditorEvent, int obje_id, HttpPostedFileBase file)
        {
            try
            {
                if (EditorEvent == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje.id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    if (EditorEvent == "Delete")
                    {
                        if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                        {
                            int deleted_id = ShareObjectBL.SHARE_OBJECT_DELETE(obje.id, Convert.ToInt32(Session["id"]));
                            return RedirectToAction("imagePanoramic360EditorDetails", "xzgqhjk", new { obje_id = obje.id });
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }

                    }
                    else
                    {
                        if (EditorEvent == "Update")
                        {
                            if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                            {
                                if (file != null)
                                {
                                    if (file.ContentType.ToLower() == "image/jpg" || file.ContentType.ToLower() == "image/jpeg" || file.ContentType.ToLower() == "image/pjpeg" || file.ContentType.ToLower() == "image/gif" || file.ContentType.ToLower() == "image/x-png" || file.ContentType.ToLower() == "image/png")
                                    {
                                        if (file.ContentLength < Convert.ToDouble(50000000))
                                        {
                                            string file_name = Path.GetFileName(file.FileName);
                                            string extension = Path.GetExtension(file.FileName);
                                            string media_Url = "/Photo/Image360/" + HELPER.PHOTO_SRC("Image360", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;

                                            file.SaveAs(Server.MapPath(media_Url));

                                            if (System.IO.File.Exists(Server.MapPath(media_Url)))
                                            {
                                                obje.image_url = media_Url;
                                            }
                                            else
                                            {
                                                return RedirectToAction("Uppss", "Error", new { P = 5 });
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    obje.image_url = obje.image_url;
                                }
                                string objeurl = obje.image_url;

                                ShareObjectBL.SHARE_OBJECT_UPDATE(obje);
                                return View(obje);
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 30 });
                            }

                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        public ActionResult videoEditorDetails(int obje_id)
        {
            try
            {

                if (Request.QueryString["EditorEvent"] == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje_id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 30 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult videoEditorDetails(ShareObject obje, string EditorEvent, int obje_id, HttpPostedFileBase file)
        {
            try
            {
                if (EditorEvent == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje.id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    if (EditorEvent == "Delete")
                    {
                        if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                        {
                            int deleted_id = ShareObjectBL.SHARE_OBJECT_DELETE(obje.id, Convert.ToInt32(Session["id"]));
                            return RedirectToAction("videoEditorDetails", "xzgqhjk", new { obje_id = obje.id });
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }

                    }
                    else
                    {
                        if (EditorEvent == "Update")
                        {
                            if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                            {
                                if (file != null)
                                {
                                    if (file.ContentType.ToLower() == "video/mp4" || file.ContentType.ToLower() == "video/webm" || file.ContentType.ToLower() == "video/ogg" || file.ContentType.ToLower() == "video/quicktime")
                                    {
                                        if (file.ContentLength < Convert.ToDouble(50000000))
                                        {
                                            string file_name = Path.GetFileName(file.FileName);
                                            string extension = Path.GetExtension(file.FileName);
                                            string media_Url = "/Photo/Video/" + HELPER.PHOTO_SRC("Video", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;

                                            file.SaveAs(Server.MapPath(media_Url));

                                            if (System.IO.File.Exists(Server.MapPath(media_Url)))
                                            {
                                                obje.image_url = media_Url;
                                            }
                                            else
                                            {
                                                return RedirectToAction("Uppss", "Error", new { P = 5 });
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    obje.image_url = obje.image_url;
                                }
                                string objeurl = obje.image_url;

                                ShareObjectBL.SHARE_OBJECT_UPDATE(obje);
                                return View(obje);
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 30 });
                            }

                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        public ActionResult audioEditorDetails(int obje_id)
        {
            try
            {

                if (Request.QueryString["EditorEvent"] == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje_id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 30 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult audioEditorDetails(ShareObject obje, string EditorEvent, int obje_id, HttpPostedFileBase file)
        {
            try
            {
                if (EditorEvent == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje.id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    if (EditorEvent == "Delete")
                    {
                        if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                        {
                            int deleted_id = ShareObjectBL.SHARE_OBJECT_DELETE(obje.id, Convert.ToInt32(Session["id"]));
                            return RedirectToAction("audioEditorDetails", "xzgqhjk", new { obje_id = obje.id });
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }

                    }
                    else
                    {
                        if (EditorEvent == "Update")
                        {
                            if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                            {
                                if (file != null)
                                {
                                    if (file.ContentType.ToLower() == "audio/mpeg" || file.ContentType.ToLower() == "audio/ogg" || file.ContentType.ToLower() == "audio/mp3" || file.ContentType.ToLower() == "audio/wav")
                                    {
                                        if (file.ContentLength < Convert.ToDouble(50000000))
                                        {
                                            string file_name = Path.GetFileName(file.FileName);
                                            string extension = Path.GetExtension(file.FileName);
                                            string media_Url = "/Photo/Audio/" + HELPER.PHOTO_SRC("Audio", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;

                                            file.SaveAs(Server.MapPath(media_Url));

                                            if (System.IO.File.Exists(Server.MapPath(media_Url)))
                                            {
                                                obje.image_url = media_Url;
                                            }
                                            else
                                            {
                                                return RedirectToAction("Uppss", "Error", new { P = 5 });
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    obje.image_url = obje.image_url;
                                }
                                string objeurl = obje.image_url;

                                ShareObjectBL.SHARE_OBJECT_UPDATE(obje);
                                return View(obje);
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 30 });
                            }

                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        public ActionResult LocationEditorDetails(int obje_id)
        {
            try
            {

                if (Request.QueryString["EditorEvent"] == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje_id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 30 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocationEditorDetails(ShareObject obje, string EditorEvent, int obje_id, string latitude, string longitude, string mapsImgSrc)
        {
            try
            {
                if (EditorEvent == null)
                {
                    ShareObject shareObje = ShareObjectBL.SHARE_OBJECT_BY_ID(obje.id);
                    int editor_id = Convert.ToInt32(Session["id"]);

                    return View(shareObje);
                }
                else
                {
                    if (EditorEvent == "Delete")
                    {
                        if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                        {
                            int deleted_id = ShareObjectBL.SHARE_OBJECT_DELETE(obje.id, Convert.ToInt32(Session["id"]));
                            return RedirectToAction("LocationEditor", "xzgqhjk", new { obje_id = obje.id });
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }

                    }
                    else
                    {
                        if (EditorEvent == "Update")
                        {
                            if (Convert.ToInt32(Session["role"]) == 1 || Convert.ToInt32(Session["id"]) == obje.member_id)
                            {
                                obje.image_url = mapsImgSrc;
                                obje.LONGITUDE = longitude;
                                obje.LATITUDE = latitude;
                                ShareObjectBL.SHARE_OBJECT_UPDATE(obje);

                                return View(obje);
                            }
                            else
                            {
                                return RedirectToAction("Uppss", "Error", new { P = 30 });
                            }

                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 30 });
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 29 });
            }
        }

        public ActionResult AdminView()
        {
            try
            {
                if (Convert.ToInt32(Session["role"]) == 1)
                {//admin
                    AdminView view_ = EditorBL.ADMIN_VIEW_PROC();
                    return View(view_);
                }
                else
                {
                    return RedirectToAction("Editor", "xzgqhjk");
                }
            }
            catch
            {
                return RedirectToAction("Index", "xzgqhjk");
            }
        }

        public ActionResult ComplaintShares(int page)
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["role"].ToString()))
                {
                    if (Request.QueryString["Event"] == null)
                    {
                        List<EditorComplaint> ComplaintList = EditorBL.COMPLAINED_SHARES(page);
                        return View(ComplaintList);
                    }
                    else if (Request.QueryString["Event"] == "memeberDelete")
                    {
                        EditorBL.EDITOR_MEMBER_DELETE(Convert.ToInt32(Request.QueryString["member_id"]));
                        List<EditorComplaint> ComplaintList = EditorBL.COMPLAINED_SHARES(page);
                        return View(ComplaintList);
                    }
                    else if (Request.QueryString["Event"] == "ShareDelete")
                    {
                        EditorBL.EDITOR_COMP_SHARE_OBJECT_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                        List<EditorComplaint> ComplaintList = EditorBL.COMPLAINED_SHARES(page);
                        return View(ComplaintList);
                    }
                    else if (Request.QueryString["Event"] == "ShareUpdate")
                    {
                        EditorBL.EDITOR_COMP_SHARE_OBJECT_UPDATE(Convert.ToInt32(Request.QueryString["obje_id"]));
                        List<EditorComplaint> ComplaintList = EditorBL.COMPLAINED_SHARES(page);
                        return View(ComplaintList);
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 0 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }

        }
        public ActionResult Comp()
        {
            return View();
        }

        public ActionResult EditorShareDetails(int obje_id, int sort, int whom_id, string Text)
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["role"].ToString()))
                {
                    if (Request.QueryString["shareEvent"] == null)
                    {
                        shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(obje_id);
                        ViewBag.obje_id = obje_id;
                        ViewBag.sort = sort;
                        return View(shareObje);
                    }
                    else if (Request.QueryString["shareEvent"] == "delete")
                    {

                        EditorBL.EDITOR_COMP_SHARE_OBJECT_DELETE(obje_id, Convert.ToInt32(Session["id"]));
                        ViewBag.obje_id = obje_id;
                        ViewBag.state = "silindi";
                        return RedirectToAction("Comp", "xzgqhjk");
                    }
                    else if (Request.QueryString["shareEvent"] == "Update")
                    {
                        EditorBL.EDITOR_COMP_SHARE_OBJECT_UPDATE(obje_id);
                        ViewBag.obje_id = obje_id;
                        ViewBag.state = "silindi";
                        return RedirectToAction("Comp", "xzgqhjk");
                    }
                    else if (Request.QueryString["shareEvent"] == "ShareShare")
                    {
                        ShareObject obje = ShareObjectBL.SHARE_OBJECT_BY_ID(Convert.ToInt32(Request.QueryString["obje_id"]));
                        ShareObject newObje = new Models.ShareObject();
                        newObje.member_id = Convert.ToInt32(Session["id"]);
                        newObje.sex = obje.sex;
                        newObje.text = Text;
                        newObje.image_url = obje.image_url;
                        newObje.sort = obje.sort;
                        newObje.date = DateTime.Now;
                        newObje.confrim = Convert.ToInt32(Enum.Parse(typeof(confrim), confrim.ShareObject.ToString()));
                        newObje.status = Convert.ToInt32(Enum.Parse(typeof(status), status.editor.ToString()));
                        newObje.LATITUDE = obje.LATITUDE;
                        newObje.LONGITUDE = obje.LONGITUDE;
                        newObje.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));

                        int result = ShareObjectBL.SHARE_OBJECT_INSERT(newObje);
                        EditorBL.EDITOR_COMP_SHARE_OBJECT_UPDATE(Convert.ToInt32(Request.QueryString["obje_id"]));
                        shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(obje_id);
                        ViewBag.obje_id = obje_id;
                        ViewBag.sort = sort;
                        return View(shareObje);
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 0 });
                }                
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 25 });
            }
        }

        public ActionResult EditorCompSurveyDetails(int obje_id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["role"].ToString()))
                {
                    if (Request.QueryString["surveyQuestionEvent"] == null)
                    {
                        List<SurveyQuestion> questionList = EditorBL.EDITOR_COMP_SURVEY_QUESTION(obje_id);
                        ViewBag.obje_id = obje_id;
                        return View(questionList);
                    }
                    else if (Request.QueryString["surveyQuestionEvent"] == "delete")
                    {
                        EditorBL.EDITOR_COMP_SHARE_OBJECT_DELETE(obje_id, Convert.ToInt32(Session["id"]));
                        ViewBag.obje_id = obje_id;
                        ViewBag.state = "silindi";
                        return RedirectToAction("Comp", "xzgqhjk");
                    }
                    else if (Request.QueryString["surveyQuestionEvent"] == "Update")
                    {
                        EditorBL.EDITOR_COMP_SHARE_OBJECT_UPDATE(obje_id);
                        ViewBag.obje_id = obje_id;
                        return RedirectToAction("Comp", "xzgqhjk");
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 0 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult EditorSharePage(int page,string Text)
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["role"].ToString()))
                {
                    if (Request.QueryString["Event"] == null)
                    {
                        List<shareStreem> ShareList = EditorBL.EDITOR_SHARE_PAGE(page);
                        return View(ShareList);
                    }
                    else if (Request.QueryString["Event"] == "ShareUpdate")
                    {
                        EditorBL.EDITOR_COMP_SHARE_OBJECT_UPDATE(Convert.ToInt32(Request.QueryString["obje_id"]));
                        List<shareStreem> ShareList = EditorBL.EDITOR_SHARE_PAGE(page);
                        return View(ShareList);
                    }                    
                    else if (Request.QueryString["Event"] == "ShareDelete")
                    {
                        EditorBL.EDITOR_COMP_SHARE_OBJECT_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                        List<shareStreem> ShareList = EditorBL.EDITOR_SHARE_PAGE(page);
                        return View(ShareList);
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 47 });
                    }
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 47 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 47 });
            }
        }

        public ActionResult CleanMedia()
        {
            var service = new MediaCleanupService();
            service.Run();

            return Content("Temizlik tamamlandı");
        }
    } 
}