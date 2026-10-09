using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Models;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Dapper;

namespace AcademyApp.data
{
    internal class GroupRepos
    {
        private readonly string conn_str;
        public GroupRepos(string connection_string)
        {

            conn_str = connection_string;
        }

        public List<group> FindAllGroups()
        {
            using (var connection = new SqlConnection(conn_str))
            {
                return connection.Query<group>("SELECT GroupId, GroupName From Groups ORDER BY GroupId ASC").ToList();
            }
        }

        public List<group> FindAllGroups2()
        {
            var groups = new List<group>();
            SqlConnection connection = new SqlConnection(conn_str);
            connection.Open();

            string sql = "SELECT GroupId, GroupName From Groups ORDER BY GroupId ASC";
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                groups.Add(
                    new group
                    {
                        GroupId = reader.GetInt32(0),
                        GroupName = reader.GetString(1)
                    }
                );

            }
            connection.Close();
            return groups;
        }
    }
}