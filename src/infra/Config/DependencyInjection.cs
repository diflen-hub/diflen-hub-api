using domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using infra.Services;
using Infra;
using Infra.Repositories;
using Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace infra.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            AddRepositories(services);
            AddServices(services);
            
            return services;
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IUnityRepository, UnityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAlternativeRepository, AlternativeRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IAlternativeService, AlternativeService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddTransient<IPlaylistService, PlaylistService>();
        }
    }
}