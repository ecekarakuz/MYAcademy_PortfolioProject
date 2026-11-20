using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Models
{
    public class ProjectSectionViewModel
    {
        public List<Project> Projects { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public int? SelectedCategoryId { get; set; }
    }
}