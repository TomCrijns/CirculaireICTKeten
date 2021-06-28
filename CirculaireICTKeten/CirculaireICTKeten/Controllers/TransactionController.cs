using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CirculaireICTKeten.Models;
using CirculaireICTKeten.Services;
using Microsoft.EntityFrameworkCore;

namespace CirculaireICTKeten.Controllers
{
    [Authorize(Roles = "employee")]
    public class TransactionController : Controller
    {
        private readonly CirculaireICTKeten_dbContext _dataContext;
        private readonly ITransactionManager _transactionManager;

        public TransactionController(CirculaireICTKeten_dbContext dataContext, ITransactionManager transactionManager)
        {
            _dataContext = dataContext;
            _transactionManager = transactionManager;
        }

        public IActionResult Index()
        {
            if (_transactionManager.ActiveTransaction != null)
            {
                return RedirectToAction("List", "Transaction");
            }

            return View();
        }

        [HttpPost]
        public IActionResult IndexPost(TransactionIndexViewModel model)
        {
            if (_transactionManager.ActiveTransaction != null)
            {
                return RedirectToAction("List", "Transaction");
            }

            ProfileDataModel customer = _dataContext.ProfileData.FirstOrDefault(q=>q.Id == model.CustomerId);

            if (customer == null)
            {
                ModelState.AddModelError(nameof(model.CustomerId), "Onbekende klant id");
                return View("Index");
            }

            _transactionManager.StartTransaction(customer);
            return RedirectToAction("AddProduct");
        }
        public IActionResult AddProduct()
        {
            if (_transactionManager.ActiveTransaction == null)
            {
                return RedirectToAction("Index", "Transaction");
            }

            return View(new TransactionAddProductViewModel()
            {
                Products = _dataContext.Artikelen
            });
        }
        
        [HttpPost]
        public IActionResult AddProductPost(TransactionAddProductViewModel model)
        {
            if (_transactionManager.ActiveTransaction == null)
            {
                return RedirectToAction("Index", "Transaction");
            }

            ArtikelenModel product = _dataContext.Artikelen.FirstOrDefault(q=>q.ArtikelID == model.SelectedProductId);
            if (product == null)
            {
                ModelState.AddModelError(nameof(model.SelectedProductId), "Onbekend product");
                return View("AddProduct", new TransactionAddProductViewModel()
                {
                    Products = _dataContext.Artikelen
                });
            }

            if (model.IsForSell)
            {
                _transactionManager.AddProductForSellToTransaction(product, model.NumberOfProducts, model.Points);
            } else
            {
                _transactionManager.AddProductToBuyToTransaction(product, model.NumberOfProducts, model.Points);
            }
            
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            if (_transactionManager.ActiveTransaction == null)
            {
                return RedirectToAction("Index", "Transaction");
            }

            var transProducts = _dataContext.TransactieArtikelen.Where(q=>q.Transactie.TransactieID == _transactionManager.ActiveTransaction.TransactieID).Include(q => q.Artikelen).AsEnumerable();
            return View(new TransactionListViewModel(transProducts, _transactionManager.ActiveTransaction.Profiel.Balans.Value));
        }

        public IActionResult Finish()
        {
            if (_transactionManager.ActiveTransaction == null)
            {
                return RedirectToAction("Index", "Transaction");
            }

            _transactionManager.EndTransaction();

            return View();
        }
    }
}
