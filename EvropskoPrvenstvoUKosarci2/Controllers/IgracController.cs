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
    public class IgracController : Controller
    {
        private EvropskoPrvenstvoUKosarciEntities1 db = new EvropskoPrvenstvoUKosarciEntities1();

        #region Index

        // GET: Igracs
        public ActionResult Index()
        {
            if (Session["uloga"] != null)
            {
                var igrac = db.Igrac.Include(i => i.Pozicija).Include(i => i.Reprezentacija);
                return View(igrac.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        #region Details

        // GET: Igracs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Igrac igrac = db.Igrac.Find(id);
                if (igrac == null)
                {
                    return HttpNotFound();
                }
                return View(igrac);
            }   
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        #region Create

        // GET: Igracs/Create
        public ActionResult Create()
        {
            if (Session["uloga"] != null)
            {
                ViewBag.PozicijaId = new SelectList(db.Pozicija, "PozicijaId", "NazivPozicije");
                ViewBag.ReprezentacijaId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija");
                return View();
            }  
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Igracs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IgracId,Ime,Prezime,Visina,Tezina,BrojDresa,PozicijaId,ReprezentacijaId")] Igrac igrac)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in db.Igrac)
                {
                    if(item.Ime == igrac.Ime && item.Prezime == igrac.Prezime && item.BrojDresa == igrac.BrojDresa && item.ReprezentacijaId == igrac.ReprezentacijaId)
                    {
                        return RedirectToAction("GreskaCreate", "Igrac");
                    }
                    else
                    {
                        db.Igrac.Add(igrac);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }            

            ViewBag.PozicijaId = new SelectList(db.Pozicija, "PozicijaId", "NazivPozicije", igrac.PozicijaId);
            ViewBag.ReprezentacijaId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija", igrac.ReprezentacijaId);
            return View(igrac);
        }

        #endregion

        #region Edit

        // GET: Igracs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Igrac igrac = db.Igrac.Find(id);
                if (igrac == null)
                {
                    return HttpNotFound();
                }
                ViewBag.PozicijaId = new SelectList(db.Pozicija, "PozicijaId", "NazivPozicije", igrac.PozicijaId);
                ViewBag.ReprezentacijaId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija", igrac.ReprezentacijaId);
                return View(igrac);
            }    
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Igracs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IgracId,Ime,Prezime,Visina,Tezina,BrojDresa,PozicijaId,ReprezentacijaId")] Igrac igrac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(igrac).State = EntityState.Modified;

                int brojac = 0;

                foreach (var item in db.Igrac)
                {
                    if (item.Ime == igrac.Ime && item.Prezime == igrac.Prezime && item.BrojDresa == igrac.BrojDresa && item.ReprezentacijaId == igrac.ReprezentacijaId)
                    {
                        brojac++;
                    }
                }

                if (brojac > 1)
                {
                    return RedirectToAction("GreskaEdit", "Igrac");
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PozicijaId = new SelectList(db.Pozicija, "PozicijaId", "NazivPozicije", igrac.PozicijaId);
            ViewBag.ReprezentacijaId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija", igrac.ReprezentacijaId);
            return View(igrac);
        }

        #endregion

        #region Delete

        // GET: Igracs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Igrac igrac = db.Igrac.Find(id);
                if (igrac == null)
                {
                    return HttpNotFound();
                }
                return View(igrac);
            }  
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Igracs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Igrac igrac = db.Igrac.Find(id);
            db.Igrac.Remove(igrac);
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
