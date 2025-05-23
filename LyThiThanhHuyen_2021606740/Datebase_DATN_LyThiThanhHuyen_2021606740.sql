USE [DATN]
GO
/****** Object:  Table [dbo].[Anh]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Anh](
	[MaAnh] [int] IDENTITY(1,1) NOT NULL,
	[TenAnh] [nvarchar](255) NOT NULL,
	[MoTa] [nvarchar](500) NULL,
	[NgayCapNhat] [datetime] NULL,
	[URL] [varchar](500) NOT NULL,
 CONSTRAINT [PK__Anh__356240DF294190D3] PRIMARY KEY CLUSTERED 
(
	[MaAnh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnhSanPham]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnhSanPham](
	[MaAnhSP] [int] IDENTITY(1,1) NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[MaAnh] [int] NOT NULL,
	[NgayCapNhat] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaAnhSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDonHang]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHang](
	[MaChiTietDonHang] [int] IDENTITY(1,1) NOT NULL,
	[MaDonHang] [int] NOT NULL,
	[MaChiTietSP] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[TongTien] [decimal](18, 0) NOT NULL,
	[Gia] [decimal](18, 0) NULL,
 CONSTRAINT [PK__ChiTietD__4B0B45DDDC6AFAA8] PRIMARY KEY CLUSTERED 
(
	[MaChiTietDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietGioHang]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietGioHang](
	[MaChiTietGio] [int] IDENTITY(1,1) NOT NULL,
	[MaGioHang] [int] NOT NULL,
	[MaChiTietSanPham] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[TongTien] [decimal](18, 0) NOT NULL,
	[NgayCapNhat] [date] NULL,
 CONSTRAINT [PK__ChiTietG__D5D9A595B3BD43BB] PRIMARY KEY CLUSTERED 
(
	[MaChiTietGio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietSanPham]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietSanPham](
	[MaChiTietSP] [int] IDENTITY(1,1) NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[MaKichThuoc] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[Gia] [decimal](18, 0) NOT NULL,
	[NgayCapNhat] [date] NULL,
	[TrangThai] [bit] NOT NULL,
	[GiaGiam] [decimal](18, 0) NULL,
 CONSTRAINT [PK__ChiTietS__651D905734456605] PRIMARY KEY CLUSTERED 
(
	[MaChiTietSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhGia]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhGia](
	[MaDanhGia] [int] IDENTITY(1,1) NOT NULL,
	[MaTaiKhoan] [int] NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[SoDiem] [int] NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[NgayCapNhat] [datetime] NULL,
 CONSTRAINT [PK__DanhGia__AA9515BF6B50B265] PRIMARY KEY CLUSTERED 
(
	[MaDanhGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiaChi]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiaChi](
	[MaDiaChi] [int] IDENTITY(1,1) NOT NULL,
	[MaTaiKhoan] [int] NOT NULL,
	[ThanhPho] [nvarchar](100) NOT NULL,
	[Huyen] [nvarchar](100) NOT NULL,
	[Xa] [nvarchar](100) NOT NULL,
	[ChiTiet] [nvarchar](255) NOT NULL,
	[HoTenNguoiNhan] [nvarchar](255) NOT NULL,
	[SoDienThoaiNguoiNhan] [varchar](15) NOT NULL,
	[NgayCapNhat] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDiaChi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[MaDonHang] [int] IDENTITY(1,1) NOT NULL,
	[MaTaiKhoan] [int] NOT NULL,
	[NgayTao] [datetime] NULL,
	[NgayCapNhat] [datetime] NULL,
	[TrangThaiVanChuyen] [nvarchar](50) NOT NULL,
	[MMaGiamGia] [int] NULL,
	[TongTien] [decimal](18, 0) NOT NULL,
	[MaPhuongThucThanhToan] [int] NOT NULL,
	[TrangThaiThanhToan] [bit] NOT NULL,
	[MaDiaChi] [int] NOT NULL,
 CONSTRAINT [PK__DonHang__129584ADE05FC1FA] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GioHang]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioHang](
	[MaGioHang] [int] IDENTITY(1,1) NOT NULL,
	[MaTaiKhoan] [int] NOT NULL,
	[NgayCapNhat] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaGioHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KichThuoc]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KichThuoc](
	[MaKichThuoc] [int] IDENTITY(1,1) NOT NULL,
	[TenKichThuoc] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](255) NULL,
	[TrangThai] [bit] NOT NULL,
	[NgayCapNhat] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKichThuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaGiamGia]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaGiamGia](
	[MMaGiamGia] [int] IDENTITY(1,1) NOT NULL,
	[MaHienThi] [varchar](50) NOT NULL,
	[NgayBatDau] [date] NOT NULL,
	[NgayKetThuc] [date] NOT NULL,
	[LoaiGiamGia] [nvarchar](50) NOT NULL,
	[GiaTri] [float] NOT NULL,
	[TrangThai] [bit] NOT NULL,
	[NgayCapNhat] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[MMaGiamGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhomHuong]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhomHuong](
	[MaNhomHuong] [int] IDENTITY(1,1) NOT NULL,
	[TenNhomHuong] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](255) NULL,
	[NgayCapNhat] [date] NULL,
	[TrangThai] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhomHuong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhuongThucThanhToan]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhuongThucThanhToan](
	[MaPhuongThuc] [int] IDENTITY(1,1) NOT NULL,
	[TenPhuongThuc] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](255) NULL,
	[NgayCapNhat] [date] NULL,
	[TrangThai] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhuongThuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSanPham] [int] IDENTITY(1,1) NOT NULL,
	[MaThuongHieu] [int] NOT NULL,
	[MaNhomHuong] [int] NOT NULL,
	[TenSanPham] [nvarchar](255) NOT NULL,
	[GioiTinh] [nvarchar](50) NOT NULL,
	[TrangThai] [bit] NULL,
	[MoTa] [nvarchar](1000) NULL,
	[NgayCapNhat] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[MaTaiKhoan] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](200) NOT NULL,
	[MatKhau] [varchar](200) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[SoDienThoai] [varchar](10) NOT NULL,
	[LoaiTaiKhoan] [int] NOT NULL,
	[TrangThai] [bit] NULL,
	[NgayCapNhat] [date] NULL,
 CONSTRAINT [PK__TaiKhoan__AD7C65299D0EE69E] PRIMARY KEY CLUSTERED 
(
	[MaTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThanhToan]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThanhToan](
	[MaThanhToan] [int] IDENTITY(1,1) NOT NULL,
	[MaTaiKhoan] [int] NULL,
	[MaDonHang] [int] NULL,
	[SoTienGiaoDich] [decimal](18, 0) NOT NULL,
	[ThoiGian] [datetime] NULL,
	[NoiDungChuyenKhoan] [nvarchar](100) NULL,
	[STKGui] [nchar](50) NULL,
 CONSTRAINT [PK__ThanhToa__D4B258440C6A38B0] PRIMARY KEY CLUSTERED 
(
	[MaThanhToan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThuongHieu]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThuongHieu](
	[MaThuongHieu] [int] IDENTITY(1,1) NOT NULL,
	[TenThuongHieu] [nvarchar](100) NOT NULL,
	[QuocGia] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](255) NULL,
	[NgayCapNhat] [date] NULL,
	[TrangThai] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaThuongHieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YeuThich]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YeuThich](
	[MaYeuThich] [int] IDENTITY(1,1) NOT NULL,
	[MaTaiKhoan] [int] NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[NgayCapNhat] [datetime] NULL,
 CONSTRAINT [PK__YeuThich__B9007E4C2A21E46D] PRIMARY KEY CLUSTERED 
(
	[MaYeuThich] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Anh] ON 

INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (91, N'SP_74_.jpg', NULL, CAST(N'2025-04-16T00:00:00.000' AS DateTime), N'/images/Products/SP_74_.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (92, N'SP_75_9d0a10bf-5ef8-4a95-8417-6da2932f5348.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_75_9d0a10bf-5ef8-4a95-8417-6da2932f5348.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (93, N'SP_75_4cd6a7f1-4704-4a76-8613-65bef3d51d05.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_75_4cd6a7f1-4704-4a76-8613-65bef3d51d05.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (96, N'SP_73_.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_73_.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (97, N'SP_70_.jpgSystem.Func`1[System.Guid]', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_70_.jpgSystem.Func`1[System.Guid]')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (101, N'SP_70_44cefc5b-991e-4dea-b50c-c601316a5b21.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_70_44cefc5b-991e-4dea-b50c-c601316a5b21.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (102, N'SP_70_71140b5e-63f2-4230-80b9-1ad33cf463f8.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_70_71140b5e-63f2-4230-80b9-1ad33cf463f8.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (103, N'SP_77_6cbec82d-e2e5-4bb2-a045-c116daa95ecb.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_77_6cbec82d-e2e5-4bb2-a045-c116daa95ecb.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (104, N'SP_78_8ade0243-9ce2-4a41-b0a8-5d7c63b3bb28.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_78_8ade0243-9ce2-4a41-b0a8-5d7c63b3bb28.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (105, N'SP_71_189eeaf8-68a0-41fe-a817-2ce9d7436ace.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_71_189eeaf8-68a0-41fe-a817-2ce9d7436ace.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (106, N'SP_76_c97205b1-a330-435d-98d8-b6e1b935aa70.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_76_c97205b1-a330-435d-98d8-b6e1b935aa70.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (107, N'SP_72_244a2232-cfe7-4c65-b073-951c3004ac6e.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_72_244a2232-cfe7-4c65-b073-951c3004ac6e.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (108, N'SP_79_8ed6fc28-e605-4d07-8c5f-c968b31ed2ce.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_79_8ed6fc28-e605-4d07-8c5f-c968b31ed2ce.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (109, N'SP_69_95f0d06f-f042-4009-becf-15e68a101d31.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_69_95f0d06f-f042-4009-becf-15e68a101d31.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (110, N'SP_80_cce63586-ecc2-44b7-b1ab-984ac9af62a0.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_80_cce63586-ecc2-44b7-b1ab-984ac9af62a0.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (111, N'SP_81_babfe5d3-f007-425a-ba32-cfa1784d350e.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_81_babfe5d3-f007-425a-ba32-cfa1784d350e.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (112, N'SP_82_2d864073-9c3a-4657-8180-1c783432c7d3.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_82_2d864073-9c3a-4657-8180-1c783432c7d3.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (113, N'SP_83_224a0812-a1a8-489f-8315-ecd6c0e22f3b.JPG', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_83_224a0812-a1a8-489f-8315-ecd6c0e22f3b.JPG')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (114, N'SP_84_b0a982ee-a2d0-4272-ae2b-50913f41af61.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_84_b0a982ee-a2d0-4272-ae2b-50913f41af61.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (115, N'SP_85_035b6e99-7b84-4116-b287-46066fdf48d2.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_85_035b6e99-7b84-4116-b287-46066fdf48d2.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (116, N'SP_86_4d151de5-d09b-47a9-95ce-343e88a4e934.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_86_4d151de5-d09b-47a9-95ce-343e88a4e934.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (117, N'SP_87_dfdc9367-5fb3-4161-a33f-56a459c50da2.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_87_dfdc9367-5fb3-4161-a33f-56a459c50da2.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (118, N'SP_88_c11d42f2-c4af-416e-94ef-7018d3a4eb45.jpg', NULL, CAST(N'2025-04-17T00:00:00.000' AS DateTime), N'/images/Products/SP_88_c11d42f2-c4af-416e-94ef-7018d3a4eb45.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (122, N'SP_82_9d8d9c64-9849-43c3-a9f5-594774b67f46.jpg', NULL, CAST(N'2025-04-30T00:00:00.000' AS DateTime), N'/images/Products/SP_82_9d8d9c64-9849-43c3-a9f5-594774b67f46.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (123, N'SP_89_351f1436-770a-4385-aebc-cc36f5871e87.jpg', NULL, CAST(N'2025-04-30T00:00:00.000' AS DateTime), N'/images/Products/SP_89_351f1436-770a-4385-aebc-cc36f5871e87.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (124, N'SP_89_385665c4-e874-4a5a-aabd-8cddb09ff26a.jpg', NULL, CAST(N'2025-04-30T00:00:00.000' AS DateTime), N'/images/Products/SP_89_385665c4-e874-4a5a-aabd-8cddb09ff26a.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (125, N'SP_72_610209f1-30b0-4634-9421-69e2f3af8c3f.jpg', NULL, CAST(N'2025-05-02T00:00:00.000' AS DateTime), N'/images/Products/SP_72_610209f1-30b0-4634-9421-69e2f3af8c3f.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (126, N'SP_72_535458d0-2b41-4590-a7d1-52518739cb8b.jpg', NULL, CAST(N'2025-05-02T00:00:00.000' AS DateTime), N'/images/Products/SP_72_535458d0-2b41-4590-a7d1-52518739cb8b.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (127, N'SP_72_33bbd132-81fe-4ae5-b67c-572b7b9dc631.jpg', NULL, CAST(N'2025-05-02T00:00:00.000' AS DateTime), N'/images/Products/SP_72_33bbd132-81fe-4ae5-b67c-572b7b9dc631.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (128, N'SP_72_7b92e151-abaa-4b61-ae6a-021e2c174e40.jpg', NULL, CAST(N'2025-05-02T00:00:00.000' AS DateTime), N'/images/Products/SP_72_7b92e151-abaa-4b61-ae6a-021e2c174e40.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (129, N'SP_90_0414507c-ea26-492c-98d1-8501e1107f38.jpg', NULL, CAST(N'2025-05-09T21:41:46.410' AS DateTime), N'/images/Products/SP_90_0414507c-ea26-492c-98d1-8501e1107f38.jpg')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (130, N'SP_90_1ff4aa89-c52e-47e6-948e-33f199aa68bf.png', NULL, CAST(N'2025-05-09T21:41:46.470' AS DateTime), N'/images/Products/SP_90_1ff4aa89-c52e-47e6-948e-33f199aa68bf.png')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (131, N'SP_90_8799cb15-cfcf-43e4-8621-2d327798dac3.png', NULL, CAST(N'2025-05-09T21:41:46.483' AS DateTime), N'/images/Products/SP_90_8799cb15-cfcf-43e4-8621-2d327798dac3.png')
INSERT [dbo].[Anh] ([MaAnh], [TenAnh], [MoTa], [NgayCapNhat], [URL]) VALUES (132, N'SP_90_05b55958-9b55-475f-9684-2459e7c393b6.jpg', NULL, CAST(N'2025-05-15T08:42:38.010' AS DateTime), N'/images/Products/SP_90_05b55958-9b55-475f-9684-2459e7c393b6.jpg')
SET IDENTITY_INSERT [dbo].[Anh] OFF
GO
SET IDENTITY_INSERT [dbo].[AnhSanPham] ON 

INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (91, 74, 91, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (92, 75, 92, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (93, 75, 93, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (96, 73, 96, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (97, 70, 101, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (98, 70, 102, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (99, 77, 103, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (100, 78, 104, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (101, 71, 105, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (102, 76, 106, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (103, 72, 107, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (104, 79, 108, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (105, 69, 109, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (106, 80, 110, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (107, 81, 111, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (108, 82, 112, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (109, 83, 113, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (110, 84, 114, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (111, 85, 115, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (112, 86, 116, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (113, 87, 117, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (114, 88, 118, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (118, 82, 122, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (121, 72, 125, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (122, 72, 126, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (123, 72, 127, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (124, 72, 128, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (125, 90, 129, CAST(N'2025-05-09' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (126, 90, 130, CAST(N'2025-05-09' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (127, 90, 131, CAST(N'2025-05-09' AS Date))
INSERT [dbo].[AnhSanPham] ([MaAnhSP], [MaSanPham], [MaAnh], [NgayCapNhat]) VALUES (128, 90, 132, CAST(N'2025-04-17' AS Date))
SET IDENTITY_INSERT [dbo].[AnhSanPham] OFF
GO
SET IDENTITY_INSERT [dbo].[ChiTietDonHang] ON 

INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (73, 84, 46, 1, CAST(1060000 AS Decimal(18, 0)), CAST(1060000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (74, 85, 53, 2, CAST(370000 AS Decimal(18, 0)), CAST(185000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (75, 86, 33, 3, CAST(3900000 AS Decimal(18, 0)), CAST(1300000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (76, 87, 31, 1, CAST(199000 AS Decimal(18, 0)), CAST(199000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (77, 88, 49, 1, CAST(520000 AS Decimal(18, 0)), CAST(520000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (78, 89, 51, 1, CAST(1345000 AS Decimal(18, 0)), CAST(1345000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (79, 90, 31, 3, CAST(597000 AS Decimal(18, 0)), CAST(199000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (80, 91, 42, 2, CAST(3140000 AS Decimal(18, 0)), CAST(1570000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (81, 92, 41, 1, CAST(970000 AS Decimal(18, 0)), CAST(970000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (82, 93, 36, 1, CAST(3150000 AS Decimal(18, 0)), CAST(3150000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (84, 96, 26, 1, CAST(230000 AS Decimal(18, 0)), CAST(230000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (85, 97, 33, 1, CAST(1300000 AS Decimal(18, 0)), CAST(1300000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (86, 98, 26, 1, CAST(230000 AS Decimal(18, 0)), CAST(230000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (87, 99, 31, 1, CAST(199000 AS Decimal(18, 0)), CAST(199000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (88, 100, 26, 2, CAST(460000 AS Decimal(18, 0)), CAST(230000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (89, 101, 26, 2, CAST(460000 AS Decimal(18, 0)), CAST(230000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (90, 102, 33, 2, CAST(2600000 AS Decimal(18, 0)), CAST(1300000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaChiTietSP], [SoLuong], [TongTien], [Gia]) VALUES (91, 102, 31, 1, CAST(199000 AS Decimal(18, 0)), CAST(199000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[ChiTietDonHang] OFF
GO
SET IDENTITY_INSERT [dbo].[ChiTietSanPham] ON 

INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (25, 70, 1, 4, CAST(1000000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 0, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (26, 74, 4, 80, CAST(250000 AS Decimal(18, 0)), CAST(N'2025-04-16' AS Date), 1, CAST(20000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (27, 75, 1, 19, CAST(7500000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (28, 75, 4, 50, CAST(3000000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(60000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (29, 76, 1, 19, CAST(7350000 AS Decimal(18, 0)), CAST(N'2025-05-08' AS Date), 1, CAST(70000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (30, 77, 4, 9, CAST(2139000 AS Decimal(18, 0)), CAST(N'2025-05-09' AS Date), 1, CAST(110000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (31, 78, 8, 0, CAST(199000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (32, 71, 4, 0, CAST(8000000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(150000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (33, 72, 2, 4, CAST(1450000 AS Decimal(18, 0)), CAST(N'2025-05-01' AS Date), 1, CAST(150000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (34, 79, 2, 5, CAST(2180000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(80000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (35, 69, 2, 14, CAST(2050000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (36, 73, 2, 7, CAST(3200000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (37, 80, 4, 0, CAST(950000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (38, 81, 2, 10, CAST(1500000 AS Decimal(18, 0)), CAST(N'2025-05-09' AS Date), 1, CAST(130000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (39, 81, 1, 5, CAST(3000000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(100000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (40, 82, 4, 0, CAST(1800000 AS Decimal(18, 0)), CAST(N'2025-05-01' AS Date), 1, CAST(120000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (41, 84, 2, 16, CAST(1050000 AS Decimal(18, 0)), CAST(N'2025-05-09' AS Date), 1, CAST(80000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (42, 85, 9, 18, CAST(1650000 AS Decimal(18, 0)), CAST(N'2025-05-09' AS Date), 1, CAST(80000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (43, 86, 9, 12, CAST(2850000 AS Decimal(18, 0)), CAST(N'2025-05-09' AS Date), 1, CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (44, 83, 4, 7, CAST(350000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (45, 87, 9, 5, CAST(2700000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(70000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (46, 88, 4, 9, CAST(1090000 AS Decimal(18, 0)), CAST(N'2025-04-17' AS Date), 1, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (47, 69, 1, 8, CAST(5000000 AS Decimal(18, 0)), CAST(N'2025-05-09' AS Date), 1, CAST(150000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (48, 86, 2, 7, CAST(200000 AS Decimal(18, 0)), CAST(N'2025-04-30' AS Date), 0, CAST(10000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (49, 70, 8, 8, CAST(550000 AS Decimal(18, 0)), CAST(N'2025-05-04' AS Date), 1, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (50, 82, 4, 100, CAST(100000 AS Decimal(18, 0)), CAST(N'2025-05-08' AS Date), 1, CAST(12000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (51, 76, 4, 6, CAST(1380000 AS Decimal(18, 0)), CAST(N'2025-05-08' AS Date), 1, CAST(35000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (52, 71, 1, 7, CAST(3500 AS Decimal(18, 0)), CAST(N'2025-05-08' AS Date), 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (53, 82, 2, 5, CAST(200000 AS Decimal(18, 0)), CAST(N'2025-05-08' AS Date), 1, CAST(15000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietSanPham] ([MaChiTietSP], [MaSanPham], [MaKichThuoc], [SoLuong], [Gia], [NgayCapNhat], [TrangThai], [GiaGiam]) VALUES (54, 90, 2, 20, CAST(4690000 AS Decimal(18, 0)), CAST(N'2025-05-09' AS Date), 1, CAST(40000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[ChiTietSanPham] OFF
GO
SET IDENTITY_INSERT [dbo].[DanhGia] ON 

INSERT [dbo].[DanhGia] ([MaDanhGia], [MaTaiKhoan], [MaSanPham], [SoDiem], [MoTa], [NgayCapNhat]) VALUES (19, 27, 78, 4, N'', CAST(N'2025-05-09T21:18:02.000' AS DateTime))
INSERT [dbo].[DanhGia] ([MaDanhGia], [MaTaiKhoan], [MaSanPham], [SoDiem], [MoTa], [NgayCapNhat]) VALUES (21, 27, 88, 5, N'Sản phẩm tốt', CAST(N'2025-04-09T21:15:23.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[DanhGia] OFF
GO
SET IDENTITY_INSERT [dbo].[DiaChi] ON 

INSERT [dbo].[DiaChi] ([MaDiaChi], [MaTaiKhoan], [ThanhPho], [Huyen], [Xa], [ChiTiet], [HoTenNguoiNhan], [SoDienThoaiNguoiNhan], [NgayCapNhat]) VALUES (1, 3, N'1', N'281', N'10366', N'Xóm 3', N'Lý Huyền', N'0374212203', CAST(N'2025-04-14' AS Date))
INSERT [dbo].[DiaChi] ([MaDiaChi], [MaTaiKhoan], [ThanhPho], [Huyen], [Xa], [ChiTiet], [HoTenNguoiNhan], [SoDienThoaiNguoiNhan], [NgayCapNhat]) VALUES (16, 27, N'1', N'281', N'10366', N'Xóm 3 - Xà Cầu', N'Thanh Huyền', N'0374212208', CAST(N'2025-05-09' AS Date))
INSERT [dbo].[DiaChi] ([MaDiaChi], [MaTaiKhoan], [ThanhPho], [Huyen], [Xa], [ChiTiet], [HoTenNguoiNhan], [SoDienThoaiNguoiNhan], [NgayCapNhat]) VALUES (17, 27, N'1', N'19', N'622', N'65 ngõ 123 Đường Phương Canh', N'Thanh Huyền', N'0374212208', CAST(N'2025-05-09' AS Date))
INSERT [dbo].[DiaChi] ([MaDiaChi], [MaTaiKhoan], [ThanhPho], [Huyen], [Xa], [ChiTiet], [HoTenNguoiNhan], [SoDienThoaiNguoiNhan], [NgayCapNhat]) VALUES (18, 25, N'1', N'281', N'10366', N'Xóm 2 - Xà Cầu', N'Anh Kiệt', N'0374212204', CAST(N'2025-05-09' AS Date))
INSERT [dbo].[DiaChi] ([MaDiaChi], [MaTaiKhoan], [ThanhPho], [Huyen], [Xa], [ChiTiet], [HoTenNguoiNhan], [SoDienThoaiNguoiNhan], [NgayCapNhat]) VALUES (19, 22, N'1', N'281', N'10366', N'Ngõ 01, số nhà 07', N'Tường Vy', N'0355101555', CAST(N'2025-05-09' AS Date))
SET IDENTITY_INSERT [dbo].[DiaChi] OFF
GO
SET IDENTITY_INSERT [dbo].[DonHang] ON 

INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (84, 27, CAST(N'2025-01-09T21:54:51.183' AS DateTime), CAST(N'2025-01-09T21:54:51.183' AS DateTime), N'Đã vận chuyển', 9, CAST(1035500 AS Decimal(18, 0)), 1, 1, 16)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (85, 27, CAST(N'2025-01-21T08:56:06.290' AS DateTime), CAST(N'2025-01-21T08:56:06.290' AS DateTime), N'Đã vận chuyển', 10, CAST(380000 AS Decimal(18, 0)), 2, 1, 16)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (86, 27, CAST(N'2025-02-09T20:56:25.550' AS DateTime), CAST(N'2025-02-09T20:56:25.550' AS DateTime), N'Đã vận chuyển', 9, CAST(3733500 AS Decimal(18, 0)), 1, 1, 16)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (87, 27, CAST(N'2025-01-01T20:56:47.360' AS DateTime), CAST(N'2025-01-01T20:56:47.360' AS DateTime), N'Đã vận chuyển', 9, CAST(217550 AS Decimal(18, 0)), 2, 1, 16)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (88, 27, CAST(N'2025-02-11T21:24:41.613' AS DateTime), CAST(N'2025-02-11T21:24:41.613' AS DateTime), N'Chờ xác nhận', 9, CAST(522500 AS Decimal(18, 0)), 1, 0, 17)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (89, 27, CAST(N'2025-03-09T21:24:53.903' AS DateTime), CAST(N'2025-03-09T21:24:53.903' AS DateTime), N'Đã vận chuyển', 9, CAST(1375000 AS Decimal(18, 0)), 1, 1, 16)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (90, 25, CAST(N'2025-05-09T21:50:31.747' AS DateTime), CAST(N'2025-05-09T21:50:31.747' AS DateTime), N'Đã vận chuyển', 10, CAST(607000 AS Decimal(18, 0)), 1, 1, 18)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (91, 25, CAST(N'2025-04-02T21:51:12.780' AS DateTime), CAST(N'2025-04-02T21:51:12.780' AS DateTime), N'Đã vận chuyển', 9, CAST(3011500 AS Decimal(18, 0)), 2, 1, 18)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (92, 22, CAST(N'2025-04-09T22:08:13.907' AS DateTime), CAST(N'2025-04-09T22:08:13.907' AS DateTime), N'Đã vận chuyển', 10, CAST(980000 AS Decimal(18, 0)), 1, 1, 19)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (93, 22, CAST(N'2025-04-20T02:20:40.727' AS DateTime), CAST(N'2025-04-20T02:20:40.727' AS DateTime), N'Đã vận chuyển', 9, CAST(3021000 AS Decimal(18, 0)), 1, 1, 19)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (96, 22, CAST(N'2025-02-09T22:14:23.733' AS DateTime), CAST(N'2025-02-09T22:14:23.733' AS DateTime), N'Đã vận chuyển', 9, CAST(247000 AS Decimal(18, 0)), 2, 1, 19)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (97, 22, CAST(N'2025-05-09T22:15:39.037' AS DateTime), CAST(N'2025-05-09T22:15:39.037' AS DateTime), N'Đã vận chuyển', 9, CAST(1263500 AS Decimal(18, 0)), 2, 1, 19)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (98, 22, CAST(N'2025-03-09T22:17:19.703' AS DateTime), CAST(N'2025-03-09T22:17:19.703' AS DateTime), N'Đã vận chuyển', 9, CAST(247000 AS Decimal(18, 0)), 2, 1, 19)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (99, 22, CAST(N'2025-03-09T22:18:09.510' AS DateTime), CAST(N'2025-03-09T22:18:09.510' AS DateTime), N'Đã vận chuyển', 9, CAST(217550 AS Decimal(18, 0)), 2, 1, 19)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (100, 22, CAST(N'2025-03-09T22:19:05.260' AS DateTime), CAST(N'2025-03-09T22:19:05.260' AS DateTime), N'Đã vận chuyển', 9, CAST(465500 AS Decimal(18, 0)), 2, 1, 19)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (101, 27, CAST(N'2025-05-15T08:50:40.897' AS DateTime), CAST(N'2025-05-15T08:50:40.897' AS DateTime), N'Chờ xác nhận', 10, CAST(470000 AS Decimal(18, 0)), 1, 0, 16)
INSERT [dbo].[DonHang] ([MaDonHang], [MaTaiKhoan], [NgayTao], [NgayCapNhat], [TrangThaiVanChuyen], [MMaGiamGia], [TongTien], [MaPhuongThucThanhToan], [TrangThaiThanhToan], [MaDiaChi]) VALUES (102, 27, CAST(N'2025-05-15T08:50:57.423' AS DateTime), CAST(N'2025-05-15T08:50:57.423' AS DateTime), N'Chờ xác nhận', 9, CAST(2687550 AS Decimal(18, 0)), 2, 0, 17)
SET IDENTITY_INSERT [dbo].[DonHang] OFF
GO
SET IDENTITY_INSERT [dbo].[GioHang] ON 

INSERT [dbo].[GioHang] ([MaGioHang], [MaTaiKhoan], [NgayCapNhat]) VALUES (4, 3, CAST(N'2025-05-09' AS Date))
INSERT [dbo].[GioHang] ([MaGioHang], [MaTaiKhoan], [NgayCapNhat]) VALUES (5, 2, CAST(N'2025-05-09' AS Date))
INSERT [dbo].[GioHang] ([MaGioHang], [MaTaiKhoan], [NgayCapNhat]) VALUES (6, 27, CAST(N'2025-05-09' AS Date))
INSERT [dbo].[GioHang] ([MaGioHang], [MaTaiKhoan], [NgayCapNhat]) VALUES (7, 25, CAST(N'2025-05-09' AS Date))
INSERT [dbo].[GioHang] ([MaGioHang], [MaTaiKhoan], [NgayCapNhat]) VALUES (8, 22, CAST(N'2025-05-09' AS Date))
SET IDENTITY_INSERT [dbo].[GioHang] OFF
GO
SET IDENTITY_INSERT [dbo].[KichThuoc] ON 

INSERT [dbo].[KichThuoc] ([MaKichThuoc], [TenKichThuoc], [MoTa], [TrangThai], [NgayCapNhat]) VALUES (1, N'200ml', N'', 1, CAST(N'2025-04-14' AS Date))
INSERT [dbo].[KichThuoc] ([MaKichThuoc], [TenKichThuoc], [MoTa], [TrangThai], [NgayCapNhat]) VALUES (2, N'100ml', N'', 1, CAST(N'2025-04-14' AS Date))
INSERT [dbo].[KichThuoc] ([MaKichThuoc], [TenKichThuoc], [MoTa], [TrangThai], [NgayCapNhat]) VALUES (4, N'50ml', N'', 1, CAST(N'2025-04-14' AS Date))
INSERT [dbo].[KichThuoc] ([MaKichThuoc], [TenKichThuoc], [MoTa], [TrangThai], [NgayCapNhat]) VALUES (7, N'20ml', N'123', 1, CAST(N'2025-04-14' AS Date))
INSERT [dbo].[KichThuoc] ([MaKichThuoc], [TenKichThuoc], [MoTa], [TrangThai], [NgayCapNhat]) VALUES (8, N'10ml', N'', 1, CAST(N'2025-04-14' AS Date))
INSERT [dbo].[KichThuoc] ([MaKichThuoc], [TenKichThuoc], [MoTa], [TrangThai], [NgayCapNhat]) VALUES (9, N'Full Size', N'', 1, CAST(N'2025-04-14' AS Date))
INSERT [dbo].[KichThuoc] ([MaKichThuoc], [TenKichThuoc], [MoTa], [TrangThai], [NgayCapNhat]) VALUES (10, N'400ml', N'', 1, CAST(N'2025-04-30' AS Date))
SET IDENTITY_INSERT [dbo].[KichThuoc] OFF
GO
SET IDENTITY_INSERT [dbo].[MaGiamGia] ON 

INSERT [dbo].[MaGiamGia] ([MMaGiamGia], [MaHienThi], [NgayBatDau], [NgayKetThuc], [LoaiGiamGia], [GiaTri], [TrangThai], [NgayCapNhat]) VALUES (9, N'HE2025', CAST(N'2025-04-08' AS Date), CAST(N'2025-06-30' AS Date), N'Giảm theo %', 5, 1, CAST(N'2025-05-06' AS Date))
INSERT [dbo].[MaGiamGia] ([MMaGiamGia], [MaHienThi], [NgayBatDau], [NgayKetThuc], [LoaiGiamGia], [GiaTri], [TrangThai], [NgayCapNhat]) VALUES (10, N'KHACHMOI', CAST(N'2025-04-25' AS Date), CAST(N'2025-07-15' AS Date), N'Giảm tiền mặt', 20000, 1, CAST(N'2025-05-08' AS Date))
SET IDENTITY_INSERT [dbo].[MaGiamGia] OFF
GO
SET IDENTITY_INSERT [dbo].[NhomHuong] ON 

INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (1, N'Thảo Mộc Phương Đông', N'1', CAST(N'2025-04-05' AS Date), 1)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (3, N'Cam Chanh (Citrus)', N'', CAST(N'2025-05-04' AS Date), 0)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (4, N'Hương hoa cỏ – Gỗ – Xạ hương', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (5, N'Hoa cỏ Chypre', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (6, N'Floral Fruity', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (7, N'Dương xỉ phương Đông', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (8, N'Fresh - Water', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (9, N'Hương Vani Phương Đông', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (10, N'Hương Hoa Cỏ Trái Cây', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (11, N'Hổ Phách', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[NhomHuong] ([MaNhomHuong], [TenNhomHuong], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (12, N'Hoa cỏ xạ hương', N'', CAST(N'2025-04-17' AS Date), 1)
SET IDENTITY_INSERT [dbo].[NhomHuong] OFF
GO
SET IDENTITY_INSERT [dbo].[PhuongThucThanhToan] ON 

INSERT [dbo].[PhuongThucThanhToan] ([MaPhuongThuc], [TenPhuongThuc], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (1, N'Thanh toán khi nhận hàng', N'', CAST(N'2025-04-22' AS Date), 1)
INSERT [dbo].[PhuongThucThanhToan] ([MaPhuongThuc], [TenPhuongThuc], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (2, N'Thanh toán online', N'', CAST(N'2025-04-22' AS Date), 1)
SET IDENTITY_INSERT [dbo].[PhuongThucThanhToan] OFF
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (69, 5, 8, N'Un Jardin Sur Le Nil', N'Unisex', 1, NULL, CAST(N'2025-04-12' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (70, 5, 4, N'Eau Ginger Eau De Parfum', N'Nữ', 1, NULL, CAST(N'2025-04-12' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (71, 9, 1, N'Baccarat Rouge 540 Extrait', N'Unisex', 1, NULL, CAST(N'2025-04-12' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (72, 10, 1, N'Daisy Love', N'Nữ', 1, NULL, CAST(N'2025-04-12' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (73, 3, 4, N'N5 L''eau Limited Edition EDT', N'Nữ', 1, NULL, CAST(N'2025-04-16' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (74, 1, 1, N'Bleu De Chanel EDP', N'Nam', 1, NULL, CAST(N'2025-04-16' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (75, 4, 4, N'Valaya Exclusif', N'Nữ', 1, NULL, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (76, 7, 6, N'Bright Crystal', N'Nữ', 1, NULL, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (77, 6, 5, N'Miss Dior EDT', N'Nữ', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (78, 7, 1, N'Eros Man EDT', N'Nam', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (79, 5, 7, N'Eau Des Merveilles', N'Unisex', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (80, 11, 9, N'Marry Me', N'Nữ', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (81, 10, 10, N'Jacobs Daisy Dream EDT', N'Nữ', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (82, 12, 10, N' La Vie Est Belle', N'Nữ', 1, NULL, CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (83, 13, 11, N'Armaf SHK I Eau De Parfum', N'Nam', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (84, 14, 3, N'Supremacy In Heaven EDP', N'Nam', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (85, 15, 7, N'Acqua Di Gio Pour Homme', N'Nam', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (86, 16, 10, N'Le Beau Paradise Garden EDP', N'Nam', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (87, 10, 12, N'Daisy Wild EDP ', N'Nữ', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (88, 17, 11, N'Run Wild For Him EDT', N'Nam', 1, N'', CAST(N'2025-04-17' AS Date))
INSERT [dbo].[SanPham] ([MaSanPham], [MaThuongHieu], [MaNhomHuong], [TenSanPham], [GioiTinh], [TrangThai], [MoTa], [NgayCapNhat]) VALUES (90, 3, 10, N'Eau Tendre Eau De Parfum', N'Nữ', 1, NULL, CAST(N'2025-05-09' AS Date))
SET IDENTITY_INSERT [dbo].[SanPham] OFF
GO
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [HoTen], [MatKhau], [Email], [SoDienThoai], [LoaiTaiKhoan], [TrangThai], [NgayCapNhat]) VALUES (2, N'Admin', N'19122003', N'admin@gmail.com', N'0374212203', 1, 1, CAST(N'2025-03-30' AS Date))
INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [HoTen], [MatKhau], [Email], [SoDienThoai], [LoaiTaiKhoan], [TrangThai], [NgayCapNhat]) VALUES (3, N'Thanh Huyền', N'19122003', N'huyenly.112@gmail.com', N'0389127233', 2, 1, CAST(N'2025-04-30' AS Date))
INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [HoTen], [MatKhau], [Email], [SoDienThoai], [LoaiTaiKhoan], [TrangThai], [NgayCapNhat]) VALUES (22, N'Tường Vy', N'19122003', N'tuongvy.2312@gmail.com', N'0389127222', 2, 1, CAST(N'2025-05-08' AS Date))
INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [HoTen], [MatKhau], [Email], [SoDienThoai], [LoaiTaiKhoan], [TrangThai], [NgayCapNhat]) VALUES (23, N'Mai Trang', N'19122003', N'maitrang.0102@gmail.com', N'0374212202', 2, 1, CAST(N'2025-05-05' AS Date))
INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [HoTen], [MatKhau], [Email], [SoDienThoai], [LoaiTaiKhoan], [TrangThai], [NgayCapNhat]) VALUES (24, N'Thế Duy', N'19122003', N'theduy.2909@gmail.com', N'0374212201', 2, 1, CAST(N'2025-05-06' AS Date))
INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [HoTen], [MatKhau], [Email], [SoDienThoai], [LoaiTaiKhoan], [TrangThai], [NgayCapNhat]) VALUES (25, N'Anh Kiệt', N'19122003', N'anhkiet.0908@gmail.com', N'0374212204', 2, 1, CAST(N'2025-05-07' AS Date))
INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [HoTen], [MatKhau], [Email], [SoDienThoai], [LoaiTaiKhoan], [TrangThai], [NgayCapNhat]) VALUES (27, N'Huyền Lý', N'19122003', N'huyenlythithanh55@gmail.com', N'0374212208', 2, 1, CAST(N'2025-05-09' AS Date))
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
GO
SET IDENTITY_INSERT [dbo].[ThuongHieu] ON 

INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (1, N'Poison – Christian Dior', N'Pháp', N'Christian Dior S.A (Dior) - một công ty xa xỉ nổi tiếng ở Pháp được thành lập 16 tháng 12 năm 1946 tại nhà riêng của Christian Dior số 30 Avenue Montaigne Paris B.', CAST(N'2025-03-23' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (3, N'Chanel', N'Pháp', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (4, N'Parfums de Marly', N'Pháp', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (5, N'Hermès ', N'Pháp', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (6, N'Dior', N'Pháp', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (7, N'Versace', N'Ý', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (9, N'Maison Francis Kurkdjian', N'Pháp', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (10, N'Marc Jacobs', N'Mỹ', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (11, N'Lanvin', N'Pháp', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (12, N'Lancôme', N'Pháp', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (13, N'Armaf', N'Ả Rập', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (14, N'Afnan Perfumes', N'Ả Rập', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (15, N'Giorgio Armani', N'Pháp', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (16, N'Jean Paul Gaultier', N'Pháp', N'', CAST(N'2025-04-17' AS Date), 1)
INSERT [dbo].[ThuongHieu] ([MaThuongHieu], [TenThuongHieu], [QuocGia], [MoTa], [NgayCapNhat], [TrangThai]) VALUES (17, N'Davidoff', N'Thụy Sỹ', N'', CAST(N'2025-04-17' AS Date), 1)
SET IDENTITY_INSERT [dbo].[ThuongHieu] OFF
GO
SET IDENTITY_INSERT [dbo].[YeuThich] ON 

INSERT [dbo].[YeuThich] ([MaYeuThich], [MaTaiKhoan], [MaSanPham], [NgayCapNhat]) VALUES (1014, 27, 90, CAST(N'2025-05-15T08:43:48.297' AS DateTime))
INSERT [dbo].[YeuThich] ([MaYeuThich], [MaTaiKhoan], [MaSanPham], [NgayCapNhat]) VALUES (1015, 27, 81, CAST(N'2025-05-15T08:44:05.017' AS DateTime))
INSERT [dbo].[YeuThich] ([MaYeuThich], [MaTaiKhoan], [MaSanPham], [NgayCapNhat]) VALUES (1016, 27, 86, CAST(N'2025-05-15T08:44:20.207' AS DateTime))
INSERT [dbo].[YeuThich] ([MaYeuThich], [MaTaiKhoan], [MaSanPham], [NgayCapNhat]) VALUES (1017, 27, 72, CAST(N'2025-05-15T08:44:29.073' AS DateTime))
SET IDENTITY_INSERT [dbo].[YeuThich] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Anh__C5B1000969506247]    Script Date: 15/05/2025 17:26:25 ******/
ALTER TABLE [dbo].[Anh] ADD  CONSTRAINT [UQ__Anh__C5B1000969506247] UNIQUE NONCLUSTERED 
(
	[URL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__KichThuo__9D0743DA3BCBCD19]    Script Date: 15/05/2025 17:26:25 ******/
ALTER TABLE [dbo].[KichThuoc] ADD UNIQUE NONCLUSTERED 
(
	[TenKichThuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__MaGiamGi__EED4DD3DF4C61609]    Script Date: 15/05/2025 17:26:25 ******/
ALTER TABLE [dbo].[MaGiamGia] ADD UNIQUE NONCLUSTERED 
(
	[MaHienThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__NhomHuon__08468B70B8E7BB91]    Script Date: 15/05/2025 17:26:25 ******/
ALTER TABLE [dbo].[NhomHuong] ADD UNIQUE NONCLUSTERED 
(
	[TenNhomHuong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__PhuongTh__B139DAA638819A35]    Script Date: 15/05/2025 17:26:25 ******/
ALTER TABLE [dbo].[PhuongThucThanhToan] ADD UNIQUE NONCLUSTERED 
(
	[TenPhuongThuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TaiKhoan__0389B7BD7E553E57]    Script Date: 15/05/2025 17:26:25 ******/
ALTER TABLE [dbo].[TaiKhoan] ADD  CONSTRAINT [UQ__TaiKhoan__0389B7BD7E553E57] UNIQUE NONCLUSTERED 
(
	[SoDienThoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TaiKhoan__A9D1053405C7B5D5]    Script Date: 15/05/2025 17:26:25 ******/
ALTER TABLE [dbo].[TaiKhoan] ADD  CONSTRAINT [UQ__TaiKhoan__A9D1053405C7B5D5] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ThuongHi__98D6A834F466F376]    Script Date: 15/05/2025 17:26:25 ******/
ALTER TABLE [dbo].[ThuongHieu] ADD UNIQUE NONCLUSTERED 
(
	[TenThuongHieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Anh] ADD  CONSTRAINT [DF__Anh__NgayCapNhat__02084FDA]  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[AnhSanPham] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[ChiTietGioHang] ADD  CONSTRAINT [DF__ChiTietGi__NgayC__3A4CA8FD]  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[ChiTietSanPham] ADD  CONSTRAINT [DF__ChiTietSa__NgayC__114A936A]  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[ChiTietSanPham] ADD  CONSTRAINT [DF__ChiTietSa__Trang__123EB7A3]  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[DanhGia] ADD  CONSTRAINT [DF__DanhGia__NgayCap__18EBB532]  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[DiaChi] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[DonHang] ADD  CONSTRAINT [DF__DonHang__NgayTao__2739D489]  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[DonHang] ADD  CONSTRAINT [DF__DonHang__NgayCap__282DF8C2]  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[GioHang] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[KichThuoc] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[KichThuoc] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[MaGiamGia] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[MaGiamGia] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[NhomHuong] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[NhomHuong] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[PhuongThucThanhToan] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[PhuongThucThanhToan] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[SanPham] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[SanPham] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[TaiKhoan] ADD  CONSTRAINT [DF__TaiKhoan__TrangT__5812160E]  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[TaiKhoan] ADD  CONSTRAINT [DF__TaiKhoan__NgayCa__59063A47]  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[ThanhToan] ADD  CONSTRAINT [DF__ThanhToan__ThoiG__40F9A68C]  DEFAULT (getdate()) FOR [ThoiGian]
GO
ALTER TABLE [dbo].[ThuongHieu] ADD  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[ThuongHieu] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[YeuThich] ADD  CONSTRAINT [DF__YeuThich__NgayCa__1DB06A4F]  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHang_ChiTietSP] FOREIGN KEY([MaChiTietSP])
REFERENCES [dbo].[ChiTietSanPham] ([MaChiTietSP])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_ChiTietDonHang_ChiTietSP]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHang_DonHang] FOREIGN KEY([MaDonHang])
REFERENCES [dbo].[DonHang] ([MaDonHang])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_ChiTietDonHang_DonHang]
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietGioHang_ChiTietSP] FOREIGN KEY([MaChiTietSanPham])
REFERENCES [dbo].[ChiTietSanPham] ([MaChiTietSP])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [FK_ChiTietGioHang_ChiTietSP]
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietGioHang_GioHang] FOREIGN KEY([MaGioHang])
REFERENCES [dbo].[GioHang] ([MaGioHang])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [FK_ChiTietGioHang_GioHang]
GO
ALTER TABLE [dbo].[ChiTietSanPham]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietSP_KichThuoc] FOREIGN KEY([MaKichThuoc])
REFERENCES [dbo].[KichThuoc] ([MaKichThuoc])
GO
ALTER TABLE [dbo].[ChiTietSanPham] CHECK CONSTRAINT [FK_ChiTietSP_KichThuoc]
GO
ALTER TABLE [dbo].[ChiTietSanPham]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietSP_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[ChiTietSanPham] CHECK CONSTRAINT [FK_ChiTietSP_SanPham]
GO
ALTER TABLE [dbo].[DanhGia]  WITH CHECK ADD  CONSTRAINT [FK_DanhGia_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[DanhGia] CHECK CONSTRAINT [FK_DanhGia_SanPham]
GO
ALTER TABLE [dbo].[DanhGia]  WITH CHECK ADD  CONSTRAINT [FK_DanhGia_TaiKhoan] FOREIGN KEY([MaTaiKhoan])
REFERENCES [dbo].[TaiKhoan] ([MaTaiKhoan])
GO
ALTER TABLE [dbo].[DanhGia] CHECK CONSTRAINT [FK_DanhGia_TaiKhoan]
GO
ALTER TABLE [dbo].[DiaChi]  WITH CHECK ADD  CONSTRAINT [FK__DiaChi__MaTaiKho__5EBF139D] FOREIGN KEY([MaTaiKhoan])
REFERENCES [dbo].[TaiKhoan] ([MaTaiKhoan])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DiaChi] CHECK CONSTRAINT [FK__DiaChi__MaTaiKho__5EBF139D]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_DiaChi] FOREIGN KEY([MaDiaChi])
REFERENCES [dbo].[DiaChi] ([MaDiaChi])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_DiaChi]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_GiamGia] FOREIGN KEY([MMaGiamGia])
REFERENCES [dbo].[MaGiamGia] ([MMaGiamGia])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_GiamGia]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_PhuongThucThanhToan] FOREIGN KEY([MaPhuongThucThanhToan])
REFERENCES [dbo].[PhuongThucThanhToan] ([MaPhuongThuc])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_PhuongThucThanhToan]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_TaiKhoan] FOREIGN KEY([MaTaiKhoan])
REFERENCES [dbo].[TaiKhoan] ([MaTaiKhoan])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_TaiKhoan]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_TaiKhoan] FOREIGN KEY([MaTaiKhoan])
REFERENCES [dbo].[TaiKhoan] ([MaTaiKhoan])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_TaiKhoan]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_NhomHuong] FOREIGN KEY([MaNhomHuong])
REFERENCES [dbo].[NhomHuong] ([MaNhomHuong])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_NhomHuong]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_ThuongHieu] FOREIGN KEY([MaThuongHieu])
REFERENCES [dbo].[ThuongHieu] ([MaThuongHieu])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_ThuongHieu]
GO
ALTER TABLE [dbo].[ThanhToan]  WITH CHECK ADD  CONSTRAINT [FK_ThanhToan_DonHang] FOREIGN KEY([MaDonHang])
REFERENCES [dbo].[DonHang] ([MaDonHang])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ThanhToan] CHECK CONSTRAINT [FK_ThanhToan_DonHang]
GO
ALTER TABLE [dbo].[ThanhToan]  WITH CHECK ADD  CONSTRAINT [FK_ThanhToan_TaiKhoan] FOREIGN KEY([MaTaiKhoan])
REFERENCES [dbo].[TaiKhoan] ([MaTaiKhoan])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ThanhToan] CHECK CONSTRAINT [FK_ThanhToan_TaiKhoan]
GO
ALTER TABLE [dbo].[YeuThich]  WITH CHECK ADD  CONSTRAINT [FK_YeuThich_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[YeuThich] CHECK CONSTRAINT [FK_YeuThich_SanPham]
GO
ALTER TABLE [dbo].[YeuThich]  WITH CHECK ADD  CONSTRAINT [FK_YeuThich_TaiKhoan] FOREIGN KEY([MaTaiKhoan])
REFERENCES [dbo].[TaiKhoan] ([MaTaiKhoan])
GO
ALTER TABLE [dbo].[YeuThich] CHECK CONSTRAINT [FK_YeuThich_TaiKhoan]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [CK__ChiTietDo__SoLuo__3493CFA7] CHECK  (([SoLuong]>(0)))
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [CK__ChiTietDo__SoLuo__3493CFA7]
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [CK__ChiTietGi__SoLuo__395884C4] CHECK  (([SoLuong]>(0)))
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [CK__ChiTietGi__SoLuo__395884C4]
GO
ALTER TABLE [dbo].[ChiTietSanPham]  WITH CHECK ADD  CONSTRAINT [CK__ChiTietSa__SoLuo__0F624AF8] CHECK  (([SoLuong]>=(0)))
GO
ALTER TABLE [dbo].[ChiTietSanPham] CHECK CONSTRAINT [CK__ChiTietSa__SoLuo__0F624AF8]
GO
ALTER TABLE [dbo].[ChiTietSanPham]  WITH CHECK ADD  CONSTRAINT [CK__ChiTietSanP__Gia__10566F31] CHECK  (([Gia]>=(0)))
GO
ALTER TABLE [dbo].[ChiTietSanPham] CHECK CONSTRAINT [CK__ChiTietSanP__Gia__10566F31]
GO
ALTER TABLE [dbo].[DanhGia]  WITH CHECK ADD  CONSTRAINT [CK__DanhGia__SoDiem__17F790F9] CHECK  (([SoDiem]>=(1) AND [SoDiem]<=(5)))
GO
ALTER TABLE [dbo].[DanhGia] CHECK CONSTRAINT [CK__DanhGia__SoDiem__17F790F9]
GO
ALTER TABLE [dbo].[DiaChi]  WITH CHECK ADD CHECK  (([SoDienThoaiNguoiNhan] like '[0-9]%'))
GO
ALTER TABLE [dbo].[MaGiamGia]  WITH CHECK ADD  CONSTRAINT [CHK_NgayKetThuc] CHECK  (([NgayKetThuc]>=[NgayBatDau]))
GO
ALTER TABLE [dbo].[MaGiamGia] CHECK CONSTRAINT [CHK_NgayKetThuc]
GO
ALTER TABLE [dbo].[MaGiamGia]  WITH CHECK ADD CHECK  (([GiaTri]>(0)))
GO
ALTER TABLE [dbo].[MaGiamGia]  WITH CHECK ADD CHECK  (([LoaiGiamGia]=N'Giảm tiền mặt' OR [LoaiGiamGia]=N'Giảm theo %'))
GO
ALTER TABLE [dbo].[ThanhToan]  WITH CHECK ADD  CONSTRAINT [CK__ThanhToan__SoTie__40058253] CHECK  (([SoTienGiaoDich]>(0)))
GO
ALTER TABLE [dbo].[ThanhToan] CHECK CONSTRAINT [CK__ThanhToan__SoTie__40058253]
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemTaiKhoan]    Script Date: 15/05/2025 17:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ThemTaiKhoan]
    @TenDangNhap NVARCHAR(50),
    @MatKhau NVARCHAR(100),
    @Email NVARCHAR(100),
    @HoTen NVARCHAR(100),
	@SoDienThoai NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra xem tài khoản đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND Email  = @Email AND SoDienThoai = @SoDienThoai)
    BEGIN
        RAISERROR ('Tài khoản đã tồn tại', 16, 1);
        RETURN;
    END

    -- Thêm tài khoản mới
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Email, HoTen, SoDienThoai, LoaiTaiKhoan)
    VALUES (@TenDangNhap, @MatKhau, @Email, @HoTen, @SoDienThoai, 2);

    -- Trả về ID của tài khoản vừa thêm
    SELECT SCOPE_IDENTITY() AS ID;
END;
GO
