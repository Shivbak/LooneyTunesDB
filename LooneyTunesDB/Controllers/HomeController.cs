using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LooneyTunesDB.Controllers
{
    public class HomeController : Controller
    {
        private looneytuneDBEntities db = new looneytuneDBEntities();
        // GET: Home
        public ActionResult Index(string searchString)
        {
            var display_data = from l in db.loonytunes
                               select l;

            if (!string.IsNullOrEmpty(searchString))
            {
                 display_data = display_data.Where(s => s.Name.Contains(searchString));
            }
          
            return View(display_data);
        }

        public ActionResult Like(int id)
        {
            loonytune loonytune = db.loonytunes.Find(id);

            int currentLikes = loonytune.Clike;
            loonytune.Clike = currentLikes + 1;


            if (ModelState.IsValid)
            {
                db.Entry(loonytune).State = EntityState.Modified;
                db.SaveChanges();
            }

            
            return RedirectToAction("Index");
        }

        public ActionResult Dislike(int id)
        {
            loonytune loonytune = db.loonytunes.Find(id);

            int currentDislikes = loonytune.Cdislike;
            loonytune.Cdislike = currentDislikes + 1;

            if(ModelState.IsValid)
            {
                db.Entry(loonytune).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            loonytune loonytune = db.loonytunes.Find(id);

            return View(loonytune);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            loonytune loonytune = db.loonytunes.Find(id);

            return View(loonytune);
        }

        [HttpPost]
        public ActionResult Edit(loonytune loonytune)
        {
            if (loonytune.Picture == null)
            {
                loonytune.Picture = "http://4vector.com/i/free-vector-looney-tunes-back-in-action_034129_looney-tunes-back-in-action.png";
            }
            if (ModelState.IsValid)
            {
      
                db.Entry(loonytune).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loonytune);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "id,Name,Bio,Fappearance,Species,Clike,Cdislike,Picture")] loonytune loonytune)
        {
            if(loonytune.Picture == null)
            {
                loonytune.Picture = "http://4vector.com/i/free-vector-looney-tunes-back-in-action_034129_looney-tunes-back-in-action.png";
            }
            if(ModelState.IsValid)
            {
                db.loonytunes.Add(loonytune);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loonytune);
        }
    }
}