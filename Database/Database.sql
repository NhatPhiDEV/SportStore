USE [ProductManagement]
GO
/****** Object:  Table [dbo].[ADMIN]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMIN](
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Fullname] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](12) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](30) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_ADMIN] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BILL]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BillID] [nvarchar](15) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[TotalMoney] [decimal](18, 0) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_BILL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CUSTOMER]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUSTOMER](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[CustomerSex] [nvarchar](7) NULL,
	[CustomerAddress] [nvarchar](max) NULL,
	[CustomerEmail] [nvarchar](50) NULL,
	[CustomerPhone] [nvarchar](11) NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CUSTOMER] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [nvarchar](10) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductPrice] [decimal](18, 0) NOT NULL,
	[ProductImage] [nvarchar](max) NOT NULL,
	[ProductCount] [int] NULL,
	[Size] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Brand] [int] NOT NULL,
	[Sale] [int] NOT NULL,
	[DateInsert] [datetime] NOT NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_PRODUCT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT_BRAND]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_BRAND](
	[Brand] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](50) NULL,
 CONSTRAINT [PK_PRODUCT_BRAND] PRIMARY KEY CLUSTERED 
(
	[Brand] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT_DETAIL]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_DETAIL](
	[DetailID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[BillID] [int] NOT NULL,
	[ProductCount] [int] NOT NULL,
 CONSTRAINT [PK_PRODUCT_DETAIL] PRIMARY KEY CLUSTERED 
(
	[DetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT_SALE]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_SALE](
	[Sale] [int] IDENTITY(1,1) NOT NULL,
	[SaleName] [int] NOT NULL,
 CONSTRAINT [PK_PRODUCT_SALE] PRIMARY KEY CLUSTERED 
(
	[Sale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT_SIZE]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_SIZE](
	[Size] [int] IDENTITY(1,1) NOT NULL,
	[SizeName] [nvarchar](5) NULL,
 CONSTRAINT [PK_PRODUCT_SIZE] PRIMARY KEY CLUSTERED 
(
	[Size] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT_TYPE]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT_TYPE](
	[Type] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](max) NULL,
 CONSTRAINT [PK_PRODUCT_TYPE] PRIMARY KEY CLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REVIEWS]    Script Date: 26/06/2021 12:02:04 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REVIEWS](
	[Reviews] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Contentt] [nvarchar](max) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_REVIEWS] PRIMARY KEY CLUSTERED 
(
	[Reviews] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PRODUCT] ON 

INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (10, N'ID00000001', N'Áo thi đấu đội tuyển Anh sân nhà 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\Anh\Trang\anhtrangx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (12, N'ID00000002', N'Áo thi đấu đội tuyển Argentina sân nhà 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\Argentina\XanhBien\xanhtrangx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (13, N'ID00000002', N'Áo thi đấu Argentina sân khách năm 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\Argentina\Xanhdam\xanhdamx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (14, N'ID00000003', N'Áo thi đấu đội tuyển Bỉ sân nhà năm 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\Bi\bix1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (15, N'ID00000004', N'Áo thi đấu đội tuyển Hà Lan sân khách năm 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\HaLan\CAM\halancamx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (16, N'ID00000004', N'Áo thi đấu Hà Lan sân khách năm 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\HaLan\DEN\halandenx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (17, N'ID00000005', N'Áo thi đấu Italy sân khách 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\Italy\Trang\ytrangx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (18, N'ID00000005', N'Áo thi đấu Italy sân nhà năm 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\Italy\Xanhbien\yxanhbienx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (19, N'ID00000006', N'Áo thi đấu sân nhà đội tuyển Pháp năm 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\Phap\phapxanhdenx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (20, N'ID00000007', N'Áo thi đấu sân nhà đội tuyển Tây Ban Nha năm 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\Taybannha\taybannhax1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (21, N'ID00000008', N'Áo thi đấu sân nhà đội tuyển Việt Nam 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\VietNam\Do\vndox1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (22, N'ID00000008', N'Áo thi đấu sân khách đội tuyển Việt Nam năm 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\VietNam\Trang\vntrangx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
INSERT [dbo].[PRODUCT] ([ID], [ProductID], [ProductName], [ProductPrice], [ProductImage], [ProductCount], [Size], [Type], [Brand], [Sale], [DateInsert], [Note]) VALUES (23, N'ID00000008', N'Áo thi đấu thủ môn đội tuyển Việt Nam năm 2021', CAST(100000 AS Decimal(18, 0)), N'Content\images\aodoituyenqg\VietNam\Thumon\vntmx1.jpg', 20, 1, 1, 1, 1, CAST(N'2021-06-25T00:00:00.000' AS DateTime), N'Sản phẩm được may bởi 100% vãi thu co giản được nhập khẩu nguyên chất từ châu âu mang đến trải nghiệm thoáng mát và thoải mái nhất cho người sử dụng.')
SET IDENTITY_INSERT [dbo].[PRODUCT] OFF
GO
SET IDENTITY_INSERT [dbo].[PRODUCT_BRAND] ON 

INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (1, N'ADIDAS')
INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (2, N'NIKE')
INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (3, N'PUMA')
INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (4, N'BROOKS')
INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (5, N'ASICS')
INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (6, N'REEBOOK')
INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (7, N'NEW BLANCE')
INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (8, N'FILA')
INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (9, N'X STORM')
INSERT [dbo].[PRODUCT_BRAND] ([Brand], [BrandName]) VALUES (10, N'KEEP & FLY')
SET IDENTITY_INSERT [dbo].[PRODUCT_BRAND] OFF
GO
SET IDENTITY_INSERT [dbo].[PRODUCT_SALE] ON 

INSERT [dbo].[PRODUCT_SALE] ([Sale], [SaleName]) VALUES (1, 0)
INSERT [dbo].[PRODUCT_SALE] ([Sale], [SaleName]) VALUES (2, 15)
INSERT [dbo].[PRODUCT_SALE] ([Sale], [SaleName]) VALUES (3, 20)
INSERT [dbo].[PRODUCT_SALE] ([Sale], [SaleName]) VALUES (4, 25)
INSERT [dbo].[PRODUCT_SALE] ([Sale], [SaleName]) VALUES (5, 30)
INSERT [dbo].[PRODUCT_SALE] ([Sale], [SaleName]) VALUES (6, 35)
INSERT [dbo].[PRODUCT_SALE] ([Sale], [SaleName]) VALUES (7, 40)
INSERT [dbo].[PRODUCT_SALE] ([Sale], [SaleName]) VALUES (8, 45)
SET IDENTITY_INSERT [dbo].[PRODUCT_SALE] OFF
GO
SET IDENTITY_INSERT [dbo].[PRODUCT_SIZE] ON 

INSERT [dbo].[PRODUCT_SIZE] ([Size], [SizeName]) VALUES (1, N'M')
INSERT [dbo].[PRODUCT_SIZE] ([Size], [SizeName]) VALUES (2, N'L')
INSERT [dbo].[PRODUCT_SIZE] ([Size], [SizeName]) VALUES (3, N'S')
INSERT [dbo].[PRODUCT_SIZE] ([Size], [SizeName]) VALUES (4, N'XL')
INSERT [dbo].[PRODUCT_SIZE] ([Size], [SizeName]) VALUES (5, N'XXL')
SET IDENTITY_INSERT [dbo].[PRODUCT_SIZE] OFF
GO
SET IDENTITY_INSERT [dbo].[PRODUCT_TYPE] ON 

INSERT [dbo].[PRODUCT_TYPE] ([Type], [TypeName]) VALUES (1, N'ÁO ĐỘI TUYỂN QUỐC GIA')
INSERT [dbo].[PRODUCT_TYPE] ([Type], [TypeName]) VALUES (2, N'ÁO CÂU LẠC BỘ')
INSERT [dbo].[PRODUCT_TYPE] ([Type], [TypeName]) VALUES (3, N'ÁO KHÔNG LOGO')
INSERT [dbo].[PRODUCT_TYPE] ([Type], [TypeName]) VALUES (4, N'GIÀY BÓNG ĐÁ')
INSERT [dbo].[PRODUCT_TYPE] ([Type], [TypeName]) VALUES (5, N'ÁO KHOÁC')
INSERT [dbo].[PRODUCT_TYPE] ([Type], [TypeName]) VALUES (6, N'ÁO TAY DÀI')
INSERT [dbo].[PRODUCT_TYPE] ([Type], [TypeName]) VALUES (7, N'ÁO TRẺ EM')
INSERT [dbo].[PRODUCT_TYPE] ([Type], [TypeName]) VALUES (8, N'PHỤ KIỆN BÓNG ĐÁ')
SET IDENTITY_INSERT [dbo].[PRODUCT_TYPE] OFF
GO
ALTER TABLE [dbo].[BILL]  WITH CHECK ADD  CONSTRAINT [FK_BILL_CUSTOMER] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[CUSTOMER] ([CustomerID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BILL] CHECK CONSTRAINT [FK_BILL_CUSTOMER]
GO
ALTER TABLE [dbo].[PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCT_PRODUCT_BRAND] FOREIGN KEY([Brand])
REFERENCES [dbo].[PRODUCT_BRAND] ([Brand])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRODUCT] CHECK CONSTRAINT [FK_PRODUCT_PRODUCT_BRAND]
GO
ALTER TABLE [dbo].[PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCT_PRODUCT_SALE] FOREIGN KEY([Sale])
REFERENCES [dbo].[PRODUCT_SALE] ([Sale])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRODUCT] CHECK CONSTRAINT [FK_PRODUCT_PRODUCT_SALE]
GO
ALTER TABLE [dbo].[PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCT_PRODUCT_SIZE] FOREIGN KEY([Size])
REFERENCES [dbo].[PRODUCT_SIZE] ([Size])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRODUCT] CHECK CONSTRAINT [FK_PRODUCT_PRODUCT_SIZE]
GO
ALTER TABLE [dbo].[PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCT_PRODUCT_TYPE] FOREIGN KEY([Type])
REFERENCES [dbo].[PRODUCT_TYPE] ([Type])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRODUCT] CHECK CONSTRAINT [FK_PRODUCT_PRODUCT_TYPE]
GO
ALTER TABLE [dbo].[PRODUCT_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCT_DETAIL_BILL] FOREIGN KEY([BillID])
REFERENCES [dbo].[BILL] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRODUCT_DETAIL] CHECK CONSTRAINT [FK_PRODUCT_DETAIL_BILL]
GO
ALTER TABLE [dbo].[PRODUCT_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCT_DETAIL_PRODUCT] FOREIGN KEY([ProductID])
REFERENCES [dbo].[PRODUCT] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRODUCT_DETAIL] CHECK CONSTRAINT [FK_PRODUCT_DETAIL_PRODUCT]
GO
ALTER TABLE [dbo].[REVIEWS]  WITH CHECK ADD  CONSTRAINT [FK_REVIEWS_CUSTOMER] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[CUSTOMER] ([CustomerID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REVIEWS] CHECK CONSTRAINT [FK_REVIEWS_CUSTOMER]
GO
ALTER TABLE [dbo].[REVIEWS]  WITH CHECK ADD  CONSTRAINT [FK_REVIEWS_PRODUCT] FOREIGN KEY([ProductID])
REFERENCES [dbo].[PRODUCT] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REVIEWS] CHECK CONSTRAINT [FK_REVIEWS_PRODUCT]
GO
