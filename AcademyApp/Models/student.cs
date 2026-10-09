using System;
using System.Runtime.CompilerServices;


namespace AcademyApp.Models
{
    public class student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int GroupId { get; set; }
        public override string ToString()
        {
            return $"Студент - {FirstName} {LastName}; возраст - {Age} лет; номер группы - {GroupId}";
        }

    }
}

