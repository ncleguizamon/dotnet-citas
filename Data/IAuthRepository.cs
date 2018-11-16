using System.Threading.Tasks;
using DatingApp.API.models;
namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> register(User user , string  password);
         Task<User> login(string username, string password);
         Task<bool> UserExists(string username);
         
    }
}