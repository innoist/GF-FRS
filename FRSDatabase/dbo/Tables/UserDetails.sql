CREATE TABLE [dbo].[UserDetails] (
    [UserDetailId]    INT            IDENTITY (1, 1) NOT NULL,
    [CompanyName]     NVARCHAR (250) NOT NULL,
    [CountryName]     NVARCHAR (40)  NOT NULL,
    [Address]         NVARCHAR (500) NOT NULL,
    [AccountType]     NVARCHAR (10)  NOT NULL,
    [UserId]          NVARCHAR (128) NOT NULL,
    [CompanyShortUrl] NVARCHAR (250) NULL,
    CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED ([UserDetailId] ASC),
    CONSTRAINT [FK_UserDetails_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

