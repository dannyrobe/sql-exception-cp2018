USE SqlExceptionDemo
GO

/*
	UpsertJobV3

	Check if Job name is unique and throw hard error if duplicate.
*/
ALTER PROCEDURE dbo.UpsertJobV3
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
			SELECT TOP(1) 'here'
			FROM dbo.job AS j
			WHERE j.job_name = @job_name
				AND (@job_id IS NULL OR j.job_id <> @job_id)
		))
		BEGIN
			
			RAISERROR('A Job with name [%s] already exists.', 12, 255, @job_name) -- hard error
		
		END

		IF (@job_type_id IS NULL)
		BEGIN
        
			INSERT INTO dbo.job_type
			(	job_type_name
			,	job_type_description
			)
			VALUES
			(	@job_type_name
			,	@job_type_name + ' contractor was used.'
			)

			SET @job_type_id = SCOPE_IDENTITY()

			RAISERROR('A new Job Type [%s] was created.', 10, 255, @job_type_name) -- soft error
			
		END
        
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
