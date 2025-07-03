using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
namespace MVCSTOK.Controllers
{
    public class DashboardİstatistiklerController : Controller
    {
        // GET: Dashboardİstatistikler
        DBMvcStokEntities db = new DBMvcStokEntities();
        DashboardViewModel model = new DashboardViewModel();
        public ActionResult Index()
        {

            //toplam müşteri
            int musterisayisi = db.tblmusteri.Where(x=> x.aktiflik==true).Count();
            model.MusteriSayisi = musterisayisi;

            int urunsayisi = db.tblurunler.Where(x => x.aktiflik == true).Count();
            model.UrunSayisi= urunsayisi;

            int personelsayisi = db.tblpersonel.Where(x => x.aktiflik == true).Count();
            model.PersonelSayisi = personelsayisi;

            decimal toplamgelir = (decimal)db.tblstokhareketleri.Sum(x=>x.fiyat);
            model.ToplamGelir = toplamgelir;


            var SonUrunler = db.tblurunler.Where(x => x.aktiflik == true).OrderByDescending(x => x.EklenmeTarihi).Take(5).ToList();
            model.SonUrunler = SonUrunler;


            //Kritik stoktaki ürünler
            var KritikUrunler = db.tblurunler
                .Include(x => x.tblkategori)
                .Where(x => x.StokAdet <= 10 && x.aktiflik==true)
                .Take(5)
                .ToList(); 
            model.KritikUrunler = KritikUrunler;

            //son 7 günün satış trendi line chart
            var bugun = DateTime.Today;
            var yediGunOnce = bugun.AddDays(-6);

            var satisVerileri = db.tblstokhareketleri
                .Where(s => s.Tarih >= yediGunOnce)
                .GroupBy(s => DbFunctions.TruncateTime(s.Tarih))
                .Select(group => new
                {
                    Tarih = group.Key,
                    ToplamAdet = group.Sum(s => s.Miktar)
                })
                .OrderBy(x => x.Tarih)
                .ToList();

            ViewBag.SatisTarihleri = satisVerileri.Select(x => x.Tarih?.ToString("dd.MM") ?? "").ToList();
            ViewBag.SatisAdetleri = satisVerileri.Select(x => x.ToplamAdet).ToList();

            return View(model);
        }
    }
}