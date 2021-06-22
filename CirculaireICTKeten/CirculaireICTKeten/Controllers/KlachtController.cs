using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CirculaireICTKeten.Models;

namespace CirculaireICTKeten.Controllers
{
    public class KlachtController : Controller
    {
        private readonly CirculaireICTKeten_dbContext _context;

        public KlachtController(CirculaireICTKeten_dbContext context)
        {
            _context = context;
        }

        // GET: Klacht
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klacht.ToListAsync());
        }

        // GET: Klacht/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klacht = await _context.Klacht
                .FirstOrDefaultAsync(m => m.KlachtID == id);
            if (klacht == null)
            {
                return NotFound();
            }

            return View(klacht);
        }

        // GET: Klacht/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klacht/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlachtID,Title,Description,Priority,CreationTime,StatusCompleted,ProfielId")] Klacht klacht)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klacht);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klacht);
        }

        // GET: Klacht/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klacht = await _context.Klacht.FindAsync(id);
            if (klacht == null)
            {
                return NotFound();
            }
            return View(klacht);
        }

        // POST: Klacht/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KlachtID,Title,Description,Priority,CreationTime,StatusCompleted,ProfielId")] Klacht klacht)
        {
            if (id != klacht.KlachtID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klacht);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlachtExists(klacht.KlachtID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(klacht);
        }

        // GET: Klacht/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klacht = await _context.Klacht
                .FirstOrDefaultAsync(m => m.KlachtID == id);
            if (klacht == null)
            {
                return NotFound();
            }

            return View(klacht);
        }

        // POST: Klacht/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klacht = await _context.Klacht.FindAsync(id);
            _context.Klacht.Remove(klacht);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlachtExists(int id)
        {
            return _context.Klacht.Any(e => e.KlachtID == id);
        }
    }
}
