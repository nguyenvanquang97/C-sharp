using Library.Models;
using Library.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.RepositoryRepo
{
    public class RepoRepository : IRepoRepository
    {
        private readonly RepoDbContext _repoDbContext;

        public RepoRepository(RepoDbContext repoDbContext)
        {
            _repoDbContext = repoDbContext;

        }
        public async Task<Repository> Delete(int id)
        {
            var repository = await _repoDbContext.Repositories.FindAsync(id);
            if (repository == null)
            {
                return null;
            }

            _repoDbContext.Repositories.Remove(repository);
            await _repoDbContext.SaveChangesAsync();
            return repository;
        }

        public async Task<IEnumerable<Repository>> GetAll()
        {
            var repository = await _repoDbContext.Repositories
           .ToListAsync();

            return repository;
        }

        public async Task<Repository> GetById(int id)
        {
            var repository = await _repoDbContext.Repositories.FindAsync(id);

            if (repository == null)
            {
                return null;
            }

            return repository;
        }

        public async Task<Repository> Post(UpsertRepositoryRequest request)
        {
            Repository repository = new Repository();
            var matchingRepos = _repoDbContext.Repositories.Where(u => u.Name == request.Name).ToList();
            if (matchingRepos.Count > 0)
            {
                return null;
            }
            repository.Name = request.Name;
            repository.Address = request.Address;


            _repoDbContext.Repositories.Add(repository);
            await _repoDbContext.SaveChangesAsync();
            return repository;
        }

        public async Task<Repository> Put(int id, UpsertRepositoryRequest request)
        {


            var repository = await _repoDbContext.Repositories.FindAsync(id);

            if (repository == null)
            {
                return null;
            }
            repository.Name = request.Name;
            repository.Address = request.Address;
          

            await _repoDbContext.SaveChangesAsync();

            return repository;
        }
    }
}
