namespace API.Web.Dtos
{
    public class ProductDto
    {
        public int? Id {get; set;}
        public string Name {get;set;}
        public double Kcal {get;set;}
        public double Protein {get;set;}
        public double Carbohydrates {get;set;}
        public double Fat {get;set;}
    }
}