using BoxTI.Challenge.CovidTracking.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoxTI.Challenge.CovidTracking.Data.Mapping
{
    public class InfoCovidMapping : IEntityTypeConfiguration<InfoCovid>
    {
        public void Configure(EntityTypeBuilder<InfoCovid> builder)
        {
            builder.ToTable("InfoCovid");

            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Id).HasColumnType("char(36)").IsRequired();

            builder.Property(p => p.ActiveCases).HasColumnType("int(15)");

            builder.Property(p => p.Country).HasColumnType("varchar(80)").IsRequired();

            builder.Property(p => p.LastUpdate).HasColumnType("datetime").IsRequired();

            builder.Property(p => p.NewCases).HasColumnType("varchar(80)").IsRequired();

            builder.Property(p => p.NewDeaths).HasColumnType("varchar(80)").IsRequired();

            builder.Property(p => p.TotalCases).HasColumnType("int(15)").IsRequired();

            builder.Property(p => p.TotalDeaths).HasColumnType("int(15)").IsRequired();

            builder.Property(p => p.TotalRecovered).HasColumnType("int(15)").IsRequired();

            builder.Property(p => p.Active).HasColumnType("bit").IsRequired();
        }
    }
}
