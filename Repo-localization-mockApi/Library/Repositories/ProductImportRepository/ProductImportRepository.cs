using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Filter;
using TestRepo.Wrappers;

namespace Library.Repositories.ProductImportRepository
{
    public class ProductImportRepository : IProductImportRepository
    {
        private readonly RepoDbContext _repoDbContext;

        public ProductImportRepository(RepoDbContext repoDbContext)
        {
            _repoDbContext = repoDbContext;

        }

        public async Task<IEnumerable<object>> GetAll(int id)
        {
            var productImports =await  _repoDbContext.ProductImports
               .Include(pi => pi.IdProductNavigation)
               .Include(pi => pi.IdImportNavigation)
               .ThenInclude(pi => pi.IdUserNavigation)
               .Where(pi => pi.IdImportNavigation.IdRepository == id)
               .ToListAsync();

            var result = productImports.Select(pi => new {
                pi.Id,
                pi.IdProduct,
                ProductName = pi.IdProductNavigation.Name,
                pi.IdImport,
                pi.Price,
                pi.Amount,
                pi.IntoMoney,
                DateImport = pi.IdImportNavigation.DateImport,
                UserImport = pi.IdImportNavigation.IdUserNavigation.UserName,
                Repository = pi.IdImportNavigation.IdRepository
            });


        
            return result;

           
        }

      
    }
}
