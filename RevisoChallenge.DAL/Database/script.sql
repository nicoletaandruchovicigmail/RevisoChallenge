USE [master]
GO
/****** Object:  Database [RevisoChallenge]    Script Date: 7/26/2017 2:57:41 AM ******/
CREATE DATABASE [RevisoChallenge]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RevisoChallenge', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\RevisoChallenge.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RevisoChallenge_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\RevisoChallenge_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RevisoChallenge].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RevisoChallenge] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RevisoChallenge] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RevisoChallenge] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RevisoChallenge] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RevisoChallenge] SET ARITHABORT OFF 
GO
ALTER DATABASE [RevisoChallenge] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RevisoChallenge] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [RevisoChallenge] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RevisoChallenge] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RevisoChallenge] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RevisoChallenge] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RevisoChallenge] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RevisoChallenge] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RevisoChallenge] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RevisoChallenge] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RevisoChallenge] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RevisoChallenge] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RevisoChallenge] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RevisoChallenge] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RevisoChallenge] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RevisoChallenge] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RevisoChallenge] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RevisoChallenge] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RevisoChallenge] SET RECOVERY FULL 
GO
ALTER DATABASE [RevisoChallenge] SET  MULTI_USER 
GO
ALTER DATABASE [RevisoChallenge] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RevisoChallenge] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RevisoChallenge] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RevisoChallenge] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RevisoChallenge', N'ON'
GO
USE [RevisoChallenge]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 7/26/2017 2:57:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [numeric](18, 0) NULL,
	[Company] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 7/26/2017 2:57:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[BillableHours] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 7/26/2017 2:57:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NULL,
	[ClientId] [int] NOT NULL,
	[CostPerHour] [smallmoney] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Task]    Script Date: 7/26/2017 2:57:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[EstimatedHours] [int] NOT NULL,
	[ActualHours] [int] NULL,
	[Start] [datetime] NULL,
	[End] [datetime] NULL,
	[ProjectId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_ToProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_ToProject]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_ToClient] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_ToClient]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_ToProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_ToProject]
GO
USE [master]
GO
ALTER DATABASE [RevisoChallenge] SET  READ_WRITE 
GO
