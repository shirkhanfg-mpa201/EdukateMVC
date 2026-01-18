using EduKateMVC.Contexts;
using EduKateMVC.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduKateMVC.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            AppUser user = new AppUser()
            {

                UserName = vm.UserName,
                FullName = vm.FullName,
                Email = vm.Email,
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", "Email or password is wrong");

                }
                return View(vm);
            }
            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Login", "Account");
        }



        public IActionResult Login()
        {
            return View();
        }

        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
           
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user is null) { ModelState.AddModelError("", "Mail or password is wrong"); return View(vm); }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false,false);

            if (!result.Succeeded) { ModelState.AddModelError("", "Mail or pasword is wrong"); return View(vm); }

            return RedirectToAction("Index","Home");
        }


        public async Task<IActionResult> CreateRole() {
            await _roleManager.CreateAsync(new IdentityRole() {Name="Admin" });
            await _roleManager.CreateAsync(new IdentityRole() {Name="Member" });
            await _roleManager.CreateAsync(new IdentityRole() {Name="Moderator" });

            return Ok("Roles are created");
        }
    }
}
