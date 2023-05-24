
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orcamentos.Models
{
    public class Orcamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("OrcamentoNomes")]
        public int orcamentoNomeId { get; set; }

        public OrcamentoNome? OrcamentoNome { get; set; }

        [Required]
        [ForeignKey("profiles")]
        public int profileId { get; set; }

        public Profile? Profile { get; set; }

        [Required]
        [ForeignKey("revenueTypes")]
        public int revenueTypeId { get; set; }
        public RevenueType? RevenueType { get; set; }

        [Required]
        [ForeignKey("businessUnits")]
        public int businessUnitId { get; set; }

        public BusinessUnit? BusinessUnit { get; set; }

     

        [Required]
        public string Marca { get; set; }

        [Required]
        public string TipoUni { get; set; }

        [Required]
        public int Partnumb { get; set; }

        [Required]
        public string modelo { get; set; }

        [Required]
        public int SerialNumb { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal UnitCost { get; set; }

        [Required]
        public decimal DescontoTabela { get; set; }

        [Required]
        public decimal PrecoParcial { get; set; }

        [Required]
        public decimal CustoTabela { get; set; }

        [Required]
        public decimal CustoDesc1 { get; set; }

        [Required]
        public decimal CustoDesc2 { get; set; }

        [Required]
        public decimal CustoDesc3 { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public decimal Margin { get; set; }

        [Required]
        [Precision(14, 3)]
        public decimal MG { get; set; }


        [Required]
        public bool Ativo { get; set; }

        [Required]
        public string DelivaryDate { get; set; }

        [Required]
        public string ExternalProvider { get; set; }
    }
}
