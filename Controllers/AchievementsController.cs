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
    public class AchievementsController : Controller
    {
        private DB MDB = new DB();
        private MyContext db = new MyContext();
        // GET: Achievements

        public ActionResult Index(string UserName = null, int F = 0)
        {
    
            DB.Username = User.Identity.GetUserName();
            ViewBag.Uname = DB.Username;
            if (DB.Username.Length <= 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var a = MDB.ToTB(UserName, F);
            ViewBag.APageNum = MDB.ApageNum;
            ViewBag.APageNum += 1;
            return View(a);
        }

        // GET: Achievements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = MDB.GetAchievement(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // GET: Achievements/Create
        public ActionResult Create()
        {

            string Username = User.Identity.GetUserName();
            if (Username.Length <= 0)
            {
                return RedirectToAction("Login", "Account");
            }
            if (Username != "admin")
            {
                return Content("<script>alert('Only administrators can operate');parent.location.href='/Achievements/index'</script>");
            }
            ViewBag.List = new SelectList(MDB.listItem, "Text", "Value");
            return View();
        }

        // POST: Achievements/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,AchievementID,AchievementName,score,AchieveTime,Description")] Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                MDB.Ach_Add(achievement);
                MDB.Save();
                return RedirectToAction("Index");
            }
            else
            {
                MDB.listItem.Add(new SelectListItem { Text = "DD", Value = "DD" });
                ViewBag.List = new SelectList(MDB.listItem, "Text", "Value");
            }
            return View(achievement);
        }

        // GET: Achievements/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.List = new SelectList(MDB.listItem, "Text", "Value");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = MDB.GetAchievement(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: Achievements/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,AchievementID,AchievementName,score,AchieveTime,Description")] Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(achievement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                MDB.listItem.Add(new SelectListItem { Text = "DD", Value = "DD" });
                ViewBag.List = new SelectList(MDB.listItem, "Text", "Value");
            }
            return View(achievement);
        }

        // GET: Achievements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = MDB.GetAchievement(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Achievement achievement = MDB.GetAchievement(id);
            MDB.Ach_Del(achievement);
            MDB.Save();
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
