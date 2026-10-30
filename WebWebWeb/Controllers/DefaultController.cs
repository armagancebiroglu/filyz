using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using WebWebWeb.BL;
using WebWebWeb.Models;

namespace WebWebWeb.Controllers
{
    public class DefaultController : Controller
    {
        //yeni üye oluşturma
        //üye login giriş
        //adresine maille üye kodu ve şifresinin gönderilmesi
        //hatalarda Uppss sayfasına yönlendirme.
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Intro()
        {
            
            return View();
        }
        public ActionResult Info()
        {
            try
            {
               
                if (Request.QueryString["w"] == "1")
                {
                    string nick_name = Session["nick_name"].ToString();
                    MemberProfilBL.IS_ONLINE(nick_name, false);
                    Session["nick_name"] = "";
                    Session["id"] = "";
                    Session["is_online"] = "";
                    Session["payment_date"] = "";
                    Session["profil_media"] = "";
                    Session["password"] = "";
                    Session["sex"] = "";
                    Session["status"] = "";

                }
                return View();
            }
            catch
            {
                return View();
            }
                       
        }
     
        public ActionResult Login()
        {
            //HttpCookie Login = Request.Cookies.Get("Login");//httpcookies
            //MemberLogin model = new MemberLogin();
            //if (Login == null)
            //{
            //   model = MemberProfilBL.MEMBER_LOGIN(Login.Value);
            //}
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection Form,MemberLogin model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.nick_name) || !string.IsNullOrEmpty(model.password))
                {                   
                    MemberLogin memberLogin = MemberProfilBL.MEMBER_LOGIN(model.nick_name);
                    if (memberLogin.nick_name == null)
                    {
                        ModelState["nick_name"].Errors.Clear();
                        ModelState["password"].Errors.Clear();
                        ModelState.AddModelError("nick_name", "Üye Kodu Bulunamadı");
                        return View();                        
                    }
                    else
                    {
                        if ((memberLogin.nick_name.Trim().ToUpper()) == (model.nick_name.Trim().ToUpper()))
                        {
                            if (memberLogin.password == model.password)
                            {
                                model.payment_date = DateTime.Now.AddDays(-20);//BURASINI SİTEYE GİREN HERKESİN ÖDEME YAPMIŞ KABUL EDİLMESİ İÇİN YAZDIM VERİ TABANINI DEĞİŞTİRMEDEN SESSİON A MÜDAHALE

                                Session["nick_name"] = memberLogin.nick_name;// session
                                Session["id"] = memberLogin.id;
                                Session["is_online"] = "true";
                                Session["payment_date"] = memberLogin.payment_date.ToString();
                               
                                Session["profil_media"] = memberLogin.profil_media;
                                Session["password"] = memberLogin.password;
                                Session["sex"] = memberLogin.sex.ToString();
                                Session["status"] = Enum.GetName(typeof(status), Convert.ToInt32(memberLogin.status));
                                Session["score"] = MemberProfilBL.SCORE_UPDATE(memberLogin.id).ToString();
                                Session["survey"] = "";

                                string[] values = Request.Form.GetValues("remember_me");
                                if (bool.Parse(values[0]))
                                {
                                    HttpCookie Login = Request.Cookies.Get("Login");//httpcookies
                                    if (Login == null)
                                    {

                                        Login = new HttpCookie("Login");
                                        Login.Value = model.nick_name;
                                        Login.Expires = DateTime.Now.AddDays(15);
                                        Response.Cookies.Add(Login);
                                    }

                                }
                                MemberProfilBL.IS_ONLINE(model.nick_name, true);//is_online
                                return JavaScript("window.location='" + Url.Action("Home", "Home") + "'");//bu tamam                                  
                                //return RedirectToAction("Home", "Home");// bu olmadı. olmamasının nedeni ajax kullandığım için -yani amacı zaten sayfa yenilenmeden iş yapmak
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

        [HttpGet]
        public ActionResult CreateMember()
        {       
           string code = HELPER.CREATE_KOD();
            ViewBag.kod = code;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMember(MemberProfil model,string nick_name)
        {
            model.nick_name = nick_name;
            if (!string.IsNullOrEmpty(model.sex))
            {
                if (model.sex.ToString() == "Kadın")
                {
                    model.profil_media = "/Media/img/woman.jpg";                  
                    // hicap fotoğraffıda olabilirdi.                   
                }
                else 
                {
                    model.hicap = false;
                    model.profil_media = "/Media/img/man.jpg";
                    //ModelState["hicap"].Errors.Clear();
                    //ModelState.SetModelValue("hicap", new ValueProviderResult("erkeklerin tesettürü mümkün değildir.", "", CultureInfo.InvariantCulture));
                }
            }            
           
            if (!string.IsNullOrEmpty(model.matiral_status))
            {
                if (model.matiral_status.ToString() == "Eşinden Ayrılmış" || model.matiral_status.ToString() == "Eşi Vefat Etmiş")
            {                
                    if (model.children==null)
                    {
                        ModelState.AddModelError("children", "Eşinizden ayrılmış veya eşiniz vefat etmiş ise çocuğunuzun olup olmadığını belirtiniz.");
                        
                        return View();
                    }
            }
            else { model.children = ""; ModelState["children"].Errors.Clear(); }
            }

            if (string.IsNullOrEmpty(model.my_story))
            {
                model.my_story = "şimdilik kendinden bahsetmedi.";
            }

                if (((int)(DateTime.Now.Year)) - ((int)(((DateTime)(model.age)).Year)) < 18)
            {
                ModelState.AddModelError("age", "Yaşınız 18 den küçük olamaz");
                return View();
            }

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

            if (!(Request.Form["confrim_password"]==model.password))
            {
                string errorMessage = "Şifre tekrarınız yanlış. Seçtiğiniz şifre:" + model.password.ToString();
                ModelState.AddModelError("password", errorMessage);
                return View();
            }
            model.score = 0;            
            model.is_online = true;
            model.last_enter_date = DateTime.Now;
            model.payment_date = DateTime.Now.AddDays(-20);

            if (HELPER.CHECK_FORBIDDEN(model.my_story))
            {
                model.status= (int)Enum.Parse(typeof(status), "in_review");
                //Enum.GetNames(typeof(status)) --  Enum.GetValues(typeof(status));    liste döner
            }
            else
            {
                //yasaklı kelime kullanan
                model.status = (int)Enum.Parse(typeof(status), "forbiddenMember");
            }





            if (MemberProfilBL.CODE_QUERY(model.nick_name.ToString()))
            {
                int id = MemberProfilBL.MEMBER_PROFIL_INSERT(model);
                if (id > 0)
                {
                    Session["nick_name"] = model.nick_name;// session a kod eklendi is_online true oldu. httpCookie girişte sorulup değer verilecek.
                    Session["id"] = model.id;
                    Session["is_online"] = model.is_online.ToString();
                    Session["payment_date"] = model.payment_date.ToString();
                    Session["profil_media"] = model.profil_media;
                    Session["password"] = model.password;
                    Session["sex"] = model.sex.ToString();
                    Session["status"] = Enum.GetName(typeof(status), Convert.ToInt32(model.status));
                    Session["survey"] = "";
                    ModelState.Clear();
                    //mail gönderimi
                    try
                    {
                        string mail = "üYE KODUNUZ: " + model.nick_name + Environment.NewLine + "ŞİFRENİZ: " + model.password + Environment.NewLine;
                        string body = mail + "http://www." + Request.Url.Host + "/Default/Info " + " " + " Sayfasından giriş yapabilirsiniz.";
                        bool value = HELPER.SEND_MAIL(model.e_mail, "ÜYE KODU VE ŞİFRE BİLDİRİMİ", body);

                        //return RedirectToAction("StatusMemberCreate", "Default", new { status = "succeeded", MC = model.nick_name });
                        return JavaScript("window.location='" + Url.Action("StatusMemberCreate", "Default", new { status = "succeeded", MC = model.nick_name }) + "'");
                    }
                    catch
                    {
                        //return RedirectToAction("StatusMemberCreate", "Default", new { status = "failed", MC = model.nick_name });
                        return JavaScript("window.location='" + Url.Action("StatusMemberCreate", "Default", new { status = "failed", MC = model.nick_name }) + "'");
                    }
                }
            }
            else
            {
                return RedirectToAction("Uppss", "Error", new { P=1});
            }
            return View(model); 
        }       
       
        public ActionResult StatusMemberCreate()
        {
            try
            {
                MemberLogin login = MemberProfilBL.MEMBER_LOGIN(Request.QueryString["MC"]);
                Session["nick_name"] = login.nick_name;
                Session["id"] = login.id.ToString();
                Session["is_online"] = login.is_online;
                Session["payment_date"] = login.payment_date;
                Session["profil_media"] = login.profil_media;
                Session["password"] = login.password;
                Session["sex"] = login.sex;
                Session["status"] = login.status;
                if (Request.QueryString["status"] == "succeeded")
                {                    
                    ViewBag.txt1 = "E-posta gönderildi";
                    ViewBag.Nick_Name = login.nick_name;
                    ViewBag.Password = login.password;
                    ViewBag.txt2 = "Üyelik kaydınız alınmıştır.Üye Kodu ve Şifre mail adresinize gönderildi. Mailiniz yanlış olma ihtimaline karşı ÜYE KODU ve ŞİFRE'nizi kaydetmenizi öneririz.";
                }
                if (Request.QueryString["status"] == "failed")
                {
                    ViewBag.txt1 = "E-posta gönderilemedi!!";
                    ViewBag.Nick_Name = login.nick_name;
                    ViewBag.Password = login.password;
                    ViewBag.txt2 = "Üyelik kaydınız alınmıştır ancak ÜYE KODU ve ŞİFRE'niz mail adresinize gönderilemedi.ÜYE KODU ve ŞİFRE'nizi kaydetmenizi öneririz.";
                }
            }
            catch 
            {
                return RedirectToAction("Uppss", "Error", new { P = 2 });
                //return View();               
            }
            return View();
            //return View(ViewBag);
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string nick)
        {
            try
            {
                MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_NICK_NAME(nick);
                if (member != null)
                {
                    string body = nick + Environment.NewLine + " Şifreniz: " + member.password + Environment.NewLine + " Şifre seçiminde herhangi bir şartımız yoktur, istediğiniz gibi kolay bir şifre seçebilirsiniz.";
                    bool value = HELPER.SEND_MAIL(member.e_mail, "filyz.com ŞİFRE HATIRLATMA", body);
                    ViewBag.send = "";
                    if (value)
                    {
                        ViewBag.send = "send";
                    }
                }
                else
                {
                    ViewBag.send = "not_send";
                }
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 44 });
            }
        }

        public ActionResult Mail()
        {
            try
            {
                ViewBag.Mail = "Size cevap yazabilmemiz için mail adresinizi yazınız.";
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
                string body = text + Environment.NewLine + " E-mail adresi: " + e_mail;
                bool value = HELPER.SEND_MAIL("filiz@filyz.com", "Site dışı mail ", body);
                ViewBag.send = "";
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

        public ActionResult conditions()
        {
            return View();
        }
        
             public ActionResult contract()
        {
            return View();
        }
    }
}