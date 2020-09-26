
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Web.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using API.Web.Entities;
using System.Threading.Tasks;
using API.Web.Result;
using Microsoft.Extensions.Configuration;

namespace API.Web.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthenticationController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthenticationController(
            ILogger<MealsController> logger, 
            UserManager<User> userManager,
            SignInManager<User> signInManager, 
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] TokenAccessDto tokenAccess)
        {
            var user = await _userManager.FindByNameAsync(tokenAccess.Login);
            
            if(user == null)
            {
                return base.FromResult(new NotFoundResult<TokenAccessDto>(tokenAccess));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user,tokenAccess.Password,false);

            if(!result.Succeeded)
            {
                return base.FromResult(new NotFoundResult<TokenAccessDto>(tokenAccess));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Audience"],
                claims,
                expires:DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
                );

            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return Created("", result);
        }
    }
}
