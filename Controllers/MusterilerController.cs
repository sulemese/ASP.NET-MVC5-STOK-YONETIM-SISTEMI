using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
using PagedList;

namespace MVCSTOK.Controllers
{
    
    public class MusterilerController : Controller
    {
        DBMvcStokEntities db = new DBMvcStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var musteriler = db.tblmusteri.Where(x=>x.aktiflik==true).OrderBy(x=>x.MüsteriId).ToPagedList(sayfa,10);
            return View(musteriler);
        }

        [HttpGet]
        public ActionResult MusteriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MusteriEkle(tblmusteri musteri)
        {
            //eğer geçerli değilse forma yeniden dönülür 
            if (!ModelState.IsValid)
            {
                return View(musteri);
            }

            // Geçerli gönderimde müşteri ekleniyor ve müşteri listesine gidiliyor
            musteri.MusteriEklenmeTarihi = DateTime.Now;
            db.tblmusteri.Add(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(int id)
        {
            var musteri = db.tblmusteri.Find(id);
            if (musteri == null)
            {
                return HttpNotFound(); // ürün bulunamadıysa 404 dön
            }
            musteri.aktiflik = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult MusteriGuncelle(int id)
        {
            var musteri = db.tblmusteri.Find(id);
            if (musteri == null)
            {
                return HttpNotFound();
            }
            
            return View("MusteriGuncelle", musteri);
        }

        [HttpPost]
        public ActionResult MusteriGuncelle(tblmusteri model)
        {
            //forma hatalı veri gönderildiyse form yeniden doldurulur
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //güncelleme yapılacak kayıt dbde bulunur.
            var musteri = db.tblmusteri.Find(model.MüsteriId);
            if (musteri == null)
            {
                return HttpNotFound();
            }
            //kayıt değişkenine veriler atanır
            musteri.MusteriAd = model.MusteriAd;
            musteri.MusteriSoyad = model.MusteriSoyad;
            musteri.sehir = model.sehir;
            musteri.MusteriTelefon = model.MusteriTelefon;
            musteri.MusteriAdres = model.MusteriAdres;
            musteri.MusteriEmail = model.MusteriEmail;
            musteri.bakiye = model.bakiye;
            //dbye herşey kaydedilir.
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}