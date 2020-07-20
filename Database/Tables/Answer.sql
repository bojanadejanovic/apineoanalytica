CREATE TABLE [dbo].[Answer](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[SelectedOptionID] [int] NULL,
	[SurveyParticipantID] [int] NULL,
	[DateAnswered] [datetime] NULL,
 CONSTRAINT [PK_Answer_AnswerID] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Question_QuestionID] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([QuestionID])
GO

ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Question_QuestionID]
GO

ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_QuestionOption_QuestionOptionID] FOREIGN KEY([SelectedOptionID])
REFERENCES [dbo].[QuestionOption] ([QuestionOptionID])
GO

ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_QuestionOption_QuestionOptionID]
GO

ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_SurveyParticipant_SurveyParticipantID] FOREIGN KEY([SurveyParticipantID])
REFERENCES [dbo].[SurveyParticipant] ([SurveyParticipantID])
GO

ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_SurveyParticipant_SurveyParticipantID]
GO