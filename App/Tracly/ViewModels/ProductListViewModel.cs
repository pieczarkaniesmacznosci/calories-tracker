using System.Collections.Generic;
using Tracly.Models;

namespace Tracly.ViewModels
{
    public class ProductListViewModel
    {
        public string Title { get; set; }
        public IEnumerable<Product> Products {get;set;}
    }
}