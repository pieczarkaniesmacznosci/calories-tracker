using System;

namespace API.Web.Dtos
{
    public class MealDto
    {
        public int Id { get; set; }
        public int MealCompositionId { get; set; }
        public int UserId { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }
    }
}