using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CirculaireICTKeten.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CirculaireICTKeten.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly CirculaireICTKeten_dbContext _context;
        private IWebHostEnvironment Environment;

        public ArticlesController(CirculaireICTKeten_dbContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }

        // GET: Articles
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["searchString"] = searchString;

            var articles = from s in _context.Artikelen
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.ArtikelNaam.Contains(searchString));
            }

            ViewData["filePaths"] = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "Images/"));
            switch (sortOrder) {
                case "name_desc":
                    articles = articles.OrderByDescending(s => s.ArtikelNaam);
                    break;
                case "points":
                    articles = articles.OrderBy(s => s.ArtikelPunten);
                    break;
                case "category":
                    articles = articles.OrderBy(s => s.ArtikelSoortId);
                    break;
                default:
                    articles = articles.OrderByDescending(s => s.ArtikelNaam);
                    break;
            }
            return View(articles);
        }
    }
}
