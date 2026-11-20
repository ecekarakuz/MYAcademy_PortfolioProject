using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Proje adı zorunludur")]
        [StringLength(100, ErrorMessage = "Proje adı en fazla 100 karakter olabilir")]
        public string ProjectTitle { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur")]
        [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir")]
        public string Description { get; set; }

        public string? ImagePath { get; set; }

        [StringLength(200, ErrorMessage = "Teknolojiler en fazla 200 karakter olabilir")]
        public string? Technologies { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Lütfen bir kategori seçiniz")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
