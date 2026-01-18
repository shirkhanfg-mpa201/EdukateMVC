using EduKateMVC.Contexts;
using EduKateMVC.Models;
using EduKateMVC.ViewModels.TeacherViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduKateMVC.Areas.Admin.Controllers;
[Area("Admin")]
//[Authorize(Roles = "Admin")]
public class TeacherController(AppDbContexts _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var teacher = await _context.Teachers.Select(x => new TeacherGetVM()
        {
            Id = x.Id,
            FullName = x.FullName
        }).ToListAsync();
        return View(teacher);
    }

    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]

    public async Task<IActionResult> Create(TeacherCreateVM vm)
    {
        if (!ModelState.IsValid) return View(vm);

        Teacher teacher = new Teacher()
        {
            FullName = vm.FullName
        };

        await _context.Teachers.AddAsync(teacher);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        var deletedTeacher = await _context.Teachers.FindAsync(id);
        if (deletedTeacher is null) return NotFound();

        _context.Teachers.Remove(deletedTeacher);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]

    public async Task<IActionResult> Update(int id) {
        var teacher = await _context.Teachers.FindAsync(id);

        if (teacher is null) return NotFound();

        TeacherUpdateVM vm = new ()
        {
            Id=teacher.Id,
            FullName=teacher.FullName

        };

        return View(vm);
    
    }

    [HttpPost]

    public async Task<IActionResult> Update(TeacherUpdateVM vm) {
        if (!ModelState.IsValid) return View(vm);

        var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == vm.Id);

        if (teacher is null) return NotFound();

        teacher.FullName = vm.FullName;

        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    
    }
}
