using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using CyberEye.Models.Entity;

namespace CyberEye.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security

        CyberEyeEntities1 db = new CyberEyeEntities1();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TBLUSER u)
        {
            var details = db.TBLUSER.FirstOrDefault(x => x.username == u.username && x.password == u.password);
            if (details != null)
            {
                FormsAuthentication.SetAuthCookie(details.NAME, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}