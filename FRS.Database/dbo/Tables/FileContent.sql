CREATE TABLE [dbo].[FileContent] (
    [FileContentId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [FileContent]   VARBINARY (MAX) NOT NULL,
    [Description]   NVARCHAR (124)  NULL,
    CONSTRAINT [PK_FileContent] PRIMARY KEY CLUSTERED ([FileContentId] ASC)
);

