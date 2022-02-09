USE [master]
GO
/****** Object:  Database [Call_center]    Script Date: 18.01.2022 12:44:58 ******/
CREATE DATABASE [Call_center]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Call_center', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SERVER\MSSQL\DATA\Call_center.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Call_center_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SERVER\MSSQL\DATA\Call_center_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Call_center] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Call_center].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Call_center] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Call_center] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Call_center] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Call_center] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Call_center] SET ARITHABORT OFF 
GO
ALTER DATABASE [Call_center] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Call_center] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Call_center] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Call_center] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Call_center] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Call_center] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Call_center] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Call_center] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Call_center] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Call_center] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Call_center] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Call_center] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Call_center] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Call_center] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Call_center] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Call_center] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Call_center] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Call_center] SET RECOVERY FULL 
GO
ALTER DATABASE [Call_center] SET  MULTI_USER 
GO
ALTER DATABASE [Call_center] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Call_center] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Call_center] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Call_center] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Call_center] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Call_center] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Call_center', N'ON'
GO
ALTER DATABASE [Call_center] SET QUERY_STORE = OFF
GO
USE [Call_center]
GO
/****** Object:  User [operator]    Script Date: 18.01.2022 12:44:59 ******/
CREATE USER [operator] FOR LOGIN [user_operator] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [manager]    Script Date: 18.01.2022 12:44:59 ******/
CREATE USER [manager] FOR LOGIN [admin_manager] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [call_center_operator]    Script Date: 18.01.2022 12:44:59 ******/
CREATE ROLE [call_center_operator]
GO
ALTER ROLE [call_center_operator] ADD MEMBER [operator]
GO
ALTER ROLE [db_owner] ADD MEMBER [manager]
GO
/****** Object:  UserDefinedTableType [dbo].[WorkloadTable]    Script Date: 18.01.2022 12:44:59 ******/
CREATE TYPE [dbo].[WorkloadTable] AS TABLE(
	[id] [int] NULL,
	[Direction] [nvarchar](50) NULL,
	[Max_WorkLoad] [int] NULL
)
GO
/****** Object:  Table [dbo].[Authorization]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authorization](
	[User_id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_id] [int] NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Authorization] PRIMARY KEY CLUSTERED 
(
	[User_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidate](
	[Candidate_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NULL,
	[E_mail] [nvarchar](50) NOT NULL,
	[Phone_number] [nvarchar](12) NOT NULL,
	[Education] [nvarchar](20) NULL,
	[Expiriense_years] [int] NOT NULL,
 CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED 
(
	[Candidate_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Candidat__31660442B77D070C] UNIQUE NONCLUSTERED 
(
	[E_mail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Candidat__7E87EC6745CD48B3] UNIQUE NONCLUSTERED 
(
	[Phone_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Department_id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_count] [int] NOT NULL,
	[Direction] [nvarchar](50) NOT NULL,
	[basic_money_per_hour] [float] NOT NULL,
	[total_money_per_hour] [float] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Department_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department_work_load]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department_work_load](
	[Schedule_id] [int] IDENTITY(1,1) NOT NULL,
	[Department_id] [int] NOT NULL,
	[Period_id] [int] NOT NULL,
	[Work_load] [float] NOT NULL,
	[Worked_hours] [float] NULL,
	[isEqualOrMore] [bit] NULL,
 CONSTRAINT [PK_Department_work_load] PRIMARY KEY CLUSTERED 
(
	[Schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dismissal]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dismissal](
	[Dismissal_id] [int] NOT NULL,
	[Employee_id] [int] NOT NULL,
	[Document_date] [date] NOT NULL,
	[Dismissal_date] [date] NOT NULL,
	[Dismissal_reason] [nvarchar](50) NOT NULL,
	[Payments] [float] NOT NULL,
 CONSTRAINT [PK_Dismissal] PRIMARY KEY CLUSTERED 
(
	[Dismissal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[Document_id] [int] NOT NULL,
	[Candidate_id] [int] NOT NULL,
	[Document_type] [nvarchar](50) NOT NULL,
	[Document_url] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Document_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Employee_id] [int] IDENTITY(1,1) NOT NULL,
	[Department_id] [int] NOT NULL,
	[Passport_serial] [int] NOT NULL,
	[Passport_number] [int] NOT NULL,
	[Interview_id] [int] NOT NULL,
	[E_mail] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee_work_load]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_work_load](
	[Addendum_id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_id] [int] NOT NULL,
	[Period_id] [int] NOT NULL,
	[Work_load_hours] [float] NOT NULL,
	[Worked_hours] [float] NULL,
	[result_salary] [float] NULL,
 CONSTRAINT [PK_Employee_work_load] PRIMARY KEY CLUSTERED 
(
	[Addendum_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interview]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interview](
	[Interview_id] [int] IDENTITY(1,1) NOT NULL,
	[Candidate_id] [int] NOT NULL,
	[IsPassed] [bit] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_Interview] PRIMARY KEY CLUSTERED 
(
	[Interview_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Period]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Period](
	[Period_id] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Total_work_load_hours] [int] NOT NULL,
 CONSTRAINT [PK_Period] PRIMARY KEY CLUSTERED 
(
	[Period_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personal_achievements]    Script Date: 18.01.2022 12:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personal_achievements](
	[Achievement_id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_id] [int] NOT NULL,
	[Period_id] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Reward] [float] NOT NULL,
 CONSTRAINT [PK_Personal_achievements] PRIMARY KEY CLUSTERED 
(
	[Achievement_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Authorization]  WITH CHECK ADD  CONSTRAINT [FK_Authorization_Employee] FOREIGN KEY([Employee_id])
REFERENCES [dbo].[Employee] ([Employee_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Authorization] CHECK CONSTRAINT [FK_Authorization_Employee]
GO
ALTER TABLE [dbo].[Department_work_load]  WITH CHECK ADD  CONSTRAINT [FK_Department_work_load_Department] FOREIGN KEY([Department_id])
REFERENCES [dbo].[Department] ([Department_id])
GO
ALTER TABLE [dbo].[Department_work_load] CHECK CONSTRAINT [FK_Department_work_load_Department]
GO
ALTER TABLE [dbo].[Department_work_load]  WITH CHECK ADD  CONSTRAINT [FK_Department_work_load_Period] FOREIGN KEY([Period_id])
REFERENCES [dbo].[Period] ([Period_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Department_work_load] CHECK CONSTRAINT [FK_Department_work_load_Period]
GO
ALTER TABLE [dbo].[Dismissal]  WITH CHECK ADD  CONSTRAINT [FK_Dismissal_Employee] FOREIGN KEY([Employee_id])
REFERENCES [dbo].[Employee] ([Employee_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Dismissal] CHECK CONSTRAINT [FK_Dismissal_Employee]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_Candidate] FOREIGN KEY([Candidate_id])
REFERENCES [dbo].[Candidate] ([Candidate_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_Candidate]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([Department_id])
REFERENCES [dbo].[Department] ([Department_id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Interview] FOREIGN KEY([Interview_id])
REFERENCES [dbo].[Interview] ([Interview_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Interview]
GO
ALTER TABLE [dbo].[Employee_work_load]  WITH CHECK ADD  CONSTRAINT [FK_Employee_work_load_Employee] FOREIGN KEY([Employee_id])
REFERENCES [dbo].[Employee] ([Employee_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee_work_load] CHECK CONSTRAINT [FK_Employee_work_load_Employee]
GO
ALTER TABLE [dbo].[Employee_work_load]  WITH CHECK ADD  CONSTRAINT [FK_Employee_work_load_Period] FOREIGN KEY([Period_id])
REFERENCES [dbo].[Period] ([Period_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee_work_load] CHECK CONSTRAINT [FK_Employee_work_load_Period]
GO
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_Candidate] FOREIGN KEY([Candidate_id])
REFERENCES [dbo].[Candidate] ([Candidate_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_Candidate]
GO
ALTER TABLE [dbo].[Personal_achievements]  WITH CHECK ADD  CONSTRAINT [FK_Personal_achievements_Employee] FOREIGN KEY([Employee_id])
REFERENCES [dbo].[Employee] ([Employee_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Personal_achievements] CHECK CONSTRAINT [FK_Personal_achievements_Employee]
GO
ALTER TABLE [dbo].[Personal_achievements]  WITH CHECK ADD  CONSTRAINT [FK_Personal_achievements_Period] FOREIGN KEY([Period_id])
REFERENCES [dbo].[Period] ([Period_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Personal_achievements] CHECK CONSTRAINT [FK_Personal_achievements_Period]
GO
USE [master]
GO
ALTER DATABASE [Call_center] SET  READ_WRITE 
GO
