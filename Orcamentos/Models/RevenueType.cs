using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orcamentos.Models
{
    public class RevenueType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public bool Ativo { get; set; }

    }
}
