using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }

        [Required(ErrorMessage = "Platform adı zorunludur")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir")]
        public string Platform { get; set; }

        [Required(ErrorMessage = "Bağlantı zorunludur")]
        [Url(ErrorMessage = "Geçerli bir bağlantı giriniz (https://...)")]
        [StringLength(300, ErrorMessage = "En fazla 300 karakter olabilir")]
        public string Url { get; set; }

        [Required(ErrorMessage = "İkon zorunludur")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir")]
        public string Icon { get; set; }
    }
}