CREATE TABLE [dbo].[MenuRight] (
    [MenuRightId] INT            IDENTITY (1, 1) NOT NULL,
    [Menu_MenuId] INT            NULL,
    [Role_Id]     NVARCHAR (128) NULL,
    CONSTRAINT [PK_dbo.MenuRight] PRIMARY KEY CLUSTERED ([MenuRightId] ASC),
    CONSTRAINT [FK_dbo.MenuRight_dbo.AspNetRoles_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[AspNetRoles] ([Id]),
    CONSTRAINT [FK_dbo.MenuRight_dbo.Menu_Menu_MenuId] FOREIGN KEY ([Menu_MenuId]) REFERENCES [dbo].[Menu] ([MenuId])
);

