using EduKateMVC.Models.Common;

namespace EduKateMVC.Models
{
    public class Course:BaseEntity
    {

        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int Rating { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
