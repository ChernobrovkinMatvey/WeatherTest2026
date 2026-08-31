using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Domain.Entities;

namespace Weather.Infrastructure.DataLayer.Configurations;

public class CityOfSubjectConfiguration : IEntityTypeConfiguration<CityOfSubject>
{
    public void Configure(EntityTypeBuilder<CityOfSubject> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName("Id")
            .HasComment("Id")
            .IsRequired();

        builder.Property(t => t.Subject)
            .HasColumnName("Subject")
            .HasComment("Субъект РФ")
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(t => t.City)
            .HasColumnName("City")
            .HasComment("Административный центр")
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(t => t.Lat)
            .HasColumnName("Lat")
            .HasPrecision(18, 8)
            .HasComment("Широта")
            .IsRequired();
        
        builder.Property(t => t.Lon)
            .HasColumnName("Lon")
            .HasPrecision(18, 8)
            .HasComment("Долгота")
            .IsRequired();
        
        builder.HasKey(t => t.Id);
        
        builder.HasIndex(t => new { t.Subject, t.City });
        
        builder.ToTable("CityOfSubjects");
    }
}