using System.ComponentModel.DataAnnotations.Schema;

namespace Orcamentos.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("profileLevels")]
        public Guid profileLevelId { get; set;}

        public ProfileLevel? ProfileLevel { get; set;}
    }
}
