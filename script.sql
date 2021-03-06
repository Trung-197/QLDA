USE [master]
GO
/****** Object:  Database [QLGT]    Script Date: 27/06/2021 11:40:01 SA ******/
CREATE DATABASE [QLGT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLGT', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QLGT.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLGT_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QLGT_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLGT] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLGT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLGT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLGT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLGT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLGT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLGT] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLGT] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLGT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLGT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLGT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLGT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLGT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLGT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLGT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLGT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLGT] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLGT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLGT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLGT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLGT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLGT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLGT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLGT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLGT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLGT] SET  MULTI_USER 
GO
ALTER DATABASE [QLGT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLGT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLGT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLGT] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QLGT] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QLGT]
GO
/****** Object:  Table [dbo].[ChiTietHopDong]    Script Date: 27/06/2021 11:40:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHopDong](
	[MaGT] [nvarchar](50) NOT NULL,
	[TenGT] [nvarchar](50) NULL,
	[Luongkhoidiem] [nvarchar](50) NULL,
	[Daingo] [nvarchar](50) NULL,
	[Ngayky] [date] NULL,
	[Ngayhethan] [date] NULL,
	[Trangthai] [nvarchar](50) NULL,
	[Mateam] [nvarchar](50) NULL,
 CONSTRAINT [PK_ChiTietHopDong] PRIMARY KEY CLUSTERED 
(
	[MaGT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HopDong]    Script Date: 27/06/2021 11:40:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HopDong](
	[Mahopdong] [nvarchar](50) NOT NULL,
	[Tenhopdong] [nvarchar](50) NULL,
	[Trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_HopDong] PRIMARY KEY CLUSTERED 
(
	[Mahopdong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HuanLuyenVien]    Script Date: 27/06/2021 11:40:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HuanLuyenVien](
	[MaHLV] [nchar](10) NOT NULL,
	[TenHLV] [nchar](10) NULL,
	[Gioitinh] [nchar](10) NULL,
	[Ngaysinh] [nchar](10) NULL,
	[Quequan] [nchar](10) NULL,
	[CMND] [nchar](10) NULL,
	[Chucvu] [nchar](10) NULL,
	[Ngaygianhap] [nchar](10) NULL,
	[Trangthai] [nchar](10) NULL,
 CONSTRAINT [PK_HuanLuyenVien] PRIMARY KEY CLUSTERED 
(
	[MaHLV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Luong]    Script Date: 27/06/2021 11:40:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Luong](
	[Maluong] [nvarchar](50) NOT NULL,
	[LCB] [int] NULL,
	[Ngaynhap] [date] NULL,
	[LCBmoi] [int] NULL,
	[Ghichu] [nvarchar](50) NULL,
 CONSTRAINT [PK_Luong] PRIMARY KEY CLUSTERED 
(
	[Maluong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NguoiDung]    Script Date: 27/06/2021 11:40:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiDung](
	[Taikhoan] [nvarchar](50) NULL,
	[Matkhau] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuanLyTeam]    Script Date: 27/06/2021 11:40:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuanLyTeam](
	[Maquanly] [nvarchar](50) NOT NULL,
	[Tenquanly] [nvarchar](50) NULL,
	[Ngaysinh] [date] NULL,
	[Gioitinh] [nvarchar](50) NULL,
	[CMND] [int] NULL,
	[Tenteam] [nvarchar](50) NULL,
 CONSTRAINT [PK_QuanLyTeam] PRIMARY KEY CLUSTERED 
(
	[Maquanly] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Team]    Script Date: 27/06/2021 11:40:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[Mateam] [nvarchar](50) NOT NULL,
	[Tenteam] [nvarchar](50) NULL,
	[Ngaythanhlap] [date] NULL,
	[Sothanhvien] [nvarchar](50) NULL,
	[Ghichu] [nvarchar](50) NULL,
	[Nhataitro] [nvarchar](50) NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[Mateam] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinGameThu]    Script Date: 27/06/2021 11:40:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinGameThu](
	[MaGT] [nvarchar](50) NOT NULL,
	[Hoten] [nvarchar](50) NULL,
	[Quequan] [nvarchar](50) NULL,
	[Ngaysinh] [date] NULL,
	[Gioitinh] [nvarchar](50) NULL,
	[Dantoc] [nvarchar](50) NULL,
	[SDT] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Tongiao] [nvarchar](50) NULL,
	[Quoctich] [nvarchar](50) NULL,
	[CMND] [int] NULL,
	[Anhhoso] [image] NULL,
	[Ngaythamgia] [date] NULL,
	[Sotruong] [nvarchar](50) NULL,
	[Trangthai] [nvarchar](50) NULL,
 CONSTRAINT [PK_ThongTinGameThu] PRIMARY KEY CLUSTERED 
(
	[MaGT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Thuong]    Script Date: 27/06/2021 11:40:01 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Thuong](
	[MaGT] [nvarchar](50) NULL,
	[Hoten] [nvarchar](50) NULL,
	[Gioitinh] [nvarchar](50) NULL,
	[Team] [nvarchar](50) NULL,
	[Maluong] [nvarchar](50) NULL,
	[Daingo] [nvarchar](50) NULL,
	[Lydo] [nvarchar](50) NULL
) ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [QLGT] SET  READ_WRITE 
GO
