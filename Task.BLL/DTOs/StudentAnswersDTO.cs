namespace Task.BLL.DTOs
{
    public class StudentAnswersDTO
    {
        public string Question { get; set; }
        public bool IsCorrect { get; set; }
        public bool TrueOrFalseAnswer { get; set; }
        public string MCQAnswer { get; set; }
    }
}
