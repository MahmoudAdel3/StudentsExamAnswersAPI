using System.Collections.Generic;
using static Task.BLL.Enums;
namespace Task.BLL.Models
{
    public class ExamQuestion
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool Answer { get; set; }

        public int ExamID { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual ICollection<MultipleChoiceQuestionAnswer> Answers { get; set; }
        public virtual ICollection<StudentExamAnswer> StudentExamAnswers { get; set; }
    }
}
