using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CirculaireICTKeten.Models
{
    public class TransactionAddProductViewModel
    {
        public IEnumerable<ArtikelenModel> Products;
        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return Products?.Select(q => new SelectListItem(q.ArtikelNaam, q.ArtikelID.ToString())) ?? new[] { new SelectListItem("No products were found", "dwaoidwhad") };
        }

        [Required]
        public int SelectedProductId { get; set; }
        
        [Required]
        [Range(0, 15)]
        public byte NumberOfProducts { get; set; }
        
        [Range(0, 150)]
        public short? Points { get; set; }

        public bool IsForSell { get; set; }
    }
}
