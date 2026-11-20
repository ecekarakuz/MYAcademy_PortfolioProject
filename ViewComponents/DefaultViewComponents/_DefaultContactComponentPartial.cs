using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultContactComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultContactComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.Contacts.FirstOrDefault()
                        ?? new Contact
                        {
                            Title = "İletişime Geçelim",
                            Description = "",
                            Email = "",
                            Location = "",
                            PhoneNumber = ""
                        };
            return View(value);
        }
    }
}