USE [VYT_TTKT]
GO
/****** Object:  Table [dbo].[FileStorage]    Script Date: 23/9/2020 1:19:03 AM ******/
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
/****** Object:  Table [dbo].[Job]    Script Date: 23/9/2020 1:19:03 AM ******/
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
/****** Object:  Table [dbo].[JobLog]    Script Date: 23/9/2020 1:19:03 AM ******/
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
	[Processed] [datetime2](7) NULL,
 CONSTRAINT [PK_JobLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_Created]  DEFAULT (getdate()) FOR [Created]
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
/****** Object:  StoredProcedure [dbo].[usp_Job_Add]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Job_Add] 
	@name nvarchar(255),	
	@languages nvarchar(250)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

	BEGIN TRANSACTION;	
	
	INSERT INTO Job (Name, Languages, State)
	VALUES (@name, @languages, 0);

	select * from Job where id = SCOPE_IDENTITY();	

	COMMIT;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_AddFile]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Job_AddFile] 
	@jobId int,	
	@type int,
	@fileSize bigint,
	@fileType nvarchar(10),
	@filePath nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	declare @desc nvarchar(50)	

	if (@type = 0) begin
		SET @desc = 'Input Image'
	end else if (@type = 1) begin
		SET @desc = 'OCR Result'	
	end		

	BEGIN TRANSACTION;	
	
	DELETE FROM FileStorage
	WHERE JobId = @jobId AND (Type = @type AND Type != 1)

	INSERT INTO FileStorage(JobId, FilePath, Type, FileSize, FileType, Description)
	VALUES (@jobId, @filePath, @type, @fileSize, @fileType, @desc);	
	select * from FileStorage where id = SCOPE_IDENTITY();

	COMMIT;	
	
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_Delete]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Job_Delete] 
	@jobId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

	BEGIN TRANSACTION;	
	
	DELETE FROM Job WHERE Id = @jobId

	COMMIT;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_Get]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Job_Get]
	@jobId int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	SELECT Id, Name, Created, State, Languages, Duration, DocumentPages, Notes
	FROM Job 
	WHERE Id = @jobId;

END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_GetByState]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Job_GetByState]
	@state int,
	@limit int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if (@limit < 1)
	begin
		SELECT * FROM JOB WHERE State = @state
	end
	else
	begin
		SELECT  TOP (@limit) *
		FROM Job
		WHERE State = @state
	end
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_GetFile]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Job_GetFile] 
	@jobId int,
	@type int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if @type < 0
	begin
		select * from FileStorage where JobId = @jobId
	end
	else 
	begin
		select * from FileStorage where JobId = @jobId and Type = @type
	end
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_GetPage]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Job_GetPage]
	@pageIndex int = 0,
	@pageSize int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	if (@pageSize <= 0) begin
		SELECT * FROM Job		
	end else begin
		WITH OrderedJobs AS
		(
			SELECT *,
				ROW_NUMBER() OVER (ORDER BY J.Id) AS RowNumber
			FROM Job J		
		)
		SELECT *
		FROM OrderedJobs
		WHERE (RowNumber - 1) BETWEEN @pageIndex * @pageSize AND ((@pageIndex + 1) * @pageSize - 1)
	end
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_Update]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Job_Update] 
	@id int,
	@state int,
	@duration bigint,
	@notes nvarchar(max),
	@documentPages int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

	BEGIN TRANSACTION;	
	
	UPDATE Job 
	SET State = @state, Duration = @duration, Notes = @notes, DocumentPages = @documentPages
	WHERE Id = @id

	COMMIT;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_JobLog_Add]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_JobLog_Add] 
	@name nvarchar(255),	
	@created datetime2,
	@state int,
	@documentPages int,
	@duration bigint,
	@processed datetime2,
	@notes nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

	BEGIN TRANSACTION;	
	
	INSERT INTO JobLog (Name, Created, State, DocumentPages, Duration, Processed, Notes)
	VALUES (@name, @created, @state, @documentPages, @duration, @processed, @notes);

	select * from JobLog where id = SCOPE_IDENTITY();	

	COMMIT;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_JobLog_GetPage]    Script Date: 23/9/2020 1:19:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_JobLog_GetPage]
	@pageIndex int = 0,
	@pageSize int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	if (@pageSize <= 0) begin
		SELECT * FROM JobLog		
	end else begin
		WITH OrderedJobs AS
		(
			SELECT *,
				ROW_NUMBER() OVER (ORDER BY J.Id) AS RowNumber
			FROM JobLog J		
		)
		SELECT *
		FROM OrderedJobs
		WHERE (RowNumber - 1) BETWEEN @pageIndex * @pageSize AND ((@pageIndex + 1) * @pageSize - 1)
	end
END
GO
