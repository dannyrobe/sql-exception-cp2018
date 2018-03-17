USE SqlExceptionDemo
GO

BEGIN TRANSACTION

INSERT INTO dbo.job
(
    job_name
  , job_type_id
  , job_date
  , job_amount
)
VALUES
(   N'I just added this job today.'
  , 3
  , '2018-03-30'
  , 50.00
)

UPDATE dbo.job
SET job_name = N'I just changed this job today.'
WHERE job_id = 1



ROLLBACK TRANSACTION
