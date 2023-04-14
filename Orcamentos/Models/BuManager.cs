using System.ComponentModel.DataAnnotations;

namespace Orcamentos.Models
{
    public class BuManager
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public bool Ativo { get; set; }
    }
}
