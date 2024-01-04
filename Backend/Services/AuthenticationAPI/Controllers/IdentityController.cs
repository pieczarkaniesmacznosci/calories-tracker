using Entities;
using JwtAuthenticationManager.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TimeSpan _tokenLifetime = TimeSpan.FromMinutes(30);

        public IdentityController(
            IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] AuthenticationRequest authenticationRequest)
        {

            User user = await _userManager.FindByNameAsync(authenticationRequest.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, authenticationRequest.Password, false);

            if (!signInResult.Succeeded)
            {
                return Unauthorized();
            }

            List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
            ];

            SigningCredentials credentials = new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TOKEN_KEY"])), SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler tokenHandler = new();

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_tokenLifetime),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = credentials
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);

            return Ok(jwt);
        }
    }
}
