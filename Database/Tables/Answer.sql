CREATE TABLE [dbo].[Answer](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[SelectedOptionID] [int] NULL,
	[SurveyParticipantID] [int] NULL,
	[DateAnswered] [datetime] NULL,
 CONSTRAINT [PK_Answer_AnswerID] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
