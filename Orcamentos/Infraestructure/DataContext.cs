
using Microsoft.EntityFrameworkCore;
using Orcamentos.Models;

namespace Orcamentos.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }


        public DbSet<ProfileLevel> profileLevels { get; set; }

        public DbSet<Profile> profiles { get; set; }

        public DbSet<RevenueType> revenueTypes { get; set; }

        public DbSet<BuManager> buManagers { get; set; }

        public DbSet<BusinessUnit> businessUnits { get; set; }

        public DbSet<Orcamento> orcamentos { get; set; }

        public DbSet<OrcamentoNome> orcamentoNomes { get; set; }


    }
}
