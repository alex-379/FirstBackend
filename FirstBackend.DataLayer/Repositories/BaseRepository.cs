namespace FirstBackend.DataLayer.Repositories;

public class BaseRepository<TDbContext>(TDbContext context)
{
    protected readonly TDbContext _ctx = context;
}
