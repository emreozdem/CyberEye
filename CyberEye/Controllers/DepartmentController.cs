using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CyberEye.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace CyberEye.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        CyberEyeEntities1 db = new CyberEyeEntities1();

        [Authorize]
        public ActionResult Index(string s)
        {
            var degerler = from d in db.TBLDEPARTMENT select d;
            if (!string.IsNullOrEmpty(s))
            {
                degerler = degerler.Where(m => m.Department.Contains(s));
            }
            return View(degerler.ToList());
            //var degerler = db.TBLDEPARTMENT.ToList();
            // return View(degerler);
        }
        [HttpGet]
        public ActionResult Add()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Add(TBLDEPARTMENT p1)
        {
            if(!ModelState.IsValid)
            {
                return View("Add");
            }

            db.TBLDEPARTMENT.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var dep = db.TBLDEPARTMENT.Find(id);
            db.TBLDEPARTMENT.Remove(dep);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var dep = db.TBLDEPARTMENT.Find(id);
            return View("Edit", dep);
        }
        public ActionResult Update(TBLDEPARTMENT p1)
        {
            var dep = db.TBLDEPARTMENT.Find(p1.DEPARTMENTID);
            dep.Department = p1.Department;
            if (!ModelState.IsValid)
            {
                return View("Edit", dep);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}