﻿CREATE TABLE [dbo].[QuestionType](
	[QuestionTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[NumberOfOptions] [smallint] NULL,
 CONSTRAINT [PK_QuestionType_QuestionTypeID] PRIMARY KEY CLUSTERED 
(
	[QuestionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO