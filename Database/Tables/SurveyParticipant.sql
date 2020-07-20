CREATE TABLE [dbo].[SurveyParticipant](
	[SurveyParticipantID] [int] IDENTITY(1,1) NOT NULL,
	[LastActiveDate] [datetime] NULL,
	[Email] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[SurveyParticipantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO