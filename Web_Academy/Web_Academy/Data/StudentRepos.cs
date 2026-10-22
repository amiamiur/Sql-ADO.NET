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
            using var connection = new SqlConnection(conn_str);
            return connection.Query<Student>(
                "SELECT StudentId, FirstName, LastName, Age, GroupId FROM Students ORDER BY LastName ASC"
            ).ToList();
        }

        public Student? FindStudentById(int id)
        {
            using var connection = new SqlConnection(conn_str);
            return connection.QueryFirstOrDefault<Student>(
                "SELECT StudentId, FirstName, LastName, Age, GroupId FROM Students WHERE StudentId = @Id",
                new { Id = id });
        }

        public int AddStudent(Student student)
        {
            using var connection = new SqlConnection(conn_str);

            const string sql = @"
                INSERT INTO Students (FirstName, LastName, Age, GroupId)
                VALUES (@FirstName, @LastName, @Age, @GroupId);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return connection.ExecuteScalar<int>(sql, new
            {
                student.FirstName,student.LastName,student.Age,student.GroupId
            });
        }
    }
}