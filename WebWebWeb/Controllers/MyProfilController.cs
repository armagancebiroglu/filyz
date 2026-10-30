using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebWebWeb.BL;
using WebWebWeb.Models;

namespace WebWebWeb.Controllers
{
    public class MyProfilController : Controller
    {
        // GET: MyProfil
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult myProfil()
        {
            //üyenin tüm bilgilerini her zaman sadece Session["nick_name"] ve Session["id"]  baz alınarak değerlendirilecek.
            //tüm myProfil bilgilerini ve partialview ler burada olacak sayfanın tam görüntüsü bu
            //profilphoto sessiondan
            try
            {
                string memberCode = Session["nick_name"].ToString();
                ViewBag.memberCode = memberCode;
            }
            catch
            {

            }
            return View();
        }

        public ActionResult textProfil()
        {
            MemberFullCard memberfullcard = new MemberFullCard();
           
            //string.IsNullOrEmpty(Session["nick_name"] as string)
            if (!string.IsNullOrEmpty((string)Session["nick_name"]))
            {
                string nick_name = Session["nick_name"].ToString();
                memberfullcard = MemberProfilBL.MEMBER_FULL_SELECT_BY_CODE(nick_name);

                //Üye kodu 
                ViewBag.p = nick_name;
                #region//Fiziksel Görünüş ve durumu ve tesettür

                ViewBag.p1 = memberfullcard.age.ToShortDateString() + " Tarihinde doğmuş. " + memberfullcard.eye_color + " gözlü ve saçları " + memberfullcard.hair_color.ToLower() + ".";
                if (memberfullcard.hicap == true || memberfullcard.sex == "kadın")
                {
                    ViewBag.p1 = ViewBag.p1 + " Tesettürlü bir hanım. Boyu " + memberfullcard.height.ToString() + " cm ve " + memberfullcard.kilo.ToString() + " kilo. ";
                }
                else if (memberfullcard.hicap == false && memberfullcard.sex == "Erkek")
                {
                    ViewBag.p1 = ViewBag.p1 + " Boyu " + memberfullcard.height.ToString() + " cm ve " + memberfullcard.kilo.ToString() + " kilo. ";
                }

                if (memberfullcard.disable == "Yok")
                {
                    ViewBag.p1 = ViewBag.p1 + " Herhangi bir fiziksel engeli yok.";
                }
                else if (memberfullcard.disable == "Görme Engelli")
                {
                    ViewBag.p1 = ViewBag.p1 + " Görme engeli bulunuyor.";
                }
                else if (memberfullcard.disable == "Bedensel Engelli")
                {
                    ViewBag.p1 = ViewBag.p1 + " Bedensel engeli bulunuyor.";
                }
                else if (memberfullcard.disable == "İşitme Engelli")
                {
                    ViewBag.p1 = ViewBag.p1 + " İşitme engeli bulunuyor.";
                }
                #endregion
                #region//bulunduğu şehir, memleketi ve yaşamak istediği şehir.
                if (memberfullcard.city != memberfullcard.country && memberfullcard.country == memberfullcard.city_wants_to_live)
                {
                    ViewBag.p2 = memberfullcard.city + "'da yaşıyor. Memleketi " + memberfullcard.country + " ve öncelikli olarak memleketinde yaşamak istiyor. ";
                }
                else
                if (memberfullcard.city == memberfullcard.country && memberfullcard.city != memberfullcard.city_wants_to_live)
                {
                    ViewBag.p2 = " Kendi memleketinde " + memberfullcard.country + "' da yaşıyor. Ama öncelikli olarak " + memberfullcard.city_wants_to_live + " 'da yaşamak istiyor. ";
                }
                else
                if (memberfullcard.city == memberfullcard.country && memberfullcard.city == memberfullcard.city_wants_to_live)
                {
                    ViewBag.p2 = " Aynı zamanda kendi memleketi de olan " + memberfullcard.city + "'da yaşıyor, ve öncelikli olarak hayatına yine burada devam etmek istiyor. ";
                }
                else
                if (memberfullcard.city != memberfullcard.country && memberfullcard.city == memberfullcard.city_wants_to_live)
                {
                    ViewBag.p2 = memberfullcard.city + "'da yaşıyor, ve öncelikli olarak hayatına yine burada devam etmek istiyor. Kendi memleketi " + memberfullcard.country + "...";
                }
                else
                {
                    ViewBag.p2 = memberfullcard.city + "'da yaşıyor. Memleketi " + memberfullcard.country + " ve öncelikli olarak " + memberfullcard.city_wants_to_live + " 'da yaşamak istiyor. ";
                }
                #endregion
                #region//medeni durumu                  

                if (memberfullcard.matiral_status == "Bekar" && memberfullcard.sex == "Erkek")
                {
                    ViewBag.P3 = " Bekar bir bey. ";
                }
                else
                if (memberfullcard.matiral_status == "Bekar" && memberfullcard.sex == "Kadın")
                {
                    ViewBag.P3 = " Bekar. ";
                }
                else
                if (memberfullcard.matiral_status == "Eşinden Ayrılmış" && memberfullcard.children == "Evet")
                {
                    ViewBag.P3 = " Daha önce evlenip ayrılmış, çocuğu var. ";
                }
                else
                if (memberfullcard.matiral_status == "Eşinden Ayrılmış" && memberfullcard.children == "Hayır")
                {
                    ViewBag.P3 = " Daha önce evlenip ayrılmış ama çocuğu yok. ";
                }
                else
                if (memberfullcard.matiral_status == "Eşi Vefat Etmiş" && memberfullcard.children == "Evet")
                {
                    ViewBag.P3 = " Daha önce evlenmiş ve maalesef eşi vefat etmiş. Çocuğu var. ";
                }
                else
                if (memberfullcard.matiral_status == "Eşi Vefat Etmiş" && memberfullcard.children == "Hayır")
                {
                    ViewBag.P3 = " Daha önce evlenmiş ve maalesef eşi vefat etmiş, çocukları yok ";
                }
                else { ViewBag.P3 = ""; }
                #endregion
                #region//Eğitim durumu
                if (memberfullcard.educational_status == "İlk Öğretim")
                {
                    ViewBag.p4 = " İlk öğretimini tamamlayıp hayata atılmış. ";
                }
                if (memberfullcard.educational_status == "Lise")
                {
                    ViewBag.p4 = " Eğitim hayatını liseye kadar sürdürmüş. ";
                }

                if (memberfullcard.educational_status == "Lisans")
                {
                    ViewBag.p4 = " Üniversite eğitimi almış. ";
                }

                if (memberfullcard.educational_status == "Yüksek Lisans")
                {
                    ViewBag.p4 = " Üniversite eğitimini Yüksek Lisansla tamamlamış. ";
                }
                #endregion
                #region Diyorki
                ViewBag.p5 = memberfullcard.my_story;
                #endregion
            }
            ViewBag.memberfullcard = memberfullcard;
            return View(memberfullcard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlbumInsert(albums album, HttpPostedFileBase file)
        {
            try
            {
                if (albumsBL.ALBUM_COUNT_GET_BY_MEMBER_ID(Convert.ToInt32(Session["id"])) < 5)
                {
                    if (Request.Files.Count > 0)
                    {
                        file = Request.Files[0];

                        if (album != null && file != null)
                        {
                            #region image

                            if (album.sort == 0)
                            {//image

                                if (file.ContentType.ToLower() == "image/jpg" ||
                                     file.ContentType.ToLower() == "image/jpeg" ||
                                     file.ContentType.ToLower() == "image/pjpeg" ||
                                     file.ContentType.ToLower() == "image/gif" ||
                                     file.ContentType.ToLower() == "image/x-png" ||
                                     file.ContentType.ToLower() == "image/png")
                                {
                                    if (file.ContentLength < Convert.ToDouble(5000000))
                                    {
                                        string file_name = Path.GetFileName(file.FileName);
                                        string extension = Path.GetExtension(file.FileName);
                                        album.member_id = Convert.ToInt32(Session["id"]);
                                        album.sort = 0;
                                        string media_Url = "/Photo/Image/" + HELPER.PHOTO_SRC("Image", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                                        album.media_url = media_Url;
                                        album.confrim = 0;

                                        file.SaveAs(Server.MapPath(media_Url));

                                        if (System.IO.File.Exists(Server.MapPath(media_Url)))
                                        {
                                            int Image_id = albumsBL.ALBUM_INSERT(album);
                                            album.id = Image_id;
                                            return RedirectToAction("MyScreen", "MyProfil", new { album_id = album.id, sort = album.sort });
                                        }
                                        else
                                        {
                                            return RedirectToAction("Uppss", "Error", new { P = 12 });
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
                            #endregion
                            #region image 180                           
                            if (album.sort == 1)
                            {//image180
                                if (file.ContentType.ToLower() == "image/jpg" ||
                                    file.ContentType.ToLower() == "image/jpeg" ||
                                    file.ContentType.ToLower() == "image/pjpeg" ||
                                    file.ContentType.ToLower() == "image/gif" ||
                                    file.ContentType.ToLower() == "image/x-png" ||
                                    file.ContentType.ToLower() == "image/png")
                                {
                                    if (file.ContentLength < Convert.ToDouble(5000000))
                                    {
                                        string file_name = Path.GetFileName(file.FileName);
                                        string extension = Path.GetExtension(file.FileName);
                                        album.member_id = Convert.ToInt32(Session["id"]);
                                        album.sort = 1;
                                        string mediaUrl = "/Photo/Image180/" + HELPER.PHOTO_SRC("Image180", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                                        album.media_url = mediaUrl;
                                        album.confrim = 0;


                                        file.SaveAs(Server.MapPath(mediaUrl));

                                        System.Threading.Thread.Sleep(1000);
                                        if (System.IO.File.Exists(Server.MapPath(mediaUrl)))
                                        {
                                            int Image_id = albumsBL.ALBUM_INSERT(album);
                                            album.id = Image_id;
                                            return RedirectToAction("MyScreen", "MyProfil", new { album_id = album.id, sort = album.sort });
                                        }
                                        else
                                        {
                                            return RedirectToAction("Uppss", "Error", new { P = 12 });
                                        }
                                        //int Image_id = albumsBL.ALBUM_INSERT(album);
                                        //album.id = Image_id;
                                        //return RedirectToAction("Screen", "MyProfil", new { album_id = album.id, sort = album.sort });
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
                            #endregion
                            #region image 360

                            if (album.sort == 2)
                            {//image360
                                if (file.ContentType.ToLower() == "image/jpg" ||
                                    file.ContentType.ToLower() == "image/jpeg" ||
                                    file.ContentType.ToLower() == "image/pjpeg" ||
                                    file.ContentType.ToLower() == "image/gif" ||
                                    file.ContentType.ToLower() == "image/x-png" ||
                                    file.ContentType.ToLower() == "image/png")
                                {
                                    if (file.ContentLength < Convert.ToDouble(5000000))
                                    {
                                        string file_name = Path.GetFileName(file.FileName);
                                        string extension = Path.GetExtension(file.FileName);
                                        album.member_id = Convert.ToInt32(Session["id"]);
                                        album.sort = 2;
                                        string mediaUrl = "/Photo/Image360/" + HELPER.PHOTO_SRC("Image360", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                                        album.media_url = mediaUrl;
                                        album.confrim = 0;


                                        file.SaveAs(Server.MapPath(mediaUrl));
                                        System.Threading.Thread.Sleep(1000);
                                        if (System.IO.File.Exists(Server.MapPath(mediaUrl)))
                                        {
                                            int Image_id = albumsBL.ALBUM_INSERT(album);
                                            album.id = Image_id;
                                            return RedirectToAction("MyScreen", "MyProfil", new { album_id = album.id, sort = album.sort });
                                        }
                                        else
                                        {
                                            return RedirectToAction("Uppss", "Error", new { P = 12 });
                                        }
                                        //int Image_id = albumsBL.ALBUM_INSERT(album);
                                        //album.id = Image_id;
                                        //return RedirectToAction("Screen", "MyProfil", new { album_id = album.id, sort = album.sort });
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
                            #endregion
                            #region video
                            if (album.sort == 4)
                            {//video//file.ContentType.ToLower() == "video/quicktime"||
                                if (DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35)
                                {

                                    if (file.ContentType.ToLower() == "video/mp4" || file.ContentType.ToLower() == "video/webm" || file.ContentType.ToLower() == "video/ogg" | file.ContentType.ToLower() == "video/quicktime")
                                    {
                                        if (file.ContentLength < Convert.ToDouble(5000000))
                                        {
                                            string file_name = Path.GetFileName(file.FileName);
                                            string extension = Path.GetExtension(file.FileName);
                                            album.member_id = Convert.ToInt32(Session["id"]);
                                            album.sort = 4;
                                            string mediaUrl = "/Photo/Video/" + HELPER.PHOTO_SRC("Video", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + extension;
                                            album.media_url = mediaUrl;
                                            album.confrim = 0;

                                            file.SaveAs(Server.MapPath(mediaUrl));

                                            System.Threading.Thread.Sleep(300);
                                            if (System.IO.File.Exists(Server.MapPath(mediaUrl)))
                                            {
                                                int Image_id = albumsBL.ALBUM_INSERT(album);
                                                album.id = Image_id;
                                                return RedirectToAction("MyScreen", "MyProfil", new { album_id = album.id, sort = album.sort });
                                            }
                                            else
                                            {
                                                return RedirectToAction("Uppss", "Error", new { P = 12 });
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
                                    return RedirectToAction("Uppss", "Error", new { P = 16 });
                                }
                            }
                            #endregion
                            #region audio

                            if (album.sort == 8)
                            {//audio
                                //TimeSpan days = DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"]));                                
                                if (DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35)
                                {
                                    if (file.ContentType.ToLower() == "audio/mpeg" ||
                                        file.ContentType.ToLower() == "audio/ogg" ||
                                        file.ContentType.ToLower() == "audio/mp3" ||
                                        file.ContentType.ToLower() == "audio/wav")
                                    {
                                        if (file.ContentLength < Convert.ToDouble(5000000))
                                        {
                                            string file_name = Path.GetFileName(file.FileName);
                                            string etxtension = Path.GetExtension(file.FileName);
                                            album.member_id = Convert.ToInt32(Session["id"]);
                                            album.sort = 8;
                                            string mediaUrl = "/Photo/Audio/" + HELPER.PHOTO_SRC("Audio", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + etxtension;
                                            album.media_url = mediaUrl;
                                            album.confrim = 0;


                                            file.SaveAs(Server.MapPath(mediaUrl));
                                            //"/Photo/Audio/") + HELPER.PHOTO_SRC("Audio", Server.MapPath("../FotoFileName.xml").ToString()) + "/" + DateTime.Now.ToFileTime() + file_name
                                            System.Threading.Thread.Sleep(1000);
                                            if (System.IO.File.Exists(Server.MapPath(mediaUrl)))
                                            {
                                                int Image_id = albumsBL.ALBUM_INSERT(album);
                                                album.id = Image_id;
                                                return RedirectToAction("MyScreen", "MyProfil", new { album_id = album.id, sort = album.sort });
                                            }
                                            else
                                            {
                                                return RedirectToAction("Uppss", "Error", new { P = 12 });
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
                                    return RedirectToAction("Uppss", "Error", new { P = 15 });
                                }
                            }

                            #endregion
                        }
                        return View();

                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 14 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 11 });
            }
        }

        [HttpGet]
        public ActionResult AlbumInsert()
        {

            return View();
        }

        public ActionResult MyScreen()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["album_id"]) && Request.QueryString["sort"] != null && !string.IsNullOrEmpty(Session["id"].ToString()))
                {
                    albums album = albumsBL.ALBUM_GET_BY_ID(Convert.ToInt32(Request.QueryString["album_id"]), Convert.ToInt32(Session["id"]), Convert.ToInt32(Session["id"]));
                    return View(album);
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
            return View();
        }

        public ActionResult AlbumDelete()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["album_id"]))
                {
                    albums album = albumsBL.ALBUM_GET_BY_ID(Convert.ToInt32(Request.QueryString["album_id"]), Convert.ToInt32(Session["id"]), Convert.ToInt32(Session["id"]));
                    if (System.IO.File.Exists(Server.MapPath(album.media_url)))
                    {
                        System.IO.File.Delete(Server.MapPath(album.media_url));
                        albumsBL.ALBUM_DELETE(album.id, Convert.ToInt32(Session["id"]));
                        return RedirectToAction("MyProfil", "MyProfil");
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 13 });
                    }

                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 12 });
            }
            return View();
        }

        [HttpGet]
        public ActionResult ProfilImageCrop()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfilImageCrop(FormCollection frm)
        {
            try
            {

                if (frm["profilhidden"] != "")
                {

                    string valueBase = frm["profilhidden"].ToString();
                    string valueBaseString = valueBase.Replace("data:image/png;base64,", String.Empty);
                    byte[] image64 = Convert.FromBase64String(valueBaseString);
                    MemoryStream ms = new MemoryStream(image64);
                    //Image img = Image.FromStream(ms);
                    //string profil_media = Server.MapPath("/Media/images/" + "/" + DateTime.Now.ToFileTime() + ".png");

                    /////
                    // 🧹 Eski foto sil
                    MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_ID(Convert.ToInt32(Session["id"]));
                    string oldPhotoPath = member.profil_media.ToString();
                    if (!string.IsNullOrEmpty(oldPhotoPath))
                    {
                        string fullOldPath = Server.MapPath(oldPhotoPath);
                        if (System.IO.File.Exists(fullOldPath))
                        {
                            System.IO.File.Delete(fullOldPath);
                        }
                    }
                    /////
                    string profil_media = "/Media/images/tr_" + DateTime.Now.ToFileTime() + ".png";
                    using (var img = Image.FromStream(ms))
                    {
                        img.Save(Server.MapPath(profil_media), ImageFormat.Png);
                        //img.Save(filename: Server.MapPath(profil_media), format: ImageFormat.Png);
                    }
                   
                    int id = MemberProfilBL.MEMBER_PRFIL_IMAGE_UPDATE(Convert.ToInt32(Session["id"]), profil_media);
                    if (id == 1)
                    {
                        //Session["profil_media"] = frm["profilhidden"].ToString();
                        Session["profil_media"] = profil_media;
                    }
                    return RedirectToAction(Url.Action("myProfil", "MyProfil"));
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 17 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 18 });

            }
        }

        public ActionResult AlbumGet(int member_id)
        {
            try
            {
                List<albums> albums = albumsBL.ALBUM_GET_ALL(member_id,Convert.ToInt32(Session["id"]));
                //bu method kim kimin albümüne bakmak istiyor surgusudur. kendi albümüne bakacak o yüzden sessiondan değer aldım SQL de hemcinsine bakmaması için kontrol var                
                return PartialView(albums);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 19 });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword ChangePassword)
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

                int id = MemberProfilBL.CHANGE_PASSWORD(Convert.ToInt32(Session["id"]), Request.Form["New_password"]);
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

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateMember()
        {
            try
            {
                MemberProfil memberProfil = null;
                if (!string.IsNullOrEmpty(Session["nick_name"].ToString()))//Url.Action("Info", "Default"); //(!string.IsNullOrEmpty(Request.QueryString["MC"]))
                {
                    memberProfil = MemberProfilBL.MEMBER_PROFIL_GET_BY_NICK_NAME(Session["nick_name"].ToString());
                    //return View(memberProfil);
                }
                else
                {
                    //Url.Action("Info","Default");
                    return RedirectToAction("Info", "Default");
                }
                return View(memberProfil);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 4 });//Update ilk görüntülenirken ki hata durumu
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMember(MemberProfil model, FormCollection frm)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.matiral_status))
                {
                    if (model.matiral_status.ToString() == "Eşinden Ayrılmış" || model.matiral_status.ToString() == "Eşi Vefat Etmiş")
                    {
                        if (model.children == null)
                        {
                            ModelState.AddModelError("children", "Eşinizden ayrılmış ve ya eşiniz vefat etmiş ise çocuğunuzun olup olmadığını belirtiniz.");
                            //return View();
                        }
                    }
                    else { model.children = ""; ModelState["children"].Errors.Clear(); }
                }

                if (((DateTime)(model.age)).Year >= 1900)
                {
                    if (((int)(DateTime.Now.Year)) - ((int)(((DateTime)(model.age)).Year)) < 18)
                    {
                        ModelState.AddModelError("age", "Yaşınız 18 den küçük olamaz");
                        // return View();
                    }
                }
                else
                {
                    model.age = DateTime.Parse(frm["age"].ToString());// bu olmuyor
                }

                #region Fiziksel engel
                if (model.disable == null)
                {
                    if (Request.Form["disableSelect"] == "Evet")
                    {
                        ModelState.AddModelError("disable", "Fiziksel engelinizin olduğunu belirttiniz ama engelinizin ne olduğunu belirtmediniz.");
                        return View();
                    }
                    if (Request.Form["disableSelect"] == "Hayır")
                    {
                        model.disable = "Yok";
                        ModelState["disable"].Errors.Clear();
                    }
                    else if (Request.Form["disableSelect"] == null)
                    {
                        ModelState.AddModelError("disable", "Fiziksel engel durumunuzu seçmediniz.");
                        return View();
                    }
                }
                #endregion

                if (HELPER.CHECK_FORBIDDEN(model.my_story))
                {
                    model.status = (int)Enum.Parse(typeof(status), "in_review");
                }
                else
                {
                    //yasaklı kelime kullanan
                    model.status = (int)Enum.Parse(typeof(status), "forbiddenMember");
                }

                int id = MemberProfilBL.MEMBER_PROFIL_UPDATE(model);

                if (id != 0)
                {
                    //Session["is_online"] = "true";//daha önceden durumunu offline yaptıysa öyle kalsın zaten belki görünmek istemiyor.Öncelikli olarak Online olanların paylaşım ve arama sonuçları gösterilecek böyle düşün.
                    //Url.Action("MyProfil", "MyProfil");
                    Session["nick_name"] = model.nick_name;// session a kod eklendi is_online true oldu. httpCookie girişte sorulup değer verilecek.
                    Session["id"] = model.id;
                    Session["is_online"] = model.is_online.ToString();
                    Session["payment_date"] = model.payment_date.ToString();
                    Session["profil_media"] = model.profil_media;
                    Session["password"] = model.password;
                    //Session["sex"] = model.sex.ToString();giriş yaptığı cinsiyeti ile devam edecek cinsiyetini değiştiremez
                    Session["status"] = Enum.GetName(typeof(status), Convert.ToInt32(model.status));

                    return RedirectToAction("myProfil", "MyProfil");
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 4 });
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 4 });//update yapılamadı
            }


            // return View(model);// myProfil sayfasına yönlendiğinden bunu dönmeye gerek yok try/catch içinde birinden birinde dönmeliydim.

        }
        public ActionResult ShareStreemMy(int page)
        {
            try
            {
                int id = Convert.ToInt32(Session["id"]);
                string sex = Session["sex"].ToString();
                List<shareStreem> ShareStreemList = Share_MemberBL.SHARE_STREEM_MY_ALL(id, page);
                return PartialView(ShareStreemList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 23 });
            }
        }    
       
        public ActionResult NoticeEventHeartMyProfil(int obje_id, int sort,int noticeTableSort)
        {
            try
            {
                if (Request.QueryString["obje_id"] == null)
                {
                    int who_id = Convert.ToInt32(Session["id"]);

                    if (obje_id != 0 || sort != 0)
                    {
                        int result_ = EnjoyBL.ENJOY_MY_PROFIL_GETCOUNT_BY_ID(obje_id, sort,noticeTableSort);

                        ViewBag.result = result_;
                        ViewBag.obje_id = obje_id;
                        ViewBag.sort = sort;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }

                }
                else
                {
                    int who_id = Convert.ToInt32(Session["id"]);

                    if (obje_id != 0 || sort != 0)
                    {
                        int result_ = EnjoyBL.ENJOY_MY_PROFIL_GETCOUNT_BY_ID(obje_id, sort, noticeTableSort);

                        ViewBag.result = result_;
                        ViewBag.obje_id = obje_id;
                        ViewBag.sort = sort;
                        return View();
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
        
        public ActionResult CommentMyProfil(string text, int obje_id, int sort, int noticeTableSort)
        {
            try
            {
                if (Request.QueryString["eventAction"] == null)
                {
                    List<Comment> CommentList = CommentBL.COMMENT_MY_PROFIL_GET_BY_ID(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]));

                    List<MemberProfil> MemberProfilList = MemberProfilBL.MEMBER_PROFIL_GET_COMMENT_LIST(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]));


                    ViewBag.MemberProfilList = MemberProfilList;
                    ViewBag.CommentList = CommentList;
                    ViewBag.who_id = Convert.ToInt32(Session["id"]);
                    ViewBag.sort = sort;
                    ViewBag.obje_id = obje_id;
                    ViewBag.noticeTableSort = noticeTableSort;
                    return View();

                }
                else
                {
                    if (Request.QueryString["eventAction"] == "comment")
                    {
                        if (DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35)
                        {
                            Comment comment = new Comment();
                            comment.obje_id = obje_id;
                            comment.who_id = Convert.ToInt32(Session["id"]);
                            comment.whom_id = Convert.ToInt32(Request.QueryString["whom_id"]);
                            comment.noticeTableSort = noticeTableSort;
                            comment.sort = Convert.ToInt32(Request.QueryString["sort"]);
                            comment.comment_text = text;

                            bool result = CommentBL.COMMENT_INSERT(comment);

                            List<Comment> CommentList = CommentBL.COMMENT_MY_PROFIL_GET_BY_ID(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]));
                            List<MemberProfil> MemberProfilList = MemberProfilBL.MEMBER_PROFIL_GET_COMMENT_LIST(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]));

                            ViewBag.MemberProfilList = MemberProfilList;
                            ViewBag.CommentList = CommentList;
                            ViewBag.who_id = Convert.ToInt32(Session["id"]);
                            ViewBag.sort = sort;
                            ViewBag.obje_id = obje_id;
                            ViewBag.noticeTableSort = noticeTableSort;

                           
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
                        {
                            bool result = CommentBL.COMMENT_DELETE(Convert.ToInt32(Request.QueryString["comment_id"]), Convert.ToInt32(Request.QueryString["obje_id"]), noticeTableSort, Convert.ToInt32(Session["id"]));

                            List<Comment> CommentList = CommentBL.COMMENT_MY_PROFIL_GET_BY_ID(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]));

                            List<MemberProfil> MemberProfilList = MemberProfilBL.MEMBER_PROFIL_GET_COMMENT_LIST(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]));

                            ViewBag.MemberProfilList = MemberProfilList;
                            ViewBag.CommentList = CommentList;
                            ViewBag.who_id = Convert.ToInt32(Session["id"]);
                            ViewBag.sort = sort;
                            ViewBag.obje_id = obje_id;
                            ViewBag.noticeTableSort = noticeTableSort;
                            return View();
                        }
                        else
                        {
                            if (Request.QueryString["eventAction"] == "deleteAll")
                            {
                                bool result = CommentBL.COMMENT_DELETE_ALL_BY_MEMBER(Convert.ToInt32(Request.QueryString["who_id"]), Convert.ToInt32(Request.QueryString["whom_id"]), Convert.ToInt32(Request.QueryString["obje_id"]), noticeTableSort);

                                List<Comment> CommentList = CommentBL.COMMENT_MY_PROFIL_GET_BY_ID(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]));

                                List<MemberProfil> MemberProfilList = MemberProfilBL.MEMBER_PROFIL_GET_COMMENT_LIST(obje_id, noticeTableSort, Convert.ToInt32(Session["id"]));

                                ViewBag.MemberProfilList = MemberProfilList;
                                ViewBag.CommentList = CommentList;
                                ViewBag.who_id = Convert.ToInt32(Session["id"]);
                                ViewBag.sort = sort;
                                ViewBag.obje_id = obje_id;
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
                
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
          
        }

        public ActionResult CommentMyProfilCount(int obje_id)
        {
            try
            {               
                ViewBag.CommentCount = (CommentBL.COMMENT_MY_PROFIL_GET_BY_ID(obje_id, Convert.ToInt32(Enum.Parse(typeof(noticeTableSort), noticeTableSort.ShareObject.ToString())), Convert.ToInt32(Session["id"]))).Count;
                shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(obje_id);
                return View(shareObje);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
            
        }

        public ActionResult MySharedObjectDetails(int obje_id, int sort,string eventAction)
        {
            try
            {// 
                if (Request.QueryString["eventAction"] == null)
                {
                    shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(obje_id);
                    ViewBag.who_id = Convert.ToInt32(Session["id"]);
                    ViewBag.obje_id = obje_id;
                    ViewBag.sort = sort;
                    return View(shareObje);
                }
                else
                {
                    if (Request.QueryString["eventAction"] == "delete")
                    {

                        int id = ShareObjectBL.SHARE_OBJECT_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]),Convert.ToInt32(Session["id"]));
                        ViewBag.obje_id = 0;
                        return RedirectToAction("myProfil", "MyProfil");
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 25 });
            }
        }

        public ActionResult Favorite()
        {
            try
            {
                List<MemberShortCard> FavoriteMyList_ = FavoriteBL.FAVORITE_ME_LIST(Convert.ToInt32(Session["id"]));
                return View(FavoriteMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult FavoriteCount(int who_id)
        {
            try
            {
                ViewBag.Count = FavoriteBL.FAVORITE_COUNT(Convert.ToInt32(Session["id"]));
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult FavoriteMyList()
        {
            try
            {
                List<MemberShortCard> FavoriteMyList_ = FavoriteBL.FAVORITE_MY_LIST(Convert.ToInt32(Session["id"]));
                return View(FavoriteMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult FavoriteMyAllList() 
        {
            try
            {
                List<MemberShortCard> FavoriteMyList_ = FavoriteBL.FAVORITE_MY_ALL_LIST(Convert.ToInt32(Session["id"]));
                return View(FavoriteMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult FavoriteMeList()
        {
            try
            {
                List<MemberShortCard> FavoriteMyList_ = FavoriteBL.FAVORITE_ME_LIST(Convert.ToInt32(Session["id"]));
                return View(FavoriteMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult FavoriteList()
        {
            try
            {
                List<MemberShortCard> FavoriteMyList_ = FavoriteBL.FAVORITE_ALL_LIST(Convert.ToInt32(Session["id"]));
                ViewBag.member_id = Convert.ToInt32(Session["id"]);
                return View(FavoriteMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult Smile()
        {
            try
            {
                List<MemberShortCard> SmileMyList_ = NoticeBL.SMILE_ME_LIST(Convert.ToInt32(Session["id"]));
                return View(SmileMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult SmileCount()
        {
            try
            {
                ViewBag.Count = NoticeBL.SMILE_COUNT(Convert.ToInt32(Session["id"]));
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult SmileMyAllList()
        {
            try
            {
                List<MemberShortCard> SmileMyList_ = NoticeBL.SMILE_MY_ALL_LIST(Convert.ToInt32(Session["id"]));
                return View(SmileMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult SmileMeList()
        {
            try
            {
                List<MemberShortCard> SmileMeList_ = NoticeBL.SMILE_ME_LIST(Convert.ToInt32(Session["id"]));
                return View(SmileMeList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult SmileList()
        {
            try
            {
                List<MemberShortCard> SmileMyList_ = NoticeBL.SMILE_ALL_LIST(Convert.ToInt32(Session["id"]));
                ViewBag.member_id = Convert.ToInt32(Session["id"]);
                return View(SmileMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult ProfilView()
        {
            try
            {
                List<MemberShortCard> ProfilMyList_ = NoticeBL.PROFIL_ME_LIST(Convert.ToInt32(Session["id"]));
                return View(ProfilMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult ProfilCount()
        {
            try
            {
                ViewBag.Count = NoticeBL.PROFIL_COUNT(Convert.ToInt32(Session["id"])); 
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult ProfilMyAllList()
        {
            try
            {
                List<MemberShortCard> ProfilMyList_ = NoticeBL.PROFIL_MY_ALL_LIST(Convert.ToInt32(Session["id"]));
                return View(ProfilMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult ProfilMeList()
        {
            try
            {
                List<MemberShortCard> ProfilMeList_ = NoticeBL.PROFIL_ME_LIST(Convert.ToInt32(Session["id"]));
                return View(ProfilMeList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult ProfilList(int page) 
        {
            try
            {               
                List<MemberShortCard> ProfilMyList_ = NoticeBL.PROFIL_ALL_LIST(Convert.ToInt32(Session["id"]),page);
                ViewBag.member_id = Convert.ToInt32(Session["id"]);
                ViewBag.Page = page;
                return View(ProfilMyList_);
            }
            catch
            {

                return RedirectToAction("Uppss", "Error", new { P = 20 });
            }
        }

        public ActionResult MessageMyCount()
        {
            try
            {
                ViewBag.messageMyCount = MessagesBL.MESSAGE_MY_COUNT(Convert.ToInt32(Session["id"]));
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        } 

        public ActionResult MessageMyAll()
        {
            try
            {
                if (Request.QueryString["MessageEventAction"] == null)
                {
                    List<Messages> MessageList = MessagesBL.MESSAGE_MY_ALL_LIST(Convert.ToInt32(Session["id"]));
                    List<MemberShortCard> MessageMyMemberList = MessagesBL.MESSAGE_MY_ALL_MEMBER_LIST(Convert.ToInt32(Session["id"]));

                    ViewBag.MessageMyMemberList = MessageMyMemberList;
                    ViewBag.MessageList = MessageList;
                    ViewBag.who_id = Convert.ToInt32(Session["id"]);
                    return View();
                }
                else
                {
                    if (Request.QueryString["MessageEventAction"] == "deleteMessageAll")
                    {
                        bool result = MessagesBL.MESSAGE_DELETE(Convert.ToInt32(Session["id"]),Convert.ToInt32(Request.QueryString["whom_id"]));

                        List<Messages> MessageList = MessagesBL.MESSAGE_MY_ALL_LIST(Convert.ToInt32(Session["id"]));
                        List<MemberShortCard> MessageMyMemberList = MessagesBL.MESSAGE_MY_ALL_MEMBER_LIST(Convert.ToInt32(Session["id"]));

                        ViewBag.MessageMyMemberList = MessageMyMemberList;
                        ViewBag.MessageList = MessageList;
                        ViewBag.who_id = Convert.ToInt32(Session["id"]);

                        ViewBag.who_id = Convert.ToInt32(Request.QueryString["who_id"]);
                        ViewBag.whom_id = Convert.ToInt32(Request.QueryString["whom_id"]);
                        ViewBag.obje_id = Convert.ToInt32(Request.QueryString["obje_id"]);
                        ViewBag.sort = Convert.ToInt32(Request.QueryString["sort"]);
                        ViewBag.noticeTableSort = Convert.ToInt32(Request.QueryString["noticeTableSort"]);
                        
                        return View();
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

        public ActionResult MessageMy()
        {
            try
            {// , 
                if (Request.QueryString["MessageEventAction"] == null)
                {
                    List<Messages> MessageList = MessagesBL.MESSAGE_MY_LIST(Convert.ToInt32(Session["id"]));
                    List<MemberShortCard> MessageMyMemberList = MessagesBL.MESSAGE_MY_MEMBER_LIST(Convert.ToInt32(Session["id"]));

                    ViewBag.MessageMyMemberList = MessageMyMemberList;
                    ViewBag.MessageList = MessageList;
                    ViewBag.who_id = Convert.ToInt32(Session["id"]);
                    return View();
                }
                else
                {
                    if (Request.QueryString["MessageEventAction"] == "deleteMessageAll")
                    {
                        bool result = MessagesBL.MESSAGE_DELETE(Convert.ToInt32(Session["id"]), Convert.ToInt32(Request.QueryString["whom_id"]));

                        List<Messages> MessageList = MessagesBL.MESSAGE_MY_LIST(Convert.ToInt32(Session["id"]));
                        List<MemberShortCard> MessageMyMemberList = MessagesBL.MESSAGE_MY_MEMBER_LIST(Convert.ToInt32(Session["id"]));

                        ViewBag.MessageMyMemberList = MessageMyMemberList;
                        ViewBag.MessageList = MessageList;
                        ViewBag.who_id = Convert.ToInt32(Session["id"]);

                        ViewBag.who_id = Convert.ToInt32(Request.QueryString["who_id"]);
                        ViewBag.whom_id = Convert.ToInt32(Request.QueryString["whom_id"]);
                        ViewBag.obje_id = Convert.ToInt32(Request.QueryString["obje_id"]);
                        ViewBag.sort = Convert.ToInt32(Request.QueryString["sort"]);
                        ViewBag.noticeTableSort = Convert.ToInt32(Request.QueryString["noticeTableSort"]);

                        return View();
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

        public ActionResult TagCount(int obje_id)
        {
            try
            {
                int count = TagBL.TAG_COUNT(Convert.ToInt32(Session["id"]), obje_id);
                ViewBag.count = count;
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }           
        }

        public ActionResult TagMemberList(int shareObje_id,int sort)
        {
            try
            {
                if (Request.QueryString["eventAction"] == null)
                {
                    List<MemberShortCard> tagMemberList = TagBL.TAG_MEMBER_LIST(Convert.ToInt32(Session["id"]), shareObje_id);
                    List<MemberShortCard> favoriteList = FavoriteBL.FAVORITE_MY_ALL_LIST(Convert.ToInt32(Session["id"]));
                    ViewBag.tagMemberList = tagMemberList;
                    ViewBag.shareObje_id = shareObje_id;
                    ViewBag.sort = sort;
                    ViewBag.favoriteList = favoriteList;
                    return View();
                }
                else
                {
                    if (Request.QueryString["eventAction"] == "add")
                    {//id, who_id, whom_id, shareObje_id, noticeTableSort, date, confrim
                        Tag tag = new Tag();
                        tag.who_id = Convert.ToInt32(Session["id"]);
                        tag.whom_id = Convert.ToInt32(Request.QueryString["whom_id"]);
                        tag.shareObje_id = shareObje_id;
                        tag.noticeTableSort =Convert.ToInt32(Enum.Parse(typeof(noticeTableSort), noticeTableSort.Tag.ToString()));
                        bool result = TagBL.TAG_INSERT(tag, Convert.ToInt32(Request.QueryString["sort"]));
                        List<MemberShortCard> tagMemberList = TagBL.TAG_MEMBER_LIST(Convert.ToInt32(Session["id"]), shareObje_id);
                        List<MemberShortCard> favoriteList = FavoriteBL.FAVORITE_MY_ALL_LIST(Convert.ToInt32(Session["id"]));
                        ViewBag.shareObje_id = shareObje_id;
                        ViewBag.sort = sort;
                        ViewBag.tagMemberList = tagMemberList;
                        ViewBag.favoriteList = favoriteList;
                        return View();
                    }
                    else
                    {
                        if (Request.QueryString["eventAction"] == "remove")
                        {
                            bool result = TagBL.TAG_DELETE(Convert.ToInt32(Session["id"]), Convert.ToInt32(Request.QueryString["whom_id"]), shareObje_id);
                            List<MemberShortCard> tagMemberList = TagBL.TAG_MEMBER_LIST(Convert.ToInt32(Session["id"]), shareObje_id);
                            List<MemberShortCard> favoriteList = FavoriteBL.FAVORITE_MY_ALL_LIST(Convert.ToInt32(Session["id"]));
                            ViewBag.shareObje_id = shareObje_id;
                            ViewBag.sort = sort;
                            ViewBag.tagMemberList = tagMemberList;
                            ViewBag.favoriteList = favoriteList;
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
        
        public ActionResult EnjoyShared(int shareObje_id, int noticeTableSort)
        {
            try
            {//,int noticeTableSort --Convert.ToInt32(Enum.Parse(typeof(noticeTableSort),noticeTableSort.ShareObject.ToString()))
                List<MemberShortCard> EnjoyMemberList = EnjoyBL.ENJOY_SHARE_ID(Convert.ToInt32(Session["id"]), shareObje_id, noticeTableSort);

                return View(EnjoyMemberList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }           
        }

        public ActionResult TagMyCount()
        {
            try
            {
                int count = TagBL.TAG_MY_COUNT(Convert.ToInt32(Session["id"]));
                ViewBag.count = count;
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public  ActionResult TagMyShared()
        {
            try
            {
                List<shareStreem> ShareStreemList = TagBL.TAG_MY_SHARED(Convert.ToInt32(Session["id"]));
                return PartialView(ShareStreemList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult TagProfilShared()
        {
            try
            {
                List<shareStreem> ShareStreemList = TagBL.TAG_PROFIL_SHARED(Convert.ToInt32(Session["id"]));
                return PartialView(ShareStreemList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult TagMyCheck(int obje_id)
        {
            try
            {

                bool result = TagBL.TAG_MY_CHECK(Convert.ToInt32(Session["id"]), obje_id);
                ViewBag.result = result;
                ViewBag.shareObje_id = obje_id;
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }

        }

        public ActionResult Survey()
        {
            return View();
        }

        public ActionResult MySurveyDetails(int? obje_id)
        {
            try
            {
                if (Request.QueryString["obje_id"] != null)
                {
                    int survey_id =Convert.ToInt32(Request.QueryString["obje_id"]);


                if (Request.QueryString["surveyEvent"] == null)
                {
                    shareStreem survey = SurveyBL.SURVEY_GET_BY_ID(survey_id, Convert.ToInt32(Session["id"]));
                    ViewBag.survey = survey;
                    ViewBag.result = survey.active;
                    ViewBag.obje_id = survey.id;
                    return View(survey);
                }
                else
                {
                    if (Request.QueryString["surveyEvent"] == "update")
                    {
                        int result = SurveyBL.SURVEY_SHARE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                        if (result != 0)
                        {
                            //List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_MY_ALL(Convert.ToInt32(Session["id"]));
                            shareStreem survey = SurveyBL.SURVEY_GET_BY_ID(survey_id, Convert.ToInt32(Session["id"]));
                            ViewBag.survey = survey;
                            ViewBag.result = result;
                            ViewBag.obje_id = survey.id;
                            // return View(ShareStreemList);
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
                            int result_ = SurveyBL.SURVEY_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                            shareStreem survey = SurveyBL.SURVEY_GET_BY_ID(survey_id, Convert.ToInt32(Session["id"]));
                            ViewBag.survey = survey;
                            ViewBag.result = result_;
                            ViewBag.obje_id = survey.id;
                            return View(survey);
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 0 });
                        }

                    }
                }

                }
                else
                {
                    ///burası boş gelirse
                     return View();
                }
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult SurveyCreate()
        {
            try
            {
                List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_MY_ALL(Convert.ToInt32(Session["id"]));
                return PartialView(ShareStreemList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult SurveyMyList()
        {
            try
            {
                if (Request.QueryString["surveyEvent"] == null)
                {
                    List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_MY_ALL(Convert.ToInt32(Session["id"]));
                    return View(ShareStreemList);
                    //return View(); 
                }
                else
                {
                    if (Request.QueryString["surveyEvent"] == "delete")
                    {
                        int result_ = SurveyBL.SURVEY_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                        List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_MY_ALL(Convert.ToInt32(Session["id"]));
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
        public ActionResult SurveyMyList(string SurveyText)
        {           
            try
            {
                if (Request.QueryString["surveyEvent"] == null)
                {
                    List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_MY_ALL(Convert.ToInt32(Session["id"]));
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
                            obje_.image_url = "yok";//null almıyor
                            obje_.sex = Session["sex"].ToString();
                            obje_.sort = Convert.ToInt32(Enum.Parse(typeof(sort), sort.survey.ToString()));
                            obje_.date = DateTime.Now;
                            obje_.confrim = Convert.ToInt32(Enum.Parse(typeof(confrim), confrim.in_review.ToString()));
                            obje_.status = Convert.ToInt32(Enum.Parse(typeof(status), Session["status"].ToString()));
                            obje_.active = Convert.ToInt32(Enum.Parse(typeof(active), active.passive.ToString()));
                           
                            int survey_id = SurveyBL.SURVEY_INSERT(obje_);
                            
                            return RedirectToAction("MySurveyDetails", "MyProfil", new { obje_id = survey_id });
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 26 });                           
                        }
                    }
                    else if (Request.QueryString["eventAction"] == "delete")
                    {
                        int result_ = SurveyBL.SURVEY_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                        List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_MY_ALL(Convert.ToInt32(Session["id"]));
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

        public ActionResult SurveyEditQeustion(int obje_id)
        { 
            try
            {
                if (Request.QueryString["surveyQuestionEvent"] == null)
                {
                    List<SurveyQuestion> questionList = SurveyBL.SURVEY_QUESTION(obje_id,Convert.ToInt32(Session["id"]));
                    ViewBag.obje_id = obje_id;
                    return View(questionList);
                }
                else if (Request.QueryString["surveyQuestionEvent"] == "delete")
                {
                    int result = SurveyBL.SURVEY_QUESTION_DELETE(Convert.ToInt32(Request.QueryString["question_id"]), Convert.ToInt32(Session["id"]));
                    List<SurveyQuestion> questionList = SurveyBL.SURVEY_QUESTION(obje_id, Convert.ToInt32(Session["id"]));
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
        public ActionResult SurveyEditQeustion(int obje_id, string eventAction, string Question, string A_option, string B_option, string C_option, string D_option)
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
                    question.confrim = 0;
                    question.survey_id = obje_id;
                    SurveyBL.SURVEY_QUESTION_INSERT(question);

                    List<SurveyQuestion> questionList = SurveyBL.SURVEY_QUESTION(obje_id, Convert.ToInt32(Session["id"]));
                    ViewBag.obje_id = obje_id;
                    return View(questionList);
                }               
                else
                {
                    List<SurveyQuestion> questionList = SurveyBL.SURVEY_QUESTION(obje_id, Convert.ToInt32(Session["id"]));
                    ViewBag.obje_id = obje_id;
                    return View(questionList);
                }               
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult SurveyRespondentsCount(int obje_id)
        {
            try
            {
                int result = SurveyBL.SURVEY_RESPONDENTS_COUNT(obje_id);
                ViewBag.result = result;               
                return View();

            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }

        }

        public ActionResult SurveyRespondentsList(int Obje_id)
        {
            try
            {
                List<MemberShortCard> SurveyRespondentsMemberList = SurveyBL.SURVEY_RESPONDENTS_LIST(Obje_id);
                ViewBag.Obje_id = Obje_id;
                return View(SurveyRespondentsMemberList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult MySurveyAnswerDetails(int obje_id)//,int whom_id
        {
            try 
            {

                if (Request.QueryString["surveyEvent"] == null)
                {
                    shareStreem survey = SurveyBL.SURVEY_GET_BY_ID(obje_id, Convert.ToInt32(Session["id"]));
                    ViewBag.survey = survey;
                    return View(survey);
                }
                else
                {
                    if (Request.QueryString["surveyEvent"] == "update")
                    {
                        int result = SurveyBL.SURVEY_SHARE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                        if (result != 0)
                        {
                            //List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_MY_ALL(Convert.ToInt32(Session["id"]));
                            shareStreem survey = SurveyBL.SURVEY_GET_BY_ID(obje_id, Convert.ToInt32(Session["id"]));
                            ViewBag.survey = survey;
                            ViewBag.result = result;
                            ViewBag.obje_id = survey.id;
                            // return View(ShareStreemList);
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
                            int result_ = SurveyBL.SURVEY_DELETE(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));
                            shareStreem survey = SurveyBL.SURVEY_GET_BY_ID(obje_id, Convert.ToInt32(Session["id"]));
                            ViewBag.survey = survey;
                            ViewBag.result = result_;
                            ViewBag.obje_id = survey.id;
                            return View(survey);
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

        public ActionResult SurveyList()
        {
            try
            {
                List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_PROFIL(Convert.ToInt32(Session["id"]) );               
                return View(ShareStreemList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

    }
}