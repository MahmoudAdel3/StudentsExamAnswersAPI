namespace Task.BLL.DTOs
{
    public class StudentExamAnswersDTO
    {
        public int QuestionID { get; set; }
        public bool Answer { get; set; }
        public int? SelectedMCQAnswer { get; set; }
    }
}
