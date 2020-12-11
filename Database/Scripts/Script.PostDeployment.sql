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
SET IDENTITY_INSERT dbo.SurveyCategory ON

	INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (1, 'Apparel & Accessories', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (2, 'Art', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (3, 'Autos & Vehicles', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (4, 'Banking & Finance', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (5, 'Business Services', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (6, 'Career', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (7, 'Consumer Electronics', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (8, 'Dating', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (9, 'DIY', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (10, 'Economy', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (11, 'Education', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (12, 'Employment', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (13, 'Ensurance', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (14, 'Entertainment', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (15, 'Events', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (16, 'Fashion & Beauty', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (17, 'Gaming', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (18, 'Gifts & Occasions', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (19, 'Hobby', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (20, 'Home & Garden', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (21, 'Local Issues', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (22, 'Marketing/Advertising', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (23, 'Medical', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (24, 'Medical Health', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (25, 'Nutrition', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (26, 'Parenthood', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (27, 'Parks and Recration', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (28, 'Pets', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (29, 'Politics', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (30, 'Real Estate', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (31, 'Schools', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (32, 'Shopping', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (33, 'Social Issues', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (34, 'Social Life', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (35, 'Social Media', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (36, 'Sports & Fitness', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (37, 'Telecom/Cable & Satellite TV Providers', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (38, 'Travel', null)
    INSERT INTO dbo.SurveyCategory (SurveyCategoryID,CategoryName,CategoryDescription) VALUES (39, 'Vacation', null)

SET IDENTITY_INSERT dbo.SurveyCategory OFF