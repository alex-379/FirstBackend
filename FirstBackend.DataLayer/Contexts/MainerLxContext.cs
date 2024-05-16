using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FirstBackend.DataLayer.Contexts;

public class MainerLxContext : DbContext
{
    public MainerLxContext(DbContextOptions<MainerLxContext> options) : base(options)
    {
    }

    public MainerLxContext()
    {
    }

    public virtual DbSet<UserDto> Users { get; set; } = default;
    public virtual DbSet<DeviceDto> Devices { get; set; } = default;
    public virtual DbSet<OrderDto> Orders { get; set; } = default;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsForEntitiesInContext();
    }
}
