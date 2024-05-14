using FirstBackend.Core.Constants.Logs.DataLayer;
using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class SaltsRepository(SaltLxContext context) : BaseRepository<SaltLxContext>(context), ISaltsRepository
{
    private readonly ILogger _logger = Log.ForContext<SaltsRepository>();

    public void AddSalt(SaltDto salt)
    {
        _ctx.Salts.Add(salt);
        _ctx.SaveChanges();
        _logger.Information(SaltsRepositoryLogs.AddSalt, salt.UserId);
    }

    public SaltDto GetSaltByUserId(Guid userId)
    {
        _logger.Information(SaltsRepositoryLogs.GetSaltByUserId, userId);

        return _ctx.Salts
            .FirstOrDefault(s => s.UserId == userId);
    }

    public void UpdateSalt(SaltDto salt)
    {
        _ctx.Salts.Update(salt);
        _ctx.SaveChanges();
        _logger.Information(SaltsRepositoryLogs.UpdateSalt, salt.UserId);
    }

    public void DeleteSalt(SaltDto salt)
    {
        _ctx.Salts.Remove(salt);
        _ctx.SaveChanges();
        _logger.Information(SaltsRepositoryLogs.DeleteSalt, salt.UserId);
    }
}
