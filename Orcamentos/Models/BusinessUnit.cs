using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orcamentos.Models
{
    public class BusinessUnit
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("buManagers")]
        public Guid buManagerId { get; set;}

        public BuManager BuManager { get; set;}

        public bool Ativo { get; set; }
    }
}
