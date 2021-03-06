USE [VYT_TTKT]
GO
/****** Object:  Table [dbo].[FileStorage]    Script Date: 11/4/2020 2:14:46 AM ******/
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
	[UserId] [int] NULL,
 CONSTRAINT [PK_FileStorage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 11/4/2020 2:14:46 AM ******/
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
	[Processed] [datetime2](7) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/4/2020 2:14:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_Languages]  DEFAULT (N'Vietnamese') FOR [Languages]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_Job_DocumentPages]  DEFAULT ((0)) FOR [DocumentPages]
GO
ALTER TABLE [dbo].[FileStorage]  WITH CHECK ADD  CONSTRAINT [FK_FileStorage_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileStorage] CHECK CONSTRAINT [FK_FileStorage_Job]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_User]
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_Add]    Script Date: 11/4/2020 2:14:48 AM ******/
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
	@userId int,
	@name nvarchar(255),	
	@languages nvarchar(250)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

	BEGIN TRANSACTION;	
	
	INSERT INTO Job (Name, Languages, State, UserId)
	VALUES (@name, @languages, 0, @userId);

	select * from Job where id = SCOPE_IDENTITY();	

	COMMIT;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_AddFile]    Script Date: 11/4/2020 2:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[usp_Job_Delete]    Script Date: 11/4/2020 2:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[usp_Job_Get]    Script Date: 11/4/2020 2:14:48 AM ******/
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
	SELECT Id, Name, Created, State, Languages, Duration, DocumentPages, Notes, Processed
	FROM Job 
	WHERE Id = @jobId;

END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_GetByState]    Script Date: 11/4/2020 2:14:48 AM ******/
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
	@userId int,
	@state int,
	@limit int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if (@limit < 1)
	begin
		SELECT * FROM JOB WHERE State = @state AND (UserId = @userId OR @userId = 0)
	end
	else
	begin
		SELECT  TOP (@limit) *
		FROM Job
		WHERE State = @state AND (UserId = @userId OR @userId = 0)
	end
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_GetFile]    Script Date: 11/4/2020 2:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[usp_Job_GetPage]    Script Date: 11/4/2020 2:14:48 AM ******/
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
	@userId int,
	@pageIndex int = 0,
	@pageSize int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	if (@pageSize <= 0) begin
		SELECT * FROM Job WHERE UserId = @userId ORDER BY Id desc		
	end else begin
		WITH OrderedJobs AS
		(
			SELECT *,
				ROW_NUMBER() OVER (ORDER BY J.Id desc) AS RowNumber
			FROM Job J 
			WHERE UserId = @userId
		)
		SELECT *
		FROM OrderedJobs
		WHERE (RowNumber - 1) BETWEEN @pageIndex * @pageSize AND ((@pageIndex + 1) * @pageSize - 1)
		ORDER BY Id desc
	end
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Job_Update]    Script Date: 11/4/2020 2:14:48 AM ******/
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
	@documentPages int,
	@processed datetime2
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

	BEGIN TRANSACTION;	
	
	UPDATE Job 
	SET State = @state, Duration = @duration, Notes = @notes, DocumentPages = @documentPages, Processed = @processed
	WHERE Id = @id

	COMMIT;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_User_Add]    Script Date: 11/4/2020 2:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_User_Add] 
	@email nvarchar(50),
	@passwordHash nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    INSERT INTO [dbo].[User] (Email, PasswordHash)
	VALUES (@email, @passwordHash);

	select * from [dbo].[User] where Id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[usp_User_Get]    Script Date: 11/4/2020 2:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_User_Get] 
	@email nvarchar(50),
	@passwordHash nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    SELECT TOP 1 * FROM [dbo].[User]
	WHERE Email = @email AND PasswordHash = @passwordHash;

END
GO
