using System.Collections.Generic;
using System.Linq;
using AcademyApp.Models;
using System.Data.SqlClient;
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
        public List<student> FindAllStudents()
        {
            var students = new List<student>();
            using(var connection = new SqlConnection(conn_str))
            {
                return connection.Query<student>("SELECT FirstName,LastName,Age,GroupId FROM Students ORDER BY LastName ASC").ToList();
            }
        }

        public student FindStudentById(string id)
        {
            var student1 = new student();
            using (var connection = new SqlConnection(conn_str))
            {
                return connection.QueryFirstOrDefault<student>(
                    "SELECT FirstName,LastName,Age,GroupId FROM Students WHERE StudentId = @Id;", 
                    new {@Id = id}
                    );
            }
        }

        public void AddStudent(string first_name, string last_name, string age, string group_id)
        {
            var student = new student
            {
                FirstName = first_name,
                LastName = last_name,
                Age = int.Parse(age),
                GroupId = int.Parse(group_id)
            };

            using(var connection = new SqlConnection(conn_str))
            {
                string sql = "INSERT INTO Students(FirstName,LastName,Age,GroupId)" +
                    "VALUES(@FirstName,@LastName,@Age,@GroupId)";
                connection.Execute(sql, new {@FirstName = first_name, @LastName = last_name, @Age = age, @GroupId = group_id});
            }
        }
    }
}