using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.Musterilers select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(p));
            }
            return View(degerler.ToList()) ;
            //var degerler = db.Musterilers.ToList();
            //return View(degerler);
        }

        [HttpGet] //sayfa yüklenince
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost] //bir şeye nbasınca
        public ActionResult YeniMusteri(Musteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Musterilers.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var musteri = db.Musterilers.Find(id);
            db.Musterilers.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.Musterilers.Find(id);
            return View("MusteriGetir",musteri);
        }
        public ActionResult Guncelle(Musteriler p1)
        {
            var musteri = db.Musterilers.Find(p1.MusteriID);
            musteri.MusteriAd = p1.MusteriAd;
            musteri.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}