using EduKateMVC.Contexts;
using EduKateMVC.FileHelpers;
using EduKateMVC.Models;
using EduKateMVC.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Threading.Tasks;

namespace EduKateMVC.Areas.Admin.Controllers;
[Area("Admin")]
//[Authorize(Roles = "Admin")]

public class CourseController : Controller
{
    private readonly AppDbContexts _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string folderPath;

    public CourseController(AppDbContexts context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        folderPath=Path.Combine(_environment.WebRootPath,"img");
    }

    public async Task<IActionResult> Index()
    {
        var result = await _context.Courses.Select(x => new CourseGetVM()
        {
            Id=x.Id,
            Title=x.Title,
            ImageUrl=x.ImageUrl,
            Rating=x.Rating,
            TeacherName=x.Teacher.FullName
        }).ToListAsync();
        return View(result);
    }

    [HttpGet]

    public async Task<IActionResult> Create()
    {
        await SendTeacherWithViewBag();
        return View();
    }


    [HttpPost]

    public async Task<IActionResult> Create(CourseCreateVM vm) {
        await SendTeacherWithViewBag();
         
        if (!ModelState.IsValid) return View(vm);

        var existTeacher = await _context.Teachers.FirstOrDefaultAsync(x=> x.Id==vm.TeacherId);

        if (existTeacher is null) return NotFound();

        if (vm.ImageUrl.CheckSize(2)) { ModelState.AddModelError("ImageUrl", "Size of image must be lower than 2"); return View(vm); }
        if (!vm.ImageUrl.CheckType("image")) { ModelState.AddModelError("ImageUrl", "Type must be an image"); return View(vm); }

        string uniqueFileName = await vm.ImageUrl.FileUploadAsync(folderPath);

        Course course = new()
        {
            Title=vm.Title,
            ImageUrl=uniqueFileName,
            Rating=vm.Rating,
            TeacherId=vm.TeacherId
        };

        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id) {
        var path = await _context.Courses.FindAsync(id);

        if (path is null) return NotFound();

        _context.Courses.Remove(path);
        await _context.SaveChangesAsync();

        string deletedPath = Path.Combine(folderPath,path.ImageUrl);

        ExtensionMethods.Delete(deletedPath);

        return RedirectToAction(nameof(Index));

    }


    [HttpGet]

    public async Task<IActionResult> Update(int id) {
        var course = await _context.Courses.FindAsync(id);

        if (course is null) return NotFound();

        CourseUpdateVM vm = new()
        {
            Id=course.Id,
            Title=course.Title,
            Rating=course.Rating,
            TeacherId=course.TeacherId
        };
        await SendTeacherWithViewBag();
        return View(vm);
    
    }

    [HttpPost]

    public async Task<IActionResult> Update(CourseUpdateVM vm) {
        await SendTeacherWithViewBag();
        if (!ModelState.IsValid) return View(vm);

        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (course is null) return NotFound();

        var existTeacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == vm.TeacherId);
        if (existTeacher is null) return NotFound();

        if (vm.ImageUrl?.CheckSize(2) ?? false) { ModelState.AddModelError("ImageUrl", "Max size is 2mb"); return View(vm); }

        if (!vm.ImageUrl?.CheckType("image") ?? false) { ModelState.AddModelError("ImageUrl", "Must be an image type"); return View(vm); }

        course.Title = vm.Title;
        course.Rating = vm.Rating;
        course.TeacherId = vm.TeacherId;

        if (vm.ImageUrl is { }) {
            var uniqueName = await vm.ImageUrl.FileUploadAsync(folderPath);
            var deletedPath = Path.Combine(folderPath,course.ImageUrl);
            ExtensionMethods.Delete(deletedPath);
            course.ImageUrl = uniqueName;
        };

        _context.Courses.Update(course);
     await   _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task SendTeacherWithViewBag()
    {
        var teachers = await _context.Teachers.ToListAsync();
        ViewBag.Teachers = teachers;
    }
}
