using Microsoft.AspNetCore.Identity;
using EduKateMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduKateMVC.Contexts
{
    public class AppUser : IdentityUser
    {
       public string FullName { get; set; }
    }
}
