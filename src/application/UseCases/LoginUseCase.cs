using System.Net;
using application.Dtos;
using Application.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class LoginUseCase(IUserRepository userRepository, IJwtService jwtService)
    {
        public async Task<UseCaseResult<LoginResponseDto>> ExecuteAsync(string email, string password)
        {
            var userFromDatabase = await userRepository.GetAsync(u => u.Email == email);

            if (userFromDatabase is null) return new()
            {
                Content = new()
                {
                    IsLogged = false,
                    Message = "Usuário ou senha incorreto",
                },
                StatusCode = HttpStatusCode.Unauthorized
            };

            if (!BCrypt.Net.BCrypt.Verify(password, userFromDatabase.Password)) return new()
            {
                Content = new()
                {
                    IsLogged = false,
                    Message = "Usuário ou senha incorreto",
                },
                StatusCode = HttpStatusCode.Unauthorized
            };

            return new()
            {
                Content = new LoginResponseDto()
                {
                    IsLogged = true,
                    AccessToken = jwtService.GenerateBearerToken(userFromDatabase),
                    ExpiresIn = jwtService.GetExpirationDate(),
                    Message = "Successfully logged."
                },
            };
        }
    }
}