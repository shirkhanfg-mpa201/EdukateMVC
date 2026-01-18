using System.ComponentModel.DataAnnotations;

namespace EduKateMVC.ViewModels.TeacherViewModels
{
    public class TeacherUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string FullName { get; set; }
    }
}
