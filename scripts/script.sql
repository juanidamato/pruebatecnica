USE [master]
GO
/****** Object:  Database [dbTekus]    Script Date: 12/08/2023 1:01:02 p. m. ******/
CREATE DATABASE [dbTekus]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbTekus', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\dbTekus.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbTekus_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\dbTekus_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [dbTekus] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbTekus].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbTekus] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbTekus] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbTekus] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbTekus] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbTekus] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbTekus] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbTekus] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbTekus] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbTekus] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbTekus] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbTekus] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbTekus] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbTekus] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbTekus] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbTekus] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbTekus] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbTekus] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbTekus] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbTekus] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbTekus] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbTekus] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbTekus] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbTekus] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbTekus] SET  MULTI_USER 
GO
ALTER DATABASE [dbTekus] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbTekus] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbTekus] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbTekus] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbTekus] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbTekus] SET QUERY_STORE = OFF
GO
USE [dbTekus]
GO
/****** Object:  Table [dbo].[CustomFieldProviderValue]    Script Date: 12/08/2023 1:01:02 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFieldProviderValue](
	[IdValueCustomField] [int] IDENTITY(1,1) NOT NULL,
	[IdCustomField] [nvarchar](20) NOT NULL,
	[IdProvider] [int] NOT NULL,
	[FieldValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CustomFieldProviderValue] PRIMARY KEY CLUSTERED 
(
	[IdValueCustomField] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomFields]    Script Date: 12/08/2023 1:01:02 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFields](
	[IdCustomField] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FieldType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CustomFields] PRIMARY KEY CLUSTERED 
(
	[IdCustomField] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Providers]    Script Date: 12/08/2023 1:01:02 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Providers](
	[IdProvider] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[InternalCode] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](50) NULL,
 CONSTRAINT [PK_Providers] PRIMARY KEY CLUSTERED 
(
	[IdProvider] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProviderService]    Script Date: 12/08/2023 1:01:02 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderService](
	[IdProviderService] [int] IDENTITY(1,1) NOT NULL,
	[IdProvider] [int] NOT NULL,
	[IdService] [int] NOT NULL,
	[IdGeography] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_ProviderService] PRIMARY KEY CLUSTERED 
(
	[IdProviderService] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 12/08/2023 1:01:02 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[IdService] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[HourlyPrice] [float] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[IdService] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Idx_CustomFieldProviderValue_IdProvider_IdCustomField]    Script Date: 12/08/2023 1:01:02 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [Idx_CustomFieldProviderValue_IdProvider_IdCustomField] ON [dbo].[CustomFieldProviderValue]
(
	[IdProvider] ASC,
	[IdCustomField] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Idx_ProviderService_IdProvider_IdService_IdGeography]    Script Date: 12/08/2023 1:01:02 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [Idx_ProviderService_IdProvider_IdService_IdGeography] ON [dbo].[ProviderService]
(
	[IdProvider] ASC,
	[IdService] ASC,
	[IdGeography] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomFieldProviderValue]  WITH CHECK ADD  CONSTRAINT [FK_CustomFieldProviderValue_CustomFields] FOREIGN KEY([IdCustomField])
REFERENCES [dbo].[CustomFields] ([IdCustomField])
GO
ALTER TABLE [dbo].[CustomFieldProviderValue] CHECK CONSTRAINT [FK_CustomFieldProviderValue_CustomFields]
GO
ALTER TABLE [dbo].[CustomFieldProviderValue]  WITH CHECK ADD  CONSTRAINT [FK_CustomFieldProviderValue_Providers] FOREIGN KEY([IdProvider])
REFERENCES [dbo].[Providers] ([IdProvider])
GO
ALTER TABLE [dbo].[CustomFieldProviderValue] CHECK CONSTRAINT [FK_CustomFieldProviderValue_Providers]
GO
ALTER TABLE [dbo].[ProviderService]  WITH CHECK ADD  CONSTRAINT [FK_ProviderService_Providers] FOREIGN KEY([IdProvider])
REFERENCES [dbo].[Providers] ([IdProvider])
GO
ALTER TABLE [dbo].[ProviderService] CHECK CONSTRAINT [FK_ProviderService_Providers]
GO
ALTER TABLE [dbo].[ProviderService]  WITH CHECK ADD  CONSTRAINT [FK_ProviderService_Services] FOREIGN KEY([IdService])
REFERENCES [dbo].[Services] ([IdService])
GO
ALTER TABLE [dbo].[ProviderService] CHECK CONSTRAINT [FK_ProviderService_Services]
GO
/****** Object:  StoredProcedure [dbo].[Provider_Select_ByPK]    Script Date: 12/08/2023 1:01:02 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Provider_Select_ByPK]
	@IdProvider int
AS
BEGIN
	SET NOCOUNT off;

    select * from Providers
	where IdProvider=@IdProvider
END
GO
/****** Object:  StoredProcedure [dbo].[Providers_Select_All]    Script Date: 12/08/2023 1:01:02 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan DAmato
-- Create date: 2023-12-08
-- Description:	Select all providers
-- =============================================
CREATE PROCEDURE [dbo].[Providers_Select_All]
AS
BEGIN
	
	SET NOCOUNT OFF;
	SELECT * FROM Providers
	ORDER BY [Name]
    
END
GO
/****** Object:  StoredProcedure [dbo].[Services_Select_ByProvider]    Script Date: 12/08/2023 1:01:02 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan DAmato
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Services_Select_ByProvider]
	@IdProvider int
AS
BEGIN
	SET NOCOUNT off;

	select @IdProvider, serv.*,prsr.IdGeography from 
	Services serv inner join ProviderService prsr
	on serv.IdService=prsr.IdService
	where prsr.IdProvider=@IdProvider
	order by serv.Name
    
END
GO
USE [master]
GO
ALTER DATABASE [dbTekus] SET  READ_WRITE 
GO
