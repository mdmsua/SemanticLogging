using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;

namespace WebApplication.Models
{
    public class DbEventEntry
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Guid ActivityId { get; set; }
        public Guid ProviderId { get; set; }
        public Guid RelatedActivityId { get; set; }
        public int Level { get; set; }
        public int Opcode { get; set; }
        public int ProcessId { get; set; }
        public int Task { get; set; }
        public int ThreadId { get; set; }
        public int Version { get; set; }
        public long Keywords { get; set; }
        public string FormattedMessage { get; set; }
        public string KeywordsDescription { get; set; }
        public string OpcodeName { get; set; }
        public string Payload { get; set; }
        public string ProviderName { get; set; }
        public string TaskName { get; set; }

        public DbEventEntry(EventEntry entry, string payload)
        {
            ActivityId = entry.ActivityId;
            EventId = entry.EventId;
            EventName = entry.Schema.EventName;
            FormattedMessage = entry.FormattedMessage;
            Keywords = (long)entry.Schema.Keywords;
            KeywordsDescription = entry.Schema.KeywordsDescription;
            Level = (int)entry.Schema.Level;
            Opcode = (int)entry.Schema.Opcode;
            OpcodeName = entry.Schema.OpcodeName;
            Payload = payload;
            ProcessId = entry.ProcessId;
            ProviderId = entry.ProviderId;
            ProviderName = entry.Schema.ProviderName;
            RelatedActivityId = entry.RelatedActivityId;
            Task = (int)entry.Schema.Task;
            TaskName = entry.Schema.TaskName;
            ThreadId = entry.ThreadId;
            Timestamp = entry.Timestamp;
            Version = entry.Schema.Version;
        }
    }
}