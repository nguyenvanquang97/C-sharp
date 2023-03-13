using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Filter;
using Test.Models;
using Test.Request;
using Test.Wrappers;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BooksContext _booksContext;
        private readonly IConfiguration _configuration;
        public UserController(BooksContext booksContext,IConfiguration configuration)
        {
            _booksContext = booksContext;
            _configuration = configuration;


        }
        // GET: api/<ValuesController>
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UpsertUserRequest request)
        {
            string password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            User user = new User();

            user.Password = password;
            user.UserName= request.UserName;
            _booksContext.Users.Add(user);
            await _booksContext.SaveChangesAsync();

            
            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UpsertUserRequest request)
        {
           

            var user = await _booksContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(request.UserName));
         
            if (user == null)
            {
                return BadRequest("UserName not found");
            }
            else
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    return BadRequest("Wrong password");
                }
           
            }
            string token = CreateToken(user);
            return Ok(token);

        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:creds
                );

            var jwt =new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
