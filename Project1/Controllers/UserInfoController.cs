using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project1.Models;

namespace Project1.Controllers
{
    public class UserInfoController : Controller
    {
        private AlorOvijatriEntities db = new AlorOvijatriEntities();

        // GET: /UserInfo/
        public ActionResult Index()
        {
            var query = (from User in db.tbl_UserInfo
                         where (User.UserTypeID != 1)
                         select User).ToList();
            //var tbl_userinfo = db.tbl_UserInfo.Include(t => t.tbl_UserType);
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"]!=null)
            {
                return View(query);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: /UserInfo/Details/5
        public ActionResult Details(string id)
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbl_UserInfo tbl_userinfo = db.tbl_UserInfo.Find(id);
                if (tbl_userinfo == null)
                {
                    return HttpNotFound();
                }
                return View(tbl_userinfo);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: /UserInfo/Create
        public ActionResult Create()
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                ViewBag.UserTypeID = new SelectList(db.tbl_UserType, "UserTypeID", "UserTypeName");
                return View();
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: /UserInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserTypeID,Name,FatherName,MotherName,DOB,PresentAddress,PermanentAddress,Occupation,Mobile,NID,Email,IsActive,Password,ConfirmPassword")] tbl_UserInfo tbl_userinfo)
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                db.tbl_UserInfo.Add(tbl_userinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: /UserInfo/Edit/5
        public ActionResult Edit(string id)
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbl_UserInfo tbl_userinfo = db.tbl_UserInfo.Find(id);
                if (tbl_userinfo == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserTypeID = new SelectList(db.tbl_UserType, "UserTypeID", "UserTypeName", tbl_userinfo.UserTypeID);
                return View(tbl_userinfo);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: /UserInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserTypeID,Name,FatherName,MotherName,DOB,PresentAddress,PermanentAddress,Occupation,Mobile,NID,Email,IsActive,Password,ConfirmPassword")] tbl_UserInfo tbl_userinfo)
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            { 
                db.Entry(tbl_userinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: /UserInfo/Delete/5
        public ActionResult Delete(string id)
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbl_UserInfo tbl_userinfo = db.tbl_UserInfo.Find(id);
                if (tbl_userinfo == null)
                {
                    return HttpNotFound();
                }
                return View(tbl_userinfo);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: /UserInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                tbl_UserInfo tbl_userinfo = db.tbl_UserInfo.Find(id);
                db.tbl_UserInfo.Remove(tbl_userinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
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
