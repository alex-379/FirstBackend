using FirstBackend.Core.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FirstBackend.DataLayer;

public class MainerLxContext:DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<DeviceDto> Devices { get; set; }
    public DbSet<OrderDto> Orders { get; set; }

    public MainerLxContext(DbContextOptions<MainerLxContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<DeviceDto>()
            .HasOne(d => d.Owner)
            .WithMany(u => u.Devices);

        modelBuilder
            .Entity<OrderDto>()
            .HasOne(o => o.Customer)
            .WithMany(u => u.Orders);
    }
}
