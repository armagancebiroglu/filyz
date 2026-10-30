using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWebWeb.Models
{
    public class HELP
    {

    }
    public enum sort
    {
        image = 0,
        image180 = 1,
        image360 = 2,
        video = 4,
        audio = 8,
        location = 16,
        survey = 32,
        album=64
    }
   
    public enum status
    {
        in_review = 0,        //incelemede
        member = 1,           //ödeme yapmamış üye
        gold_member = 2,      //ödeme yapmış üye
        fake = 4,             //uydurma üye
        forbiddenMember = 8,  //yasaklı kelime kullanan üye
        editor=16, //admin
        promo_member=32//promosyonlu üye
    }
    public enum confrim
    {
        in_review = 0,//incelemede olan normal paylaşım
        ShareObject = 1,//normal paylaşım---complained = 1(şikayet edildi idi)
        promo = 2,//reklam
        advert = 4,//ilan
        doctrine = 8,//öğretici bilgi
        surveyAdmin=16,//zorunlu admin anketi
        surveyEditor=32,//seçmeli admin anketi
        //approwed=64 //denetlenmiş onaylı paylaşım
    }

    public enum active
    {
        passive=0,
        active=1        
    }
    public enum noticeTableSort 
    {//bildirimler buradan da yönlendirilebilecek.
        ShareObject= 0,
        albums = 1, 
        Notice = 2,
        Favorite = 4,
        message=8,      
        Tag = 16,
        MemberProfil=32//complaint şikayet de edilebilsin diye kendi profil sayfasında kullanmak için 
            
    }
  public enum favoriBlocked
    {
        noutral=0,
        favorite=1,
        blocked=2 
    }
}