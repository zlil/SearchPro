using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SearchPro.Models;

namespace SearchPro.Controllers
{

 
    public class GymsController : Controller
    {
        private GymContext db = new GymContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string searchname, string searchadr, string searchtime) //Add Filtering Functionality to the Index Method 
        {
            var g = 0;
            var gym = from s in db.Gyms 
                       select s; 
           if ((!String.IsNullOrEmpty(searchname) || (!String.IsNullOrEmpty(searchadr)) || (!String.IsNullOrEmpty(searchtime))))
           {
               gym = gym.Where(s => (s.GymName.Contains(searchname) && s.GymAddress.Contains(searchadr) && s.GymOpenTime.Contains(searchtime) ));// query that return just the specific data base.
               g = 1;
           }
            //Group By
           IList<Gyms> dbobjs = gym.GroupBy(t => t.GymPrice).SelectMany(group => group).ToList();
          
               return View(dbobjs);         
                                             //return all the list(null/empty case) 
        }

        // GET: Gyms/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gyms gyms = db.Gyms.Find(id);
            if (gyms == null)
            {
                return HttpNotFound();
            }
            return View(gyms);
        }

        // GET: Gyms/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gyms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GymID,GymName,GymPrice,GymAddress,GymOpenTime,GymCloseTime")] Gyms gyms)
        {
            if (ModelState.IsValid)
            {
                db.Gyms.Add(gyms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gyms);
        }

        // GET: Gyms/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gyms gyms = db.Gyms.Find(id);
            if (gyms == null)
            {
                return HttpNotFound();
            }
            return View(gyms);
        }

        // POST: Gyms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GymID,GymName,GymPrice,GymAddress,GymOpenTime,GymCloseTime")] Gyms gyms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gyms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gyms);
        }

        // GET: Gyms/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gyms gyms = db.Gyms.Find(id);
            if (gyms == null)
            {
                return HttpNotFound();
            }
            return View(gyms);
        }

        // POST: Gyms/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gyms gyms = db.Gyms.Find(id);
            db.Gyms.Remove(gyms);
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
        [Authorize(Roles = "Admin")]
        [HttpPost] //adding Products to Gym
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct([Bind(Include = "ProductID,GymID,ProductName,ProductPrice,ProductRate,ProductType")] Products Product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GymID = new SelectList(db.Products, "GymID", "courseName", Product.GymID);
            return View(Product);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost] //adding Courses to Gym
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse([Bind(Include = "courseID,GymID,courseName,courseTime,courseLength,courseInstructor,courseType")] Courses Course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(Course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GymID = new SelectList(db.Courses, "GymID", "courseName", Course.GymID);
            return View(Course);
        }

        //EDIT COURSES
        [Authorize(Roles = "Admin")]
        public ActionResult EditCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }


        //EDIT COURSES
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse([Bind(Include = "courseID,GymID,courseName,courseTime,courseLength,courseInstructor,courseType")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); //TO DO: back to the current gym
            }
            return View(courses);
        }

        //EDIT PRODUCTS
        [Authorize(Roles = "Admin")]
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }


        //EDIT PRODUCTS
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ProductID,GymID,ProductName,ProductPrice,ProductRate,ProductType")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); //TO DO: back to the current gym
            }
            return View(products);
        }

        //DELETE COURSES
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCourses(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
           
            db.Courses.Remove(courses);
            db.SaveChanges();
            return RedirectToAction("Index"); //TO DO: back to the current gym
           
        }

        //DELETE PRODUCTS
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteProducts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }

            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index"); //TO DO: back to the current gym
        }
    }
}

