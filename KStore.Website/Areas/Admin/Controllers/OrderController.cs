using KStore.Data;
using KStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KStore.Website.Areas.Admin.Controllers
{
    public class OrderController : AdminController
    {
        private EcommerceDbContext db = new EcommerceDbContext();

        // GET: /Admin/Order/
        public ActionResult Index()
        {


            return View(db.Orders.Include("Customer").Include("OrderStatus").ToList());
        }

        // GET: /Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include("OrderLines.Product").Include("Customer").Include("OrderStatus").Where(p => p.Id == id).FirstOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: /Admin/Order/Edit/5
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
            return View(order);
        }

        // POST: /Admin/Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Created,Completed,Delivery")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
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
