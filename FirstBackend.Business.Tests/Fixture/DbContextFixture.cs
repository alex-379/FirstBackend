using FirstBackend.Core.Constants.Tests;
using FirstBackend.DataLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FirstBackend.Business.Tests.Fixture;

public class DbContextFixture : IDisposable
{
    public MainerLxContext ContextMainer { get; private set; }
    public SaltLxContext ContextSalt { get; private set; }

    public DbContextFixture()
    {
        var optionsMainer = new DbContextOptionsBuilder<MainerLxContext>()
            .UseNpgsql(Environment.GetEnvironmentVariable(Enviroments.MainerLxDb))
            .Options;

        var optionsSalt = new DbContextOptionsBuilder<SaltLxContext>()
            .UseNpgsql(Environment.GetEnvironmentVariable(Enviroments.SaltLxDb))
            .Options;

        ContextMainer = new MainerLxContext(optionsMainer);
        ContextSalt = new SaltLxContext(optionsSalt);
        ContextMainer.Database.EnsureCreated();
        ContextSalt.Database.EnsureCreated();
    }

    public void Dispose()
    {
        ContextMainer.Database.EnsureDeleted();
        ContextSalt.Database.EnsureDeleted();
        ContextMainer.Dispose();
        ContextSalt.Dispose();
        GC.SuppressFinalize(this);
    }
}