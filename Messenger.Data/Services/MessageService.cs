using System;
using System.Collections.Generic;


using Messenger.Data.Models;

using Microsoft.Extensions.Configuration;

using Npgsql;

namespace Messenger.Data.Services
{
    public class MessageRepository : IMessageService
    {
        private readonly string connectionString;

        public MessageRepository()
        {
            this.connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }

        public void CreateNewMessage(Message message)
        {
            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"INSERT INTO messages (text, createdat, number) values ('{message.Text}', TIMESTAMP '{message.CreatedAt}', '{message.Number}')";

                    command.ExecuteNonQuery();
                }
            }
        }

        public Message GetMessageById(int id)
        {
            var message = new Message();

            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM messages WHERE Id={id}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            message.Text = reader.GetString(reader.GetOrdinal("text"));
                            message.CreatedAt = reader.GetDateTime(reader.GetOrdinal("createdat"));
                            message.Number = reader.GetInt32(reader.GetOrdinal("number"));
                        }
                    }
                }
            }

            return message;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            var messages = new List<Message>();

            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM messages";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = new Message
                            {
                                Text = reader.GetString(reader.GetOrdinal("text")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("createdat")),
                                Number = reader.GetInt32(reader.GetOrdinal("number"))
                            };

                            messages.Add(message);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }

            return messages;
        }

        public IEnumerable<Message> GetMessagesInInterval(DateIntervalParams dateIntervalParams)
        {
            var messages = new List<Message>();

            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    var dateTo = new DateTime(dateIntervalParams.To.Year, dateIntervalParams.To.Month, dateIntervalParams.To.Day, 23, 59, 59);

                    command.CommandText = $"SELECT * FROM messages WHERE createdat BETWEEN '{dateIntervalParams.From}' AND '{dateTo}'";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = new Message
                            {
                                Text = reader.GetString(reader.GetOrdinal("text")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("createdat")),
                                Number = reader.GetInt32(reader.GetOrdinal("number"))
                            };

                            messages.Add(message);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }

            return messages;
        }
    }
}
