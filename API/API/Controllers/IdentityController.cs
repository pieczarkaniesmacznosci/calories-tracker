using API.Dtos;
using API.Result;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class IdentityController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TimeSpan _tokenLifetime = TimeSpan.FromMinutes(30);

        public IdentityController(
            ILogger<MealsController> logger,
            IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken(TokenAccessDto tokenAccess)
        {
            User user = await _userManager.FindByNameAsync(tokenAccess.Login);

            if (user == null)
            {
                return FromResult(new UnauthorizedResult<TokenAccessDto>(tokenAccess));
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, tokenAccess.Password, false);

            if (!signInResult.Succeeded)
            {
                return FromResult(new UnauthorizedResult<TokenAccessDto>(tokenAccess));
            }

            List<Claim> claims = new()
            {
                new (JwtRegisteredClaimNames.Sub, user.Email ),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Email, user.Email),
            };

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

            return FromResult(new SuccessResult<string>(jwt));
        }
    }
}
