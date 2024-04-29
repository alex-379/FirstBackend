using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class SaltsRepository(SaltLxContext context) : ISaltsRepository
{
    protected readonly SaltLxContext _ctx = context;
    private readonly ILogger _logger = Log.ForContext<UsersRepository>();

    public void AddSalt(SaltDto salt)
    {

        _ctx.Salts.Add(salt);
        _ctx.SaveChanges();
        _logger.Information("Вносим в базу данных соль пользователя с ID {id}", salt.UserId);
    }
}
