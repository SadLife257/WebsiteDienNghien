USE [master]
GO
/****** Object:  Database [QuanLyTiemDien]    Script Date: 05/13/23 5:33:01 PM ******/
CREATE DATABASE [QuanLyTiemDien]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyTiemDien', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuanLyTiemDien.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyTiemDien_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuanLyTiemDien_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QuanLyTiemDien] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyTiemDien].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyTiemDien] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyTiemDien] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyTiemDien] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyTiemDien] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyTiemDien] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyTiemDien] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyTiemDien] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyTiemDien] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyTiemDien] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyTiemDien] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyTiemDien] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyTiemDien] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyTiemDien', N'ON'
GO
ALTER DATABASE [QuanLyTiemDien] SET QUERY_STORE = OFF
GO
USE [QuanLyTiemDien]
GO
/****** Object:  Table [dbo].[account]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](64) NOT NULL,
	[firstname] [nvarchar](64) NOT NULL,
	[lassname] [nvarchar](64) NOT NULL,
	[email] [varchar](254) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[address] [nvarchar](max) NULL,
	[phonenumber] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[account_role]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account_role](
	[account_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[account_id] ASC,
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[datebegin] [smalldatetime] NOT NULL,
	[total] [float] NOT NULL,
	[isOrder] [bit] NOT NULL,
	[accountid] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart_product]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart_product](
	[cartid] [int] NOT NULL,
	[productid] [int] NOT NULL,
	[quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[cartid] ASC,
	[productid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[link] [nvarchar](max) NOT NULL,
	[meta] [nvarchar](50) NOT NULL,
	[hide] [bit] NOT NULL,
	[order] [int] NOT NULL,
	[datebegin] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[feedback]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[feedback](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subject] [nvarchar](50) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[email] [nvarchar](64) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[datebegin] [smalldatetime] NOT NULL,
	[read] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[menu]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[menu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[link] [nvarchar](max) NOT NULL,
	[meta] [nvarchar](50) NOT NULL,
	[hide] [bit] NOT NULL,
	[order] [int] NOT NULL,
	[datebegin] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[news]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[news](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[img] [nvarchar](30) NOT NULL,
	[hide] [bit] NOT NULL,
	[order] [int] NOT NULL,
	[datebegin] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[price] [float] NOT NULL,
	[img] [nvarchar](50) NOT NULL,
	[detail] [ntext] NOT NULL,
	[size] [nvarchar](10) NOT NULL,
	[color] [nvarchar](30) NOT NULL,
	[meta] [nvarchar](50) NOT NULL,
	[hide] [bit] NOT NULL,
	[order] [int] NOT NULL,
	[datebegin] [smalldatetime] NOT NULL,
	[categoryid] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 05/13/23 5:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cart_product] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[account_role]  WITH CHECK ADD FOREIGN KEY([account_id])
REFERENCES [dbo].[account] ([id])
GO
ALTER TABLE [dbo].[account_role]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([accountid])
REFERENCES [dbo].[account] ([id])
GO
ALTER TABLE [dbo].[cart_product]  WITH CHECK ADD FOREIGN KEY([cartid])
REFERENCES [dbo].[cart] ([id])
GO
ALTER TABLE [dbo].[cart_product]  WITH CHECK ADD FOREIGN KEY([productid])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD FOREIGN KEY([categoryid])
REFERENCES [dbo].[category] ([id])
GO
USE [master]
GO
ALTER DATABASE [QuanLyTiemDien] SET  READ_WRITE 
GO
