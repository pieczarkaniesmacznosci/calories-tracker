using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Web.Entities
{
    public class Meal : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public double Weight => MealProducts.Sum(x => x.Weight);
        public bool IsSaved { get; set; }
        public string MealName { get; set; }
        public DateTime DateEaten { get; set; }
        public IEnumerable<MealLog> MealLogs { get; set; }
        public IEnumerable<MealProduct> MealProducts { get; set; }
    }
}