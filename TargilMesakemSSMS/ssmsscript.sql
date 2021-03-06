USE [TargilMesakemEF]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 09/01/2021 21:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[District_ID] [bigint] NOT NULL,
	[Mayor] [text] NOT NULL,
	[Population] [int] NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Districts]    Script Date: 09/01/2021 21:30:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Districts](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Population] [int] NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (1, N'Haifa', 1, N'Hezi', 20000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (2, N'Qiryat_Shmona', 1, N'Josh', 15000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (3, N'Jerusalem', 3, N'David', 50000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (4, N'Tel_Aviv', 4, N'Ron', 25000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (5, N'Petah_Tikva', 5, N'Roni', 13000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (6, N'Eilat', 2, N'Hezi', 83000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (7, N'Afula', 1, N'Danny', 500000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (8, N'Ariel', 3, N'Gal', 40000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (9, N'Beni_Brak', 5, N'H''arav_something', 105000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (10, N'Beer_Sheva', 2, N'Gidi', 88000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (11, N'Herzeliya', 4, N'Shimon', 79000)
INSERT [dbo].[Cities] ([ID], [Name], [District_ID], [Mayor], [Population]) VALUES (12, N'Ramat_Gan', 5, N'Kobi', 150000)
SET IDENTITY_INSERT [dbo].[Cities] OFF
GO
SET IDENTITY_INSERT [dbo].[Districts] ON 

INSERT [dbo].[Districts] ([ID], [Name], [Population]) VALUES (1, N'North', 535000)
INSERT [dbo].[Districts] ([ID], [Name], [Population]) VALUES (2, N'South', 171000)
INSERT [dbo].[Districts] ([ID], [Name], [Population]) VALUES (3, N'East', 90000)
INSERT [dbo].[Districts] ([ID], [Name], [Population]) VALUES (4, N'West', 104000)
INSERT [dbo].[Districts] ([ID], [Name], [Population]) VALUES (5, N'Center', 268000)
SET IDENTITY_INSERT [dbo].[Districts] OFF
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Districts] FOREIGN KEY([District_ID])
REFERENCES [dbo].[Districts] ([ID])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Districts]
GO
