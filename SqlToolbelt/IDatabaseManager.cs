namespace SqlToolbelt
{
    public interface IDatabaseManager
    {
        void CreateDatabase(string databaseName);
        void DropDatabase(string databaseName);
    }
}