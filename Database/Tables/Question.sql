CREATE TABLE [dbo].[Question](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionТypeID] [int] NOT NULL,
	[AnswerOptional] [bit] NULL,
	[SurveyID] [int] NOT NULL,
 CONSTRAINT [PK_Question_QuestionID] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO