using AcademyApp.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace AcademyApp.data
{
    internal class Studentrepos
    {
        private readonly string conn_str;
        public Studentrepos(string connection_string)
        {
            conn_str = connection_string;
        }
        public List<Student> FindAllStudents()
        {
            var Students = new List<Student>();
            using (var connection = new SqlConnection(conn_str))
            {
                return connection.Query<Student>("SELECT FirstName,LastName,Age,GroupId FROM Students ORDER BY LastName ASC").ToList();
            }
        }

        public Student FindStudentById(string id)
        {
            var student1 = new Student();
            using (var connection = new SqlConnection(conn_str))
            {
                return connection.QueryFirstOrDefault<Student>(
                    "SELECT FirstName,LastName,Age,GroupId FROM Students WHERE StudentId = @Id;",
                    new { @Id = id }
                    );
            }
        }

        public void AddStudent(Student student)
        {
            using (var connection = new SqlConnection(conn_str))
            {
                string sql = @"INSERT INTO Students(FirstName,LastName,Age,GroupId)" +
                    "VALUES(@FirstName,@LastName,@Age,@GroupId)";
                connection.Execute(sql, student);
            }
        }
    }
}