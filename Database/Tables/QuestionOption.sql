CREATE TABLE [dbo].[QuestionOption](
	[QuestionOptionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[QuestionOptionValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuestionOption_QuestionOptionID] PRIMARY KEY CLUSTERED 
(
	[QuestionOptionID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO