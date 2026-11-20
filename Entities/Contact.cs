using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur")]
        public string Description { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Konum zorunludur")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Telefon zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string PhoneNumber { get; set; }
    }
}