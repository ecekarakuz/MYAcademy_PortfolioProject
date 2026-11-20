using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultNavbarComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultNavbarComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.BrandName = _context.HeroSections.Select(h => h.Title).FirstOrDefault() ?? "Portfolyo";
            return View();
        }
    }
}