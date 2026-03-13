using Application.Dtos;
using Domain.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases
{
    public class GetLessonsUseCase(IUnityRepository unityRepository, ILessonRepository lessonRepository, ILessonService lessonService)
    {
        public async Task<UseCaseResult<List<LessonDtoOut>>> ExecuteAsync(string unityName, Guid publicUserId)
        {
            var unity = await unityRepository.GetAsync(u => u.Name == unityName);
            if (unity is null) return new() { Content = [] };

            var dblessons = await lessonRepository.GetListAsync(l => l.UnityId == unity.Id);

            var lessons = new List<LessonDtoOut>();

            foreach (var lesson in dblessons)
            {
                lessons.Add(new LessonDtoOut
                {
                    PublicId = lesson.PublicId,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    VideoUrl = lesson.VideoUrl,
                    Concluded = await lessonService.LessonAreAlreadyAnswered(publicUserId, lesson.PublicId),
                });
            }

            return new()
            {
                Content = lessons,
            };
        }
    }
}