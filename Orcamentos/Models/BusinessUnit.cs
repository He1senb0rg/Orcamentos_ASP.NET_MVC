using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orcamentos.Models
{
    public class BusinessUnit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("buManagers")]
        public int buManagerId { get; set; }

        public BuManager? BuManager { get; set; }

        [Required]
        public bool Ativo { get; set; }
    }
}
