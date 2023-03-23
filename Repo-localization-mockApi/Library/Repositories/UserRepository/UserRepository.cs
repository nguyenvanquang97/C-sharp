using Azure.Core;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly RepoDbContext _repoDbContext;
        private readonly IConfiguration _configuration;
        public UserRepository(RepoDbContext repoDbContext, IConfiguration configuration)
        {
            _repoDbContext = repoDbContext;
            _configuration = configuration;

        }
 

        public async Task<string> ChooseRepository(int repositoryId, string token)
        {

            var userName = GetUserNameFromToken(token.Substring(7));

            var user = _repoDbContext.Users.Include(u => u.IdRepositories).SingleOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return null;
            }

            var repository = await _repoDbContext.Repositories.FindAsync(repositoryId);
            user.IdRepositories.Clear();
            user.IdRepositories.Add(repository);
            _repoDbContext.SaveChanges();

            string tokenRusult = CreateToken(user, repository.Name);
            return tokenRusult;
        }

        public string CreateToken(User user, string repoName)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.UserName));
            claims.Add(new Claim("RepoName", repoName));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                   _configuration["Jwt:Issuer"],
                   _configuration["Jwt:Audience"],
                   claims,
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _repoDbContext.Users
          .Include(i => i.IdRepositories)
          .FirstOrDefaultAsync(i => i.Id == id);

            if (user == null)
            {
                return null;
            }
            return user;
        }

        public string GetUserNameFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken securityToken;
            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var claim = claimsPrincipal.FindFirst(ClaimTypes.GivenName);

            if (claim != null)
            {
                return claim.Value;
            }

            return null;
        }

        public async Task<string> Login(string userName, string password)
        {

            var user = await _repoDbContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));

            if (user == null)
            {
                return "UserName not found";
            }
            else
            {
                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return "Wrong password";
                }

            }
            var token = CreateToken(user, "");
            return token;
        }

        public async Task<User> Register(string userName, string passWord)
        {
            string password = BCrypt.Net.BCrypt.HashPassword(passWord);
            User user = new User();

            user.Password = password;
            var matchingUsers = _repoDbContext.Users.Where(u => u.UserName == userName).ToList();
            if (matchingUsers.Count > 0)
            {
                return null;
            }
            user.UserName = userName;

            _repoDbContext.Users.Add(user);
            await _repoDbContext.SaveChangesAsync();
            return user;
        }

      
    }
}
