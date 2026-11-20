using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult UpdateContact()
        {
            var value = _context.Contacts.FirstOrDefault();

            if (value == null)
            {
                value = new Contact
                {
                    Title = "İletişim",
                    Description = "Benimle iletişime geçin.",
                    Email = "ornek@mail.com",
                    Location = "İstanbul, Türkiye",
                    PhoneNumber = "+90 500 000 00 00"
                };
                _context.Contacts.Add(value);
                _context.SaveChanges();
            }

            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateContact(Contact model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var value = _context.Contacts.FirstOrDefault(x => x.ContactId == model.ContactId);
            if (value == null) return NotFound();

            value.Title = model.Title;
            value.Description = model.Description;
            value.Email = model.Email;
            value.Location = model.Location;
            value.PhoneNumber = model.PhoneNumber;
            _context.SaveChanges();

            TempData["Success"] = "İletişim bilgileri güncellendi.";
            return RedirectToAction("UpdateContact");
        }
    }
}