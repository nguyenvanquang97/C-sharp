using Library.Models;
using Library.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.SupplierRepository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly RepoDbContext _repoDbContext;

        public SupplierRepository(RepoDbContext repoDbContext)
        {
            _repoDbContext = repoDbContext;

        }
        public async Task<Supplier> Delete(int id)
        {
            var supplier = await _repoDbContext.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return null;
            }

            _repoDbContext.Suppliers.Remove(supplier);
            await _repoDbContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {

            var suppliers = await _repoDbContext.Suppliers
           .ToListAsync();

            return suppliers;
        }

        public async Task<Supplier> GetById(int id)
        {
            var supplier = await _repoDbContext.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return null;
            }

            return supplier;
        }

        public async Task<Supplier> Post(UpsertSupplierRequest request)
        {

            Supplier supplier = new Supplier();
            supplier.Name = request.Name;
            supplier.PhoneNumber = request.PhoneNumber;
            supplier.Address = request.Address;
            supplier.Email = request.Email;


            _repoDbContext.Suppliers.Add(supplier);
            await _repoDbContext.SaveChangesAsync();

            return supplier;

        }

        public async Task<Supplier> Put(int id, Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return null;
            }

            _repoDbContext.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _repoDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierItemExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return supplier;
        }

        private bool SupplierItemExists(long id)
        {
            return _repoDbContext.Suppliers.Any(e => e.Id == id);
        }
    }
}
