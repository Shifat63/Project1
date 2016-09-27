using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project1.Models;
using Rotativa;
using Rotativa.Options;

namespace Project1.Controllers
{
    public class DepositController : Controller
    {
        private AlorOvijatriEntities db = new AlorOvijatriEntities();

        // GET: /Deposit/
        public ActionResult Index()
        { 
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                var tbl_deposit = db.tbl_Deposit.Include(t => t.tbl_UserInfo);
                ViewBag.tbl_UserInfo = db.tbl_UserInfo;
                return View(tbl_deposit.ToList());
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult IndexForPDF()
        {
            Session.Remove("UserID");
            Session.Remove("UserType");
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public ActionResult SaveAsPDFDeposit(DepositSearch depositSearch)
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                ViewBag.tbl_UserInfo = db.tbl_UserInfo;
                var query = db.tbl_Deposit.AsQueryable();
                if (!string.IsNullOrEmpty(depositSearch.UserID))
                {
                    query = query.Where(x => x.UserID.Equals(depositSearch.UserID));
                }
                if (!string.IsNullOrEmpty(depositSearch.StartDate))
                {
                    var FromDate = DateTime.Parse(depositSearch.StartDate);
                    query = query.Where(x => x.PaymentDate >= FromDate);
                }
                if (!string.IsNullOrEmpty(depositSearch.EndDate))
                {
                    var ToDate = DateTime.Parse(depositSearch.EndDate);
                    query = query.Where(x => x.PaymentDate <= ToDate);
                }
                if (!string.IsNullOrEmpty(depositSearch.IsVerified))
                {
                    bool status = false;
                    if (depositSearch.IsVerified.Equals("1"))
                    {
                        status = true;
                    }
                    else if (depositSearch.IsVerified.Equals("2"))
                    {
                        status = false;
                    }
                    query = query.Where(x => x.IsVerified == status);
                }

                ViewBag.PDFTitle = "View Deposits";
                ViewBag.PDFCreationDate = DateTime.Today.ToString("dd-MMM-yyyy");
                ViewBag.DepositSearch = depositSearch;

                return new ViewAsPdf("IndexForPDF", query.ToList())
                {
                    FileName = "Deposit " + DateTime.Today.ToString("dd-MMM-yyyy") + " " + DateTime.Now.ToString("h:mm:ss tt") + ".pdf",
                    PageSize = Size.A4,
                    PageOrientation = Orientation.Landscape,
                    //PageWidth = 650, // it's in millimeters
                    //PageHeight = 250,
                    PageMargins = { Left = 10, Right = 10 }
                };
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult SaveAsPDFDepositDirector(DepositSearch depositSearch)
        {
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                ViewBag.tbl_UserInfo = db.tbl_UserInfo;
                String UserID = Session["UserID"].ToString();
                var query = (from Deposit in db.tbl_Deposit
                             where Deposit.UserID.Equals(UserID)
                             select Deposit).ToList().AsQueryable();
                if (!string.IsNullOrEmpty(depositSearch.StartDate))
                {
                    var FromDate = DateTime.Parse(depositSearch.StartDate);
                    query = query.Where(x => x.PaymentDate >= FromDate);
                }
                if (!string.IsNullOrEmpty(depositSearch.EndDate))
                {
                    var ToDate = DateTime.Parse(depositSearch.EndDate);
                    query = query.Where(x => x.PaymentDate <= ToDate);
                }
                if (!string.IsNullOrEmpty(depositSearch.IsVerified))
                {
                    bool status = false;
                    if (depositSearch.IsVerified.Equals("1"))
                    {
                        status = true;
                    }
                    else if (depositSearch.IsVerified.Equals("2"))
                    {
                        status = false;
                    }
                    query = query.Where(x => x.IsVerified == status);
                }

                ViewBag.PDFTitle = "My Deposits";
                ViewBag.PDFCreationDate = DateTime.Today.ToString("dd-MMM-yyyy");
                ViewBag.DepositSearch = depositSearch;

                return new ViewAsPdf("IndexForPDF", query.ToList())
                {
                    FileName = "My Deposit " + DateTime.Today.ToString("dd-MMM-yyyy") + " " + DateTime.Now.ToString("h:mm:ss tt") + ".pdf",
                    PageSize = Size.A4,
                    PageOrientation = Orientation.Landscape,
                    //PageWidth = 122, // it's in millimeters
                    //PageHeight = 44,
                    PageMargins = { Left = 10, Right = 10 }
                };
                
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DepositSearch depositSearch)
        {
            //var tbl_deposit = db.tbl_Deposit.Include(t => t.tbl_UserInfo);
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                //String UserID = depositSearch.UserID;
                //String From = depositSearch.StartDate;
                //String To = depositSearch.EndDate;
                ViewBag.tbl_UserInfo = db.tbl_UserInfo;
                var query = db.tbl_Deposit.AsQueryable();
                if (!string.IsNullOrEmpty(depositSearch.UserID))
                {
                    query = query.Where(x => x.UserID.Equals(depositSearch.UserID));
                }
                if (!string.IsNullOrEmpty(depositSearch.StartDate))
                {
                    var FromDate = DateTime.Parse(depositSearch.StartDate);
                    query = query.Where(x => x.PaymentDate >= FromDate);
                }
                if (!string.IsNullOrEmpty(depositSearch.EndDate))
                {
                    var ToDate = DateTime.Parse(depositSearch.EndDate);
                    query = query.Where(x => x.PaymentDate <= ToDate);
                }
                if (!string.IsNullOrEmpty(depositSearch.IsVerified))
                {
                    bool status=false;
                    if (depositSearch.IsVerified.Equals("1"))
                    {
                        status = true;
                    }
                    else if (depositSearch.IsVerified.Equals("2"))
                    {
                        status = false;
                    }
                    query = query.Where(x => x.IsVerified == status);
                }
                
                return View(query);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndexForDirectors(DepositSearch depositSearch)
        {
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                String UserID = Session["UserID"].ToString();
                var query = (from Deposit in db.tbl_Deposit
                             where Deposit.UserID.Equals(UserID)
                             select Deposit).ToList().AsQueryable();
                if (!string.IsNullOrEmpty(depositSearch.StartDate))
                {
                    var FromDate = DateTime.Parse(depositSearch.StartDate);
                    query = query.Where(x => x.PaymentDate >= FromDate);
                }
                if (!string.IsNullOrEmpty(depositSearch.EndDate))
                {
                    var ToDate = DateTime.Parse(depositSearch.EndDate);
                    query = query.Where(x => x.PaymentDate <= ToDate);
                }
                if (!string.IsNullOrEmpty(depositSearch.IsVerified))
                {
                    bool status = false;
                    if (depositSearch.IsVerified.Equals("1"))
                    {
                        status = true;
                    }
                    else if (depositSearch.IsVerified.Equals("2"))
                    {
                        status = false;
                    }
                    query = query.Where(x => x.IsVerified == status);
                }

                return View(query);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }


        public ActionResult IndexForDirectors()
        {
            //var tbl_deposit = db.tbl_Deposit.Include(t => t.tbl_UserInfo);
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                String UserID = Session["UserID"].ToString();
                var query = (from Deposit in db.tbl_Deposit
                             where Deposit.UserID.Equals(UserID)
                             select Deposit).ToList();
                return View(query);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: /Deposit/Details/5
        public ActionResult Details(string id)
        {
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbl_Deposit tbl_deposit = db.tbl_Deposit.Find(id);
                if (tbl_deposit == null)
                {
                    return HttpNotFound();
                }
                return View(tbl_deposit);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: /Deposit/Create
        public ActionResult Create()
        {
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserID", "Name");
                return View();
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: /Deposit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PaymentID,UserID,Amount,PaymentDate,EntryDate,SourceOfFund,Comment,IsVerified")] tbl_Deposit tbl_deposit)
        {
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                db.tbl_Deposit.Add(tbl_deposit);
                db.SaveChanges();
                ViewBag.SuccessMsg = "Deposit is sent for admin verification";
                String UserID = Session["UserID"].ToString();
                var query = (from Deposit in db.tbl_Deposit
                             where Deposit.UserID.Equals(UserID)
                             select Deposit).ToList();
                return View("IndexForDirectors", query);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
           
        }

        // GET: /Deposit/Edit/5
        public ActionResult Edit(string id)
        {
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbl_Deposit tbl_deposit = db.tbl_Deposit.Find(id);
                if (tbl_deposit == null)
                {
                    return HttpNotFound();
                }
                
                if (!tbl_deposit.IsVerified && tbl_deposit.UserID.Equals(Session["UserID"].ToString()))
                {
                    ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserID", "Name", tbl_deposit.UserID);
                    return View(tbl_deposit);
                }
                else
                {
                    if ("Director".Equals(Session["UserType"]))
                    {
                        return RedirectToAction("IndexForDirectors");
                    }
                    else if ("Super Admin".Equals(Session["UserType"]))
                    {
                        if (tbl_deposit.UserID.Equals(Session["UserID"].ToString()))
                        {
                            return RedirectToAction("IndexForDirectors");
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }  
                    }
                    else
                    {
                        Session.Remove("UserID");
                        Session.Remove("UserType");
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: /Deposit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PaymentID,UserID,Amount,PaymentDate,EntryDate,SourceOfFund,Comment,IsVerified")] tbl_Deposit tbl_deposit)
        {
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                db.Entry(tbl_deposit).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.SuccessMsg = "Deposit information updated successfully";
                String UserID = Session["UserID"].ToString();
                var query = (from Deposit in db.tbl_Deposit
                             where Deposit.UserID.Equals(UserID)
                             select Deposit).ToList();
                return View("IndexForDirectors", query);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: /Deposit/Delete/5
        public ActionResult Delete(string id)
        {
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbl_Deposit tbl_deposit = db.tbl_Deposit.Find(id);
                if (tbl_deposit == null)
                {
                    return HttpNotFound();
                }
                //return View(tbl_deposit);

                if (!tbl_deposit.IsVerified && (tbl_deposit.UserID.Equals(Session["UserID"].ToString()) || "Super Admin".Equals(Session["UserType"])))
                {
                    ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserID", "Name", tbl_deposit.UserID);
                    return View(tbl_deposit);
                }
                else
                {
                    if ("Director".Equals(Session["UserType"]))
                    {
                        return RedirectToAction("IndexForDirectors");
                    }
                    else if ("Super Admin".Equals(Session["UserType"]))
                    {
                        if (tbl_deposit.UserID.Equals(Session["UserID"].ToString()))
                        {
                            return RedirectToAction("IndexForDirectors");
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        Session.Remove("UserID");
                        Session.Remove("UserType");
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Approve(string id)
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbl_Deposit tbl_deposit = db.tbl_Deposit.Find(id);
                if (tbl_deposit == null)
                {
                    return HttpNotFound();
                }
                //return View(tbl_deposit);

                if (!tbl_deposit.IsVerified )
                {
                    ViewBag.UserID = new SelectList(db.tbl_UserInfo, "UserID", "Name", tbl_deposit.UserID);
                    return View(tbl_deposit);
                }
                else
                {
                    return RedirectToAction("Index");     
                }
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }


        // POST: /Deposit/Approve/5
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveConfirmed(string id)
        {
            if ("Super Admin".Equals(Session["UserType"]) && Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbl_Deposit result = (from p in db.tbl_Deposit
                                      where p.PaymentID.Equals(id)
                                      select p).SingleOrDefault();

                result.IsVerified = true;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e)
                    // Provide for exceptions.
                }
                ViewBag.tbl_UserInfo = db.tbl_UserInfo;
                ViewBag.SuccessMsg = "Deposit approved successfully";
                return View("Index", db.tbl_Deposit);
            }
            else
            {
                Session.Remove("UserID");
                Session.Remove("UserType");
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: /Deposit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (("Director".Equals(Session["UserType"]) || "Super Admin".Equals(Session["UserType"])) && Session["UserID"] != null)
            {
                tbl_Deposit tbl_deposit = db.tbl_Deposit.Find(id);
                db.tbl_Deposit.Remove(tbl_deposit);
                db.SaveChanges();
                ViewBag.SuccessMsg = "Deposit deleted successfully";
                String UserID = Session["UserID"].ToString();
                var query = (from Deposit in db.tbl_Deposit
                             where Deposit.UserID.Equals(UserID)
                             select Deposit).ToList();
                return View("IndexForDirectors", query);
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
