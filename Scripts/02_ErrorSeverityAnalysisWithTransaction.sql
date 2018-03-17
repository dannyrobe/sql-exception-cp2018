USE SqlExceptionDemo
GO

/*
	EXEC dbo.ErrorSeverityAnalysisWithTransaction @UseThrow=0

	EXEC dbo.ErrorSeverityAnalysisWithTransaction @UseThrow=1

	With TRANSACTION
		RAISERROR severity 1-10 => soft error
		RAISERROR severity 11-16 => hard error (stops and further execution)
		THROW => hard error (stops and further execution)
*/
ALTER PROCEDURE dbo.ErrorSeverityAnalysisWithTransaction
(	@UseThrow	BIT = 0
)
AS
	
	DECLARE @TranCount INT = @@TRANCOUNT
	
	IF (@TranCount = 0)
		BEGIN TRANSACTION
		
	BEGIN TRY
	
		PRINT 'LINE 1'
		RAISERROR('This is a test error (severity = 10; state = 1)', 10, 1);

		PRINT 'LINE 2'
		IF (@UseThrow = 0)
			RAISERROR('This is a test error (severity = 16; state = 2)', 16, 2);
		ELSE
			THROW 50001, 'This is a test THROW (severity = 16; state = 2)', 2
			
		PRINT 'LINE 3'
		RAISERROR('This is a test error (severity = 10; state = 3)', 10, 3);

		PRINT 'LINE 4 - FINISHED!'
    	
    	IF (@TranCount = 0)
    		COMMIT TRANSACTION
    		
    END TRY
    BEGIN CATCH
    
    	IF (@TranCount = 0)
    		ROLLBACK TRANSACTION
    
    	;THROW
    	
    END CATCH
    
GO
