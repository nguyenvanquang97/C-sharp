using Library.Filter;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRepo.Wrappers;

namespace Library.Services.Pagging
{
    public class Pagging : IPagging
    {
        private readonly RepoDbContext _repoDbContext;

        public Pagging(RepoDbContext repoDbContext)
        {
            _repoDbContext = repoDbContext;

        }

        public PagedResponse<object> paging(IEnumerable<object> request, PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = request
                  .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                  .Take(validFilter.PageSize)
                  .ToList();

            var totalRecords = _repoDbContext.ProductImports.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)validFilter.PageSize);
            var totalRecordsInAllPages = validFilter.PageSize * totalPages;


            var response = new PagedResponse<object>(pagedData, totalRecordsInAllPages, totalPages, validFilter.PageNumber, validFilter.PageSize);

            return response;
        }
    }
}
