using System;

namespace API.Dtos
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public double Kcal { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public double Fat { get; set; }
        public bool IsDefault { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
