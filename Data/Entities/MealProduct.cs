namespace Data.Entities
{
    public class MealProduct : Entity
    {
        public double Weight { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
