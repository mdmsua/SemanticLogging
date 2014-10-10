CREATE TABLE [dbo].[Event]
(
	[Id]					BIGINT				NOT NULL	CONSTRAINT PK_Event_Id PRIMARY KEY IDENTITY (1,1),
	[ActivityId]			UNIQUEIDENTIFIER	NOT NULL,
	[EventId]				INT					NOT NULL,
	[EventName]				NVARCHAR(MAX)		NOT NULL,
	[FormattedMessage]		NVARCHAR(MAX)		NOT NULL,
	[Keywords]				BIGINT				NOT NULL,
	[KeywordsDescription]	NVARCHAR(MAX)			NULL,
	[Level]					INT					NOT NULL,
	[Opcode]				INT					NOT NULL,
	[OpcodeName]			NVARCHAR(MAX)		NOT NULL,
	[Payload]				XML					NOT NULL,
	[ProcessId]				INT					NOT NULL,
	[ProviderId]			UNIQUEIDENTIFIER	NOT NULL,
	[ProviderName]			NVARCHAR(MAX)		NOT NULL,
	[RelatedActivityId]		UNIQUEIDENTIFIER	NOT NULL,
	[Task]					INT					NOT NULL,
	[TaskName]				NVARCHAR(MAX)		NOT NULL,
	[ThreadId]				INT					NOT NULL,
	[Timestamp]				DATETIMEOFFSET(7)	NOT NULL,
	[Version]				INT					NOT NULL
)
