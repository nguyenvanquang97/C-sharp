using Library.Models;
using Library.Repositories.RepositoryRepo;
using Library.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;


namespace TestRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepoRepository repositoryRepository;

        public RepositoryController(IRepoRepository repositoryRepository)
        {
            this.repositoryRepository = repositoryRepository;

        }
        [HttpGet]
        public async Task<ActionResult> getAllRepository()
        {
            var repositories =await repositoryRepository.GetAll();
            return Ok(repositories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Repository>> GetRepositoryItem(int id)
        {
            var repository = await repositoryRepository.GetById(id);


            return Ok(repository);
        }

        [HttpPost]
        public async Task<ActionResult<Repository>> PostRepositoryItem(UpsertRepositoryRequest request)
        {


            var repository =await repositoryRepository.Post(request);
            return Ok(repository);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepositoryItem(int id, UpsertRepositoryRequest request)
        {

            var repository = await repositoryRepository.Put(id, request);

            return Ok(repository);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepositoryItem(int id)
        {
            var repository = await repositoryRepository.Delete(id);


            return Ok(repository);
        }


    }
}
