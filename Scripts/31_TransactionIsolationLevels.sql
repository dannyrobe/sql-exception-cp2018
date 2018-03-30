USE SqlExceptionDemo
GO

UPDATE dbo.job
SET job_name = 'Name has been changed!'
WHERE job_id = 3

