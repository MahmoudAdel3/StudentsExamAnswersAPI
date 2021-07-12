namespace Task.BLL.Models
{
    public class StudentExamAnswer
    {
        public int ID { get; set; }

        public bool ISCorrect { get; set; }
        public bool TrueOrFalseAnswer { get; set; }

        public int ExamQuestionID { get; set; }
        public int StudentExamID { get; set; }
        public int? MultipleChoiceQuestionAnswerID { get; set; }

        public virtual MultipleChoiceQuestionAnswer MultipleChoiceQuestionAnswer { get; set; }
        public virtual StudentExam StudentExam { get; set; }
        public virtual ExamQuestion ExamQuestion { get; set; }
    }
}
