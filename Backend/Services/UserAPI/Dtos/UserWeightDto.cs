namespace UserService.Dtos
{
    public class UserWeightDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }
    }
}
