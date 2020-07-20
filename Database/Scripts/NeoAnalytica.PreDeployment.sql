/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


/* Clean up all the tables */

--DELETE FROM dbo.[Answer]
--DELETE FROM dbo.[Question]
--DELETE FROM dbo.[QuestionOption]
--DELETE FROM dbo.[QuestionType]
--DELETE FROM dbo.[ApplicationRole]
--DELETE FROM dbo.[SurveyParticipant]
--DELETE FROM dbo.[SurveyCategory]
--DELETE FROM dbo.[Survey]
--DELETE FROM dbo.[ApplicationUserRole]
--DELETE FROM dbo.[ApplicationUser]
