using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizApp.DAL;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class TestController : Controller
    {
        private QuizContext _db = new QuizContext();

        // GET: Test
        public ActionResult Index()
        {
            var tests = _db.Tests.Include(t => t.Category).Include(t => t.Subcategory).Include(t => t.SubSubcategory);
            return View(tests.ToList());
        }        

        // GET: Test/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "CategoryName");
            ViewBag.SubcategoryId = new SelectList(_db.Subcategories, "Id", "SubcategoryName");
            ViewBag.SubSubcategoryId = new SelectList(_db.SubSubcategories, "Id", "SubSubcategoryName");
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TestTitle,Description,Duration,CategoryId,SubcategoryId,SubSubcategoryId")] Test test)
        {
            if (ModelState.IsValid)
            {
                _db.Tests.Add(test);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "CategoryName", test.CategoryId);
            ViewBag.SubcategoryId = new SelectList(_db.Subcategories, "Id", "SubcategoryName", test.SubcategoryId);
            ViewBag.SubSubcategoryId = new SelectList(_db.SubSubcategories, "Id", "SubSubcategoryName", test.SubSubcategoryId);
            return View(test);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = _db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "CategoryName", test.CategoryId);
            ViewBag.SubcategoryId = new SelectList(_db.Subcategories, "Id", "SubcategoryName", test.SubcategoryId);
            ViewBag.SubSubcategoryId = new SelectList(_db.SubSubcategories, "Id", "SubSubcategoryName", test.SubSubcategoryId);
            return View(test);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TestTitle,Description,Duration,CategoryId,SubcategoryId,SubSubcategoryId")] Test test)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(test).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "CategoryName", test.CategoryId);
            ViewBag.SubcategoryId = new SelectList(_db.Subcategories, "Id", "SubcategoryName", test.SubcategoryId);
            ViewBag.SubSubcategoryId = new SelectList(_db.SubSubcategories, "Id", "SubSubcategoryName", test.SubSubcategoryId);
            return View(test);
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = _db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Test test = _db.Tests.Find(id);
            _db.Tests.Remove(test);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
