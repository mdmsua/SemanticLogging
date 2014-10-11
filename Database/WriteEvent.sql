CREATE PROCEDURE [dbo].[WriteEvent]
	@EventEntry EventEntry READONLY
AS
	INSERT INTO [dbo].[Event](
		[ActivityId],
		[EventId],
		[FormattedMessage],
		[Keywords],
		[Level],
		[Opcode],
		[Payload],
		[ProcessId],
		[ProviderId],
		[ProviderName],
		[RelatedActivityId],
		[Task],
		[ThreadId],
		[Timestamp],
		[Version]
	)
	SELECT
		[ActivityId],
		[EventId],
		[FormattedMessage],
		[Keywords],
		[Level],
		[Opcode],
		[Payload],
		[ProcessId],
		[ProviderId],
		[ProviderName],
		[RelatedActivityId],
		[Task],
		[ThreadId],
		[Timestamp],
		[Version]
	 FROM @EventEntry