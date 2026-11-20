using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillList()
        {
            var values = _context.Skills
                .Include(s => s.SkillItems)
                .OrderByDescending(s => s.Level)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSkill()
        {
            return View(new Skill { Level = 50 });
        }

        [HttpPost]
        public IActionResult CreateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return View(skill);
            }

            _context.Skills.Add(skill);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek başarıyla eklendi.";
            return RedirectToAction("SkillList");
        }

        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return View(skill);
            }

            var value = _context.Skills.Find(skill.SkillId);
            if (value == null) return NotFound();

            value.Title = skill.Title;
            value.Description = skill.Description;
            value.IconUrl = skill.IconUrl;
            value.Level = skill.Level;
            _context.SaveChanges();

            TempData["Success"] = "Yetenek başarıyla güncellendi.";
            return RedirectToAction("SkillList");
        }

        [HttpPost]
        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value == null) return NotFound();

            bool hasItems = _context.SkillItems.Any(i => i.SkillId == id);
            if (hasItems)
            {
                TempData["Error"] = "Bu yeteneğe bağlı alt kalemler (SkillItem) var, önce onları silin.";
                return RedirectToAction("SkillList");
            }

            _context.Skills.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek silindi.";
            return RedirectToAction("SkillList");
        }
    }
}