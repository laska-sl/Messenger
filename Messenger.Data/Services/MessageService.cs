using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Messenger.Data.Models;

using Microsoft.Extensions.Configuration;

using Npgsql;

namespace Messenger.Data.Services
{
    public class MessageRepository : IMessageService
    {
        private readonly string connectionString;

        public MessageRepository(IConfiguration configuration)
        {
            this.connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            // For development purposes
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateNewMessage(Message message, CancellationToken cancellationToken)
        {
            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"INSERT INTO messages (text, createdat, number) values ('{message.Text}', TIMESTAMP '{message.CreatedAt}', '{message.Number}')";

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }

        public async Task<Message> GetMessageById(int id, CancellationToken cancellationToken)
        {
            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM messages WHERE Id={id}";

                    using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            var message = new Message
                            {
                                Text = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("text"), cancellationToken),
                                CreatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("createdat"), cancellationToken),
                                Number = await reader.GetFieldValueAsync<int>(reader.GetOrdinal("number"), cancellationToken)
                            };
                            
                            return message;
                        }

                        return null;
                    }
                }
            }
        }

        public async Task<IEnumerable<Message>> GetMessagesInInterval(DateIntervalParams dateIntervalParams, CancellationToken cancellationToken)
        {
            using (var connection = new NpgsqlConnection(this.connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                using (var command = connection.CreateCommand())
                {
                    var dateTo = new DateTime(dateIntervalParams.To.Year, dateIntervalParams.To.Month, dateIntervalParams.To.Day, 23, 59, 59);

                    command.CommandText = $"SELECT * FROM messages WHERE createdat BETWEEN '{dateIntervalParams.From}' AND '{dateTo}'";

                    using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        var messages = new List<Message>();

                        while (await reader.ReadAsync(cancellationToken))
                        {
                            var message = new Message
                            {
                                Text = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("text"), cancellationToken),
                                CreatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("createdat"), cancellationToken),
                                Number = await reader.GetFieldValueAsync<int>(reader.GetOrdinal("number"), cancellationToken)
                            };

                            messages.Add(message);
                        }

                        return messages;
                    }
                }
            }
        }
    }
}