using Sistema_de_controle_cursos_alunos.Context;
using Sistema_de_controle_cursos_alunos.Model;

namespace Sistema_de_controle_cursos_alunos.Controller
{
    public class StudentController
    {
        private readonly AppDbContext context;

        public StudentController(AppDbContext context)
        {
            this.context = context;
        }

        public void AddStudent(string name, string email, string birthDate)
        {
            context.Add(new Student { Name = name, Email = email, BirthDate = birthDate });
            context.SaveChanges();
        }

        public void RemoveStudent(int id)
        {
            var student = context.Students.Find(id);
            if (student != null)
            {
                context.Remove(student);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(int id, string name, string email, string birthDate)
        {
            var student = context.Students.Find(id);
            if (student != null)
            {
                student.Name = name;
                student.Email = email;
                student.BirthDate = birthDate;
                context.SaveChanges();
            }
        }

        public Student? GetStudentById(int id)
        {
            return context.Students?.Find(id);
        }

        public Student[] GetAllStudents()
        {
            return context.Students?.ToArray() ?? [];
        }
    }
}
