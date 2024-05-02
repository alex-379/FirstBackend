using FirstBackend.DataLayer.Contexts;

namespace FirstBackend.DataLayer.Repositories;

public class BaseRepository(MainerLxContext context)
{
    protected readonly MainerLxContext _ctx = context;
}
