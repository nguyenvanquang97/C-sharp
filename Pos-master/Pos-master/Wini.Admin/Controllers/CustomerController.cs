using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wini.Admin.Models;
using Wini.DA;
using Wini.Database;
using Wini.Simple;

namespace Wini.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private const string Issuer = "wini";
        private readonly ILogger<HomeController> _logger;
        private readonly IUserDA _userDa;
        private readonly AppSetting _appSettings;

        public CustomerController(ILogger<HomeController> logger, IUserDA userDa, IOptions<AppSetting> appSettings)
        {
            _logger = logger;
            _userDa = userDa;
            _appSettings = appSettings.Value;
        }

        //public BaseResponse<IList<Users>> Index()
        //{
        //    var b = _customerDA.GetAllAsync();
        //    var c = new BaseResponse<IList<Users>>() { Data = b };
        //    return c;
        //}

        [HttpPost]
        public BaseResponse<bool> Registor([FromBody] Users user)
        {
            var b = _userDa.Registor(user);
            _userDa.Save();
            return b;
        }

        [HttpPost]
        public BaseResponse<string> Login([FromBody] Users user)
        {
            var b = _userDa.Login(user);
            if (b.Code != 200)
            {
                return new BaseResponse<string>() { Code = -1, Message = b.Message };
            }

            var token = GenerateToken(b.Data);


            return new BaseResponse<string>() { Code = 200, Message = token };
        }

        //[HttpPost]
        //public BaseResponse<IList<Users>> GetAll()
        //{
        //    var b = _customerDA.GetAllAsync();

        //    return new BaseResponse<IList<Users>>() { Code = 200, Data = b.Take(10).ToList() };
        //}



        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim("Id",user.Id.ToString()),

            };
            var token = new JwtSecurityToken(Issuer,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }

}