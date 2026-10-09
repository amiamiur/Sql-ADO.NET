using System;
using System.Data.SqlClient;
using AcademyApp.data;



namespace AcademyApp
{
    internal class Program
    {
        static string conn_str = "Data Source=COMP7A2\\SQLEXPRESS;" +
                              "Initial Catalog=Academy;" +
                              "Integrated Security=True;" +
                              "TrustServerCertificate=True;";

        static Studentrepos student_repo = new Studentrepos(conn_str);
        static GroupRepos group_repo = new GroupRepos(conn_str);
        static void Main(string[] args)
        {

            bool is_running = true;



            while (is_running)
            {
                Console.WriteLine("1. Просмотр всех студентов");
                Console.WriteLine("2. Найти студента по номеру");
                Console.WriteLine("3. Просмотр всех групп учащихся");
                Console.WriteLine("4. Добавить cтудента");
                Console.Write("Введите номер действия: ");
                string choise = Console.ReadLine();

                switch (choise)
                {
                    case "1":
                        ShowAllStudents();
                        break;
                    case "2":
                        ShowStudentById();
                        break;
                    case "3":
                        ShowAllGroups();
                        break;
                    case "4":
                        CreateStudent();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Введите 1 или 0.");
                        break;
                }

                if (is_running)
                {
                    Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void ShowAllStudents()
        {
            var students = student_repo.FindAllStudents();
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        static void ShowStudentById()
        {
            Console.WriteLine("Введите номер студента");
            string choice = Console.ReadLine();
            var student = student_repo.FindStudentById(choice);
            Console.WriteLine(student);
        }

        static void CreateStudent()
        {
            Console.WriteLine("Введите имя:");
            string first_name = Console.ReadLine();
            Console.WriteLine("Введите фамилию:");
            string last_name = Console.ReadLine();
            Console.WriteLine("Введите возраст:");
            string age = Console.ReadLine();
            Console.WriteLine("Введите группу учащегося:");
            string group_id = Console.ReadLine();
            student_repo.AddStudent(first_name, last_name, age, group_id);
        }

        static void ShowAllGroups()
        {
            var groups = group_repo.FindAllGroups();
            foreach (var group in groups)
            {
                Console.WriteLine(group);
            }
        }
    }
}