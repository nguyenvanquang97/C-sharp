using Library.Models;
using Library.Repositories.PersonRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Library.Models;

namespace TestRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET api/persons
        [HttpPost]
        public async Task<IActionResult> Get()
        {
           var persons=await _personRepository.GetAll();

            return Ok(persons);

        }
    }
}
