using System;
using System.Linq;
using System.Web.Mvc;
using Laba2V19.Models;

namespace Laba2V19.Controllers
{
    public class HomeController : Controller
    {
        private Database19Entities _db = new Database19Entities();

        // GET: Home
        public ActionResult Index()
        {
            return View((from MarkaClock in _db.MarkaClocks
                         select MarkaClock).ToList());
        }

        // Get details for brand
        public ActionResult Details(int id)
        {
            try
            {
                return View((from PosClock in _db.PosClocks
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
                        markaClock.IdMarka = (from marka in _db.MarkaClocks
                                              select marka.IdMarka).Max() + 1;
                    }
                    catch (Exception)
                    {
                        markaClock.IdMarka = 1;
                    }
                    _db.MarkaClocks.Add(markaClock);
                    _db.SaveChanges();
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
            return View((from marka in _db.MarkaClocks
                         where marka.IdMarka == id
                         select marka).First());
        }

        // Edit brand
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection)
        {
            var markaClock = (from marka in _db.MarkaClocks
                           where marka.IdMarka == id
                           select marka).First();
            try
            {
                UpdateModel(markaClock);
                _db.SaveChanges();
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
            return View((from marka in _db.MarkaClocks
                         where marka.IdMarka == id
                         select marka).First());
        }

        // Delete brand
        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            var markaClock = (from marka in _db.MarkaClocks
                             where marka.IdMarka == id
                             select marka).First();
            try
            {
                _db.MarkaClocks.Remove(markaClock);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(markaClock);
            }
        }
    }
}