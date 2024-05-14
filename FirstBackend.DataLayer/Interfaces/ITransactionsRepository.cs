using Microsoft.EntityFrameworkCore.Storage;

namespace FirstBackend.DataLayer.Interfaces
{
    public interface ITransactionsRepository<TDbContext>
    {
        IDbContextTransaction BeginTransaction();
        void CommitTransaction(IDbContextTransaction transactionContext);
    }
}