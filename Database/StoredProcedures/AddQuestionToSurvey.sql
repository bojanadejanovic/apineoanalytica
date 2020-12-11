CREATE PROCEDURE [dbo].[AddQuestionsToSurvey]
	@Answers AS dbo.StringList READONLY,
	@Text nvarchar(max),
	@SurveyID int,
	@AnswerOptional bit,
	@QuestionTypeID int,
	@QuestionID int out
AS
BEGIN
	INSERT INTO dbo.Question (QuestionТypeID, QuestionText, AnswerOptional, SurveyID) 
	VALUES(@QuestionTypeID, @Text, @AnswerOptional, @SurveyID);
	SET @QuestionID = (SELECT CAST(SCOPE_IDENTITY() as int));

	CREATE TABLE #AnswersToAdd
	(
		QuestionID int,
		AnswerText nvarchar(max)
	)

	INSERT INTO #AnswersToAdd
	SELECT @QuestionID, Item FROM @Answers

	INSERT INTO dbo.Answer(QuestionID, AnswerText) 
	SELECT QuestionID, AnswerText  FROM #AnswersToAdd
END
RETURN 0

