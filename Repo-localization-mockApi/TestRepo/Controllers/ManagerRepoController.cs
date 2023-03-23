using Library.Repositories.ProductImportRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Library.Filter;
using TestRepo.Wrappers;
using Library.Services.Pagging;

namespace TestRepo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ManagerRepoController : ControllerBase
    {
        private readonly IProductImportRepository productImportRepository;
        private readonly IPagging pagging;
        public ManagerRepoController(IProductImportRepository productImportRepository,IPagging pagging)
        {
            this.productImportRepository = productImportRepository;
            this.pagging = pagging;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> getAllProductInRepo(int id, [FromQuery] PaginationFilter filter)
        {
            var productImports = await productImportRepository.GetAll(id);
            var result= pagging.paging(productImports,filter);
            return Ok(result);
        }


        private string GeneratePageUrl(int pageNumber, int pageSize)
        {
            return Url.Link("GetAllProducts", new { PageNumber = pageNumber, PageSize = pageSize });
        }

    }
   
}
