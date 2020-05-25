CREATE TABLE [dbo].[SurveySection](
	[SurveySectionID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[SurveyId] [int] NULL,
 CONSTRAINT [PK_SurveySection_SurveySectionID] PRIMARY KEY CLUSTERED 
(
	[SurveySectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SurveySection]  WITH CHECK ADD  CONSTRAINT [FK_SurveySection_Survey_SurveyID] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[Survey] ([SurveyID])
GO

ALTER TABLE [dbo].[SurveySection] CHECK CONSTRAINT [FK_SurveySection_Survey_SurveyID]
GO