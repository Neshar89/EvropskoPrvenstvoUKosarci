using EvropskoPrvenstvoUKosarci2.Models; //d
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; //d

namespace EvropskoPrvenstvoUKosarci2.Controllers
{
    public class AccountController : Controller
    {
        private EvropskoPrvenstvoUKosarciEntities1 db = new EvropskoPrvenstvoUKosarciEntities1();

        #region Login
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<Korisnici> korisnici = (from k in db.Korisnici
                                             where k.KorisnickoIme != null
                                             select k).ToList();

                int duzinaListe;
                duzinaListe = korisnici.Count;

                var ulogovan = db.Korisnici.SingleOrDefault(m => m.KorisnickoIme == model.KorisnickoIme && m.Sifra == model.Sifra);

                for (int i = 0; i < duzinaListe; i++)
                {
                    if (model.KorisnickoIme != korisnici[i].KorisnickoIme && model.Sifra != korisnici[i].Sifra)
                    {
                        continue;
                    }

                    if (model.KorisnickoIme == korisnici[i].KorisnickoIme && model.Sifra == korisnici[i].Sifra)
                    {

                        if (ulogovan != null)
                        {

                            Session["uloga"] = ulogovan.Uloga;
                            return RedirectToAction("Pocetna", "Prvenstvo");
                        }

                        else
                        {
                            return View(model);
                        }
                    }
                }
                return RedirectToAction("Greska", "Account");
            }
            return View(model);
        }

        #endregion

        #region Logout

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
           
        }

        #endregion

        #region Greska

        public ActionResult Greska()
        {
            return View();
        }

        #endregion
    }
}