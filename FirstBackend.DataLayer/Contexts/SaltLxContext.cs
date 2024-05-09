using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FirstBackend.DataLayer.Contexts;

public class SaltLxContext(DbContextOptions<SaltLxContext> options) : DbContext(options)
{
    public DbSet<SaltDto> Salts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsForEntitiesInContext();
    }
}
