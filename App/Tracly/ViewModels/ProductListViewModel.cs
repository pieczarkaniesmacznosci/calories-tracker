using System.Collections.Generic;
using App.Tracly.Models;

namespace App.Tracly.ViewModels
{
    public class ProductListViewModel
    {
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}