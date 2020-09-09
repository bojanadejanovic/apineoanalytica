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

    INSERT INTO dbo.SurveyCategory VALUES (1, 'Apparel & Accessories', null)
    INSERT INTO dbo.SurveyCategory VALUES (2, 'Art', null)
    INSERT INTO dbo.SurveyCategory VALUES (3, 'Autos & Vehicles', null)
    INSERT INTO dbo.SurveyCategory VALUES (4, 'Banking & Finance', null)
    INSERT INTO dbo.SurveyCategory VALUES (5, 'Business Services', null)
    INSERT INTO dbo.SurveyCategory VALUES (6, 'Career', null)
    INSERT INTO dbo.SurveyCategory VALUES (7, 'Consumer Electronics', null)
    INSERT INTO dbo.SurveyCategory VALUES (8, 'Dating', null)
    INSERT INTO dbo.SurveyCategory VALUES (9, 'DIY', null)
    INSERT INTO dbo.SurveyCategory VALUES (10, 'Economy', null)
    INSERT INTO dbo.SurveyCategory VALUES (11, 'Education', null)
    INSERT INTO dbo.SurveyCategory VALUES (12, 'Employment', null)
    INSERT INTO dbo.SurveyCategory VALUES (13, 'Ensurance', null)
    INSERT INTO dbo.SurveyCategory VALUES (14, 'Entertainment', null)
    INSERT INTO dbo.SurveyCategory VALUES (15, 'Events', null)
    INSERT INTO dbo.SurveyCategory VALUES (16, 'Fashion & Beauty', null)
    INSERT INTO dbo.SurveyCategory VALUES (17, 'Gaming', null)
    INSERT INTO dbo.SurveyCategory VALUES (18, 'Gifts & Occasions', null)
    INSERT INTO dbo.SurveyCategory VALUES (19, 'Hobby', null)
    INSERT INTO dbo.SurveyCategory VALUES (20, 'Home & Garden', null)
    INSERT INTO dbo.SurveyCategory VALUES (21, 'Local Issues', null)
    INSERT INTO dbo.SurveyCategory VALUES (22, 'Marketing/Advertising', null)
    INSERT INTO dbo.SurveyCategory VALUES (23, 'Medical', null)
    INSERT INTO dbo.SurveyCategory VALUES (24, 'Medical Health', null)
    INSERT INTO dbo.SurveyCategory VALUES (25, 'Nutrition', null)
    INSERT INTO dbo.SurveyCategory VALUES (26, 'Parenthood', null)
    INSERT INTO dbo.SurveyCategory VALUES (27, 'Parks and Recration', null)
    INSERT INTO dbo.SurveyCategory VALUES (28, 'Pets', null)
    INSERT INTO dbo.SurveyCategory VALUES (29, 'Politics', null)
    INSERT INTO dbo.SurveyCategory VALUES (30, 'Real Estate', null)
    INSERT INTO dbo.SurveyCategory VALUES (31, 'Schools', null)
    INSERT INTO dbo.SurveyCategory VALUES (32, 'Shopping', null)
    INSERT INTO dbo.SurveyCategory VALUES (33, 'Social Issues', null)
    INSERT INTO dbo.SurveyCategory VALUES (34, 'Social Life', null)
    INSERT INTO dbo.SurveyCategory VALUES (35, 'Social Media', null)
    INSERT INTO dbo.SurveyCategory VALUES (36, 'Sports & Fitness', null)
    INSERT INTO dbo.SurveyCategory VALUES (37, 'Telecom/Cable & Satellite TV Providers', null)
    INSERT INTO dbo.SurveyCategory VALUES (38, 'Travel', null)
    INSERT INTO dbo.SurveyCategory VALUES (39, 'Vacation', null)

SET IDENTITY_INSERT dbo.SurveyCategory OFF