CREATE DATABASE Customer
GO

USE Customer
GO

CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[MonthlyIncome] [bigint] NOT NULL,
	[MonthlyExpense] [bigint] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Account](
    [AccountNo] [bigint] IDENTITY(1000000,1),	
	[Email] [varchar](50) NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] ,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


