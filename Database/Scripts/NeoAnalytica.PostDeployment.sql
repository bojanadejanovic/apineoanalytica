/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/* Insert data to SurveyCategory */

SET IDENTITY_INSERT [dbo].[SurveyCategory] ON 

INSERT [dbo].[SurveyCategory] ([SurveyCategoryID], [CategoryName], [CategoryDescription]) VALUES (1, N'Tech & Business', NULL)
INSERT [dbo].[SurveyCategory] ([SurveyCategoryID], [CategoryName], [CategoryDescription]) VALUES (2, N'Health', NULL)
INSERT [dbo].[SurveyCategory] ([SurveyCategoryID], [CategoryName], [CategoryDescription]) VALUES (3, N'Insurance', NULL)
INSERT [dbo].[SurveyCategory] ([SurveyCategoryID], [CategoryName], [CategoryDescription]) VALUES (4, N'Shopping', NULL)
INSERT [dbo].[SurveyCategory] ([SurveyCategoryID], [CategoryName], [CategoryDescription]) VALUES (5, N'Education', NULL)
SET IDENTITY_INSERT [dbo].[SurveyCategory] OFF