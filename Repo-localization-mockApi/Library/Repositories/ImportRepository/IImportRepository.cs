using Library.Models;
using Library.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.ImportRepository
{
    public interface IImportRepository
    {
        Task<IEnumerable<Import>> GetAll();
        Task<Import> GetById(int id);
        Task<Import> Post(UpsertImportRequest request,string token);
        Task<Import> Put(int id, Import import);
        Task<Import> Delete(int id);
    }
}
