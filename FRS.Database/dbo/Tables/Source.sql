CREATE TABLE [dbo].[Source] (
    [Value]    TINYINT       IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (15) NOT NULL,
    [StatusId] TINYINT       CONSTRAINT [DF_Source_StatusId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Source] PRIMARY KEY CLUSTERED ([Value] ASC)
);

