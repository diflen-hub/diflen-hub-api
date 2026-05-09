using System.Net;
using application.Dtos;
using domain.Interfaces.Repositories;
using domain.Models;
using static BCrypt.Net.BCrypt;

namespace application.UseCases
{
    public class RegisterUseCase(IUserRepository userRepository)
    {
        public async Task<UseCaseResult<string>> ExecuteAsync(string username, string password)
        {
            var user = new User()
            {
                Username = username,
                Password = HashPassword(password)
            };

            await userRepository.InsertAsync(user);

            return new()
            {
                StatusCode = HttpStatusCode.Created,
                Content = "Usuário criado com sucesso"
            };
        }
    }
}