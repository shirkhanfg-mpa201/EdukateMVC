using System.ComponentModel.DataAnnotations;

namespace EduKateMVC.ViewModels.CourseViewModels
{
    public class CourseUpdateVM
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile? ImageUrl { get; set; }
        [Required, Range(0, 5)]

        public int Rating { get; set; }

        [Required]

        public int TeacherId { get; set; }
    }
}
