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
    public class prizesController : Controller
    {
        private MyContext db = new MyContext();
        private DB MDB = new DB();
        // GET: prizes
        public ActionResult Index()
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
            return View(MDB.Pri_FindAll());
        }
       
        [HttpPost]
        public ActionResult Index(int? id)
        {
            string Username = User.Identity.GetUserName();
            if (Username.Length <= 0)
            {
                return RedirectToAction("Login", "Account");
            }
            if (Username != "admin")
            {
                return Content("<script>alert('Only administrators can operate');parent.location.href='/prizes/Details'</script>");
            }
            return View(MDB.Pri_FindAll());
        }

        // GET: prizes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    prize prize = db.prize.Find(id);
        //    if (prize == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var query = db.prize;
        //    return View(query);
        //}

        // GET: prizes/Create
        public ActionResult Create()
        {
            string Username = User.Identity.GetUserName();
            if (Username.Length <= 0)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: prizes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Price,Pic,Integral")] prize prize)
        {

            if (ModelState.IsValid)
            {
                string[] str = prize.Pic.Split('\\');
                prize.Pic = "~/Content/images/" + str[str.Count() - 1].ToString();
                MDB.Pri_Add(prize);
                MDB.Save();
                return RedirectToAction("Index");
            }

            return View(prize);
        }

        public ActionResult Details(int? id, int sel = 0,int Point=0)
        {
            string Username = User.Identity.GetUserName();
            bool flag=  MDB.calc(Point, Username);
            ViewBag.integral = DB.inte;
            if (Username.Length<=0)
            {
                return Content("<script>alert('You are not a member and cannot exchange prizes！！！');parent.location.href='/Account/Login'</script>");
            }
            if (sel == 3 & flag == false)
            {
                return Content("<script>alert('Not enough points to exchange for prizes！！！');parent.location.href='/prizes/Details'</script>");
            }
            string msg= MDB.PrizeSel(id, sel,Username);
            ViewBag.PpageNum = MDB.PpageNum;
            ViewBag.PpageNum += 1;
            IEnumerable<prize> query = Enumerable.Empty<prize>();
            if (msg == null)
            {
                query = db.prize.OrderBy(m => m.ProductID).Skip(MDB.PpageNum).Take(1);
            }
            else
            {
                msg = msg.Replace("\r\n", "");
                return Content("<script>alert('" +  msg + "');parent.location.href='/prizes/Details'</script>");
            }
            return View(query.ToList());
        }

        // GET: prizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string Username = User.Identity.GetUserName();
            if (Username.Length <= 0)
            {
                return RedirectToAction("Login", "Account");
            }
            if (Username != "admin")
            {
                return Content("<script>alert('Only administrators can operate');parent.location.href='/prizes/Details'</script>");
            }
            prize prize = db.prize.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // POST: prizes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,Price,Pic,Integral")] prize prize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prize).State = EntityState.Modified;
                string[] str = prize.Pic.Split('\\');
                prize.Pic = "~/Content/images/" + str[str.Count()-1].ToString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prize);
        }

        // GET: prizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prize prize = db.prize.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // POST: prizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            prize prize = MDB.GetPrize(id);
            MDB.Pri_Del(prize);
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
