using DatingApp.API.models;
using Microsoft.EntityFrameworkCore;
namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options):base(options){}

        public DbSet<value>   Values {get;set;}// llama el 
        public DbSet<User> Users{get;set;}
        public DbSet<Photo> Photos {get; set;}
        
        
        
    }
}