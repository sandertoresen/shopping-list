using MySql.Data.MySqlClient;
using ShoppingListAPI.models;

namespace ShoppingListAPI.Database;

public class DatabaseCalls
{
    private string _connectionString;

    public DatabaseCalls(string connectionString)
    {
        _connectionString = connectionString;
        SetupTable();
    }
    
    private void SetupTable()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            Console.WriteLine("Connected to database!");

            string tableName = "Item";

            string checkTableExistsQuery = $"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'database' AND table_name = '{tableName}'";
            using (var command = new MySqlCommand(checkTableExistsQuery, connection))
            {
                int tableCount = Convert.ToInt32(command.ExecuteScalar());
                if (tableCount == 0)
                {
                    // Table does not exist, create it
                    string createTableQuery = $@"
                    CREATE TABLE {tableName} (
                        Id INT PRIMARY KEY AUTO_INCREMENT,
                        Name VARCHAR(255),
                        Count INT
                    )";
                    using (var createCommand = new MySqlCommand(createTableQuery, connection))
                    {
                        createCommand.ExecuteNonQuery();
                        Console.WriteLine($"Table '{tableName}' created successfully.");
                    }
                }
                else
                {
                    Console.WriteLine($"Table '{tableName}' already exists.");
                }
            }

            connection.Close();
        }
    }

    public IList<Item> GetItems()
    {
        List<Item> items = new List<Item>();
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        Console.WriteLine("Connected to database!");

        string tableName = "Item";

        string fetchAllQuery = $"SELECT * FROM Item;";
        using (var command = new MySqlCommand(fetchAllQuery, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = new Item
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Count = Convert.ToInt32(reader["Count"])
                    };

                    items.Add(item);
                }
            }
        }

        connection.Close();

        return items;
    }

    public IList<Item> PutItem(int itemId, int increment)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            string updateItemQuery = $"UPDATE Item SET Count = Count + {increment} WHERE Id = {itemId}";

            using (var updateCommand = new MySqlCommand(updateItemQuery, connection))
            {
                updateCommand.ExecuteNonQuery();
            }

            connection.Close();
        }
        
        return GetItems();
    }

    public IList<Item> PostItem(string itemName, int increment)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            string insertNewItemQuery = $"INSERT INTO Item (Name, Count) VALUES ('{itemName}', {increment});";
            using (var insertCommand = new MySqlCommand(insertNewItemQuery, connection))
            {
                insertCommand.ExecuteNonQuery();
            }

            connection.Close();
        }

        // Return the updated list of items
        return GetItems();
    }

    public IList<Item> DeleteItem(int itemId)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            string updateItemQuery = $"Delete FROM Item WHERE Id = {itemId}";

            using (var updateCommand = new MySqlCommand(updateItemQuery, connection))
            {
                updateCommand.ExecuteNonQuery();
            }

            connection.Close();
        }
        
        return GetItems();
    }
}