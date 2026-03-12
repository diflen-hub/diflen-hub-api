using System.Net;
using Application.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Models;
using static BCrypt.Net.BCrypt;

namespace Application.UseCases
{
    public class RegisterUseCase(IUserRepository userRepository)
    {
        public async Task<UseCaseResult<object>> ExecuteAsync(string email, string username, string password)
        {
            var user = new User()
            {
                Email = email,
                Username = username,
                Password = HashPassword(password)
            };

            await userRepository.InsertAsync(user);

            return new()
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Usuário criado com sucesso"
            };
        }
    }
}