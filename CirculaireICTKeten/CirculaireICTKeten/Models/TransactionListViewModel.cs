using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CirculaireICTKeten.Models
{
    public class TransactionListViewModel
    {


        public TransactionListViewModel(IEnumerable<TransactieArtikelenModel> transactionProducts, int currentCustomerSaldo)
        {
            CurrentCustomerSaldo = currentCustomerSaldo;
            TransactionProducts = transactionProducts;
            int saldo = 0;
            foreach (var transProduct in transactionProducts)
            {
                if (transProduct.IsVerkoop)
                {
                    saldo -= (transProduct.Punten * transProduct.Aantal);
                }
                else
                {
                    saldo += (transProduct.Punten * transProduct.Aantal);
                }
            }
            EndSaldo = saldo;
        }
        public readonly IEnumerable<TransactieArtikelenModel> TransactionProducts;
        public readonly int EndSaldo;
        public readonly int CurrentCustomerSaldo;
    }
}
