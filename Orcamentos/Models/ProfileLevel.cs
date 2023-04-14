using System.ComponentModel.DataAnnotations;

namespace Orcamentos.Models
{
    public class ProfileLevel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Ativo { get; set; }


    }
}
