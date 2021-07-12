using System.Collections.Generic;

namespace Task.BLL.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<StudentExam> Exams { get; set; }

    }
}
