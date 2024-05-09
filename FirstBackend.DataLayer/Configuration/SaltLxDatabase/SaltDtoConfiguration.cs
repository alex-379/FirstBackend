using FirstBackend.Core.Constants;
using FirstBackend.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstBackend.DataLayer.Configuration.SaltLxDatabase;

public class SaltDtoConfiguration : IEntityTypeConfiguration<SaltDto>
{
    public void Configure(EntityTypeBuilder<SaltDto> builder)
    {
        builder
           .HasKey(s => s.UserId);
        builder
            .Property(s => s.Salt)
            .IsRequired()
            .HasMaxLength(DatabasesProperties.SaltLength);
    }
}
