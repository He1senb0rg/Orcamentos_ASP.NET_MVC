
namespace Orcamentos.Models
{
    public class Orcamento
    {
        public Guid Id { get; set; }

        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }


        public Guid RevTypeId { get; set; }
        public RevenueType RevenueType { get; set; }


        public Guid BuManagerId { get; set; }
        public BuManager BuManager { get; set; }


        public string Marca { get; set; }

        public string TipoUni { get; set; }

        public int Partnumb { get; set; }

        public string modelo { get; set;}

        public int SerialNumb { get; set; }

        public string ProductName  { get; set; }

        public string Estado { get; set; }

    }
}
