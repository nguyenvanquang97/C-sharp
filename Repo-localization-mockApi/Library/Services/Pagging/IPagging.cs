using Library.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRepo.Wrappers;

namespace Library.Services.Pagging
{
    public interface IPagging
    {
        PagedResponse<object> paging(IEnumerable<object> request, PaginationFilter filter);
    }
}
