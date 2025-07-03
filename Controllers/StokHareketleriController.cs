using MVCSTOK.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSTOK.Controllers
{
    public class StokHareketleriController : Controller
    {
        // GET: StokHareketleri
        DBMvcStokEntities db = new DBMvcStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var satislar = db.tblstokhareketleri.OrderBy(x => x.HareketId).ToPagedList(sayfa, 10);
            return View(satislar);
        }

        [HttpGet]
        public ActionResult SatisEkle()
        {
            // Aktif müşterileri çekip ViewBag'e doldur
            ViewBag.MusteriListesi = db.tblmusteri.Where(k => k.aktiflik == true).Select(k => new SelectListItem
            {
                Text = k.MusteriAd+" "+k.MusteriSoyad,
                Value = k.MüsteriId.ToString()
            }).ToList();

            // Aktif ürünleri çekip ViewBag'e doldur
            ViewBag.UrunListesi = db.tblurunler.Where(k => k.aktiflik == true).Select(k => new SelectListItem
            {
                Text = k.UrunAd,
                Value = k.UrunId.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult SatisEkle(tblstokhareketleri satis)
        {
            //eğer geçerli değilse forma yeniden dönülür 
            if (!ModelState.IsValid)
            {
                // Aktif müşterileri çekip ViewBag'e doldur
                ViewBag.MusteriListesi = db.tblmusteri.Where(k => k.aktiflik == true).Select(k => new SelectListItem
                {
                    Text = k.MusteriAd + " " + k.MusteriSoyad,
                    Value = k.MüsteriId.ToString()
                }).ToList();

                // Aktif ürünleri çekip ViewBag'e doldur
                ViewBag.UrunListesi = db.tblurunler.Where(k => k.aktiflik == true).Select(k => new SelectListItem
                {
                    Text = k.UrunAd,
                    Value = k.UrunId.ToString()
                }).ToList();


                return View(satis);
            }

            // Geçerli gönderimde satış ekleniyor ve satış listesine gidiliyor
            satis.Tarih = DateTime.Now;          
            var islem = db.tblislemler.Where(x => x.İslemTipi == "Satış").FirstOrDefault();
            satis.İşlemId = islem.İslemId;
            //!PERSONEL İŞLEMLERİ BİTİNCE BURAYA İŞLEMİ YAPAN PERSONELİ DE GETİRMEYİ UNUTMA
            db.tblstokhareketleri.Add(satis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}