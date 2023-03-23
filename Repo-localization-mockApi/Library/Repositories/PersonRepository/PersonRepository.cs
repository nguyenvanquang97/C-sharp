using Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.PersonRepository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly RepoDbContext _repoDbContext;
        private readonly HttpClient httpClient;
        public PersonRepository(RepoDbContext repoDbContext, HttpClient httpClient)
        {
            _repoDbContext = repoDbContext;
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Person>> GetAll()
        {
            string json = await httpClient.GetStringAsync("https://641ad8401f5d999a44546d9a.mockapi.io/sdfe");
            List<Person> persons = JsonConvert.DeserializeObject<List<Person>>(json);

            _repoDbContext.People.AddRangeAsync(persons);
            _repoDbContext.SaveChangesAsync();

            return persons;
        }
    }
}
