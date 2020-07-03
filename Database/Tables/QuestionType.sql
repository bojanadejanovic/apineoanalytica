CREATE TABLE [dbo].[QuestionType](
	[QuestionTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[NumberOfOptions] [smallint] NULL,
 CONSTRAINT [PK_QuestionType_QuestionTypeID] PRIMARY KEY CLUSTERED 
(
	[QuestionTypeID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO