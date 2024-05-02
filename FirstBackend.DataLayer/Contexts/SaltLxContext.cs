using FirstBackend.Core.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FirstBackend.DataLayer.Contexts;

public class SaltLxContext(DbContextOptions<SaltLxContext> options) : DbContext(options)
{
    public DbSet<SaltDto> Salts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<SaltDto>()
            .HasKey(s => s.UserId);
    }
}
