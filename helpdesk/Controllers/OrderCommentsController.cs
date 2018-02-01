using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using helpdesk.Models;

namespace helpdesk.Controllers
{
    public class OrderCommentsController : Controller
    {
        private helpdeskContext db = new helpdeskContext();

        // GET: OrderComments
        public ActionResult Index()
        {
            return View(db.OrderComment.ToList());
        }

        // GET: OrderComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderComment orderComment = db.OrderComment.Find(id);
            if (orderComment == null)
            {
                return HttpNotFound();
            }
            return View(orderComment);
        }

        // GET: OrderComments/Create
        public ActionResult Create(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusName");

            OrderComment orderComment = new OrderComment();
            orderComment.OrderId = (int)orderId;
            
            if (orderComment == null)
            {
                return HttpNotFound();
            }
            return View(orderComment);
        }

        // POST: OrderComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderCommentId,Text,OrderId")] OrderComment orderComment, int StatusId)
        {
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusName");
            if (ModelState.IsValid)
            {
                Status status = db.Status.Find(StatusId);
                Order order = db.Orders.Find(orderComment.OrderId);
                order.Status = db.Status.Find(StatusId);
                if (status == db.Status.Single(s => s.StatusName == "zamknięte"))
                {
                    order.TimeClosed = DateTime.Now;
                }

                orderComment.Time = DateTime.Now;
                orderComment.Status = status;
                orderComment.Order = order;
                db.OrderComment.Add(orderComment);
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Orders", new { Id = orderComment.OrderId });
            }
            return View(orderComment);
        }

        // GET: OrderComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderComment orderComment = db.OrderComment.Find(id);
            if (orderComment == null)
            {
                return HttpNotFound();
            }
            return View(orderComment);
        }

        // POST: OrderComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderCommentId,Time,Text")] OrderComment orderComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderComment);
        }

        // GET: OrderComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderComment orderComment = db.OrderComment.Find(id);
            if (orderComment == null)
            {
                return HttpNotFound();
            }
            return View(orderComment);
        }

        // POST: OrderComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderComment orderComment = db.OrderComment.Find(id);
            db.OrderComment.Remove(orderComment);
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
