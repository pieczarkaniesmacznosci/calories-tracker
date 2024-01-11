using System;

namespace Entities
{
    public class MealLog : Entity
    {
        public Guid UserId { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public DateTime DateEaten { get; set; }
    }
}
