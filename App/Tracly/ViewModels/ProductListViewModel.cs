using System.Collections.Generic;
using API.Web.Entities;

namespace App.Tracly.ViewModels
{
    public class ProductListViewModel
    {
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}