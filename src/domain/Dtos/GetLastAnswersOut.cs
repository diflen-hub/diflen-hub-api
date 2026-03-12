namespace Domain.Dtos
{
    public class GetLastAnswersOut
    {
        public List<AnswerVerifyOut> Answers { get; set; } = [];
        public int CurrentPointsWeight { get; set; }
        public bool WasAllQuestionsCorrectlyAnswered { get; set; }
        public bool WasCertificateAlreadyIssued { get; set; }
    }
}