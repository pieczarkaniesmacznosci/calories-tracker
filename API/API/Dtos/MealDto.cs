using System;
using System.Collections.Generic;

namespace API.Dtos
{
    public class MealDto
    {
        public int? Id { get; set; }
        public bool Deleted { get; set; }
        public string MealName { get; set; }
        public double Weight { get; set; }
        public DateTime? DateEaten { get; set; }
        public List<MealProductDto> MealProducts { get; set; }
    }
}
