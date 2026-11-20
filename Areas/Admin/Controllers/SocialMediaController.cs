using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly PortfolioContext _context;

        public SocialMediaController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SocialMediaList()
        {
            var values = _context.SocialMedias.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View(new SocialMedia());
        }

        [HttpPost]
        public IActionResult CreateSocialMedia(SocialMedia socialMedia)
        {
            if (!ModelState.IsValid)
            {
                return View(socialMedia);
            }

            _context.SocialMedias.Add(socialMedia);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya bağlantısı eklendi.";
            return RedirectToAction("SocialMediaList");
        }

        [HttpGet]
        public IActionResult UpdateSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateSocialMedia(SocialMedia socialMedia)
        {
            if (!ModelState.IsValid)
            {
                return View(socialMedia);
            }

            var value = _context.SocialMedias.Find(socialMedia.SocialMediaId);
            if (value == null) return NotFound();

            value.Platform = socialMedia.Platform;
            value.Url = socialMedia.Url;
            value.Icon = socialMedia.Icon;
            _context.SaveChanges();

            TempData["Success"] = "Sosyal medya bağlantısı güncellendi.";
            return RedirectToAction("SocialMediaList");
        }

        [HttpPost]
        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value == null) return NotFound();

            _context.SocialMedias.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya bağlantısı silindi.";
            return RedirectToAction("SocialMediaList");
        }
    }
}
