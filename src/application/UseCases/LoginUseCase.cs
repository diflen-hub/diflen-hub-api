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
        public async Task<UseCaseResult<LoginResponseDto>> ExecuteAsync(string email, string password)
        {
            var userFromDatabase = await userRepository.GetAsync(u => u.Email == email);

            var userDoesntExist = userFromDatabase is null;
            var passwordIsIncorrect = !Verify(password, userFromDatabase?.Password);

            if (userDoesntExist || passwordIsIncorrect) return new()
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
                    ExpiresIn = jwtService.GetExpirationDate(),
                    Message = "Successfully logged."
                },
            };
        }
    }
}