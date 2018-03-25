USE SqlExceptionDemo
GO

-- #4 = new job with duplicate name
	SELECT * FROM dbo.job
	--
	DECLARE @id1 INT = NULL

	EXEC dbo.UpsertJobV3
		@job_id			=  @id1 OUTPUT
	,	@job_name		= 'Louisville Home #2 - HVAC'
	,	@job_type_name	= 'HVAC'
	,	@job_date		= '2018-03-30'
	,	@job_amount		= 7500.00

	PRINT '@id1 = ' + CAST(@id1 AS VARCHAR)
	--
	SELECT * FROM dbo.job

-- #5 = new job with unique name; then update name to duplicate
	SELECT * FROM dbo.job
	--
	DECLARE @id2 INT = NULL

	EXEC dbo.UpsertJobV3
		@job_id			=  @id2 OUTPUT
	,	@job_name		= 'Louisville Home #3 - Electrical'
	,	@job_type_name	= 'Electrical'
	,	@job_date		= '2018-03-31'
	,	@job_amount		= 1500.00

	PRINT '@id2 = ' + CAST(@id2 AS VARCHAR)
	--
	SELECT * FROM dbo.job
	--
	DECLARE @id3 INT = __ENTER_ID__ -- should match @id from above

	EXEC dbo.UpsertJobV3
		@job_id			=  @id3 OUTPUT
	,	@job_name		= 'Louisville Home #2 - HVAC'
	,	@job_type_name	= 'HVAC'
	,	@job_date		= '2018-03-31'
	,	@job_amount		= 1500.00

	PRINT '@id3 = ' + CAST(@id3 AS VARCHAR)
	--
	SELECT * FROM dbo.job

