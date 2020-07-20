CREATE TABLE [dbo].[Survey](
	[SurveyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Description] [nvarchar](max) NULL,
	[UserID] [int] NULL,
	[SurveyCategoryID] [int] NULL,
 CONSTRAINT [PK_Survey_SurveyID] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Survey]  WITH CHECK ADD  CONSTRAINT [FK_Survey_ApplicationUser_Id] FOREIGN KEY([UserID])
REFERENCES [dbo].[ApplicationUser] ([Id])
GO

ALTER TABLE [dbo].[Survey] CHECK CONSTRAINT [FK_Survey_ApplicationUser_Id]
GO

ALTER TABLE [dbo].[Survey]  WITH CHECK ADD  CONSTRAINT [FK_Survey_SurveyCategory_SuveyCategoryID] FOREIGN KEY([SurveyCategoryID])
REFERENCES [dbo].[SurveyCategory] ([SuveyCategoryID])
GO

ALTER TABLE [dbo].[Survey] CHECK CONSTRAINT [FK_Survey_SurveyCategory_SuveyCategoryID]
GO