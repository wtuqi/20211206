using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using mmsc.Models;
using System.Web.Security;
//using DotNetOpenAuth.AspNet;
//using Microsoft.Web.WebPages.OAuth;
//using WebMatrix.WebData;

namespace mmsc.Controllers
{
    public class LoginController : Controller
    {
        //private mmscEntities db = new mmscEntities();

        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

 
        //
        // GET: /Login/Details/5

        public ActionResult Details()
        {
            return View();
        }

        //
        // GET: /Login/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create

        //[HttpPost]
        //public ActionResult Create(sys_user_role sys_user_role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.sys_user_role.AddObject(sys_user_role);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(sys_user_role);
        //}

        //
        // GET: /Login/Edit/5

        public ActionResult Edit()
        {
            //sys_user_role sys_user_role = db.sys_user_role.Single(s => s.id == id);
            //if (sys_user_role == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        //
        // POST: /Login/Edit/5

        //[HttpPost]
        //public ActionResult Edit(sys_user_role sys_user_role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.sys_user_role.Attach(sys_user_role);
        //        db.ObjectStateManager.ChangeObjectState(sys_user_role, EntityState.Modified);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(sys_user_role);
        //}

        //
        // GET: /Login/Delete/5

        public ActionResult Delete()
        {
            //sys_user_role sys_user_role = db.sys_user_role.Single(s => s.id == id);
            //if (sys_user_role == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        //
        // POST: /Login/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            //sys_user_role sys_user_role = db.sys_user_role.Single(s => s.id == id);
            //db.sys_user_role.DeleteObject(sys_user_role);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}