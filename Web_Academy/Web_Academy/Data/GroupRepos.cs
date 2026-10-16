using AcademyApp.Models;
using Microsoft.Data.SqlClient;
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
    }
}
