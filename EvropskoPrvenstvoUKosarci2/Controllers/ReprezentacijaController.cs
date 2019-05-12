using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EvropskoPrvenstvoUKosarci2.Models;

namespace EvropskoPrvenstvoUKosarci2.Controllers
{
    public class ReprezentacijaController : Controller
    {
        private EvropskoPrvenstvoUKosarciEntities1 db = new EvropskoPrvenstvoUKosarciEntities1();

        #region Index

        // GET: Reprezentacijas
        public ActionResult Index()
        {
            if (Session["uloga"] != null)
            {
                var reprezentacija = db.Reprezentacija.Include(r => r.Zemlje);
                return View(reprezentacija.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }   
        }

        #endregion

        #region Details

        // GET: Reprezentacijas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reprezentacija reprezentacija = db.Reprezentacija.Find(id);
            if (reprezentacija == null)
            {
                return HttpNotFound();
            }
            return View(reprezentacija);
        }

        #endregion

        #region Create

        // GET: Reprezentacijas/Create
        public ActionResult Create()
        {
            if (Session["uloga"] != null)
            {
                ViewBag.ZemljeId = new SelectList(db.Zemlje, "ZemljeId", "NazivZemlje");
                return View();
            } 
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Reprezentacijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReprezentacijaId,NazivReprezentacija,ZemljeId")] Reprezentacija reprezentacija)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in db.Reprezentacija)
                {
                    if(item.ZemljeId == reprezentacija.ZemljeId)
                    {
                        return RedirectToAction("GreskaCreate", "Reprezentacija");
                    }
                    else
                    {
                        db.Reprezentacija.Add(reprezentacija);
                        
                    }  
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ZemljeId = new SelectList(db.Zemlje, "ZemljeId", "NazivZemlje", reprezentacija.ZemljeId);
            return View(reprezentacija);
        }

        #endregion

        #region Edit

        // GET: Reprezentacijas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reprezentacija reprezentacija = db.Reprezentacija.Find(id);
                if (reprezentacija == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ZemljeId = new SelectList(db.Zemlje, "ZemljeId", "NazivZemlje", reprezentacija.ZemljeId);
                return View(reprezentacija);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Reprezentacijas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReprezentacijaId,NazivReprezentacija,ZemljeId")] Reprezentacija reprezentacija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reprezentacija).State = EntityState.Modified;

                int brojac = 0;


                foreach (var item in db.Reprezentacija)
                {
                    if (item.ZemljeId == reprezentacija.ZemljeId)
                    {
                        brojac++;
                    }
                }

                if (brojac > 1)
                {
                    return RedirectToAction("GreskaEdit", "Reprezentacija");
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ZemljeId = new SelectList(db.Zemlje, "ZemljeId", "NazivZemlje", reprezentacija.ZemljeId);
            return View(reprezentacija);
        }

        #endregion

        #region Delete

        // GET: Reprezentacijas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reprezentacija reprezentacija = db.Reprezentacija.Find(id);
                if (reprezentacija == null)
                {
                    return HttpNotFound();
                }
                return View(reprezentacija);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Reprezentacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reprezentacija reprezentacija = db.Reprezentacija.Find(id);
            db.Reprezentacija.Remove(reprezentacija);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Override

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Greska

        public ActionResult GreskaCreate()
        {
            return View();
        }

        public ActionResult GreskaEdit()
        {
            return View();
        }

        #endregion
    }
}
