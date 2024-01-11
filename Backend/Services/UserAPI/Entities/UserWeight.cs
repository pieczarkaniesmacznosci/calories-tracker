namespace Entities
{
    public class UserWeight : Entity
    {
        public Guid UserId { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }
    }
}
