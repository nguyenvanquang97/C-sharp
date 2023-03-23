using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.PersonRepository
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
    }
}
