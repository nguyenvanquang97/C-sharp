using Library.Models;
using Library.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.RepositoryRepo
{
    public interface IRepoRepository
    {
        Task<IEnumerable<Repository>> GetAll();
        Task<Repository> GetById(int id);
        Task<Repository> Post(UpsertRepositoryRequest request);
        Task<Repository> Put(int id, UpsertRepositoryRequest request);
        Task<Repository> Delete(int id);
    }
}
