using System.Net;
using Application.Dtos;
using Domain.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class GetLessonUseCase(ILessonRepository lessonRepository, ILessonService lessonService)
    {
        public async Task<UseCaseResult<LessonDtoOut>> ExecuteAsync(Guid publicLessonId, Guid publicUserId)
        {
            var lessonFromDb = await lessonRepository.GetLesson(publicLessonId);
            if (lessonFromDb is null) return new() { StatusCode = HttpStatusCode.NoContent };

            return new()
            {
                Content = new LessonDtoOut()
                {
                    PublicId = lessonFromDb.PublicId,
                    Description = lessonFromDb.Description,
                    Title = lessonFromDb.Title,
                    VideoUrl = lessonFromDb.VideoUrl,
                    Concluded = await lessonService.LessonAreAlreadyAnswered(publicUserId, lessonFromDb.PublicId),
                },
            };
        }
    }
}