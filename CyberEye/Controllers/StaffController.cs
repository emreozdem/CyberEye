using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CyberEye.Models.Entity;


namespace CyberEye.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        CyberEyeEntities1 db = new CyberEyeEntities1();
        public ActionResult Index(string s)
        {
            var degerler = from d in db.TBLSTAFF select d;
            if(!string.IsNullOrEmpty(s))
            {
                degerler = degerler.Where(m => m.NAME.Contains(s));
            }
            return View(degerler.ToList());
            //var degerler = db.TBLSTAFF.ToList();
            //return View(degerler);
        }

        [HttpGet]
        public ActionResult Add()
        {
            List<SelectListItem> degerler = (from i in db.TBLDEPARTMENT.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Department,
                                                 Value = i.DEPARTMENTID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult Add(TBLSTAFF p1)
        {
           
            var ktg = db.TBLDEPARTMENT.Where(m => m.DEPARTMENTID == p1.TBLDEPARTMENT.DEPARTMENTID).FirstOrDefault();
            p1.TBLDEPARTMENT = ktg;
            db.TBLSTAFF.Add(p1);

            

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var emp = db.TBLSTAFF.Find(id);
            db.TBLSTAFF.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var emp = db.TBLSTAFF.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLDEPARTMENT.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Department,
                                                 Value = i.DEPARTMENTID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("Edit", emp);
        }

        public ActionResult Update(TBLSTAFF p1)
        {
            var emp = db.TBLSTAFF.Find(p1.STAFFID);
            emp.NAME = p1.NAME;
            emp.SURNAME = p1.SURNAME;
            emp.BIRTHDAY = p1.BIRTHDAY;
            emp.SALLARY = p1.SALLARY;
            emp.TCNO = p1.TCNO;
            emp.IBAN = p1.IBAN;
            emp.ADDRESS = p1.ADDRESS;
            emp.PHONE = p1.PHONE;
            
            var ktg = db.TBLDEPARTMENT.Where(m => m.DEPARTMENTID == p1.TBLDEPARTMENT.DEPARTMENTID).FirstOrDefault();
            emp.DEPARMENTID = ktg.DEPARTMENTID;
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}