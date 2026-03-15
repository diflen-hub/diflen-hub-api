using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace api.Controllers.Requests
{
    public class LoginRequestDto
    {

        [Description("E-mail previamente criado através do endpoint `api/user/register`.")]
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Description("Senha previamente criada através do endpoint `api/user/register`.")]
        [Required]
        public required string Password { get; set; }
    }
}