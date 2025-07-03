using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
using PagedList;

namespace MVCSTOK.Controllers
{
    public class ÜrünlerController : Controller
    {
        // GET: Ürünler
        DBMvcStokEntities db = new DBMvcStokEntities();
        public ActionResult Index(string arama ,int sayfa= 1)
        {
            var urunler = db.tblurunler.Where(x => x.aktiflik == true).OrderBy(x => x.UrunId).ToPagedList(sayfa, 10);
            if(!string.IsNullOrEmpty(arama))
            {
                urunler = db.tblurunler.Where(x => x.UrunAd.Contains(arama) && x.aktiflik==true).OrderBy(x=> x.UrunId).ToPagedList(sayfa, 10);
            }
            return View(urunler);
        }

        [HttpGet]
        public ActionResult ÜrünEkle()
        {
            // Aktif kategorileri çekip ViewBag'e doldur
            ViewBag.KategoriListesi = db.tblkategori.Where(k => k.aktiflik == true).Select(k => new SelectListItem
            {
                Text = k.KategoriAd,
                Value = k.KategoriId.ToString()
            }).ToList();
         
            return View();
        }


        [HttpPost]
        public ActionResult ÜrünEkle(tblurunler urun)
        {
           
            if (!ModelState.IsValid)
            {
                // ❗ Hatalı gönderimde ViewBag tekrar tanımlanmalı
                ViewBag.KategoriListesi = db.tblkategori.Where(k => k.aktiflik == true).Select(k => new SelectListItem
                {
                    Text = k.KategoriAd,
                    Value = k.KategoriId.ToString()
                }).ToList();

                return View(urun);
            }

            // Geçerli gönderimde ürün ekleniyor
            urun.EklenmeTarihi = DateTime.Now;          
            db.tblurunler.Add(urun);
            db.SaveChanges();
            return RedirectToAction("Index");        
        }

        public ActionResult ÜrünSil(int id)
        {
            var urun = db.tblurunler.Find(id);
            if (urun == null)
            {
                return HttpNotFound(); // ürün bulunamadıysa 404 dön
            }
            urun.aktiflik = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult ÜrünGüncelle(int id)
        {
            var urun = db.tblurunler.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            // Kategori dropdownlist için
            ViewBag.KategoriListesi = db.tblkategori.Where(k => k.aktiflik == true).Select(k => new SelectListItem
            {
                Text = k.KategoriAd,
                Value = k.KategoriId.ToString()
                
            }).ToList();


            return View("ÜrünGüncelle", urun);
        }

        [HttpPost]
        public ActionResult ÜrünGüncelle(tblurunler model)
        {
            //forma hatalı veri gönderildiyse form yeniden doldurulur
            if (!ModelState.IsValid)
            {
                // Kategori dropdownlist için
                ViewBag.KategoriListesi = db.tblkategori.Where(k => k.aktiflik == true).Select(k => new SelectListItem
                {
                    Text = k.KategoriAd,
                    Value = k.KategoriId.ToString()
                }).ToList();

                return View(model);
            }

            //güncelleme yapılacak kayıt dbde bulunur.
            var urun = db.tblurunler.Find(model.UrunId);
            if (urun == null)
            {
                return HttpNotFound();
            }
            //kayıt değişkenine veriler atanır
            urun.UrunAd = model.UrunAd;
            urun.marka = model.marka;
            urun.StokAdet = model.StokAdet;
            urun.alisfiyat = model.alisfiyat;
            urun.satisfiyat = model.satisfiyat;
            urun.KategoriId = model.KategoriId;
            //dbye herşey kaydedilir.
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}