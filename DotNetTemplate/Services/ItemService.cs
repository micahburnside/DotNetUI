// ItemService.cs
using Npgsql;
using System.Collections.Generic;

namespace NETTemplate.Services
{
    public class ItemService
    {
        private readonly string _connectionString;

        public ItemService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<string> GetAllItems()
        {
            var items = new List<string>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT name FROM items", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(reader.GetString(0));
                    }
                }
            }
            return items;
        }

        public void SaveItem(string itemName)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO items (name) VALUES (@name)", conn))
                {
                    cmd.Parameters.AddWithValue("name", itemName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateItem(string oldName, string newName)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE items SET name = @newName WHERE name = @oldName", conn))
                {
                    cmd.Parameters.AddWithValue("newName", newName);
                    cmd.Parameters.AddWithValue("oldName", oldName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItem(string itemName)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM items WHERE name = @name", conn))
                {
                    cmd.Parameters.AddWithValue("name", itemName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}