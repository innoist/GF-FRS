CREATE TABLE [dbo].[Status] (
    [Value] TINYINT       IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([Value] ASC)
);

