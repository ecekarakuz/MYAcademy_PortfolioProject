using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Pasif kayıtlar sitede görünmez, son eklenen üstte
            var values = _context.Experiences
                .Where(e => e.IsActive)
                .OrderByDescending(e => e.ExperienceId)
                .ToList();
            return View(values);
        }
    }
}