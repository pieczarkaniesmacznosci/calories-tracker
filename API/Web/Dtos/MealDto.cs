using System;
using System.Collections.Generic;
using API.Web.Entities;

namespace API.Web.Dtos
{
    public class MealDto
    {
        public int Id { get; set; }
        public IEnumerable<MealElementDto> MealElements {get;set;}
        public double Weight { get; set; }
        public DateTime Date { get; set; }
    }
}