
using System.ComponentModel.DataAnnotations.Schema;

namespace Orcamentos.Models
{
    public class Orcamento
    {
        public Guid Id { get; set; }

        [ForeignKey("profiles")]
        public Guid profileId { get; set; }
        public Profile? Profile { get; set; }

        [ForeignKey("revenueTypes")]
        public Guid revenueTypeId { get; set; }
        public RevenueType? RevenueType { get; set; }

        [ForeignKey("buManagers")]
        public Guid buManagerId { get; set; }
        public BuManager? BuManager { get; set; }


        public string Marca { get; set; }

        public string TipoUni { get; set; }

        public int Partnumb { get; set; }

        public string modelo { get; set;}

        public int SerialNumb { get; set; }

        public string ProductName  { get; set; }

        public bool Ativo { get; set; }

    }
}
