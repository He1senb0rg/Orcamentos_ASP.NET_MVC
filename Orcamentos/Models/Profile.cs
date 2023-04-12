namespace Orcamentos.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Ativo { get; set; }

        public Guid ProfileLevelId { get; set;}

        public ProfileLevel ProfileLevel { get; set;}
    }
}
