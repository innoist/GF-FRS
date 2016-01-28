CREATE TABLE [dbo].[LoadType] (
    [Value]    TINYINT       IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (15) NOT NULL,
    [StatusId] TINYINT       CONSTRAINT [DF_LoadType_StatusId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_LoadType] PRIMARY KEY CLUSTERED ([Value] ASC)
);

