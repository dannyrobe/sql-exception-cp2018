USE SqlExceptionDemo
GO


SET TRANSACTION ISOLATION LEVEL READ COMMITTED -- (default) 

SELECT * FROM dbo.job AS j
WHERE j.job_id = 3

SELECT * FROM dbo.job AS j
-- NO WHERE CLAUSE (waits on transactions)
--
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- possible dirty reads

SELECT * FROM dbo.job AS j

