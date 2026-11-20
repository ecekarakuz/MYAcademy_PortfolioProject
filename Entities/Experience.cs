using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        [Required(ErrorMessage = "Deneyim başlığı zorunludur")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Şirket / kurum adı zorunludur")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur")]
        [StringLength(1000, ErrorMessage = "En fazla 1000 karakter olabilir")]
        public string Description { get; set; }

        // Çalışma türü olarak kullanılıyor (Tam Zamanlı, Hibrit, Uzaktan...)
        [Required(ErrorMessage = "Çalışma türü zorunludur")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir")]
        public string CategoryName { get; set; }

        [StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir")]
        public string? DateRange { get; set; }

        public bool IsActive { get; set; } = true;
    }
}