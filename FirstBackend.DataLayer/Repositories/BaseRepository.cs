namespace FirstBackend.DataLayer.Repositories;

public class BaseRepository
{
    protected readonly MainerLxContext _ctx;

    public BaseRepository(MainerLxContext context)
    {
        _ctx = context;
    }
}
