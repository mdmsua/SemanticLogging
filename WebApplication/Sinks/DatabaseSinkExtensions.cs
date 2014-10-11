using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;

namespace WebApplication.Sinks
{
    public static class DatabaseSinkExtensions
    {
        public static SinkSubscription<DatabaseSink> LogToDatabase(this IObservable<EventEntry> eventStream, TimeSpan flushInterval)
        {
            var sink = new DatabaseSink(flushInterval);
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<DatabaseSink>(subscription, sink);
        }

        public static SinkSubscription<DatabaseSink> LogToDatabase(this IObservable<EventEntry> eventStream, string databaseName, TimeSpan flushInterval)
        {
            var sink = new DatabaseSink(databaseName, flushInterval);
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<DatabaseSink>(subscription, sink);
        }
    }
}