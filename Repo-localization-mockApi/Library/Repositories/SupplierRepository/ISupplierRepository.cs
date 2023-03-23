using Library.Models;
using Library.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.SupplierRepository
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAll();
        Task<Supplier> GetById(int id);
        Task<Supplier> Post(UpsertSupplierRequest request);
        Task<Supplier> Put(int id, Supplier supplier);
        Task<Supplier> Delete(int id);
    }
}
