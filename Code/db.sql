USE [VYT_TTKT]
GO
/****** Object:  Table [dbo].[FileStorage]    Script Date: 9/19/2020 5:43:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileStorage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[FileSize] [bigint] NULL,
	[Description] [nvarchar](50) NULL,
	[FilePath] [nvarchar](255) NULL,
	[FileType] [nvarchar](10) NULL,
 CONSTRAINT [PK_FileStorage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 9/19/2020 5:43:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[State] [int] NOT NULL,
	[Languages] [nvarchar](250) NOT NULL,
	[Duration] [bigint] NULL,
	[Notes] [nvarchar](max) NULL,
	[DocumentPages] [int] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobLog]    Script Date: 9/19/2020 5:43:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[State] [int] NOT NULL,
	[DocumentPages] [int] NOT NULL,
	[Duration] [bigint] NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_JobLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_Languages]  DEFAULT (N'Vietnamese') FOR [Languages]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_DocumentPages]  DEFAULT ((0)) FOR [DocumentPages]
GO
ALTER TABLE [dbo].[JobLog] ADD  CONSTRAINT [DF_JobLog_DocumentPages]  DEFAULT ((0)) FOR [DocumentPages]
GO
ALTER TABLE [dbo].[FileStorage]  WITH CHECK ADD  CONSTRAINT [FK_FileStorage_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileStorage] CHECK CONSTRAINT [FK_FileStorage_Job]
GO
