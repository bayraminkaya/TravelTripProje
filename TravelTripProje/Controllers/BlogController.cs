using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTripProje.Models.Siniflar;

namespace TravelTripProje.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        Context c = new Context();
        BlogYorum by = new BlogYorum();
        public ActionResult Index()
        {
            by.Deger1=c.Blogs.ToList();
            by.Deger3 = c.Blogs.OrderByDescending(x => x.ID).Take(3).ToList();
            //var bloglar = c.Blogs.ToList();
            return View(by);
        }
        
        public ActionResult BlogDetay(int id)
        {
             
            //var blogbul = c.Blogs.Where(x => x.ID == id).ToList();
            by.Deger1=c.Blogs.Where(x => x.ID == id).ToList();
            by.Deger2=c.Yorumlars.Where(x => x.Blogid == id).ToList();
            return View(by);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YorumYap(Yorumlar y)
        {
            if (!ModelState.IsValid)
            {
                // Hatalı durumlarda aynı detay sayfasına geri dön
                TempData["YorumHata"] = "Lütfen tüm alanları doldurun.";
                return RedirectToAction("BlogDetay", new { id = y.Blogid });
            }

            c.Yorumlars.Add(y);
            c.SaveChanges();

            TempData["YorumOk"] = "Yorumunuz kaydedildi.";
            // ÖNEMLİ: Redirect! Böylece F5’te tekrar POST yapılmaz.
            return RedirectToAction("BlogDetay", new { id = y.Blogid });
        }
        [HttpGet]
        public PartialViewResult YorumYap(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }

    }
}