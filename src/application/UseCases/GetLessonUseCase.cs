using System.Net;
using Application.Dtos;
using Domain.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class GetLessonUseCase(IUnityRepository unityRepository, ILessonRepository lessonRepository, ILessonService lessonService, IAnswerService answerService)
    {
        public async Task<UseCaseResult<LessonDtoOut>> ExecuteAsync(string unityName, Guid publicLessonId, Guid publicUserId)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return new() { StatusCode = HttpStatusCode.NoContent };

            var lessonFromDb = await lessonRepository.GetLesson(publicLessonId);
            if (lessonFromDb is null) return new() { StatusCode = HttpStatusCode.NoContent };

            var questions = lessonFromDb.Questions
                .Select(q => new QuestionDtoOut
                {
                    PublicId = q.PublicId,
                    Statement = q.Statement,
                    Alternatives = q.Alternatives
                    .Select(a => new AlternativeDtoOut
                    {
                        PublicId = a.PublicId,
                        QuestionId = a.QuestionId,
                        Text = a.Text
                    })
                    .ToList(),
                })
                .ToList();

            return new()
            {
                Content = new LessonDtoOut()
                {
                    PublicId = lessonFromDb.PublicId,
                    Description = lessonFromDb.Description,
                    Title = lessonFromDb.Title,
                    VideoUrl = lessonFromDb.VideoUrl,
                    Questions = questions,
                    GetLastAnswersOut = await answerService.GetLastAnswersAsync(publicUserId, publicLessonId),
                    Concluded = await lessonService.LessonAreAlreadyAnswered(publicUserId, lessonFromDb.PublicId),
                },
            };
        }
    }
}