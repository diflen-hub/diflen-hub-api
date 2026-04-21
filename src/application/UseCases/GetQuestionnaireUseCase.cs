using Application.Dtos;
using Domain.Dtos;
using Domain.Interfaces.Repositories;

namespace application.UseCases
{
    public class GetQuestionnaireUseCase(ILessonRepository _lessonRepository, IQuestionRepository _questionRepository)
    {
        public async Task<UseCaseResult<IEnumerable<QuestionDtoOut>>> ExecuteAsync(string unityName, string lessonName)
        {
            var lessonFromDb = await _lessonRepository.GetAsync(u => u.Title == lessonName && u.Unity.Name == unityName);
            if (lessonFromDb is null) return new();

            var questionsFromDb = await _questionRepository.GetListAsync(lessonFromDb.Id);

            return new()
            {
                Content = questionsFromDb.Select(question => new QuestionDtoOut
                {
                    PublicId = question.PublicId,
                    Statement = question.Statement,
                    Alternatives = question.Alternatives.Select(alt => new AlternativeDtoOut
                    {
                        PublicId = alt.PublicId,
                        Text = alt.Text
                    })
                }),
            };
        }
    }
}