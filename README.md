# EarnaClick
Mojoportal VB plugin enabling click swapping for web traffic generation

You will need the following SQL tables in the MSSQL db:
USE [earnaclickC]
GO

/****** Object:  Table [dbo].[links]    Script Date: 07/26/2015 12:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[links](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [nvarchar](50) NULL,
	[link] [nvarchar](255) NULL,
	[dateEntered] [datetime2](7) NOT NULL,
	[usertype] [nvarchar](50) NULL
) ON [PRIMARY]

GO

USE [earnaclickC]
GO

/****** Object:  Table [dbo].[clickhistory]    Script Date: 07/26/2015 12:38:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[clickhistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[clickdatetime] [datetime2](7) NOT NULL,
	[clickIP] [nvarchar](12) NULL,
	[clicklocation] [nvarchar](50) NULL,
	[linkid] [bigint] NULL
) ON [PRIMARY]

GO

USE [earnaclickC]
GO

/****** Object:  Table [dbo].[receivervalidation]    Script Date: 07/26/2015 12:40:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[receivervalidation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[clickdatetime] [datetime2](7) NOT NULL,
	[userid] [int] NOT NULL,
	[code] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO


USE [earnaclickC]
GO

/****** Object:  Table [dbo].[userinformation]    Script Date: 07/26/2015 12:41:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[userinformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[paymentdatetime] [datetime2](7) NULL,
	[userid] [int] NOT NULL,
	[usertype] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO


