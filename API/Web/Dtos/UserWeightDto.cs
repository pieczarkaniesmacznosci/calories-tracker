using System;

namespace API.Web.Dtos
{
    public class UserWeightDto
    {
        public int Id {get;set;}

        public int UserId {get;set;}
        public double Weight {get;set;}
        public DateTime Date {get;set;}
    }
}