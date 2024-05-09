using FirstBackend.Core.Constants;
using FirstBackend.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstBackend.DataLayer.Configuration.MainerLxDatabase;

public class OrderDtoConfiguration : IEntityTypeConfiguration<OrderDto>
{
    public void Configure(EntityTypeBuilder<OrderDto> builder)
    {
        builder
            .HasKey(o => o.Id);
        builder
            .HasOne(o => o.Customer)
            .WithMany(u => u.Orders);
        builder
            .Property(o => o.Description)
            .IsRequired()
            .HasMaxLength(DatabasesProperties.DeviceNameLength);
        builder
            .Property(o => o.IsDeleted)
            .HasDefaultValue(false);
    }
}
