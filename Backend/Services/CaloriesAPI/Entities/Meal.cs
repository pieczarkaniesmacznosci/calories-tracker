using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Meal : Entity
    {
        public Guid UserId { get; set; }
        public double Weight => MealProducts.Sum(x => x.Weight);
        public bool Deleted { get; set; }
        public string MealName { get; set; }
        public DateTime? DateEaten { get; set; }
        public IEnumerable<MealLog> MealLogs { get; set; }
        public IEnumerable<MealProduct> MealProducts { get; set; }
    }
}
