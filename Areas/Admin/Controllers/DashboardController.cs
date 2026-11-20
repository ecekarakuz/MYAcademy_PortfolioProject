using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Models;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new DashboardViewModel
            {
                TotalProjects = _context.Projects.Count(),
                ActiveProjects = _context.Projects.Count(p => p.IsActive),
                SkillCount = _context.Skills.Count(),
                ExperienceCount = _context.Experiences.Count(),
                EducationCount = _context.Educations.Count(),
                TotalMessages = _context.Messages.Count(),
                UnreadMessages = _context.Messages.Count(m => !m.IsRead),

                LastMessages = _context.Messages
                    .OrderByDescending(m => m.SendDate)
                    .Take(5)
                    .ToList(),

                LastProjects = _context.Projects
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.CreatedDate)
                    .Take(3)
                    .ToList(),

                CategorySummaries = _context.Projects
                    .GroupBy(p => p.Category.CategoryName)
                    .Select(g => new CategorySummary
                    {
                        CategoryName = g.Key,
                        ProjectCount = g.Count()
                    })
                    .OrderByDescending(x => x.ProjectCount)
                    .ToList()
            };

            return View(vm);
        }
    }
}