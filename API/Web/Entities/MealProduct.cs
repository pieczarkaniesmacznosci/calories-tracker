namespace API.Web.Entities
{
    public class MealProduct
    {
        public int Id {get;set;}
        public double Weight {get;set;}



        public int ProductId {get;set;}
        public Product Product {get;set;}

        public int MealId {get;set;}
        public Meal Meal {get;set;}
    }
}