using Azure.Core;
using Library.Models;
using Library.Request;
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

namespace Library.Repositories.ImportRepository
{

    public class ImportRepository : IImportRepository
    {
        private readonly RepoDbContext _repoDbContext;
        private readonly IConfiguration _configuration;
        public ImportRepository(RepoDbContext repoDbContext, IConfiguration configuration)
        {
            _repoDbContext = repoDbContext;
            _configuration = configuration;
        }
        public async Task<Import> Delete(int id)
        {
            var import = await _repoDbContext.Imports.FindAsync(id);
            if (import == null)
            {
                return null;
            }

            _repoDbContext.Imports.Remove(import);
            await _repoDbContext.SaveChangesAsync();
            return import;
        }

        public async Task<IEnumerable<Import>> GetAll()
        {
            var imports = await _repoDbContext.Imports.ToListAsync();


            return imports;
        }

        public async Task<Import> GetById(int id)
        {
            var import = await _repoDbContext.Imports
            .Include(i => i.ProductImports)
            .ThenInclude(pi => pi.IdProductNavigation)
            .FirstOrDefaultAsync(i => i.Id == id);


            if (import == null)
            {
                return null;
            }

            return import;
        }

        public async Task<Import> Post(UpsertImportRequest request,string token)
        {
            Import import = new Import();
           
            Hashtable userAndRepo = GetUserNameAndRepoFromToken(token.Substring(7));
            var userName = userAndRepo["userName"];
            var user = _repoDbContext.Users.SingleOrDefault(u => u.UserName == userName);
            var repoName = userAndRepo["repository"];
            var repository = _repoDbContext.Repositories.SingleOrDefault(u => u.Name == repoName);
            if (user == null)
            {
                return null;
            }
            if (repository == null)
            {
                return null;
            }

            import.IdUser = user.Id;
            import.DateImport = DateTime.Now;


            import.IdRepository = repository.Id;
            decimal? totalMoney = 0;
            foreach (var r in request.productImportRequest)
            {
                ProductImport productImport = new ProductImport();
                productImport.IdProduct = r.IdProduct;
                productImport.Price = r.Price;
                productImport.Amount = r.Amount;
                productImport.IntoMoney = r.Price * r.Amount;
                var p = await _repoDbContext.Products.FindAsync(r.IdProduct);
                if (p != null)
                {

                    productImport.IdProductNavigation = p;

                }
                totalMoney += productImport.IntoMoney;
                import.ProductImports.Add(productImport);
            }
            import.TotalMoney = totalMoney;

            _repoDbContext.Imports.Add(import);
            await _repoDbContext.SaveChangesAsync();

            return import;
        }

        public async Task<Import> Put(int id, Import import)
        {
            _repoDbContext.Entry(import).State = EntityState.Modified;

            try
            {
                await _repoDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportItemExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return import;
        }

        private Hashtable GetUserNameAndRepoFromToken(string token)
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

            var userName = claimsPrincipal.FindFirst(ClaimTypes.GivenName);
            var repository = claimsPrincipal.FindFirst("RepoName");
            Hashtable result = new Hashtable();
            result.Add("userName", userName.Value);
            result.Add("repository", repository.Value);
            if (result != null)
            {
                return result;
            }

            return null;
        }
        private bool ImportItemExists(long id)
        {
            return _repoDbContext.Imports.Any(e => e.Id == id);
        }
    }
}
