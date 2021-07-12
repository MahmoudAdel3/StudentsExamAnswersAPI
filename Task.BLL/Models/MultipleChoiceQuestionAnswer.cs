using System.Collections.Generic;

namespace Task.BLL.Models
{
    public class MultipleChoiceQuestionAnswer
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public int ExamQuestionID { get; set; }

        public virtual ExamQuestion ExamQuestion { get; set; }
        public virtual ICollection<StudentExamAnswer> StudentExamAnswers { get; set; }
    }
}
