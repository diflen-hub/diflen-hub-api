using System.Net;
using application.Dtos;
using Application.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using static BCrypt.Net.BCrypt;

namespace Application.UseCases
{
    public class LoginUseCase(IUserRepository userRepository, IJwtService jwtService)
    {
        public async Task<UseCaseResult<LoginResponseDto>> ExecuteAsync(string username, string password)
        {
            var userFromDatabase = await userRepository.GetAsync(u => u.Username == username);

            var passwordIsIncorrect = !Verify(password, userFromDatabase?.Password);

            if (userFromDatabase is null || passwordIsIncorrect) return new()
            {
                Content = new()
                {
                    Message = "E-mail ou senha incorretos",
                },
                StatusCode = HttpStatusCode.Unauthorized
            };

            return new()
            {
                Content = new LoginResponseDto()
                {
                    IsLogged = true,
                    AccessToken = jwtService.GenerateBearerToken(userFromDatabase),
                    Username = userFromDatabase.Username,
                    ExpiresIn = jwtService.GetExpirationDate(),
                    Message = "Successfully logged."
                },
            };
        }
    }
}