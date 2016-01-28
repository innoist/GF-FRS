CREATE TABLE [dbo].[Currency] (
    [Value] TINYINT       IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [Sign]  NVARCHAR (10) NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([Value] ASC)
);

