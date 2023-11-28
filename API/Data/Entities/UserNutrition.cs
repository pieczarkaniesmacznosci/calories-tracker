namespace Data.Entities
{
    public class UserNutrition : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public double Kcal { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public double Fat { get; set; }
        public DateTime Date { get; set; }
    }
}
