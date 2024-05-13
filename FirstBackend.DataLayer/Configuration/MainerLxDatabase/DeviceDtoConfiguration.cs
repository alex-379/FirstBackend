using FirstBackend.Core.Constants;
using FirstBackend.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstBackend.DataLayer.Configuration.MainerLxDatabase;

public class DeviceDtoConfiguration : IEntityTypeConfiguration<DeviceDto>
{
    public void Configure(EntityTypeBuilder<DeviceDto> builder)
    {
        builder
            .HasKey(d => d.Id);
        builder
            .Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(DatabasesProperties.DeviceNameLength);
        builder
            .Property(d => d.Type)
            .IsRequired();
        builder
            .Property(d => d.Address)
            .IsRequired()
            .HasMaxLength(DatabasesProperties.DeviceAddressLength);
        builder
            .Property(d => d.IsDeleted)
            .HasDefaultValue(false);
    }
}
