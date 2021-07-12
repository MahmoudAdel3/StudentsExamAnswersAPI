using System.Collections.Generic;

namespace Task.BLL.DTOs
{
    public class ExamStudentAnswersDTO
    {
        public int ExamID { get; set; }
        public List<StudentExamAnswersDTO> ExamAnswers { get; set; }
    }
}
