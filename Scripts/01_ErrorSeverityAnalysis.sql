USE SqlExceptionDemo
GO

/*
	EXEC dbo.ErrorSeverityAnalysis @UseThrow=0

	EXEC dbo.ErrorSeverityAnalysis @UseThrow=1

	Without TRANSACTION
		RAISERROR severity 1-10 => soft error
		RAISERROR severity 11-16 => soft error
		THROW => hard error (stops and further execution)

	https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-error-severities
*/
ALTER PROCEDURE dbo.ErrorSeverityAnalysis
(	@UseThrow	BIT = 0
)
AS
	
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
    	
GO
