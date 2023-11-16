
using API.Dtos;
using API.Result;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Result;

namespace API.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class AuthenticationController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthenticationController(
            ILogger<MealsController> logger,
            SignInManager<User> signInManager,
            IConfiguration config,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(TokenAccessDto tokenAccess)
        {
            var user = await _userManager.FindByNameAsync(tokenAccess.Login);

            if (user == null)
            {
                return base.FromResult(new UnauthorizedResult<TokenAccessDto>(tokenAccess));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, tokenAccess.Password, false);

            if (!result.Succeeded)
            {
                return base.FromResult(new UnauthorizedResult<TokenAccessDto>(tokenAccess));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
                );

            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return base.FromResult(new SuccessResult<object>(results));
        }
    }
}
