using EduKateMVC.Models.Common;

namespace EduKateMVC.Models
{
    public class Teacher :BaseEntity
    {
        public string FullName { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
