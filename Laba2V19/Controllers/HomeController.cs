using System;
using System.Linq;
using System.Web.Mvc;
using Laba2V19.Models;

namespace Laba2V19.Controllers
{
    public class HomeController : Controller
    {
        private Database19Entities db = new Database19Entities();

        // GET: Home
        public ActionResult Index()
        {
            return View((from MarkaClock in db.MarkaClocks
                         select MarkaClock).ToList());
        }

        // Get details for brand
        public ActionResult Details(int id)
        {
            try
            {
                return View((from PosClock in db.PosClocks
                             where PosClock.IdMarka == id
                             select PosClock.TypeClock).First());
            }
            catch (Exception)
            {
                return View(new TypeClock
                {
                    Name = "Тип не вказаний."
                });
            }
        }

        // Open page for add brand
        [HttpGet]
        public ActionResult Create()
        {
            return View(new MarkaClock());
        }

        // Add new brand
        [HttpPost]
        public ActionResult Create(MarkaClock markaClock)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        markaClock.IdMarka = (from marka in db.MarkaClocks
                                              select marka.IdMarka).Max() + 1;
                    }
                    catch (Exception)
                    {
                        markaClock.IdMarka = 1;
                    }
                    db.MarkaClocks.Add(markaClock);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(String.Empty, exception);
            }
            return View(markaClock);
        }

        // Open page for edit brand
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View((from marka in db.MarkaClocks
                         where marka.IdMarka == id
                         select marka).First());
        }

        // Edit brand
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection)
        {
            var markaClock = (from marka in db.MarkaClocks
                           where marka.IdMarka == id
                           select marka).First();
            try
            {
                UpdateModel(markaClock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(markaClock);
            }
        }

        // Open page for delete brand
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View((from marka in db.MarkaClocks
                         where marka.IdMarka == id
                         select marka).First());
        }

        // Delete brand
        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            var markaClock = (from marka in db.MarkaClocks
                             where marka.IdMarka == id
                             select marka).First();
            try
            {
                db.MarkaClocks.Remove(markaClock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(markaClock);
            }
        }
    }
}