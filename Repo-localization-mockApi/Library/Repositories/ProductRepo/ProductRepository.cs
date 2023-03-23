using Library.Models;
using Library.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly RepoDbContext _repoDbContext;

        public ProductRepository(RepoDbContext repoDbContext)
        {
            _repoDbContext = repoDbContext;
            
        }

        public async Task<Product> Delete(int id)
        {
            var product = await _repoDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            _repoDbContext.Products.Remove(product);
            await _repoDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products =await _repoDbContext.Products
           .Include(pi => pi.IdSupplierNavigation)
           .ToListAsync();

            return products;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _repoDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<Product> Post(UpsertProductRequest request)
        {
            Product product = new Product();
            product.Name = request.Name;
            product.IdSupplier = request.IdSupplier;
            product.Price = request.Price;
            product.Unit = request.Unit;

            var supplier = await _repoDbContext.Suppliers.FindAsync(request.IdSupplier);
            if (supplier != null)
            {

                product.IdSupplierNavigation = supplier;

            }
            _repoDbContext.Products.Add(product);
            await _repoDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Put(int id, UpsertProductRequest request)
        {

            var product = await _repoDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }
            product.Name = request.Name;
            product.IdSupplier = request.IdSupplier;
            product.Price = request.Price;
            product.Unit  = request.Unit;
       
            await _repoDbContext.SaveChangesAsync();

            return product;
        }
    }
}
