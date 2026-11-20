using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;

        public DefaultController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.OwnerName = _context.HeroSections.Select(h => h.Title).FirstOrDefault() ?? "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(
            [Bind("SenderName,SenderEmail,Subject,MessageDetail")] Message message)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value!.Errors.First().ErrorMessage);
                return BadRequest(new { errors });
            }

            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();

            return Ok(new { message = "Mesajınız başarıyla gönderildi. En kısa sürede dönüş yapacağım." });
        }
    }
}