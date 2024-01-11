namespace Entities
{
    public class UserNutrition : Entity
    {
        public Guid UserId { get; set; }
        public double Kcal { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public double Fat { get; set; }
        public DateTime Date { get; set; }
    }
}
