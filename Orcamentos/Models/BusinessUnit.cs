namespace Orcamentos.Models
{
    public class BusinessUnit
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid BumId { get; set;}

        public BuManager BuManager { get; set;}
    }
}
