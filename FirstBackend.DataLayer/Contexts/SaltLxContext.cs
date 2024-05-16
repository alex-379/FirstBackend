using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FirstBackend.DataLayer.Contexts;

public class SaltLxContext : DbContext
{
    public SaltLxContext(DbContextOptions<SaltLxContext> options) : base(options)
    {
    }

    public SaltLxContext()
    {
    }

    public virtual DbSet<SaltDto> Salts { get; set; } = default;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsForEntitiesInContext();
    }
}
