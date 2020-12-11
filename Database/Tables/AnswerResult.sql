CREATE TABLE [dbo].[AnswerResult](
	[AnswerResultID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[SelectedOptionID] [int] NULL,
	[SurveyParticipantID] [int] NULL,
	[DateAnswered] [datetime] NULL,
 CONSTRAINT [PK_AnswerResult_AnswerResultID] PRIMARY KEY CLUSTERED 
(
	[AnswerResultID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AnswerResult]  WITH CHECK ADD  CONSTRAINT [FK_AnswerResult_SurveyParticipant_SurveyParticipantID] FOREIGN KEY([SurveyParticipantID])
REFERENCES [dbo].[SurveyParticipant] ([SurveyParticipantID])
GO