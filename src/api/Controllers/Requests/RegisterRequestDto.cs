using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace api.Controllers.Requests
{
    public class RegisterRequestDto
    {
        [Description("E-mail comum.")]
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Description("Nome de usuário, nickname")]
        [Required]
        public required string Username { get; set; }
        
        [Description("Senha que será utilizada no login")]
        [Required]
        public required string Password { get; set; }
    }
}