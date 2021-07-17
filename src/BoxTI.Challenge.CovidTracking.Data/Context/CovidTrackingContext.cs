using BoxTI.Challenge.CovidTracking.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BoxTI.Challenge.CovidTracking.Data.Context
{
    public class CovidTrackingContext : DbContext
    {
        public CovidTrackingContext(DbContextOptions<CovidTrackingContext> options) : base(options)
        {
        }

        public DbSet<InfoCovid> InfoCovid { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CovidTrackingContext).Assembly);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
