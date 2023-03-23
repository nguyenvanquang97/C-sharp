using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Models;
using Library.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using NuGet.Packaging;
using Microsoft.AspNetCore.Authorization;
using Library.Repositories.UserRepository;
using Microsoft.Extensions.Localization;

namespace TestRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IStringLocalizer<SharedResource> stringLocalizer;
        public UserController(IUserRepository userRepository,IStringLocalizer<SharedResource> stringLocalizer)
        {
          this.userRepository = userRepository;
          this.stringLocalizer= stringLocalizer;

        }
     
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterRequest request)
        {
            User user = await userRepository.Register(request.UserName,request.Password);
            if (user == null)
            {
                var response = stringLocalizer["UserName exist"];
                return BadRequest(response.Value);
            }
            return user;
        }
        [HttpPut]
        [Route("users/repositories")]
        [Authorize]
        public async Task<ActionResult<User>> AddRepositories([FromBody]int repositoryId)
        {
            string token = Request.Headers["Authorization"];

            var tokenResult = userRepository.ChooseRepository(repositoryId,token);
            return Ok(tokenResult.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
          var user=await userRepository.GetById(id);
            if(user == null)
            {
                var request = stringLocalizer["Not found"];
                return BadRequest(request.Value + id);
            }
            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(RegisterRequest LoginRequest)
        {

            var token = await userRepository.Login(LoginRequest.UserName, LoginRequest.Password);
            if (token.Equals("UserName not found")) {
                var request = stringLocalizer["UserName not found"];
                return BadRequest(request.Value);
            }
            if(token.Equals("Wrong password"))
            {
                var request = stringLocalizer["Wrong password"];
                return BadRequest(request.Value);
            }
            return Ok(token);   

        }
      

            
      
     

    }
}
