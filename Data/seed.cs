using System.Collections.Generic;
using DatingApp.API.models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class seed
    {
        private readonly DataContext _context;
        public seed(DataContext context)
        {
            _context = context;
        }
        public void SeedUser()
        {
            // _context.Users.RemoveRange(_context.Users);
            // _context.SaveChanges();

            // seed users
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var  users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                //create the password hash
                byte[] passwordHash, passwordSalt;
                createPasswordHash("password", out passwordHash , out passwordSalt);
                user.PasswordHash= passwordHash;
                user.PasswordSalt =passwordSalt;
                user.Username = user.Username.ToLower();
                _context.Users.Add(user);
            }
_context.SaveChanges();// guarda la accion 

        }
   private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}