using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Integral_exchange.Models;
using Microsoft.AspNet.Identity;

namespace Integral_exchange.Controllers
{
    public class WantListsController : Controller
    {
        private MyContext db = new MyContext();

        // GET: WantLists
        public ActionResult Index()
        {
            string Username = User.Identity.GetUserName();
            if (Username.Length <= 0)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(db.WantList.ToList());
        }

        // GET: WantLists/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WantList wantList = db.WantList.Find(id);
            if (wantList == null)
            {
                return HttpNotFound();
            }
            return View(wantList);
        }

        // GET: WantLists/Create
        public ActionResult Create()
        {
            string Username = User.Identity.GetUserName();
            if (Username.Length <= 0)
            {
                return RedirectToAction("Login", "Account");
            }
            if (Username != "admin")
            {
                return Content("<script>alert('Only administrators can operate');parent.location.href='/WantLists/index'</script>");
            }
            return View();
        }

        // POST: WantLists/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,ProductID,ProductName")] WantList wantList)
        {
            if (ModelState.IsValid)
            {
                db.WantList.Add(wantList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wantList);
        }

        // GET: WantLists/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WantList wantList = db.WantList.Find(id);
            if (wantList == null)
            {
                return HttpNotFound();
            }
            return View(wantList);
        }

        // POST: WantLists/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,ProductID,ProductName")] WantList wantList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wantList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wantList);
        }

        // GET: WantLists/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WantList wantList = db.WantList.Find(id);
            if (wantList == null)
            {
                return HttpNotFound();
            }
            return View(wantList);
        }

        // POST: WantLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WantList wantList = db.WantList.Find(id);
            db.WantList.Remove(wantList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
