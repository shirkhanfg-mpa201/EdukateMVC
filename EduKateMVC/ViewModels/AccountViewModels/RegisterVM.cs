using System.ComponentModel.DataAnnotations;

namespace EduKateMVC.ViewModels.AccountViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(256)]
        public string FullName { get; set; }
        [Required, MaxLength(256)]

        public string UserName { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }
    }
}
