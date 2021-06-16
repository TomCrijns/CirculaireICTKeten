using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CirculaireICTKeten.Models;

namespace CirculaireICTKeten.Controllers
{
    public class TransactieController : Controller
    {
        private readonly CirculaireICTKeten_dbContext _db;

        public TransactieController(CirculaireICTKeten_dbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TransactieModel> objList = _db.Transacties;
            return View(objList);
        }

        // GET - Create
        public IActionResult Create()
        {
            return View();
        }

        // POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransactieModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Transacties.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);

        }

        // GET - Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Transacties.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }

        // POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TransactieModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Transacties.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);

        }

        // GET - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Transacties.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }
        // POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Transacties.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Transacties.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
