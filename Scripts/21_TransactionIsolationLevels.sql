USE SqlExceptionDemo
GO


SET TRANSACTION ISOLATION LEVEL READ COMMITTED -- (default) waits on transactions

SELECT * FROM dbo.job AS j

--
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- possible dirty reads

SELECT * FROM dbo.job AS j



-- see https://docs.microsoft.com/en-us/sql/t-sql/statements/set-transaction-isolation-level-transact-sql
-- for details on the other levels listed below
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
--
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
--
SET TRANSACTION ISOLATION LEVEL SNAPSHOT -- requires databse to allow this level first
