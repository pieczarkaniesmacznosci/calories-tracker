using System;

namespace API.Dtos
{
    public class ResponseTokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
