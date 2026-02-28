using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class RegisterDtoIn
    {
        [Description("E-mail comum.")]
        [Required]
        public required string Email { get; set; }

        [Description("Nome de usuário, nickname")]
        [Required]
        public required string Username { get; set; }
        
        [Description("Senha que será utilizada no login")]
        [Required]
        public required string Password { get; set; }
    }
}