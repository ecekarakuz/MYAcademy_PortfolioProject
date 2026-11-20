using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext _context;

        public ExperienceController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            var values = _context.Experiences.OrderByDescending(e => e.ExperienceId).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View(new Experience());
        }

        [HttpPost]
        public IActionResult CreateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return View(experience);
            }

            _context.Experiences.Add(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim başarıyla eklendi.";
            return RedirectToAction("ExperienceList");
        }

        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return View(experience);
            }

            var value = _context.Experiences.Find(experience.ExperienceId);
            if (value == null) return NotFound();

            value.Title = experience.Title;
            value.CompanyName = experience.CompanyName;
            value.Description = experience.Description;
            value.CategoryName = experience.CategoryName;
            value.DateRange = experience.DateRange;
            value.IsActive = experience.IsActive;
            _context.SaveChanges();

            TempData["Success"] = "Deneyim başarıyla güncellendi.";
            return RedirectToAction("ExperienceList");
        }

        [HttpPost]
        public IActionResult DeleteExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value == null) return NotFound();

            _context.Experiences.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim silindi.";
            return RedirectToAction("ExperienceList");
        }
    }
}
