using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Library.Request;


namespace Library.Repositories.ProductRepo
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Post(UpsertProductRequest request);
        Task<Product> Put(int id,UpsertProductRequest request);
        Task<Product> Delete(int id);
    }
}
