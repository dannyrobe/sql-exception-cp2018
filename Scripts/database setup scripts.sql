USE [SqlExceptionDemo]
GO

/****** Object:  Table [dbo].[job]    Script Date: 3/15/2018 1:26:58 PM ******/
DROP TABLE [dbo].[job]
GO

/****** Object:  Table [dbo].[job]    Script Date: 3/15/2018 1:26:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[job](
	[job_id] [INT] IDENTITY(1,1) NOT NULL,
	[job_name] [NVARCHAR](50) NOT NULL,
	[job_type_id] [INT] NOT NULL,
	[job_date] [DATE] NULL,
	[job_amount] [MONEY] NULL,
 CONSTRAINT [PK_job] PRIMARY KEY CLUSTERED 
(
	[job_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[job_type]    Script Date: 3/15/2018 1:27:10 PM ******/
DROP TABLE [dbo].[job_type]
GO

/****** Object:  Table [dbo].[job_type]    Script Date: 3/15/2018 1:27:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[job_type](
	[job_type_id] [int] IDENTITY(1,1) NOT NULL,
	[job_type_name] [nvarchar](50) NOT NULL,
	[job_type_description] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_job_type] PRIMARY KEY CLUSTERED 
(
	[job_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



ALTER TABLE [dbo].[job]  WITH CHECK ADD  CONSTRAINT [FK_job_job_type] FOREIGN KEY([job_type_id])
REFERENCES [dbo].[job_type] ([job_type_id])
GO

ALTER TABLE [dbo].[job] CHECK CONSTRAINT [FK_job_job_type]
GO



