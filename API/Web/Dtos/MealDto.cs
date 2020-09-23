using System;
using System.Collections.Generic;
using API.Web.Entities;

namespace API.Web.Dtos
{
    public class MealDto
    {
        public int Id { get; set; }
        public bool IsSaved {get;set;}
        public string MealName {get;set;}
        public double Weight { get; set; }
        public DateTime DateEaten { get; set; }
        public IEnumerable<MealProductDto> MealProducts {get;set;}
    }
}