USE SqlExceptionDemo
GO

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

BEGIN TRANSACTION

SELECT * FROM dbo.job AS j WHERE j.job_id = 3

WAITFOR DELAY '00:00:10' -- DOING WORK HERE...

SELECT * FROM dbo.job AS j WHERE j.job_id = 3

COMMIT TRANSACTION


/* reset job name

UPDATE dbo.job
SET job_name = 'Louisville Home #2 - HVAC'
WHERE job_id = 3

*/

-- see https://docs.microsoft.com/en-us/sql/t-sql/statements/set-transaction-isolation-level-transact-sql
-- for details on the other levels listed below
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
--
SET TRANSACTION ISOLATION LEVEL SNAPSHOT -- requires database to allow this level first
