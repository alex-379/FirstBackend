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
            .HasMany(d => d.Devices)
            .WithMany(o => o.Orders)
            .UsingEntity<DevicesOrders>(
                i => i
                    .HasOne(i => i.Device)
                    .WithMany(d => d.DevicesOrders)
                    .HasForeignKey(i => i.DeviceId),
                i => i
                    .HasOne(i => i.Order)
                    .WithMany(o => o.DevicesOrders)
                    .HasForeignKey(i => i.OrderId),
                i =>
                {
                    i.Property(i => i.NumberDevices).HasDefaultValue(1);
                    i.HasKey(i => new { i.DeviceId, i.OrderId });
                });
        builder
            .Property(o => o.Description)
            .IsRequired()
            .HasMaxLength(DatabasesProperties.DeviceNameLength);
        builder
            .Property(o => o.IsDeleted)
            .HasDefaultValue(false);
    }
}
