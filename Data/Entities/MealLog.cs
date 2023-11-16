using System;

namespace Data.Entities
{
    public class MealLog : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public DateTime DateEaten { get; set; }
    }
}
