using System.Collections.Generic;
using API.Web.Dtos;

namespace Tracly.Models
{
    public class MealViewModel
    {
        public List<ProductDto> Products {get;set;}
        public MealLogDto MealLog {get;set;}
        public bool IsEdit {get;set;}
        public string Title {get;set;}
    }
}