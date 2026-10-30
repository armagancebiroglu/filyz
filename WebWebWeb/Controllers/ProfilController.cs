using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWebWeb.BL;
using WebWebWeb.Models;

namespace WebWebWeb.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profil(int member_id)
        {
            try
            {
                MemberProfil memberProfil = new MemberProfil();
                if (!string.IsNullOrEmpty(Request.QueryString["member_id"].ToString()))
                    {
                    memberProfil = MemberProfilBL.MEMBER_PROFIL_GET_BY_ID(Convert.ToInt32(Request.QueryString["member_id"]));

                    if (Session["sex"].ToString() != memberProfil.sex)
                    {//kendi hemcinsinin profiline bakamaz url injectiona karşı


                        //Üye kodu 
                        ViewBag.p = memberProfil.nick_name;
                        #region //profil fotoğrafı
                        ViewBag.p0 = memberProfil.profil_media;
                        #endregion
                        #region//Fiziksel Görünüş ve durumu ve tesettür

                        ViewBag.p1 = memberProfil.age.ToShortDateString() + " Tarihinde doğmuş. " + memberProfil.eye_color + " gözlü ve saçları " + memberProfil.hair_color.ToLower() + ".";
                        if (memberProfil.hicap == true || memberProfil.sex == "kadın")
                        {
                            ViewBag.p1 = ViewBag.p1 + " Tesettürlü bir hanım. Boyu " + memberProfil.height.ToString() + " cm ve " + memberProfil.kilo.ToString() + " kilo. ";
                        }
                        else if (memberProfil.hicap == false || memberProfil.sex == "Erkek")
                        {
                            ViewBag.p1 = ViewBag.p1 + " Boyu " + memberProfil.height.ToString() + " cm ve " + memberProfil.kilo.ToString() + " kilo. ";
                        }

                        if (memberProfil.disable == "Yok")
                        {
                            ViewBag.p1 = ViewBag.p1 + " Herhangi bir fiziksel engeli yok.";
                        }
                        else if (memberProfil.disable == "Görme Engelli")
                        {
                            ViewBag.p1 = ViewBag.p1 + " Görme engeli bulunuyor.";
                        }
                        else if (memberProfil.disable == "Bedensel Engelli")
                        {
                            ViewBag.p1 = ViewBag.p1 + " Bedensel engeli bulunuyor.";
                        }
                        else if (memberProfil.disable == "İşitme Engelli")
                        {
                            ViewBag.p1 = ViewBag.p1 + " İşitme engeli bulunuyor.";
                        }
                        #endregion
                        #region//bulunduğu şehir, memleketi ve yaşamak istediği şehir.
                        if (memberProfil.city != memberProfil.country && memberProfil.country == memberProfil.city_wants_to_live)
                        {
                            ViewBag.p2 = memberProfil.city + "'da yaşıyor. Memleketi " + memberProfil.country + " ve öncelikli olarak memleketinde yaşamak istiyor. ";
                        }
                        else
                        if (memberProfil.city == memberProfil.country && memberProfil.city != memberProfil.city_wants_to_live)
                        {
                            ViewBag.p2 = " Kendi memleketinde " + memberProfil.country + "' da yaşıyor. Ama öncelikli olarak " + memberProfil.city_wants_to_live + " 'da yaşamak istiyor. ";
                        }
                        else
                        if (memberProfil.city == memberProfil.country && memberProfil.city == memberProfil.city_wants_to_live)
                        {
                            ViewBag.p2 = " Aynı zamanda kendi memleketi de olan " + memberProfil.city + "'da yaşıyor, ve öncelikli olarak hayatına yine burada devam etmek istiyor. ";
                        }
                        else
                        if (memberProfil.city != memberProfil.country && memberProfil.city == memberProfil.city_wants_to_live)
                        {
                            ViewBag.p2 = memberProfil.city + "'da yaşıyor, ve öncelikli olarak hayatına yine burada devam etmek istiyor. Kendi memleketi " + memberProfil.country + "...";
                        }
                        else
                        {
                            ViewBag.p2 = memberProfil.city + "'da yaşıyor. Memleketi " + memberProfil.country + " ve öncelikli olarak " + memberProfil.city_wants_to_live + " 'da yaşamak istiyor. ";
                        }
                        #endregion
                        #region//medeni durumu                  

                        if (memberProfil.matiral_status == "Bekar" && memberProfil.sex == "Erkek")
                        {
                            ViewBag.P3 = " Bekar bir bey. ";
                        }
                        else
                        if (memberProfil.matiral_status == "Bekar" && memberProfil.sex == "Kadın")
                        {
                            ViewBag.P3 = " Bekar. ";
                        }
                        else
                        if (memberProfil.matiral_status == "Eşinden Ayrılmış" && memberProfil.children == "Evet")
                        {
                            ViewBag.P3 = " Daha önce evlenip ayrılmış, çocuğu var. ";
                        }
                        else
                        if (memberProfil.matiral_status == "Eşinden Ayrılmış" && memberProfil.children == "Hayır")
                        {
                            ViewBag.P3 = " Daha önce evlenip ayrılmış ama çocuğu yok. ";
                        }
                        else
                        if (memberProfil.matiral_status == "Eşi Vefat Etmiş" && memberProfil.children == "Evet")
                        {
                            ViewBag.P3 = " Daha önce evlenmiş ve maalesef eşi vefat etmiş. Çocuğu var. ";
                        }
                        else
                        if (memberProfil.matiral_status == "Eşi Vefat Etmiş" && memberProfil.children == "Hayır")
                        {
                            ViewBag.P3 = " Daha önce evlenmiş ve maalesef eşi vefat etmiş, çocukları yok ";
                        }
                        else { ViewBag.P3 = ""; }
                        #endregion
                        #region//Eğitim durumu
                        if (memberProfil.educational_status == "İlk Öğretim")
                        {
                            ViewBag.p4 = " İlk öğretimini tamamlayıp hayata atılmış. ";
                        }
                        if (memberProfil.educational_status == "Lise")
                        {
                            ViewBag.p4 = " Eğitim hayatını liseye kadar sürdürmüş. ";
                        }

                        if (memberProfil.educational_status == "Lisans")
                        {
                            ViewBag.p4 = " Üniversite eğitimi almış. ";
                        }

                        if (memberProfil.educational_status == "Yüksek Lisans")
                        {
                            ViewBag.p4 = " Üniversite eğitimini Yüksek Lisansla tamamlamış. ";
                        }
                        #endregion
                        #region Diyorki
                        ViewBag.p5 = memberProfil.my_story;
                        #endregion

                        #region profil görüntüleme
                        Notice ProfilView = new Notice();
                        ProfilView.active = true;
                        ProfilView.date = DateTime.Now;
                        ProfilView.noticeTableSort = Convert.ToInt32(Enum.Parse(typeof(noticeTableSort), noticeTableSort.Notice.ToString()));
                        ProfilView.obje_id = 0;
                        ProfilView.sort = 0;
                        ProfilView.text = "sizin profilinizi inceledi.";
                        ProfilView.whom_id = Convert.ToInt32(Request.QueryString["member_id"]);
                        ProfilView.who_id = Convert.ToInt32(Session["id"]);
                        int query = NoticeBL.PROFIL_VIEW_INSERT(ProfilView);
                        #endregion
                        ViewBag.memberProfil = memberProfil;
                        ViewBag.member_id = member_id;
                        return View(memberProfil);
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

        public ActionResult ProfilAlbum(int member_id, int who_id)
        {
            try
            {//hemcisini albumune bakmaması için sql de kim kimin albümüne sorgusu var
                List<albums> albums = albumsBL.ALBUM_GET_ALL(member_id, Convert.ToInt32(Session["id"]));
                ViewBag.who_id = Convert.ToInt32(Session["id"]);
                return PartialView(albums);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 19 });
            }
        }

        public ActionResult ShareStreemProfil(int member_id,int page)
        {
            try
            {// kim kime kontrolü sql veri tabanında yapılır
                List<shareStreem> ShareStreemList = Share_MemberBL.SHARE_STREEM_MEMBER_ALL(member_id, Convert.ToInt32(Session["id"]), page);
                return PartialView(ShareStreemList);
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
           
        }

        public ActionResult ProfilScreen(int album_id,int whom_id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["album_id"]) && Request.QueryString["sort"] != null && !string.IsNullOrEmpty(Request.QueryString["whom_id"]))
                {
                    albums album = albumsBL.ALBUM_GET_BY_ID(Convert.ToInt32(Request.QueryString["album_id"]), Convert.ToInt32(Request.QueryString["whom_id"]),Convert.ToInt32(Session["id"]));
                    
                    return View(album);
                } 
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
            return View();
        }

        public ActionResult Favorite(int whom_id, string eventAction)
        {//yıldız koyup kaldırma ,bool favoriBlocked  insertBlocked
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["eventAction"]))
                {
                    int result = FavoriteBL.FAVORITE_GET_BY_ID(Convert.ToInt32(Session["id"]), whom_id);  //0 nötür, 1:favori, 2:engelli Convert.ToInt32(Enum.Parse(typeof(favoriBlocked), favoriBlocked.favorite.ToString())))
                    ViewBag.result = result;
                    ViewBag.whom_id = whom_id;
                    return View();
                }
                else 
                {
                    if (Request.QueryString["eventAction"] == "insert")
                    {
                        Favorite favorite = new Models.Favorite();
                        favorite.who_id = Convert.ToInt32(Session["id"]);
                        favorite.whom_id = whom_id;
                        favorite.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                        favorite.favoriBlocked = Convert.ToInt32(Enum.Parse(typeof(favoriBlocked), favoriBlocked.favorite.ToString()));
                        int result = FavoriteBL.FAVORITE_INSERT(favorite);
                        ViewBag.result = favorite.favoriBlocked;
                        ViewBag.whom_id = whom_id;
                        return View();
                    }
                    else
                    {
                        if (Request.QueryString["eventAction"] == "insertBlocked")
                        {
                            Favorite favorite = new Models.Favorite();
                            favorite.who_id = Convert.ToInt32(Session["id"]);
                            favorite.whom_id = whom_id;
                            favorite.active = Convert.ToInt32(Enum.Parse(typeof(active), active.active.ToString()));
                            favorite.favoriBlocked = Convert.ToInt32(Enum.Parse(typeof(favoriBlocked), favoriBlocked.blocked.ToString()));
                            int result = FavoriteBL.FAVORITE_INSERT(favorite);
                            ViewBag.result = favorite.favoriBlocked;
                            ViewBag.whom_id = whom_id;
                            return View();
                        }
                        else
                        {
                            if (Request.QueryString["eventAction"] == "delete")
                            {
                                int result = FavoriteBL.FAVORITE_DELETE(Convert.ToInt32(Session["id"]), whom_id);
                                ViewBag.result = result;
                                ViewBag.whom_id = whom_id;
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
       
        public ActionResult Smile(int whom_id, string eventAction)
        {//alt+# 1,2 ☺ ☻ gülümse 
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["eventAction"]))
                {
                    bool result = NoticeBL.SMILE_CHECK(Convert.ToInt32(Session["id"]),whom_id);
                    ViewBag.result = result;
                    ViewBag.whom_id = whom_id;
                    return View();
                }
                else
                {
                    if (Request.QueryString["eventAction"] == "insert")
                    {
                        Notice smile = new Notice();
                        smile.active = true;
                        smile.date = DateTime.Now;
                        smile.noticeTableSort = Convert.ToInt32(Enum.Parse(typeof(noticeTableSort), noticeTableSort.Notice.ToString()));
                        smile.obje_id = 0;
                        smile.sort = 0;
                        smile.text = " size gülümsedi.";
                        smile.whom_id = whom_id;
                        smile.who_id = Convert.ToInt32(Session["id"]);                        
                        int query= NoticeBL.NOTICE_INSERT(smile);
                        bool result;
                        if (query>0)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                        ViewBag.result = result;
                        ViewBag.whom_id = whom_id;
                        return View();
                    }
                    else
                    {
                        if (Request.QueryString["eventAction"] == "delete")
                        {
                            bool result = NoticeBL.NOTICE_DELETE(Convert.ToInt32(Session["id"]), whom_id, 0,Convert.ToInt32(Enum.Parse(typeof(noticeTableSort), noticeTableSort.Notice.ToString())));
                            ViewBag.result = result;
                            ViewBag.whom_id = whom_id;
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

        public ActionResult MessageCount( int whom_id)
        {
            try
            {
                int messageCount = MessagesBL.MESSAGE_COUNT(Convert.ToInt32(Session["id"]), whom_id);
                ViewBag.messageCount = messageCount;
                ViewBag.whom_id = whom_id;
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult Message(string text,int whom_id)
        {

            if (Request.QueryString["eventAction"] == null)
            {
                //MemberShortCard member = MemberProfilBL.MEMBER_SHORT_CARD(whom_id);
                List<Messages> MessageList = MessagesBL.MESSAGE_LIST(Convert.ToInt32(Session["id"]), whom_id);
                               
                //ViewBag.member = member;
                ViewBag.MessageList = MessageList;
                ViewBag.who_id = Convert.ToInt32(Session["id"]);
                ViewBag.whom_id = whom_id;
                return View();
            }
            else
            {
                if (Request.QueryString["eventAction"] == "message")
                {//id, who_id, whom_id, text, date, active
                    if (DateTime.Now.Subtract(Convert.ToDateTime(Session["payment_date"])).Days < 35)
                    {
                        Messages message = new Messages();
                        message.who_id = Convert.ToInt32(Session["id"]);
                        message.whom_id = whom_id;
                        message.text = text;
                        bool result = MessagesBL.MESSAGE_INSERT(message);

                        MemberShortCard member = MemberProfilBL.MEMBER_SHORT_CARD(whom_id);
                        List<Messages> MessageList = MessagesBL.MESSAGE_LIST(Convert.ToInt32(Session["id"]), whom_id);


                        ViewBag.member = member;
                        ViewBag.MessageList = MessageList;
                        ViewBag.who_id = Convert.ToInt32(Session["id"]);
                        ViewBag.whom_id = whom_id;


                        return View(member);
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 39 });
                    }                   
                }
                else
                {
                    if (Request.QueryString["eventAction"] == "delete")
                    {
                        bool result = MessagesBL.MESSAGE_DELETE_ID(Convert.ToInt32(Request.QueryString["obje_id"]), Convert.ToInt32(Session["id"]));

                        MemberShortCard member = MemberProfilBL.MEMBER_SHORT_CARD(whom_id);
                        List<Messages> MessageList = MessagesBL.MESSAGE_LIST(Convert.ToInt32(Session["id"]), whom_id);

                        ViewBag.member = member;
                        ViewBag.MessageList = MessageList;
                        ViewBag.who_id = Convert.ToInt32(Session["id"]);
                        ViewBag.whom_id = whom_id;
                        return View(member);
                    }
                    else
                    {
                        if (Request.QueryString["eventAction"] == "delete_all")
                        {
                            bool result = MessagesBL.MESSAGE_DELETE_ALL(Convert.ToInt32(Session["id"]), whom_id);

                            MemberShortCard member = MemberProfilBL.MEMBER_SHORT_CARD(whom_id);
                            List<Messages> MessageList = MessagesBL.MESSAGE_LIST(Convert.ToInt32(Session["id"]), whom_id);

                            ViewBag.member = member;
                            ViewBag.MessageList = MessageList;
                            ViewBag.who_id = Convert.ToInt32(Session["id"]);
                            ViewBag.whom_id = whom_id;
                            return View(member);
                        }
                        else
                        {
                            return RedirectToAction("Uppss", "Error", new { P = 0 });
                        }
                    }
                }
            }
        }

        public ActionResult ProfilMessage(int whom_id)
        {
            try
            {
                if (Request.QueryString["MessageEventAction"] == null)
                {
                    MemberShortCard member = MemberProfilBL.MEMBER_SHORT_CARD(whom_id);
                    List<Messages> MessageList = MessagesBL.MESSAGE_LIST(Convert.ToInt32(Session["id"]), whom_id);

                    ViewBag.member = member;
                    ViewBag.MessageList = MessageList;
                    ViewBag.who_id = Convert.ToInt32(Session["id"]);
                    ViewBag.whom_id = whom_id;
                    return View(member);
                }
                else
                {
                    if (Request.QueryString["MessageEventAction"] == "deleteMessageAll")
                    {
                        bool result = MessagesBL.MESSAGE_DELETE(Convert.ToInt32(Session["id"]), Convert.ToInt32(Request.QueryString["whom_id"]));

                        MemberShortCard member = MemberProfilBL.MEMBER_SHORT_CARD(whom_id);
                        List<Messages> MessageList = MessagesBL.MESSAGE_LIST(Convert.ToInt32(Session["id"]), whom_id);

                        ViewBag.member = member;
                        ViewBag.MessageList = MessageList;
                        ViewBag.who_id = Convert.ToInt32(Session["id"]);
                        ViewBag.whom_id = whom_id;
                        return View(member);
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

        public ActionResult TagCheck(int whom_id, int obje_id)
        {
            try
            {
                if (Request.QueryString["eventAction"] == null)
                {
                    bool result = TagBL.TAG_CHECK(Convert.ToInt32(Session["id"]), whom_id, obje_id);
                    ViewBag.result = result;
                    ViewBag.whom_id = whom_id;
                    ViewBag.shareObje_id = obje_id;
                    return View();
                }
                else
                {
                    if (Request.QueryString["eventAction"] == "remove")
                    {
                        bool delete= TagBL.TAG_DELETE(Convert.ToInt32(Request.QueryString["whom_id"]), Convert.ToInt32(Session["id"]), obje_id);
                        bool result = TagBL.TAG_CHECK(Convert.ToInt32(Session["id"]), whom_id, obje_id);
                        ViewBag.result = result;
                        ViewBag.whom_id = whom_id;
                        ViewBag.shareObje_id = obje_id;
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

        public ActionResult ProfilSurveyDetails(int survey_id,int whom_id)
        {
            try
            {
                if (Request.QueryString["ProfilSurveyAction"] == null)
                {
                    shareStreem survey = SurveyBL.SURVEY_GET_BY_ID(survey_id, whom_id);
                    MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_ID(whom_id);
                    if (Session["sex"].ToString() != survey.sex.ToString())
                    {
                        ViewBag.member = member;
                        ViewBag.survey = survey;
                        ViewBag.survey_id = survey_id;
                        ViewBag.whom_id = whom_id;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 0 });
                    }             
                }
                else
                {
                    if (Request.QueryString["ProfilSurveyAction"] == "DeleteAllAnswer")
                    {
                        int result_ = SurveyBL.SURVEY_ALL_ANSWER_DELETE(Convert.ToInt32(Session["id"]), survey_id);
                        shareStreem survey = SurveyBL.SURVEY_GET_BY_ID(survey_id, whom_id);
                        MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_ID(whom_id);
                        ViewBag.member = member;
                        ViewBag.survey = survey;
                        ViewBag.survey_id = survey_id;
                        ViewBag.whom_id = whom_id;

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

        public ActionResult SurveyAnswerProfilDetails(int survey_id)
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
                    shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(survey_id);
                    return View(shareObje);
                }
                else if (Request.QueryString["surveyEvent"] == "answer")
                {
                    SurveyAnswer answerItem = new SurveyAnswer();
                    answerItem.survey_id=Convert.ToInt32(Request.QueryString["survey_id"]);
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
                    shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(survey_id);
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
                        shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(survey_id);
                        return View(shareObje);
                    }

                    else
                    {
                        return RedirectToAction("Uppss", "Error", new { P = 28 });
                    }
                }
                else if (Request.QueryString["surveyEvent"] == "check")
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

                    shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(survey_id);
                    Notice notice = new Notice();
                    notice.noticeTableSort = Convert.ToInt32(Enum.Parse(typeof(noticeTableSort), noticeTableSort.ShareObject.ToString()));
                    notice.text = "Sizin bir anketinizi cevapladı.";
                    notice.who_id = Convert.ToInt32(Session["id"]);
                    notice.whom_id = shareObje.member_id;
                    notice.obje_id = shareObje.id;
                    notice.sort= Convert.ToInt32(Enum.Parse(typeof(sort), sort.survey.ToString()));
                    notice.active= Convert.ToBoolean(Enum.Parse(typeof(active), active.active.ToString()));
                    int result = NoticeBL.NOTICE_INSERT(notice);
                    ViewBag.result = result;
                    return View(shareObje);
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

        public ActionResult SurveyCount(int whom_id)
        {
            try
            {
                int result = SurveyBL.SURVEY_COUNT(whom_id);                
                ViewBag.result = result;
                ViewBag.whom_id = whom_id;
                //ViewBag.shareObje_id = obje_id;
                return View();

            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }

        }

        public ActionResult SurveyList(int whom_id)
        {
            try
            {
                List<shareStreem> ShareStreemList = SurveyBL.SURVEY_LIST_PROFIL_ALL(whom_id);
                MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_ID(whom_id);
                if (member.sex != Session["sex"].ToString())
                {
                    ViewBag.member = member;
                    ViewBag.isonline = member.is_online;
                    return View(ShareStreemList);
                }
                else
                {
                    return RedirectToAction("Uppss", "Error", new { P = 0 });
                }
                //return View(); 
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }           
        }

        public ActionResult SurveyCheck(int whom_id, int obje_id)
        {
            try
            {
                bool result = SurveyBL.SURVEY_CHECK(Convert.ToInt32(Session["id"]), obje_id);
                ViewBag.result = result;
                ViewBag.whom_id = whom_id;
                ViewBag.Obje_id = obje_id;
                return View();
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }
      
        public ActionResult SurveyQuestionAnswer(int whom_id)
        {
            try
            {

                List<SurveyQA> SurveyQuestionAnswerList = SurveyBL.SURVEY_ANSWER_PORFIL_ALL(whom_id);
                MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_ID(whom_id);               
                return View(SurveyQuestionAnswerList);              
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult SurveyAnswerDetails(int whom_id, int obje_id)
        {
            try
            {
                shareStreem survey = SurveyBL.SURVEY_GET_BY_ID(obje_id, Convert.ToInt32(Session["id"]));
                MemberProfil member = MemberProfilBL.MEMBER_PROFIL_GET_BY_ID(whom_id);
              
                    ViewBag.member = member;
                    ViewBag.survey = survey;
                    ViewBag.survey_id = obje_id;
                    ViewBag.whom_id = whom_id;
                    ViewBag.surveytext = survey.text;

                    return View(survey);
               
            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }

        public ActionResult SurveyAnswerProfilList(int whom_id, int obje_id)
        {
            try
            {

                List<SurveyAnswerItem> answerList = SurveyBL.SURVEY_ANSWER(obje_id, whom_id);
                List<SurveyQuestion> SurveyQuestionList = SurveyBL.SURVEY_ANSWER_ALL_QUESTION(obje_id);
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
                ViewBag.survey_id = obje_id;
                shareStreem shareObje = Share_MemberBL.SHARE_OBJECT_GET_BY_ID(obje_id);
                return View(shareObje);

            }
            catch
            {
                return RedirectToAction("Uppss", "Error", new { P = 0 });
            }
        }
    }
}