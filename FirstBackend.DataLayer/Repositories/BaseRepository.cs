using Microsoft.EntityFrameworkCore;

namespace FirstBackend.DataLayer.Repositories;

public class BaseRepository<TDbContext>(TDbContext context) where TDbContext : DbContext
{
    protected readonly TDbContext _ctx = context;
}
