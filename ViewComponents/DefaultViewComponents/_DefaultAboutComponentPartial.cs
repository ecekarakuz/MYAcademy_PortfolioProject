using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultAboutComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var hero = _context.HeroSections.FirstOrDefault()
                       ?? new HeroSectioncs { Greeting = "", Title = "", Description = "", MainImageUrl = "" };

            ViewBag.Location = _context.Contacts.Select(c => c.Location).FirstOrDefault() ?? "";
            ViewBag.ProjectCount = _context.Projects.Count(p => p.IsActive);
            ViewBag.SkillCount = _context.Skills.Count();
            ViewBag.ExperienceCount = _context.Experiences.Count(e => e.IsActive);

            return View(hero);
        }
    }
}