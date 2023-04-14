using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orcamentos.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        [ForeignKey("profileLevels")]
        public int profileLevelId { get; set; }

        public ProfileLevel? ProfileLevel { get; set; }
    }
}
