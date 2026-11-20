using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Models
{
    public class DashboardViewModel
    {
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }
        public int EducationCount { get; set; }
        public int TotalMessages { get; set; }
        public int UnreadMessages { get; set; }

        public List<Message> LastMessages { get; set; } = new();
        public List<Project> LastProjects { get; set; } = new();
        public List<CategorySummary> CategorySummaries { get; set; } = new();
    }

    public class CategorySummary
    {
        public string CategoryName { get; set; }
        public int ProjectCount { get; set; }
    }
}