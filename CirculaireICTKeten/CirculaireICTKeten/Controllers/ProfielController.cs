using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CirculaireICTKeten.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CirculaireICTKeten.Controllers
{
    public class ProfielController : Controller
    {
        private readonly CirculaireICTKeten_dbContext _db;

        public ProfielController(CirculaireICTKeten_dbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ProfileDataModel> objList = _db.ProfileData;

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
        public IActionResult Create(ProfileDataModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProfileData.Add(obj);
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

            var obj = _db.ProfileData.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }
        //Post - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProfileDataModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProfileData.Update(obj);
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

            var obj = _db.ProfileData.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }
        //Post - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.ProfileData.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ProfileData.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

