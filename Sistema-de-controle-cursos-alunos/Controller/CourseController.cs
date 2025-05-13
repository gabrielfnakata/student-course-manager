using Sistema_de_controle_cursos_alunos.Context;
using Sistema_de_controle_cursos_alunos.Model;

namespace Sistema_de_controle_cursos_alunos.Controller
{
    public class CourseController
    {
        private readonly AppDbContext context;

        public CourseController(AppDbContext context)
        {
            this.context = context;
        }

        public void AddCourse(string name, string description, string duration)
        {
            context.Add(new Course { Name = name, Description = description, Duration = duration });
            context.SaveChanges();
        }

        public void RemoveCourse(int id)
        {
            var course = context.Courses.Find(id);
            if (course != null)
            {
                context.Remove(course);
                context.SaveChanges();
            }
        }

        public void UpdateCourse(int id, string name, string description, string duration)
        {
            var course = context.Courses.Find(id);
            if (course != null)
            {
                course.Name = name;
                course.Description = description;
                course.Duration = duration;
                context.SaveChanges();
            }
        }

        public Course? GetCourseById(int id)
        {
            return context.Courses?.Find(id);
        }

        public Course[] GetAllCourses()
        {
            return context.Courses?.ToArray() ?? [];
        }
    }
}