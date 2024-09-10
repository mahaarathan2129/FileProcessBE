Insert into Users(FirstName,Email,PasswordHash,Role,IsActive)
values('admin','admin@gmail.com','admin','Admin',1)
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] NOT NULL,
	[ProductName] [varchar](255) NULL,
	[ProductDescription] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[ModifyFileStatus]    Script Date: 9/10/2024 4:48:50 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[ModifyFileStatus]
GO
/****** Object:  StoredProcedure [dbo].[GetFilesByUserId]    Script Date: 9/10/2024 4:48:50 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[GetFilesByUserId]
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 9/10/2024 4:48:50 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[AddUser]
GO
/****** Object:  StoredProcedure [dbo].[AddFile]    Script Date: 9/10/2024 4:48:50 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[AddFile]
GO
/****** Object:  StoredProcedure [dbo].[AddFile]    Script Date: 9/10/2024 4:48:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddFile]
    @FilePath NVARCHAR(255),
    @FileName NVARCHAR(255),
    @FileSize BIGINT,
    @CreatedBy BIGINT, 
    @UserId BIGINT
AS
BEGIN

IF NOT EXISTS(SELECT 1 FROM Files WHERE FileName = @FileName AND FilePath = @FilePath AND CreatedBy = @CreatedBy)
BEGIN
    INSERT INTO Files
    (
        FilePath,
        FileName,
        FileSize,
        Status,
        UploadDate,
        LastProcessedAt,
        ProcessingDuration,
        ProcessingDetails,
        CreatedAt,
        CreatedBy,
        UserId
    )
    VALUES
    (
        @FilePath,
        @FileName,
        @FileSize,
        'Pending',
        GETDATE(),
        NULL,
        NULL,
        '',
        GETDATE(), 
        @CreatedBy,
        @UserId
    );

    -- Optionally, return the ID of the newly inserted record
    SELECT SCOPE_IDENTITY() AS NewFileId;

 END;
ELSE
BEGIN
 
 SELECT ID AS NewFileId FROM Files WHERE FileName = @FileName AND FilePath = @FilePath;
 END
END;


GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 9/10/2024 4:48:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser]
    @FirstName NVARCHAR(200),
    @LastName NVARCHAR(200),
    @Email NVARCHAR(510),
    @PasswordHash NVARCHAR(MAX),
    @Role NVARCHAR(MAX),
    @IsActive BIT,
    @CreatedBy BIGINT
AS
BEGIN

    -- Insert into the table
    INSERT INTO Users(
        FirstName, 
        LastName, 
        Email, 
        PasswordHash, 
        Role, 
        IsActive, 
        CreatedAt, 
        CreatedBy
    )
    VALUES (
        @FirstName, 
        @LastName, 
        @Email, 
        @PasswordHash, 
        @Role, 
        @IsActive, 
        GETDATE(), 
        @CreatedBy
    );
    
    -- Optionally, return the ID of the newly inserted record
    SELECT SCOPE_IDENTITY() AS NewRecordId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetFilesByUserId]    Script Date: 9/10/2024 4:48:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFilesByUserId]
    @UserId BIGINT
AS
BEGIN
    SELECT 
        Id,
        FilePath,
        FileName,
        FileSize,
        Status,
        UploadDate,
        LastProcessedAt,
        ProcessingDuration,
        ProcessingDetails,
        CreatedAt,
        ModifiedAt,
        CreatedBy,
        ModifiedBy,
        UserId
    FROM 
        Files
    WHERE 
        UserId = @UserId;
END;

GO
/****** Object:  StoredProcedure [dbo].[ModifyFileStatus]    Script Date: 9/10/2024 4:48:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ModifyFileStatus]
    @FileName NVARCHAR(200),
    @FilePath NVARCHAR(200),
    @Status NVARCHAR(200),
    @Id bigint
AS
BEGIN
declare @fileId bigint;
select @fileId = Id from Files where FileName = @FileName AND FilePath = @FilePath;
    insert into ProcessingLogs(FileId,LastProcessedOffset,LastUpdatedAt,RetryCount)
    values (@fileId,0,GETDATE(),0)
   Update Files set Status = @Status WHERE Id = @fileId;
END;

GO
