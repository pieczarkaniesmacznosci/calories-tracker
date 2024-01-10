using System;

namespace Tracly.Dtos
{
    public class UserNutritionDto
    {
        public int Id { get; set; }
        public double Kcal { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public double Fat { get; set; }
        public DateTime Date { get; set; }
    }
}
