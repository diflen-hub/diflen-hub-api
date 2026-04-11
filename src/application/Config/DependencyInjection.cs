using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace application.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            AddUseCases(services);
            return services;
        }

        private static void AddUseCases(IServiceCollection services)
        {
            // usecases
            services.AddScoped<GetLessonsUseCase>();
            services.AddScoped<GetLessonUseCase>();
            services.AddScoped<IssueCertificateUseCase>();
            services.AddScoped<LoginUseCase>();
            services.AddScoped<RegisterUseCase>();
            services.AddScoped<VerifyAnswersUseCase>();
            services.AddScoped<GetUnityUseCase>();
        }
    }
}