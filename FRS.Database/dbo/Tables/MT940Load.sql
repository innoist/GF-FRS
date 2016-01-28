CREATE TABLE [dbo].[MT940Load] (
    [MT940LoadId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Path]                   NVARCHAR (50)  NOT NULL,
    [FileName]               NVARCHAR (50)  NOT NULL,
    [FileExtension]          NVARCHAR (10)  NOT NULL,
    [StatusId]               TINYINT        NOT NULL,
    [CreatedBy]              NVARCHAR (128) NOT NULL,
    [CreatedOn]              DATETIME       CONSTRAINT [DF_MT940LoadDetail_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [ModifiedBy]             NVARCHAR (128) NOT NULL,
    [ModifiedOn]             DATETIME       CONSTRAINT [DF_MT940LoadDetail_ModifiedOn] DEFAULT (getdate()) NOT NULL,
    [CustomerStatementCount] INT            NOT NULL,
    [FileContentId]          BIGINT         NOT NULL,
    CONSTRAINT [PK_MT940LoadDetail] PRIMARY KEY CLUSTERED ([MT940LoadId] ASC),
    CONSTRAINT [FK_MT940Load_FileContent] FOREIGN KEY ([FileContentId]) REFERENCES [dbo].[FileContent] ([FileContentId]),
    CONSTRAINT [FK_MT940Load_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Value])
);

