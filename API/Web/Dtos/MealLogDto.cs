using System;

namespace API.Web.Dtos
{
    public class MealLogDto
    {
        public int MealId {get;set;}
        public DateTime? DateEaten { get; set; }
    }
}