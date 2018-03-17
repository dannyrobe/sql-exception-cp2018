USE [SqlExceptionDemo]
GO

TRUNCATE TABLE [dbo].[job]

DELETE FROM [dbo].[job_type] WHERE job_type_id IS NOT NULL
DBCC CHECKIDENT ('SqlExceptionDemo.dbo.job_type', RESEED, 0)

INSERT INTO [dbo].[job_type] ([job_type_name] ,[job_type_description])
VALUES
	('Plumbing', 'Plumbing contractor was used'),
	('Framing', 'Framing contractor was used'),
	('Electrical', 'Electrical contractor was used')

