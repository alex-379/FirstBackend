using FirstBackend.Core.Constants;
using FirstBackend.Core.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FirstBackend.DataLayer.Contexts;

public class MainerLxContext(DbContextOptions<MainerLxContext> options) : DbContext(options)
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<DeviceDto> Devices { get; set; }
    public DbSet<OrderDto> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<DeviceDto>()
            .HasMany(d => d.Orders)
            .WithMany(o => o.Devices);

        modelBuilder
            .Entity<OrderDto>()
            .HasOne(o => o.Customer)
            .WithMany(u => u.Orders);

        modelBuilder
            .Entity<UserDto>()
            .Property(u => u.Name).IsRequired().HasMaxLength(DatabasesProperties.UserNameLength);

        modelBuilder
            .Entity<UserDto>()
            .Property(u => u.Mail).IsRequired().HasMaxLength(DatabasesProperties.MailLength);

        modelBuilder
            .Entity<UserDto>()
            .Property(u => u.Password).IsRequired().HasMaxLength(DatabasesProperties.PasswordLength);

        modelBuilder
            .Entity<UserDto>()
            .Property(u => u.Role).IsRequired();

        modelBuilder
            .Entity<OrderDto>()
            .Property(o => o.Description).IsRequired().HasMaxLength(DatabasesProperties.DeviceNameLength);

        modelBuilder
            .Entity<DeviceDto>()
            .Property(d => d.Name).IsRequired().HasMaxLength(DatabasesProperties.DeviceNameLength);

        modelBuilder
            .Entity<DeviceDto>()
            .Property(d => d.Type).IsRequired();

        modelBuilder
            .Entity<DeviceDto>()
            .Property(d => d.Address).IsRequired().HasMaxLength(DatabasesProperties.DeviceAddressLength);
    }
}
