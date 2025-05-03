using MySql.Data.MySqlClient;

namespace DbConnection.Services;

public class DbClient
{
    private readonly string connectionString;

    public DbClient(string server, string database, string user, string password)
    {
        this.connectionString = $"Server={server};Database={database};User ID={user};Password={password};";
    }

    /// <summary>
    /// Connect to the database and get the list of tables.
    /// </summary>
    /// <returns>List of tables in the database</returns>
    /// <exceptions>Exception</exceptions>
    public async Task<(bool success, string error, List<string> tables)> GetTablesAsync()
    {
        var tables = new List<string>();
        try
        {
            using var connection = await ConnectAsync();
            using var command = new MySqlCommand("SHOW TABLES", connection); //set command
            using var reader = await command.ExecuteReaderAsync(); // execute command

            while (await reader.ReadAsync())
            {
                // Add each table name to the list
                tables.Add(reader.GetString(0));
            }
        }
        catch (Exception ex)
        {
            var error = $"Failed to connect to the database. Details:{ex.Message}";
            return (false, error, tables);
        }

        return (true, string.Empty, tables);
    }

    public async Task<MySqlConnection> ConnectAsync()
    {
        var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
