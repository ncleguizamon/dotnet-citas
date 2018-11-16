using System.ComponentModel.DataAnnotations;
namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(45, MinimumLength= 4 , ErrorMessage="Formato no valido maximo 45 minimo 4 caracteres")] 
        public string Username{get;set;}
         [Required]
         [StringLength(8 , MinimumLength= 4 , ErrorMessage="Formato no valido maximo 8 minimo 4 caracteres")] 
        public string Password{get;set;}
        
    }
}