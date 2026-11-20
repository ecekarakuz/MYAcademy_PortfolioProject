using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Models;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            int? selectedId = null;
            if (int.TryParse(HttpContext.Request.Query["categoryId"].ToString(), out var id))
                selectedId = id;

            // Pasif projeler sitede görünmez
            var query = _context.Projects
                .Include(p => p.Category)
                .Where(p => p.IsActive);

            if (selectedId.HasValue)
                query = query.Where(p => p.CategoryId == selectedId.Value);

            var vm = new ProjectSectionViewModel
            {
                SelectedCategoryId = selectedId,
                Projects = query.OrderByDescending(p => p.CreatedDate).ToList(),
                // Sadece aktif projesi olan kategoriler filtre butonu olarak görünür
                Categories = _context.Categories
                    .Where(c => c.Projects.Any(p => p.IsActive))
                    .OrderBy(c => c.CategoryName)
                    .ToList()
            };

            return View(vm);
        }
    }
}