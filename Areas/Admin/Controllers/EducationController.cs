using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult EducationList()
        {
            var values = _context.Educations.OrderByDescending(e => e.EducationId).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View(new Education());
        }

        [HttpPost]
        public IActionResult CreateEducation(Education education)
        {
            if (!ModelState.IsValid)
            {
                return View(education);
            }

            _context.Educations.Add(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim kaydı eklendi.";
            return RedirectToAction("EducationList");
        }

        [HttpGet]
        public IActionResult UpdateEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateEducation(Education education)
        {
            if (!ModelState.IsValid)
            {
                return View(education);
            }

            var value = _context.Educations.Find(education.EducationId);
            if (value == null) return NotFound();

            value.Title = education.Title;
            value.SchoolName = education.SchoolName;
            value.Description = education.Description;
            value.Years = education.Years;
            value.IsActive = education.IsActive;
            _context.SaveChanges();

            TempData["Success"] = "Eğitim kaydı güncellendi.";
            return RedirectToAction("EducationList");
        }

        [HttpPost]
        public IActionResult DeleteEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value == null) return NotFound();

            _context.Educations.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim kaydı silindi.";
            return RedirectToAction("EducationList");
        }
    }
}
