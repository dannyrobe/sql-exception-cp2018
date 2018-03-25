USE SqlExceptionDemo
GO

-- #3 = new job where job type does not exist (will now create)
	SELECT * FROM dbo.job_type
	--
	DECLARE @id INT = NULL

	EXEC dbo.UpsertJobV2
		@job_id			=  @id OUTPUT
	,	@job_name		= 'Louisville Home #2 - HVAC'
	,	@job_type_name	= 'HVAC'
	,	@job_date		= '2018-03-30'
	,	@job_amount		= 7500.00

	PRINT '@id = ' + CAST(@id AS VARCHAR)
	--
	SELECT * FROM dbo.job
	SELECT * FROM dbo.job_type

-- #4 = new job with duplicate name
	SELECT * FROM dbo.job
	--
	DECLARE @id1 INT = NULL

	EXEC dbo.UpsertJobV2
		@job_id			=  @id1 OUTPUT
	,	@job_name		= 'Louisville Home #2 - HVAC'
	,	@job_type_name	= 'HVAC'
	,	@job_date		= '2018-03-30'
	,	@job_amount		= 7500.00

	PRINT '@id1 = ' + CAST(@id1 AS VARCHAR)
	--
	SELECT * FROM dbo.job
