CREATE TABLE [dbo].[Question](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionТypeID] [int] NOT NULL,
	[AnswerOptional] [bit] NULL,
	[SurveyID] [int] NOT NULL,
 CONSTRAINT [PK_Question_QuestionID] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Survey_SurveyID] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[Survey] ([SurveyID])
GO

ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Survey_SurveyID]
GO