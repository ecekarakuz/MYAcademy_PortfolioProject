using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class HeroSectioncs
    {
        [Key]
        public int HeroSectionId { get; set; }

        [Required(ErrorMessage = "Selamlama zorunludur")]
        public string Greeting { get; set; }

        [Required(ErrorMessage = "İsim / unvan zorunludur")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tanıtım yazısı zorunludur")]
        public string Description { get; set; }

        public string MainImageUrl { get; set; }
    }
}