USE [master]
GO

IF DB_ID('FDS') IS NOT NULL
  set noexec on               -- prevent creation when already exists

/****** Object:  Database [FDS] ******/
CREATE DATABASE [FDS];
GO

USE [FDS]
GO

/****** Object:  Table [dbo].[Package] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
  [CurrentVersion] [varchar](50) NOT NULL,
  [LatestVersion] [varchar](50) NOT NULL,
  [Score] [int] NULL,
  [Url] [varchar](200) NOT NULL,
  [Status] [int] DEFAULT(1) NOT NULL,
  [Description] [varchar](1000) NULL,
  [UpdatedOn] [datetime] NULL
 ) ON [PRIMARY]
GO
