using System.ComponentModel.DataAnnotations.Schema;

namespace AcademyApp.Models
{
    [Table("Groups")] 
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}