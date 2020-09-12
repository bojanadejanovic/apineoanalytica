CREATE PROCEDURE [dbo].[GetAndUpdateUserLoginInfo]
	@username nvarchar(200),
	@loginTime datetime,
	@userId int out
AS
	SET @userId = (SELECT Id  FROM [dbo].ApplicationUser WHERE Email = @username)

	UPDATE dbo.ApplicationUser SET LastLoggedIn = @loginTime WHERE Id = @userId

RETURN 0
