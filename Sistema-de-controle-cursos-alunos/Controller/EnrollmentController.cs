using Sistema_de_controle_cursos_alunos.Context;
using Sistema_de_controle_cursos_alunos.Model;

namespace Sistema_de_controle_cursos_alunos.Controller
{
    public class EnrollmentController
    {
        private readonly AppDbContext context;

        public EnrollmentController(AppDbContext context)
        {
            this.context = context;
        }

        public void AddEnrollment(int idStudent, int idCourse, DateTime date)
        {
            context.Add(new Enrollment { StudentId = idStudent, CourseId = idCourse, EnrollmentDate = date });
            context.SaveChanges();
        }

        public void RemoveEnrollment(int id)
        {
            var enrollment = context.Enrollments.Find(id);
            if (enrollment != null)
            {
                context.Remove(enrollment);
                context.SaveChanges();
            }
        }

        public void UpdateEnrollment(int id, int idStudent, int idCourse, DateTime date)
        {
            var enrollment = context.Enrollments.Find(id);
            if (enrollment != null)
            {
                enrollment.StudentId = idStudent;
                enrollment.CourseId = idCourse;
                enrollment.EnrollmentDate = date;
                context.SaveChanges();
            }
        }

        public Enrollment? GetEnrollmentById(int id)
        {
            return context.Enrollments?.Find(id);
        }

        public Enrollment[] GetAllEnrollments()
        {
            return context.Enrollments?.ToArray() ?? [];
        }
    }
}