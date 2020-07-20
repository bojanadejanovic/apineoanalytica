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

DELETE FROM [Answer]
DELETE FROM [Question]
DELETE FROM [QuestionOption]
DELETE FROM [QuestionType]
DELETE FROM [ApplicationRole]
DELETE FROM [SurveyParticipant]
DELETE FROM [SurveyCategory]
DELETE FROM [Survey]
DELETE FROM [ApplicationUserRole]
DELETE FROM [ApplicationUser]
