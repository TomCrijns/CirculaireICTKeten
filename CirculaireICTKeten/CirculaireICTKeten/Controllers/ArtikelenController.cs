using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CirculaireICTKeten.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CirculaireICTKeten.Controllers
{
    public class ArtikelenController : Controller
    {
        private readonly CirculaireICTKeten_dbContext _db;

        public ArtikelenController(CirculaireICTKeten_dbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ArtikelenModel> objList = _db.Artikelen;

            return View(objList);
        }

        //get for create
        public IActionResult Create()
        {
            return View();
        }

        //post for create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArtikelenModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Artikelen.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);

        }

        //Get Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Artikelen.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }
        //Post - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArtikelenModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Artikelen.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);

        }

        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Artikelen.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }
        //Post - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? ArtikelID)
        {
            var obj = _db.Artikelen.Find(ArtikelID);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Artikelen.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
