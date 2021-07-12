using System.Collections.Generic;

namespace Task.BLL.Models
{
    public class StudentExam
    {
        public int ID { get; set; }

        public int StudentID { get; set; }
        public int ExamID { get; set; }

        public virtual Student Student { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual ICollection<StudentExamAnswer> ExamAnswers { get; set; } = new HashSet<StudentExamAnswer>();
    }
}
