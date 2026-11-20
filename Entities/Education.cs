using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Education
    {
        public int EducationId { get; set; }

        [Required(ErrorMessage = "Bölüm / derece zorunludur")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Okul adı zorunludur")]
        [StringLength(150, ErrorMessage = "En fazla 150 karakter olabilir")]
        public string SchoolName { get; set; }

        [StringLength(500, ErrorMessage = "En fazla 500 karakter olabilir")]
        public string? Description { get; set; }

        [StringLength(20, ErrorMessage = "En fazla 20 karakter olabilir")]
        public string? Years { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
