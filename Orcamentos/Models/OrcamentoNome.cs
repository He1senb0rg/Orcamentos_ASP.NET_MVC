using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orcamentos.Models
{
    public class OrcamentoNome
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        
        [Required]
        public string ProposalNumber { get; set; }

        [Required]
        public bool Ativo { get; set; }


    }
}
