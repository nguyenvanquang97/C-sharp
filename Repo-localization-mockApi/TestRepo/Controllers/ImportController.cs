using Library.Models;
using Library.Repositories.ImportRepository;
using Library.Repositories.RepositoryRepo;
using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace TestRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IImportRepository importRepository;
       
        public ImportController(IImportRepository importRepository)
        {
            this.importRepository = importRepository;
           
    }
        [HttpGet]
        public async Task<ActionResult> getAllImport()
        {
            var imports = await importRepository.GetAll();
            return Ok(imports);
        }
     
        [HttpGet("{id}")]
        public async Task<ActionResult<Import>> GetImportItem(int id)
        {
            var import = await importRepository.GetById(id);

          
            return Ok(import);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Import>> PostImportItem(UpsertImportRequest request)
        {
           
            string token = Request.Headers["Authorization"];
            var import =await importRepository.Post(request, token);
            return Ok(import);
        }


    
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportItem(int id, Import request)
        {

            var import= await importRepository.Put(id,request);

            return Ok(import);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportItem(int id)
        {
            var import = await importRepository.Delete(id);
           

            return Ok(import);
        }
 
    
    }
}
