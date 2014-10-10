using System;
using System.Diagnostics.Tracing;

namespace WebApplication.Logging
{
    [EventSource(Name = "WebApplication")]
    public class SemanticLogging : EventSource
    {
        public static SemanticLogging EventSource
        {
            get
            {
                return eventSource.Value;
            }
        }

        private static readonly Lazy<SemanticLogging> eventSource = new Lazy<SemanticLogging>(() => new SemanticLogging());

        private SemanticLogging() { }

        [Event(1, Level = EventLevel.Informational, Message = "Index", Opcode = EventOpcode.Info)]
        public void Index()
        {
            if (IsEnabled())
                WriteEvent(1);
        }

        [Event(2, Level = EventLevel.Informational, Message = "About", Opcode = EventOpcode.Info)]
        public void About()
        {
            if (IsEnabled())
                WriteEvent(2);
        }

        [Event(3, Level = EventLevel.Informational, Message = "Contact", Opcode = EventOpcode.Info)]
        public void Contact()
        {
            if (IsEnabled())
                WriteEvent(3);
        }
    }
}