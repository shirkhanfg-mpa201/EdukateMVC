using System.ComponentModel.DataAnnotations;

namespace EduKateMVC.ViewModels.TeacherViewModels
{
    public class TeacherCreateVM
    {
        [Required, MaxLength(256), MinLength(3)]
        public string FullName { get; set; }
    }
}
