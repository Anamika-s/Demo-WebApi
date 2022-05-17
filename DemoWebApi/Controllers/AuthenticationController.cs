using DemoWebApi.DBContext;
using DemoWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthenticationController(AppDbContext context , IConfiguration config)
        {
            _config = config;
            _context = context;

        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();
            var obj = Authenticate(user);
            if(user!=null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        User Authenticate (User user)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == user.UserName
            && x.Password == user.Password);
        }
        string GenerateJSONWebToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Isssuer"],
                _config["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
