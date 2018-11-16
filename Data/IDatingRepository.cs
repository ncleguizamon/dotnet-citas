using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> Saveall();
        Task<IEnumerable<User>> getUsers();
        Task<User> getUser(int id);
        

         
    }
}