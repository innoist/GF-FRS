CREATE TABLE [dbo].[WebApiUser] (
    [Id]            INT            NOT NULL,
    [UserName]      NVARCHAR (256) NULL,
    [PasswordHash]  NVARCHAR (MAX) NULL,
    [UserDomainKey] BIGINT         NOT NULL,
    CONSTRAINT [PK_WebApiUser] PRIMARY KEY CLUSTERED ([Id] ASC)
);



