namespace FirstBackend.Core.Constants.Logs.DataLayer;

public static class TransactionsRepositoryLogs
{
    public const string BeginTransaction = "Начало транзакции для базы данных {_ctx}]";
    public const string CommitTransaction = "Подтверждение транзакции для базы данных {_ctx}]";
}
