﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.IO;
using KStore.Data;
using KStore.Domain.Model;

namespace KStore.Website.Areas.Admin.Controllers
{

    public class ProductController : AdminController
    {
        private EcommerceDbContext db = new EcommerceDbContext();

        // GET: /Product/
        public ActionResult Index()
        {
            return View(db.Products.Include("Category").Include("Brand").ToList());
        }

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include("Image").Include("Category").Include("Brand").Where(p => p.Id == id).SingleOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Product/Create
        public ActionResult Create()
        {
      
            ViewBag.categories = db.ProductCategories.ToList();
            ViewBag.brands = db.Brands.ToList();
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Description,DeliveryTime,Views,Price,Visible,PurchasePrice,StockStatus")] Product product)
        {
            string fileName="";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
               
                if (file != null && file.ContentLength > 0)
                {
                   
                     fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);
                    
                }

            }
            
            if (ModelState.IsValid)
            {
                product.ImagePath = "/Images/" + fileName;
                product.Created = DateTime.Now;
                product.Modified = null;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.categories = db.ProductCategories.ToList();
            ViewBag.brands = db.Brands.ToList();
            return View(product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Description,DeliveryTime,Views,Price,Visible,PurchasePrice,StockStatus,Created,Modified")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Modified = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
