USE [master]
GO
/****** Object:  Database [Work]    Script Date: 13.09.2023 12:13:14 ******/
CREATE DATABASE [Work]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Work', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Work.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Work_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Work_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Work] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Work].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Work] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Work] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Work] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Work] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Work] SET ARITHABORT OFF 
GO
ALTER DATABASE [Work] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Work] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Work] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Work] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Work] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Work] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Work] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Work] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Work] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Work] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Work] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Work] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Work] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Work] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Work] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Work] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Work] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Work] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Work] SET  MULTI_USER 
GO
ALTER DATABASE [Work] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Work] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Work] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Work] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Work] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Work] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Work] SET QUERY_STORE = OFF
GO
USE [Work]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 13.09.2023 12:13:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[departmentId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[phone] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[departmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 13.09.2023 12:13:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[employeeId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[surname] [nvarchar](50) NULL,
	[phone] [nvarchar](15) NULL,
	[companyId] [int] NULL,
	[passportId] [int] NULL,
	[departmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[employeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Passport]    Script Date: 13.09.2023 12:13:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Passport](
	[passportId] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](100) NULL,
	[number] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[passportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 
GO
INSERT [dbo].[Department] ([departmentId], [name], [phone]) VALUES (1, N'IT', N'3253235')
GO
INSERT [dbo].[Department] ([departmentId], [name], [phone]) VALUES (2, N'QA', N'25236')
GO
INSERT [dbo].[Department] ([departmentId], [name], [phone]) VALUES (3, N'TEST', N'9210512')
GO
INSERT [dbo].[Department] ([departmentId], [name], [phone]) VALUES (4, N'Development', N'5352361')
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([employeeId], [name], [surname], [phone], [companyId], [passportId], [departmentId]) VALUES (2, N'Oleg', N'Igorevich', N'89182395446', 1, 2, 1)
GO
INSERT [dbo].[Employee] ([employeeId], [name], [surname], [phone], [companyId], [passportId], [departmentId]) VALUES (3, N'Darya', N'Markova', N'89284291521', 1, 3, 2)
GO
INSERT [dbo].[Employee] ([employeeId], [name], [surname], [phone], [companyId], [passportId], [departmentId]) VALUES (4, N'Dmitriy', N'Bulkovich', N'89128429145', 2, 4, 2)
GO
INSERT [dbo].[Employee] ([employeeId], [name], [surname], [phone], [companyId], [passportId], [departmentId]) VALUES (5, N'Asuka', N'Langley', N'52817590720', 0, 10, 1)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Passport] ON 
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (1, N'passport', N'235235')
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (2, N'passport', N'5335632')
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (3, N'passport', N'124245')
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (4, N'passport', N'326327')
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (5, N'passport', N'3647535')
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (6, N'passport', N'2566325')
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (7, N'passport', N'2367353')
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (8, N'passport', N'3261535')
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (10, N'passport', N'6236236')
GO
INSERT [dbo].[Passport] ([passportId], [type], [number]) VALUES (11, N'passport', N'9292952')
GO
SET IDENTITY_INSERT [dbo].[Passport] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Departme__72E12F1B3814B744]    Script Date: 13.09.2023 12:13:14 ******/
ALTER TABLE [dbo].[Department] ADD UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Employee__D09A45B7F74E68AC]    Script Date: 13.09.2023 12:13:14 ******/
ALTER TABLE [dbo].[Employee] ADD UNIQUE NONCLUSTERED 
(
	[passportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Passport__FD291E41B8C5B058]    Script Date: 13.09.2023 12:13:14 ******/
ALTER TABLE [dbo].[Passport] ADD UNIQUE NONCLUSTERED 
(
	[number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([departmentId])
REFERENCES [dbo].[Department] ([departmentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([passportId])
REFERENCES [dbo].[Passport] ([passportId])
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [Work] SET  READ_WRITE 
GO
