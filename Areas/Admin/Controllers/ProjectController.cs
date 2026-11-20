using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
using MYAcademy_PortfolioProject.Helpers;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public ProjectController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Dropdown'ı dolduran ortak metot (hata durumunda da çağrılmalı)
        private void LoadCategories(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(
                _context.Categories.ToList(), "CategoryId", "CategoryName", selectedId);
        }

        public IActionResult ProjectList()
        {
            var values = _context.Projects
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            LoadCategories();
            return View(new Project());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project, IFormFile? imageFile)
        {
            if (project.CategoryId == 0)
                ModelState.AddModelError("CategoryId", "Lütfen bir kategori seçiniz");

            bool hasImage = imageFile != null && imageFile.Length > 0;
            if (hasImage)
            {
                var error = ImageHelper.Validate(imageFile!);
                if (error != null)
                    ModelState.AddModelError("imageFile", error);
            }

            if (!ModelState.IsValid)
            {
                LoadCategories(project.CategoryId);   // unutma: hata durumunda tekrar doldur
                return View(project);
            }

            if (hasImage)
                project.ImagePath = await ImageHelper.SaveAsync(imageFile!, _env.WebRootPath);

            project.CreatedDate = DateTime.Now;
            _context.Projects.Add(project);
            _context.SaveChanges();

            TempData["Success"] = "Proje başarıyla eklendi.";
            return RedirectToAction("ProjectList");
        }

        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null) return NotFound();

            LoadCategories(value.CategoryId);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(Project project, IFormFile? imageFile)
        {
            var value = _context.Projects.Find(project.ProjectId);
            if (value == null) return NotFound();

            if (project.CategoryId == 0)
                ModelState.AddModelError("CategoryId", "Lütfen bir kategori seçiniz");

            bool hasImage = imageFile != null && imageFile.Length > 0;
            if (hasImage)
            {
                var error = ImageHelper.Validate(imageFile!);
                if (error != null)
                    ModelState.AddModelError("imageFile", error);
            }

            if (!ModelState.IsValid)
            {
                LoadCategories(project.CategoryId);
                project.ImagePath = value.ImagePath;  // formda görünen mevcut görsel kaybolmasın
                return View(project);
            }

            // Yeni görsel seçildiyse eskisini sil, yenisini kaydet. Seçilmediyse eskisi kalır.
            if (hasImage)
            {
                ImageHelper.Delete(value.ImagePath, _env.WebRootPath);
                value.ImagePath = await ImageHelper.SaveAsync(imageFile!, _env.WebRootPath);
            }

            value.ProjectTitle = project.ProjectTitle;
            value.Description = project.Description;
            value.Technologies = project.Technologies;
            value.CategoryId = project.CategoryId;
            value.IsActive = project.IsActive;
            // CreatedDate bilerek değiştirilmiyor

            _context.SaveChanges();

            TempData["Success"] = "Proje başarıyla güncellendi.";
            return RedirectToAction("ProjectList");
        }

        [HttpPost]
        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null) return NotFound();

            ImageHelper.Delete(value.ImagePath, _env.WebRootPath);
            _context.Projects.Remove(value);
            _context.SaveChanges();

            TempData["Success"] = "Proje silindi.";
            return RedirectToAction("ProjectList");
        }

        // Listeden tek tıkla Aktif/Pasif değiştirme
        [HttpPost]
        public IActionResult ToggleActive(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null) return NotFound();

            value.IsActive = !value.IsActive;
            _context.SaveChanges();

            TempData["Success"] = value.IsActive ? "Proje aktif yapıldı." : "Proje pasif yapıldı.";
            return RedirectToAction("ProjectList");
        }
    }
}
