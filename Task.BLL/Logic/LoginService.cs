using System.Linq;
using Task.BLL.Models;

namespace Task.BLL.Logic
{
    public class LoginService
    {
        private readonly ApplicationDbContext _context;
        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Student Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var student = _context.Student.FirstOrDefault(x => x.Email == email && x.Password == password);

            // check if username exists
            if (student == null)
                return null;

            return student;
        }
        public bool StudentExists(int userID)
        {
            return _context.Student.Any(s => s.ID == userID);
        }
    }
}
