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
  [VersionId] [int] NULL,
  [VersionName] [varchar](50) NULL,
  [Status] [int] DEFAULT(1) NOT NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED ([Id] ASC)
 ) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Version] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Version](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
  [CreatedOn] [datetime] NOT NULL,
  [PackageId] [int] NOT NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED([Id] ASC)
 ) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Package] ON
GO
INSERT [dbo].[Package] ([Id], [Name]) VALUES (1, N'Headphones')
GO
INSERT [dbo].[Package] ([Id], [Name]) VALUES (2, N'Mouse')
GO
INSERT [dbo].[Package] ([Id], [Name]) VALUES (3, N'Keyboard')
GO
INSERT [dbo].[Package] ([Id], [Name]) VALUES (4, N'Camera')
GO
INSERT [dbo].[Package] ([Id], [Name]) VALUES (5, N'Speakers')
GO
SET IDENTITY_INSERT [dbo].[Package] OFF
GO

SET IDENTITY_INSERT [dbo].[Version] ON
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (1, N'1.1', DATEADD(day, -10, GETDATE()), 1)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (2, N'1.2', DATEADD(day, -8, GETDATE()), 1)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (3, N'1.3', DATEADD(day, -5, GETDATE()), 1)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (4, N'2.0', DATEADD(day, -2, GETDATE()), 1)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (5, N'2.1', DATEADD(day, -20, GETDATE()), 2)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (6, N'2.2', DATEADD(day, -14, GETDATE()), 2)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (7, N'3.0', DATEADD(day, -8, GETDATE()), 2)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (8, N'3.1', DATEADD(day, -1, GETDATE()), 2)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (9, N'1.0', DATEADD(day, -21, GETDATE()), 3)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (10, N'1.1', DATEADD(day, -17, GETDATE()), 3)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (11, N'1.2', DATEADD(day, -12, GETDATE()), 3)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (12, N'1.3', DATEADD(day, -10, GETDATE()), 3)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (13, N'2.0', DATEADD(day, -22, GETDATE()), 4)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (14, N'3.0', DATEADD(day, -6, GETDATE()), 4)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (15, N'1.0', DATEADD(day, -15, GETDATE()), 5)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (16, N'1.1', DATEADD(day, -7, GETDATE()), 5)
GO
INSERT [dbo].[Version] ([Id], [Name], [CreatedOn], [PackageId]) VALUES (17, N'2.0', DATEADD(day, -4, GETDATE()), 5)
GO
SET IDENTITY_INSERT [dbo].[Version] OFF
GO

UPDATE [dbo].[Package]
SET [dbo].[Package].[VersionId] = 2, [dbo].[Package].[VersionName] = N'1.2'
WHERE [dbo].[Package].[Id] = 1
GO
UPDATE [dbo].[Package]
SET [dbo].[Package].[VersionId] = 7, [dbo].[Package].[VersionName] = N'3.0'
WHERE [dbo].[Package].[Id] = 2
GO
UPDATE [dbo].[Package]
SET [dbo].[Package].[VersionId] = 9, [dbo].[Package].[VersionName] = N'1.0'
WHERE [dbo].[Package].[Id] = 3
GO
UPDATE [dbo].[Package]
SET [dbo].[Package].[VersionId] = 13, [dbo].[Package].[VersionName] = N'2.0'
WHERE [dbo].[Package].[Id] = 4
GO
UPDATE [dbo].[Package]
SET [dbo].[Package].[VersionId] = 16, [dbo].[Package].[VersionName] = N'1.1'
WHERE [dbo].[Package].[Id] = 5
GO

ALTER TABLE [dbo].[Package]  WITH CHECK ADD CONSTRAINT [FK_Package_Version] FOREIGN KEY([VersionId])
REFERENCES [dbo].[Version] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Package] CHECK CONSTRAINT [FK_Package_Version]
GO

ALTER TABLE [dbo].[Version]  WITH CHECK ADD CONSTRAINT [FK_Version_Package] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([Id])
ON UPDATE NO ACTION
ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[Version] CHECK CONSTRAINT [FK_Version_Package]
GO