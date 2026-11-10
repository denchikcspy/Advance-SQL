using MySqlConnector;
using System;
using System.Collections.Generic;
using WindowsFormsApp8.Entities;

namespace WindowsFormsApp8.Repos
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private User CreateObject(MySqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32("id"),
                Name = reader.IsDBNull(reader.GetOrdinal("name")) ? "" : reader.GetString("name"),
                Email = reader.GetString("email"),
                Password = reader.GetInt32("password"),
                CreatedAt = reader.GetDateTime("createdAt"),
                UpdatedAt = reader.GetDateTime("updatedAt"),
                BirthDay = reader.GetDateTime("birthDay")
            };
        }

        public int Create(string name, string email, int password, DateTime birthDay)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var command = new MySqlCommand(
                    "INSERT INTO users (name, email, password, updatedAt, birthDay) VALUES (@Name, @Email, @Password, @UpdatedAt, @BirthDay); SELECT LAST_INSERT_ID();",
                    connection);

                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                command.Parameters.AddWithValue("@BirthDay", birthDay);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public List<User> GetAll()
        {
            var users = new List<User>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var command = new MySqlCommand("SELECT id, name, email, password, createdAt, updatedAt, birthDay FROM users", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(CreateObject(reader));
                    }
                }
            }

            return users;
        }
    }
}
