using Entities.DTOs.UsersDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {

        public static string HataSonuc="Hatalı işlem";
        public static string UserNotFound = "Kullanıcı bulunamadı";



        public static string UserRegistered = "Kullanıcı kayıt oldu";

        public static string SuccessfulLogin = "Başarılı kayıt";
        public static string UserAlreadyExists = "kullanıcı mevcut";

        public static string SuccessfulLog = "Başarılı bir giriş yapıldı";
        public static string PasswordError = "pasaword hatalı";
        public static string UserInfo="Kullanıcı Bilgiler;";
        public static string UserUpdate="Kullanıcı bilgileri güncellendi";
        public static string UserAdded="Kullanıcı eklendi ";
        public static string AuthorizationDenied="Yetkiniz yok";
        public static string AccessTokenCreated="Token üretildi";
        public static string CustomerNot = "Müşteri bulunamadı";
        public static string CustomerAdded = "Müşteri eklendi";
        public static string CustomerDelete = "Müşteri silindi";
        public static string CustomerListed = "Müşteriler listelendi";
        public static string CustomerGet = "Müşteri getirildi";
        public static string CustomerUpdate = "Müşteri güncellendi";
        public static string NoteIsNotShared="Not paylaşılabilir Değil !!!";
        public static string BookDetail="Detaylar getirildi";
        public static string BookUpdate = "Güncelleme Başarılı";
        public static string BookAdd = "Ekleme başarılı";
        public static string BookByNote="Kitabın Notları Listelendi";
        public static string UserByNote="Kullanıcının Notları Listelendi";
        public static string NoteUpdate = "Güncelleme Başarılı";
        public static string NoteList = "Lİsteleme başarılı";
        public static string NoteDetail="Detaylar getirildi";
        public static string NoteDelete = "Silme başarılı";
        public static string NoteAdd = "Ekleme başarılı";
        public static string ShareTrue="Paylaşım başarılı";
        public static string ShareFail= "Paylaşım başarısız";
        public static string ShareUpdate = "Güncelleme Başarılı";
        public static string ShareDetail="Detaylar getirildi";
        public static string ShareAdd = "Ekleme başarılı";
        public static string ShareDelete = "Silme başarılı";
        public static string ShelfdAdd = "Ekleme başarılı";
        public static string ShelfdList = "Lİsteleme başarılı";
        public static string ShelfDetail = "Detaylar getirildi";
        public static string ShareNot = "Paylaşım yapılamaz";
        public static string NotFriend="Paylaşılan kişi arkadaş değil";
        public static string UserFriendList="Kullanıcı arkadaşları listelendi";
        public static string FriendNotFound ="Kullanıcının arkadaşı bulunmamaktadır";
        public static string FriendAdd = "Arkadaş eklendi";
        public static string UserDetailList="Kullanıcı bilgileri listelendi";
        public static string BookShelfDetail="Kitaplar raf biligileriyle listelenmiştir";
        public static string BooksList="Kitaplar listelendi";
        public static string BookFilter="Kitaplar uygun biçimde filtrelendi";
        public static string ClaimAdded="Claim eklendi";
        public static string UserAddClaim="Kullanıcıya claim eklendi";
    }
}
