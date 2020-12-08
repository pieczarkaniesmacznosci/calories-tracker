using System;
using System.Collections.Generic;

namespace API.Web.Dtos
{
    public class MealDto
    {
        public int Id { get; set; }
        public bool IsSaved {get;set;}
        public string MealName {get;set;}
        public double Weight { get; set; }
        public DateTime? DateEaten { get; set; }
        public List<MealProductDto> MealProducts {get;set;}
    }
}