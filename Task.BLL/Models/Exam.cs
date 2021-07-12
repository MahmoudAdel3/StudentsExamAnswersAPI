using System;
using System.Collections.Generic;

namespace Task.BLL.Models
{
    public class Exam
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<StudentExam> Students { get; set; }
        public virtual ICollection<ExamQuestion> Questions { get; set; } = new HashSet<ExamQuestion>();
    }
}
