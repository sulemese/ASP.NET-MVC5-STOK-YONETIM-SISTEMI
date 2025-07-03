using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class GirisController : Controller
    {
        DBMvcStokEntities db = new DBMvcStokEntities();
        // GET: Auth
        [HttpGet]
        public ActionResult GirisYap()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(tblkullanici kullanici)
        {
            //kullanıcı adı ve şifresini giren kişinin böyle bir kayıt dbde varmı buna bakılır. 
            var kayıt = db.tblkullanici.FirstOrDefault(x => x.KullaniciAd == kullanici.KullaniciAd && x.Sifre == kullanici.Sifre);

            if (kayıt != null)
            {
                // Yetki çerezi oluşturulur (Rol bilgisi dahil)
                FormsAuthentication.SetAuthCookie(kayıt.KullaniciAd, false);
                // Session’a ek bilgiler yazılır
                Session["Kullanici"] = kayıt.KullaniciAd;
                Session["Rol"] = kayıt.Rol;

                // Role göre yönlendirme
                if (kayıt.Rol == "Admin")
                    return RedirectToAction("Index", "HomePage"); // Admin panel sayfası
                else
                    return RedirectToAction("GirisYap", "Giris");
            }
            ViewBag.Hata = "Geçersiz kullanıcı adı veya şifre";
            return View();

        }

      

    }


        
     

    

}