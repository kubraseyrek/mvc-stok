using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.Urunlers.ToList();
            return View(degerler);
        }
        [HttpGet] //sayfa yüklenince
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.Kategorilers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAdi,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.deger = degerler;
            return View();
        }

        [HttpPost] //bir şeye nbasınca
        public ActionResult YeniUrun(Urunler p1)
        {
            var kat = db.Kategorilers.Where(m => m.KategoriID == p1.Kategoriler.KategoriID).FirstOrDefault();
            p1.Kategoriler = kat;
            db.Urunlers.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.Urunlers.Find(id);
            db.Urunlers.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.Urunlers.Find(id);
            List<SelectListItem> degerler = (from i in db.Kategorilers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAdi,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.deger = degerler;
            return View("UrunGetir",urun);
        }
        public ActionResult Guncelle(Urunler p)
        {
            var urun = db.Urunlers.Find (p.UrunID);
            urun.UrunAdi = p.UrunAdi;
            urun.Marka = p.Marka;
            urun.Stok = p.Stok;
            urun.Fiyat = p.Fiyat;
            //urun.UrunKategori = p.UrunKategori;
            var kat = db.Kategorilers.Where(m => m.KategoriID ==p.Kategoriler.KategoriID).FirstOrDefault();
            urun.UrunKategori = kat.KategoriID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}