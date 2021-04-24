using System;

namespace Data.Entities
{
    public class UserWeight : Entity
    {
        public int UserId {get;set;}
        public User User {get;set;}
        public double Weight {get;set;}
        public DateTime Date {get;set;}
    }
}