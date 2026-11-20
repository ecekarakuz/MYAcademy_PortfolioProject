using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Yetenek adı zorunludur")]
        [StringLength(100, ErrorMessage = "Yetenek adı en fazla 100 karakter olabilir")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string IconUrl { get; set; }

        [Range(0, 100, ErrorMessage = "Seviye 0 ile 100 arasında olmalıdır")]
        public int Level { get; set; }

        public List<SkillItem>? SkillItems { get; set; }
    }
}