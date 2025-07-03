using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class AdminController : Controller
    {
        DBMvcStokEntities db = new DBMvcStokEntities();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(tblkullanici kullanici)
        {
            kullanici.Rol = "Admin";
            db.tblkullanici.Add(kullanici);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}