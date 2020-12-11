CREATE PROCEDURE [dbo].[UpdateQuestion]
    @QuestionID int,
	@Text nvarchar(max),
	@AnswerOptional bit,
	@AnswersToUpdate AS dbo.IdTitleListType READONLY
AS
BEGIN
	SET NOCOUNT ON

	UPDATE dbo.Question
	SET [QuestionText] = @Text,
	    [AnswerOptional] = @AnswerOptional
	WHERE QuestionID = @QuestionID

	CREATE TABLE #AnswersToUpdate
	(
		AnswerID int,
		AnswerText nvarchar(max)
	)

	INSERT INTO #AnswersToUpdate
	SELECT ID, Title FROM @AnswersToUpdate

	UPDATE  answer
	  SET answer.AnswerText = toUpdate.AnswerText
	  FROM dbo.Answer as answer
	  INNER JOIN #AnswersToUpdate AS toUpdate
	  ON answer.AnswerID = toUpdate.AnswerID
END
	
RETURN 0
