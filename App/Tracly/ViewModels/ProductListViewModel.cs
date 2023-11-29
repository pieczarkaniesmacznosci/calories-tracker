using System.Collections.Generic;
using Tracly.Dtos;

namespace Tracly.ViewModels
{
    public class ProductListViewModel
    {
        public string Title { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
