using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Login
{
    public class LoginDtoIn
    {

        [Description("E-mail previamente criado através do endpoint `api/user/register`.")]
        [Required]
        public required string Email { get; set; }

        [Description("Senha previamente criada através do endpoint `api/user/register`.")]
        [Required]
        public required string Password { get; set; }
    }
}