using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MVCSTOK.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.Kategorilers.ToList();
            var degerler = db.Kategorilers.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }
        [HttpGet] //sayfa yüklenince
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost] //bir şeye nbasınca
        public ActionResult YeniKategori(Kategoriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.Kategorilers.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult Sil(int id)
        {
            var kategori = db.Kategorilers.Find(id);
            db.Kategorilers.Remove (kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var kat = db.Kategorilers.Find(id);
            return View("KategoriGetir",kat);
        }

        public ActionResult Guncelle(Kategoriler p1)
        {
            var kat = db.Kategorilers.Find(p1.KategoriID);
            kat.KategoriAdi = p1.KategoriAdi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}