CREATE PROCEDURE [dbo].[GetAndUpdateUserLoginInfo]
	@username nvarchar(200),
	@loginTime datetime
AS
	DECLARE @UserID int
	SET @UserID = (SELECT Id  FROM [dbo].ApplicationUser WHERE Email = @username)

	UPDATE dbo.ApplicationUser SET LastLoggedIn = @loginTime WHERE Id = @UserID

RETURN 0
