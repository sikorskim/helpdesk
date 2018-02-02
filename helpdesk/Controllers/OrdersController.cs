using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using helpdesk.Models;
using helpdesk.ViewModels;
using PagedList;

namespace helpdesk.Controllers
{
    public class OrdersController : Controller
    {
        private helpdeskContext db = new helpdeskContext();

        // GET: Orders
        public ActionResult Index(int? page, int? StatusId, int? CategoryId, string Query)
        {
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");

            string username = getUserName();
            bool isAdmin = checkCredentials(username);
            ViewBag.isAdmin = isAdmin;
            visitCount();

            IQueryable<Order> orders =null;
            if (isAdmin)
            {
                orders = db.Orders.Include(o => o.Category);
            }
            else
            {
                orders = db.Orders.Where(o => o.UserName == username).Include(o => o.Category);
            }

            if (!String.IsNullOrEmpty(Query))
            {
                orders = orders.Where(s => s.Content.Contains(Query));
            }

            if (StatusId > 0)
            {
                orders = orders.Where(p => p.Status.StatusId == StatusId);
            }

            if (CategoryId > 0)
            {
                orders = orders.Where(p => p.Category.CategoryId==CategoryId);
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            
             IPagedList<Order> ordersPaged = orders.OrderBy(o => o.Status.StatusId).ThenBy(o => o.TimeCreated).ToPagedList(pageNumber, pageSize);

            return View(ordersPaged);
        }

        string getUserName()
        {
            return User.Identity.Name.ToLower();
        }

        bool checkCredentials(string username)
        {
            bool isAdmin = false;
            AppUser user = db.AppUser.Include(p => p.Credential).Single(p => p.Username == username);
            if (user.Credential.CredentialId==1)
            {
                isAdmin = true;
            }
            return isAdmin;
        }

        void visitCount()
        {
            string username = getUserName();
            if (db.AppUser.Any(p => p.Username == username))
            {
                AppUser appUser = db.AppUser.Single(p => p.Username == username);
                appUser.Visits++;
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                Credential credential = db.Credentials.Single(p => p.CredentialId==2);
                AppUser appUser = new AppUser { Username = username, Visits = 1, Credential= credential };
                db.AppUser.Add(appUser);
                db.SaveChanges();
            }
        }
        
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string username = getUserName();
            ViewBag.isAdmin = checkCredentials(username);

            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel();
            orderDetailsViewModel.Order = db.Orders.Find(id);
            orderDetailsViewModel.OrderComments = db.OrderComment.Where(p => p.Order.OrderId == id).Include(p=>p.Status);

            if (orderDetailsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(orderDetailsViewModel);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,Urgent,Content")] Order order, int CategoryId)
        {
            if (ModelState.IsValid)
            {
                order.Category = db.Categories.Single(p => p.CategoryId == CategoryId);
                order.Status = db.Status.Single(p => p.StatusName == "nowe");
                order.TimeCreated = DateTime.Now;
                order.UserName = getUserName();
                db.Orders.Add(order);

                OrderComment orderComment = new OrderComment();
                orderComment.Order = order;
                orderComment.Status = order.Status;
                orderComment.Time = order.TimeCreated;
                orderComment.Text = "Utworzono zgłoszenie";
                db.OrderComment.Add(orderComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", order.Category);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,UserName,CategoryId,TimeCreated,TimeClosed,Urgent,Content")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", order.Category);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Orders/Cancel/5
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id, string comment)
        {
            Order order = db.Orders.Find(id);
            order.Status = db.Status.Single(p => p.StatusName == "anulowane");
            order.TimeClosed = DateTime.Now;

            OrderComment orderComment = new OrderComment();
            orderComment.Order = order;
            orderComment.Status = order.Status;
            orderComment.Time = order.TimeCreated;
            orderComment.Text = comment;

            db.OrderComment.Add(orderComment);
            db.Entry(order).State = EntityState.Modified;
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
