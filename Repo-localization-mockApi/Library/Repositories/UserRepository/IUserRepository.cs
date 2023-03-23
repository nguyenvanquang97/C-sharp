using Library.Models;
using Library.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.UserRepository
{
    public interface IUserRepository
    {
       
        Task<User> GetById(int id);
        Task<string> Login(string userName,string passWord);
        Task<User> Register(string userName, string password);
        string CreateToken(User user,string repoName);
        Task<string> ChooseRepository(int id,string token);
        String GetUserNameFromToken(string token);
    }
}
