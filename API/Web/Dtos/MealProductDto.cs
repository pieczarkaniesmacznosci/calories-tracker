namespace API.Web.Dtos
{
    public class MealProductDto
    {
        public int? Id {get;set;}
        public int ProductId {get;set;}
        public ProductDto Product {get;set;}
        public double Weight {get;set;}
    }
}