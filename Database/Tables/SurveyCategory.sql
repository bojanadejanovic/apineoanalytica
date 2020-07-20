CREATE TABLE [dbo].[SurveyCategory](
	[SurveyCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](250) NULL,
	[CategoryDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_SurveyCategory_SuveyCategoryID] PRIMARY KEY CLUSTERED 
(
	[SurveyCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO