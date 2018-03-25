USE SqlExceptionDemo
GO

	SELECT * FROM dbo.job
	SELECT * FROM dbo.job_type

-- #1 = new job where job type exists
	DECLARE @id1 INT = NULL

	EXEC dbo.UpsertJob
		@job_id			=  @id1 OUTPUT
	,	@job_name		= 'Louisville Home #1 - Plumbing'
	,	@job_type_name	= 'Plumbing'
	,	@job_date		= '2018-03-30'
	,	@job_amount		= 2500.00

	PRINT '@id1 = ' + CAST(@id1 AS VARCHAR)
	--
	SELECT * FROM dbo.job

-- #2 = update existing job
	DECLARE @id2 INT = 1

	EXEC dbo.UpsertJob
		@job_id			=  @id2 OUTPUT
	,	@job_name		= 'Louisville Home #1 - Plumbing'
	,	@job_type_name	= 'Plumbing'
	,	@job_date		= '2018-03-30'
	,	@job_amount		= 1500.00

	PRINT '@id2 = ' + CAST(@id2 AS VARCHAR)
	--
	SELECT * FROM dbo.job

-- #3 = new job where job type does not exist
--      note that severity of error is 16
	DECLARE @id3 INT = NULL

	EXEC dbo.UpsertJob
		@job_id			=  @id3 OUTPUT
	,	@job_name		= 'Louisville Home #2 - HVAC'
	,	@job_type_name	= 'HVAC'
	,	@job_date		= '2018-03-30'
	,	@job_amount		= 7500.00

	PRINT '@id3 = ' + CAST(@id3 AS VARCHAR)
	--
	SELECT * FROM dbo.job
