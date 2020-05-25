CREATE TABLE [dbo].[Question](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[QuestionTypeID] [int] NULL,
	[AnswerOptional] [bit] NULL,
	[SectionID] [int] NULL,
 CONSTRAINT [PK_Question_QuestionID] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_QuestionType_QuestionTypeID] FOREIGN KEY([QuestionTypeID])
REFERENCES [dbo].[QuestionType] ([QuestionTypeID])
GO

ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_QuestionType_QuestionTypeID]
GO

ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_SurveySection_SurveySectionID] FOREIGN KEY([SectionID])
REFERENCES [dbo].[SurveySection] ([SurveySectionID])
GO

ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_SurveySection_SurveySectionID]
GO
