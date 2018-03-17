USE SqlExceptionDemo
GO

/*
	3/15/2018	STRATEGIC\dharrigan
*/
ALTER PROCEDURE dbo.UpsertJob
(	@job_id			INT	OUTPUT
,	@job_name		NVARCHAR(50)
,	@job_type_name	NVARCHAR(50)
,	@job_date		DATE			= NULL
,	@job_amount		MONEY			= NULL
)
AS
	
	SET NOCOUNT ON
	
	DECLARE @TranCount		INT = @@TRANCOUNT
	,		@job_type_id	INT
	
	-- Get job_type_id for update/insert to job table
	SELECT @job_type_id = job_type_id
	FROM dbo.job_type
	WHERE job_type_name = @job_type_name

	IF (@TranCount = 0)
		BEGIN TRANSACTION
		
	BEGIN TRY
	
    	IF (EXISTS(
			SELECT TOP 1 'here'
			FROM dbo.job
			WHERE job_id = @job_id
		)) BEGIN
			
			UPDATE dbo.job
			SET	job_name	= @job_name
			,	job_type_id	= @job_type_id
			,	job_date	= @job_date
			,	job_amount	= @job_amount
			WHERE job_id = @job_id

		END
		ELSE BEGIN

			INSERT INTO dbo.job
			(	job_name
			,	job_type_id
			,	job_date
			,	job_amount
			)
			VALUES
			(	@job_name
			,	@job_type_id
			,	@job_date
			,	@job_amount
			)

			SET @job_id = SCOPE_IDENTITY()

		END
    	
    	IF (@TranCount = 0)
    		COMMIT TRANSACTION
    		
    END TRY
    BEGIN CATCH
    
    	IF (@TranCount = 0)
    		ROLLBACK TRANSACTION
    
    	;THROW
    	
    END CATCH
    
GO
