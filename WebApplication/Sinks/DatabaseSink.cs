using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using WebApplication.Models;

namespace WebApplication.Sinks
{
    public class DatabaseSink : IObserver<EventEntry>
    {
        private readonly IEventTextFormatter formatter = new XmlEventTextFormatter(EventTextFormatting.Indented);
        private readonly Database database;
        private readonly ConcurrentQueue<DbEventEntry> events = new ConcurrentQueue<DbEventEntry>();
        private readonly Timer timer;
        private const string type = "EventEntry";
        private const string text = "WriteEvent";
        private static readonly List<PropertyInfo> properties;

        static DatabaseSink()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            properties = (typeof(DbEventEntry)
                .GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public)
                .OrderBy(p => p.Name))
                .ToList();
        }

        public DatabaseSink() : this(string.Empty) { }

        public DatabaseSink(string name)
        {
            database = string.IsNullOrEmpty(name) ? DatabaseFactory.CreateDatabase() : DatabaseFactory.CreateDatabase(name);
            timer = new Timer(OnTick, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(EventEntry value)
        {
            if (value == null) return;
            using (var writer = new StringWriter())
            {
                formatter.WriteEvent(value, writer);
                events.Enqueue(new DbEventEntry(value, writer.ToString()));
            }
        }

        private void OnTick(object state)
        {
            if (events.Count == 0) return;
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = text;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(BuildTableValuedParameter());
                    command.ExecuteNonQuery();
                }
            }
        }

        private DbParameter BuildTableValuedParameter()
        {
            var dataTable = new DataTable(type);
            dataTable.Columns.AddRange(properties.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            while (events.Count > 0)
            {
                DbEventEntry @event;
                if (events.TryDequeue(out @event))
                    dataTable.Rows.Add(properties.Select(p => p.GetValue(@event)).ToArray());
            }
            return new SqlParameter(type, SqlDbType.Structured)
            {
                TypeName = type,
                SqlValue = dataTable
            };
        }
    }
}