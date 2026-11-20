using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
using MYAcademy_PortfolioProject.Helpers;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult UpdateAbout()
        {
            var value = _context.HeroSections.FirstOrDefault();

            // Kayıt yoksa varsayılan bir kayıt oluştur
            if (value == null)
            {
                value = new HeroSectioncs
                {
                    Greeting = "Merhaba, ben",
                    Title = "Adınız Soyadınız",
                    Description = "Kendinizi buradan tanıtın.",
                    MainImageUrl = ""
                };
                _context.HeroSections.Add(value);
                _context.SaveChanges();
            }

            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(HeroSectioncs model, IFormFile? imageFile)
        {
            // Görsel dosya olarak geliyor, alanın kendisi formdan gelmez
            ModelState.Remove("MainImageUrl");

            var value = _context.HeroSections.FirstOrDefault(x => x.HeroSectionId == model.HeroSectionId);
            if (value == null) return NotFound();

            bool hasImage = imageFile != null && imageFile.Length > 0;
            if (hasImage)
            {
                var error = ImageHelper.Validate(imageFile!);
                if (error != null)
                    ModelState.AddModelError("imageFile", error);
            }

            if (!ModelState.IsValid)
            {
                model.MainImageUrl = value.MainImageUrl;  // mevcut görsel formda kaybolmasın
                return View(model);
            }

            if (hasImage)
            {
                ImageHelper.Delete(value.MainImageUrl, _env.WebRootPath);
                value.MainImageUrl = await ImageHelper.SaveAsync(imageFile!, _env.WebRootPath, "about");
            }

            value.Greeting = model.Greeting;
            value.Title = model.Title;
            value.Description = model.Description;
            _context.SaveChanges();

            TempData["Success"] = "Hakkımda bilgileri güncellendi.";
            return RedirectToAction("UpdateAbout");
        }
    }
}