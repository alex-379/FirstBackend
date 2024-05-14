using FirstBackend.Core.Constants.Logs.DataLayer;
using FirstBackend.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;

namespace FirstBackend.DataLayer;

public class TransactionsRepository<TDbContext>(TDbContext context) : ITransactionsRepository<TDbContext> where TDbContext : DbContext
{
    private readonly TDbContext _ctx = context;
    private readonly ILogger _logger = Log.ForContext<TransactionsRepository<TDbContext>>();

    public IDbContextTransaction BeginTransaction()
    {
        _logger.Information(TransactionsRepositoryLogs.BeginTransaction, _ctx);
        var transactionContext = _ctx.Database.BeginTransaction();

        return transactionContext;
    }

    public void CommitTransaction(IDbContextTransaction transactionContext)
    {
        transactionContext.Commit();
        _logger.Information(TransactionsRepositoryLogs.CommitTransaction, _ctx);
    }
}
