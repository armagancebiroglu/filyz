using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWebWeb.BL;

namespace WebWebWeb.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Uppss()
        {
            string message = "";
            if (!(Request.QueryString["P"] == null))
            {
                if (Convert.ToUInt32(Request.QueryString["P"]) == 0)
                {
                    message = "Bir yerlerde bir hata var. Sisteme tekrar girmeyi deneyebilirsiniz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 1)
                {
                    message = "Tekrar Eden Üye Kodu Hatası Oluştu. Lütfen Kayıt Sayfasını 'Yenile' yaparak Tekrar Kayıt Olunuz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 2)
                {
                    message = "ÜYE KODU ve ŞİFRE mail adresinize gönderilirken bir hata oluştu." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 3)
                {
                    message = "ÜYE KODU veya ŞİFRE de bir hata var. Sisteme tekrar girerek deneyiniz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 4)
                {
                    message = "Üyelik bilgileriniz güncellenirken bir hata oluştu. Tekrar deneyiniz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 5)
                {
                    message = "Görsel yüklenirken hata oluştu. Tekrar deneyiniz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 6)
                {
                    message = "Oturum Seçeneğiniz Boş Görünmekte. Lütfen Online/Offline Durumunu Seçiniz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 7)
                {
                    message = "Dosya boyutu 5MB tan daha büyük olamaz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 8)
                {
                    message = "Image dosyaları jpeg, jpg, png ve gif formatında olmalıdır." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 9)
                {
                    message = "Video dosyaları MP4, WebM, quicktime ve Ogg formatında olmalıdır." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                 else if (Convert.ToUInt32(Request.QueryString["P"]) == 10)                
                {
                    message = "Audio dosyaları MP3, Wav ve Ogg formatında olmalıdır." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                 else if (Convert.ToUInt32(Request.QueryString["P"]) == 11)
                {
                    message = "Dosya formatı ya da boyutu istenilen şekilde değil. Lütfen tekrar deneyiniz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 12)
                {
                    message = "Dosya silinemedi, lütfen tekrar deneyiniz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 13)
                {
                    message = "Silinecek dosya bulunamadı??" + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 14)
                {
                    message = "Albümde en fazla 5 görsel olabilir." + Environment.NewLine + " Yeni görsel eklemek için birini silmeniz gerekir." + Environment.NewLine + "Silinen görsel kişisel bilgi güvenliğiniz için sistemden geri dönülemez şekilde kaldırılır.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 15)
                {
                    message = "Ses dosyası yüklemek için ödeme yapıp üyeliğinizi yükseltmeniz gerekir." + Environment.NewLine + "Menuden ödeme sayfasına bakabilirsiniz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 16)
                {
                    message = "Video dosyası yüklemek için ödeme yapıp üyeliğinizi yükseltmeniz gerekir." + Environment.NewLine + "Menuden ödeme sayfasına bakabilirsiniz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 17)
                {
                    message = "Profil fotoğrafınız oluşturulamadı." + Environment.NewLine + "Daha sonra tekrar deneyiniz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 18)
                {
                    message = "Fotoğraf algılanamadı. " + Environment.NewLine + "Daha sonra tekrar deneyiniz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 19)
                {
                    message = "Albüm görsellerinin görüntülenmesiyle ilgili bir hata oluştu." + Environment.NewLine + "Sayfanızı tekrar yenileyin." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 20)
                {
                    message = "Veri tabanında bu görsele ait bilgi bulunamadı." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 21)
                {
                    message = "Paylaşım yapılamadı. Yazı ve Görsel olmadan Paylaşım Yapılamaz." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 22)
                {
                    message = "Konum bilgilerine ulaşılamadı. cihazınızın konum erişimini açınız" + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 23)
                {
                    message = "Şifre değiştirilemedi." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 24)
                {
                    message = "Haber kaynağına erişilemedi." + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 25)
                {
                    message = "Haber kaynağına öğesine ulaşılamadı. " + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 26)
                {
                    message = "Anket Konusu Boş Geçilemez. " + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 27)
                {
                    message = "Ankete soru eklemelisiniz. " + Environment.NewLine + " Tamamlanmayan anket yayınlanamaz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 28)
                {
                    message = "Ankete cevabı silinemedi. " + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 29)
                {
                    message = "Tüm Seçenekler Tam Değil. " + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 30)
                {
                    message = "Kendi Paylaşımınızda Değişiklik Yapabilirsiniz. " + Environment.NewLine + " Tabi Eğer Admin Değilsen :)";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 31)
                {
                    message = "Bildirim ile ilgi bir hata oluştu. " + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 32)
                {
                    message = "Bildirim isteği geçersiz. " + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 33)
                {
                    message = "Arama sayfasında bir hata var. " + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 34)
                {
                    message = "Üye kodu ile aramada bir hata oluştu veya aynı cinsten arama yapamazsınız. " + Environment.NewLine + " Oluşan Hata İçin Özür Dileriz";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 35)
                {
                    message = "Bu üye koduyla kayıtlı kimse yok. " + Environment.NewLine + " Tekrar deneyiniz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 36)
                {
                    message = "Konuma göre listelemede bir hata oluştu. " + Environment.NewLine + " Tekrar deneyiniz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 37)
                {
                    message = " Sayfa yüklenemedi. " + Environment.NewLine + " Tekrar deneyiniz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 38)
                {
                    message = " Paylaşım yapabilmeniz için üyeliğinizi yükseltmeniz gerekir. " + Environment.NewLine + " Üyelik Yükseltme İçin Menüden Ödeme Sayfasına Geçiniz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 39)
                {
                    message = " Mesaj yazabilmeniz için üyeliğinizi yükseltmeniz gerekir. " + Environment.NewLine + " Üyelik Yükseltme İçin Menüden Ödeme Sayfasına Geçiniz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 40)
                {
                    message = " Yorum yazabilmeniz için üyeliğinizi yükseltmeniz gerekir. " + Environment.NewLine + " Üyelik Yükseltme İçin Menüden Ödeme Sayfasına Geçiniz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 41)
                {
                    message = " Canlı yazışma yapabilmeniz için üyeliğinizi yükseltmeniz gerekir. " + Environment.NewLine + " Üyelik Yükseltme İçin Menüden Ödeme Sayfasına Geçiniz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 42)
                {
                    message = " Cevaplamadığınız zorunlu anket soruları bulunmaktadır. " + Environment.NewLine + " Kendinizi daha iyi anlatabilmeniz için bu soruları cevaplamanız gerekmektedir.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 43)
                {
                    message = " Silme işlemi gerçekleşmedi tekrar deneyiniz. " + Environment.NewLine + "  Oluşan Hata İçin Özür Dileriz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 44)
                {
                    message = " Mail gönderilemedi daha sonra tekrar deneyiniz. " + Environment.NewLine + "  Oluşan Hata İçin Özür Dileriz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 45)
                {
                    message = " Şifre değiştirilemedi daha sonra tekrar deneyiniz. " + Environment.NewLine + "  Oluşan Hata İçin Özür Dileriz.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 46)
                {
                    message = " Admin paneline ulaşmaya yetkiniz yok. " + Environment.NewLine + "  Durum için yönetimi arayınız.";
                }
                else if (Convert.ToUInt32(Request.QueryString["P"]) == 47)
                {
                    message = " Admin de hata yapar. " + Environment.NewLine + "  Hatasız kul olmaz.";
                }
            }//Buradan else if deyip diğer hatalar eklenecek. 1 için bu, 2 için bu nereden gelirse oraya yönlendirilecek yine.
            else
            {
                     message = "Bilinmeyen Bir Hata Oluştu!";
            }
           
            ViewBag.message = message;
            ViewBag.P = Convert.ToUInt32(Request.QueryString["P"]);
            return View(); 
        }
    }
}