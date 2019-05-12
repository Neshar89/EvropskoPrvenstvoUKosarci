using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EvropskoPrvenstvoUKosarci2.Controllers
{
    public class PrvenstvoController : Controller
    {
        #region Pocetna
        // GET: Prvenstvo
        public ActionResult Pocetna()
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

        #endregion
    }
}