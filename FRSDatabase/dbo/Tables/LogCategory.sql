CREATE TABLE [dbo].[LogCategory] (
    [LogCategoryID] INT            IDENTITY (1, 1) NOT NULL,
    [CategoryName]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_LogCategory] PRIMARY KEY CLUSTERED ([LogCategoryID] ASC)
);

