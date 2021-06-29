using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CirculaireICTKeten.Services;
using Microsoft.AspNetCore.Authorization;
using CirculaireICTKeten.Models;

namespace CirculaireICTKeten.Controllers
{
    [Authorize]
    public class MyTransactionsController : Controller
    {
        private readonly CirculaireICTKeten_dbContext _context;

        public MyTransactionsController(CirculaireICTKeten_dbContext context)
        {
            _context = context;
        }

        // GET: MyTransactions
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Transacties;
            return View(await dataContext.ToListAsync());
        }

        // GET: MyTransactions/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var transaction = await _context.Transacties
                .Include(t => t.TransactieArtikelen)
                    .ThenInclude(q=>q.Artikelen)
                .FirstOrDefaultAsync(m => m.TransactieID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }
    }
}
