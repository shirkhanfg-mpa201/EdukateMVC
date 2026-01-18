using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace EduKateMVC.ViewModels.CourseViewModels
{
    public class CourseCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile ImageUrl { get; set; }
        [Required,Range(0,5)]

        public int Rating { get; set; }
        [Required]

        public int TeacherId { get; set; }
    }
}
