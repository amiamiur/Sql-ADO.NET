namespace AcademyApp.Models
{
    public class group
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }
        public override string ToString()
        {
            return $"номер {GroupId} у группы {GroupName}";
        }

    }
}