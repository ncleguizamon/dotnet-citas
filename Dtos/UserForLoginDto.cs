using System.ComponentModel.DataAnnotations;
namespace DatingApp.API.Dtos
{
    public class UserForLoginDto
    {
        public int Id{get;set;}
        public string Username{get;set;}
    
        public string Password{get;set;}
          public int rol {get; set;}
    }
}