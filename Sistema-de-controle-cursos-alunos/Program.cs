using Sistema_de_controle_cursos_alunos.Controller;
using Sistema_de_controle_cursos_alunos.Context;
using Sistema_de_controle_cursos_alunos.Model;

namespace Sistema_de_controle_cursos_alunos
{
    public class Program
    {
        private static AppDbContext Context { get; } = new();
        private static EnrollmentController EnrollmentC => new(Context);
        private static StudentController StudentC => new(Context);
        private static CourseController CourseC => new(Context);
        private static bool isRunning = true;

        public static void Main(string[] args)
        {
            while (isRunning)
            {
                isRunning = Menu();
                Console.WriteLine();
            }
        }

        private static bool Menu()
        {
            Console.Write(
@"1 - Register student
2 - Register course (name, description, duration)
3 - Enroll a student in a course
4 - List of students by course
5 - List of courses by student
6 - Filter students by name
7 - Exit

$ ");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    RegisterStudent();
                    break;
                case "2":
                    RegisterCourse();
                    break;
                case "3":
                    EnrollStudentInCourse();
                    break;
                case "4":
                    ListStudentsByCourse();
                    break;
                case "5":
                    ListCoursesByStudent();
                    break;
                case "6":
                    FilterStudentsByName();
                    break;
                case "7":
                    Console.WriteLine("Exiting...");
                    return false;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

            return true;
        }

        private static void FilterStudentsByName()
        {
            Console.Write("Enter student's name:\n$ ");
            string studentName = Console.ReadLine() ?? throw new Exception();

            var students = StudentC.GetAllStudents().Where(student => student.Name.ToLower().Equals(studentName.ToLower()));
            Console.WriteLine($"\nList of students named {studentName}:");
            foreach (var student in students)
            {
                Console.WriteLine($"Id - {student.Id}/Email - {student.Email}/Birth Date - {student.BirthDate}");
            }
        }

        private static void ListCoursesByStudent()
        {
            Console.Write("Enter student's id\n$ ");
            var studentId = int.Parse(Console.ReadLine() ?? throw new Exception());

            var allCourses = CourseC.GetAllCourses();
            var coursesByStudent = EnrollmentC.GetAllEnrollments()
                .Where(enr => enr.StudentId == studentId)
                .Select(enr => allCourses.First(course => course.Id == enr.CourseId).Name);

            Console.WriteLine("\nList of courses:");
            foreach (var course in coursesByStudent)
            {
                Console.WriteLine($"- {course}");
            }
        }

        private static void ListStudentsByCourse()
        {
            Console.Write("Enter course's id\n$ ");
            var courseId = int.Parse(Console.ReadLine() ?? throw new Exception());

            var allStudents = StudentC.GetAllStudents();
            var studentsByCourse = EnrollmentC.GetAllEnrollments()
                .Where(enr => enr.CourseId == courseId)
                .Select(enr => allStudents.First(student => student.Id == enr.StudentId).Name);

            Console.WriteLine("\nList of students:");
            foreach (var student in studentsByCourse)
            {
                Console.WriteLine($"- {student}");
            }
        }

        private static void EnrollStudentInCourse()
        {
            Console.WriteLine("Enter student's id\n$ ");
            int studentId = int.Parse(Console.ReadLine() ?? throw new Exception());
            Console.WriteLine("Enter course's id\n$ ");
            int courseId = int.Parse(Console.ReadLine() ?? throw new Exception());

            EnrollmentC.AddEnrollment(studentId, courseId, DateTime.Now);
        }

        private static void RegisterCourse()
        {
            Console.Write("Enter course's name\n$ ");
            string courseName = Console.ReadLine() ?? throw new Exception();
            Console.Write("Enter course's description\n$ ");
            string courseDescription = Console.ReadLine() ?? throw new Exception();
            Console.Write("Enter course's duration\n$ ");
            string courseDuration = Console.ReadLine() ?? throw new Exception();

            CourseC.AddCourse(courseName, courseDescription, courseDuration);
        }

        private static void RegisterStudent()
        {
            Console.Write("Enter student's name\n$ ");
            string studentName = Console.ReadLine() ?? throw new Exception();
            Console.Write("Enter student's email\n$ ");
            string studentEmail = Console.ReadLine() ?? throw new Exception();
            Console.Write("Enter student's birth date\n$ ");
            string studentDate = Console.ReadLine() ?? throw new Exception();

            StudentC.AddStudent(studentName, studentEmail, studentDate);
        }
    }
}