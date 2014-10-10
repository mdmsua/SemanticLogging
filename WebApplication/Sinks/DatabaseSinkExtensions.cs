using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;

namespace WebApplication.Sinks
{
    public static class DatabaseSinkExtensions
    {
        public static SinkSubscription<DatabaseSink> LogToDatabase(this IObservable<EventEntry> eventStream)
        {
            var sink = new DatabaseSink();
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<DatabaseSink>(subscription, sink);
        }

        public static SinkSubscription<DatabaseSink> LogToDatabase(this IObservable<EventEntry> eventStream, string name)
        {
            var sink = new DatabaseSink(name);
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<DatabaseSink>(subscription, sink);
        }
    }
}