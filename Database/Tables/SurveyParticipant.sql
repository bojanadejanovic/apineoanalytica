CREATE TABLE [dbo].[SurveyParticipant](
	[SurveyParticipantId] [int] IDENTITY(1,1) NOT NULL,
	[LastActiveDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SurveyParticipantId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

