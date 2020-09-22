namespace API.Web.Entities
{
    public class DishProduct
    {
        public int Id {get;set;}


        public int ProductId {get;set;}
        public Product Product {get;set;}

        public int DishId {get;set;}
        public Dish Dish {get;set;}
    }
}