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
    public class UtakmiceController : Controller
    {
        private EvropskoPrvenstvoUKosarciEntities1 db = new EvropskoPrvenstvoUKosarciEntities1();

        #region Index

        // GET: Utakmice
        public ActionResult Index()
        {
            if(Session["uloga"] != null)
            {
                var utakmice = db.Utakmice.Include(u => u.Grupa).Include(u => u.Hala).Include(u => u.Reprezentacija).Include(u => u.Reprezentacija1);
                return View(utakmice.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        #region Details

        // GET: Utakmice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utakmice utakmice = db.Utakmice.Find(id);
            if (utakmice == null)
            {
                return HttpNotFound();
            }
            return View(utakmice);
        }

        #endregion

        #region Create

        // GET: Utakmice/Create
        public ActionResult Create()
        {
            if (Session["uloga"] != null)
            {
                ViewBag.GrupaId = new SelectList(db.Grupa, "GrupaId", "NazivGrupe");
                ViewBag.HalaId = new SelectList(db.Hala, "HalaId", "NazivHale");
                ViewBag.ReprezentacijaDomacinId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija");
                ViewBag.ReprezentacijaGostId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Utakmice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UtakmiceId,ReprezentacijaDomacinId,ReprezentacijaGostId,GrupaId,HalaId,VremeOdigravanja,Q1_Domacin,Q1_Gost,Q2_Domacin,Q2_Gost,Q3_Domacin,Q3_Gost,Q4_Domacin,Q4_Gost,FinalDomacin,FinalGost")] Utakmice utakmice)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in db.Utakmice)
                {
                    if(item.VremeOdigravanja == utakmice.VremeOdigravanja && item.ReprezentacijaDomacinId == utakmice.ReprezentacijaDomacinId && item.ReprezentacijaGostId == utakmice.ReprezentacijaGostId)
                    {
                        return RedirectToAction("GreskaCreate", "Utakmice");
                    }
                    else
                    {
                        db.Utakmice.Add(utakmice);
                    }
                }                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrupaId = new SelectList(db.Grupa, "GrupaId", "NazivGrupe", utakmice.GrupaId);
            ViewBag.HalaId = new SelectList(db.Hala, "HalaId", "NazivHale", utakmice.HalaId);
            ViewBag.ReprezentacijaDomacinId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija", utakmice.ReprezentacijaDomacinId);
            ViewBag.ReprezentacijaGostId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija", utakmice.ReprezentacijaGostId);
            return View(utakmice);
        }

        #endregion

        #region Edit

        // GET: Utakmice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utakmice utakmice = db.Utakmice.Find(id);
                if (utakmice == null)
                {
                    return HttpNotFound();
                }
                ViewBag.GrupaId = new SelectList(db.Grupa, "GrupaId", "NazivGrupe", utakmice.GrupaId);
                ViewBag.HalaId = new SelectList(db.Hala, "HalaId", "NazivHale", utakmice.HalaId);
                ViewBag.ReprezentacijaDomacinId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija", utakmice.ReprezentacijaDomacinId);
                ViewBag.ReprezentacijaGostId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija", utakmice.ReprezentacijaGostId);
                return View(utakmice);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Utakmice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UtakmiceId,ReprezentacijaDomacinId,ReprezentacijaGostId,GrupaId,HalaId,VremeOdigravanja,Q1_Domacin,Q1_Gost,Q2_Domacin,Q2_Gost,Q3_Domacin,Q3_Gost,Q4_Domacin,Q4_Gost,FinalDomacin,FinalGost")] Utakmice utakmice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utakmice).State = EntityState.Modified;

                int brojac = 0;

                foreach (var item in db.Utakmice)
                {
                    if (item.VremeOdigravanja == utakmice.VremeOdigravanja && item.ReprezentacijaDomacinId == utakmice.ReprezentacijaDomacinId && item.ReprezentacijaGostId == utakmice.ReprezentacijaGostId)
                    {
                        brojac++;
                    }
                }

                if (brojac > 1)
                {
                    return RedirectToAction("GreskaEdit", "Utakmice");
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GrupaId = new SelectList(db.Grupa, "GrupaId", "NazivGrupe", utakmice.GrupaId);
            ViewBag.HalaId = new SelectList(db.Hala, "HalaId", "NazivHale", utakmice.HalaId);
            ViewBag.ReprezentacijaDomacinId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija", utakmice.ReprezentacijaDomacinId);
            ViewBag.ReprezentacijaGostId = new SelectList(db.Reprezentacija, "ReprezentacijaId", "NazivReprezentacija", utakmice.ReprezentacijaGostId);
            return View(utakmice);
        }

        #endregion

        #region Delete

        // GET: Utakmice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["uloga"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utakmice utakmice = db.Utakmice.Find(id);
                if (utakmice == null)
                {
                    return HttpNotFound();
                }
                return View(utakmice);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Utakmice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utakmice utakmice = db.Utakmice.Find(id);
            db.Utakmice.Remove(utakmice);
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
