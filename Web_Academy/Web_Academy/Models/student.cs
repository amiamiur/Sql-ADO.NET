namespace AcademyApp.Models
{
    public class Student
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public int GroupId { get; set; }
        public override string ToString()
        {
            return $"Студент - {FirstName} {LastName}; возраст - {Age} лет; номер группы - {GroupId}";
        }

    }
}
