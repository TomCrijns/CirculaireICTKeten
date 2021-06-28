using CirculaireICTKeten.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CirculaireICTKeten.Services
{
    class TransactionManager : ITransactionManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly CirculaireICTKeten_dbContext _dataContext;
        private readonly Lazy<TransactieModel> _activeTransaction;

        public TransactieModel ActiveTransaction => _activeTransaction.Value;

        public TransactionManager(IHttpContextAccessor httpContextAccessor, CirculaireICTKeten_dbContext dataContext)
        {
            _contextAccessor = httpContextAccessor;
            _dataContext = dataContext;
            
            int employeeId = int.Parse(_contextAccessor.HttpContext.User.FindFirst(q => q.Type == ClaimTypes.NameIdentifier).Value);
            _activeTransaction = new Lazy<TransactieModel>(dataContext.Transacties.Include(q=>q.Profiel).SingleOrDefault(q => q.Datum == DateTimeOffset.MinValue));
        }

        public TransactieArtikelenModel AddProductForSellToTransaction(ArtikelenModel product, byte numberOf, int? points) => 
            AddProductToTransaction(product, numberOf, points, true);

        public TransactieArtikelenModel AddProductToBuyToTransaction(ArtikelenModel product, byte numberOf, int? points) =>
            AddProductToTransaction(product, numberOf, points, false);

        public void EndTransaction()
        {
            IEnumerable<TransactieArtikelenModel> products = _dataContext.TransactieArtikelen.Where(q => q.TransactieID == ActiveTransaction.TransactieID);
            ProfileDataModel customer = _dataContext.ProfileData.Find(ActiveTransaction.ProfielId);
            foreach (TransactieArtikelenModel product in products)
            {
                if (product.IsVerkoop)
                {
                    customer.Balans += product.Punten * product.Aantal;
                } else
                {
                    customer.Balans -= product.Punten * product.Aantal;
                }
            }
            ActiveTransaction.Datum = DateTime.UtcNow;
            _dataContext.SaveChanges();
            
        }

        public TransactieModel StartTransaction(ProfileDataModel customer)
        {
            if (_contextAccessor.HttpContext.User.Identity?.IsAuthenticated != true || !_contextAccessor.HttpContext.User.IsInRole("employee"))
            {
                throw new SecurityException("Trying to start transaction while not logged in or not having the right permissions");
            }

            if (_dataContext.Transacties.Any(q=>q.Datum == DateTimeOffset.MinValue))
            {
                throw new InvalidOperationException("Cannot start transaction while there is one open");
            }

            TransactieModel transaction = new TransactieModel()
            {
                ProfielId = customer.Id,
                Donatie = false,
                Lening = false,
            };
            _dataContext.Transacties.Add(transaction);

            _dataContext.SaveChanges();
            return transaction;
        }

        private TransactieArtikelenModel AddProductToTransaction(ArtikelenModel product, byte numberOf, int? points, bool isForSell)
        {
            int? sellPoints = points ?? product.ArtikelPunten;

            if (!sellPoints.HasValue)
            {
                throw new InvalidOperationException("There is no default points for this product, while no points were given");
            }

            TransactieArtikelenModel transactionProduct = new TransactieArtikelenModel()
            {
                IsVerkoop = isForSell,
                ArtikelID = product.ArtikelID,
                Punten = sellPoints.Value,
                Aantal = numberOf,
                TransactieID = ActiveTransaction.TransactieID,
            };
            _dataContext.TransactieArtikelen.Add(transactionProduct);
            _dataContext.SaveChanges();

            return transactionProduct;
        }
    }

    public interface ITransactionManager
    {
        public TransactieModel ActiveTransaction { get; }
        public TransactieModel StartTransaction(ProfileDataModel customer);
        public TransactieArtikelenModel AddProductForSellToTransaction(ArtikelenModel product, byte numberOf, int? points);
        public TransactieArtikelenModel AddProductToBuyToTransaction(ArtikelenModel product, byte numberOf, int? points);
        public void EndTransaction();
    }
}
