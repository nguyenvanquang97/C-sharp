using Test.Filter;

namespace Test.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
