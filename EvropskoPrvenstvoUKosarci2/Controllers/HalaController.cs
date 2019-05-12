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
    public class HalaController : Controller
    {
        private EvropskoPrvenstvoUKosarciEntities1 db = new EvropskoPrvenstvoUKosarciEntities1();

        #region Index

        // GET: Hala
        public ActionResult Index()
        {
            if (Session["uloga"] != null)
            {
                return View(db.Hala.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }    
        }

        #endregion

        #region Details

        // GET: Hala/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Hala hala = db.Hala.Find(id);
                if (hala == null)
                {
                    return HttpNotFound();
                }
                return View(hala);
            }  
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        #region Create

        // GET: Hala/Create
        public ActionResult Create()
        {
            if (Session["uloga"] != null)
            {
                return View();
            }                
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Hala/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HalaId,NazivHale,Adresa,Telefon")] Hala hala)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in db.Hala)
                {
                    if(item.NazivHale == hala.NazivHale)
                    {
                        return RedirectToAction("GreskaCreate", "Hala");
                    }
                    else
                    {
                        db.Hala.Add(hala);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hala);
        }

        #endregion

        #region Edit

        // GET: Hala/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Hala hala = db.Hala.Find(id);
                if (hala == null)
                {
                    return HttpNotFound();
                }
                return View(hala);
            }                
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Hala/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HalaId,NazivHale,Adresa,Telefon")] Hala hala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hala).State = EntityState.Modified;

                int brojac = 0;

                foreach (var item in db.Hala)
                {
                    if (item.NazivHale == hala.NazivHale || item.Adresa == hala.Adresa)
                    {
                        brojac++;
                    }
                }

                if (brojac > 1)
                {
                    return RedirectToAction("GreskaEdit", "Hala");
                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hala);
        }

        #endregion

        #region Delete

        // GET: Hala/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Hala hala = db.Hala.Find(id);
                if (hala == null)
                {
                    return HttpNotFound();
                }
                return View(hala);
            }                
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Hala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hala hala = db.Hala.Find(id);
            db.Hala.Remove(hala);
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
