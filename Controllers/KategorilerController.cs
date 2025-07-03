using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVCSTOK.Models.Entity;
using PagedList;
namespace MVCSTOK.Controllers
{
    public class KategorilerController : Controller
    {
        DBMvcStokEntities db = new Models.Entity.DBMvcStokEntities(); 
        public ActionResult Index(int sayfa=1)
        {
            var kategoriler = db.tblkategori.Where(x=> x.aktiflik==true).OrderBy(x => x.KategoriId).ToPagedList(sayfa, 10);
            return View(kategoriler);
        }


        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult KategoriEkle(tblkategori ktg)
        {
            if (!ModelState.IsValid)
            {
                return View(ktg); // ❗ Hataları tekrar view'a geri gönderiyoruz
            }
            db.tblkategori.Add(ktg);
            db.SaveChanges();           
            return RedirectToAction("Index");
        }


        public ActionResult KategoriSil(int id)
        {
            var ktg = db.tblkategori.Find(id);
            ktg.aktiflik = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult KategoriGuncelle(int id)
        {
            var ktg = db.tblkategori.Find(id);
            return View("KategoriGuncelle",ktg);
        }

        [HttpPost]
        public ActionResult KategoriGuncelle(tblkategori ktg)
        {
            var model = db.tblkategori.Find(ktg.KategoriId);
            model.KategoriAd = ktg.KategoriAd;
            model.Aciklama = ktg.Aciklama;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}