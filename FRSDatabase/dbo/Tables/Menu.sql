CREATE TABLE [dbo].[Menu] (
    [MenuId]               INT            IDENTITY (1, 1) NOT NULL,
    [MenuKey]              INT            NOT NULL,
    [MenuTitle]            NVARCHAR (MAX) NULL,
    [SortOrder]            INT            NOT NULL,
    [MenuTargetController] NVARCHAR (MAX) NULL,
    [MenuImagePath]        NVARCHAR (MAX) NULL,
    [MenuFunction]         NVARCHAR (MAX) NULL,
    [PermissionKey]        NVARCHAR (MAX) NULL,
    [IsRootItem]           BIT            NOT NULL,
    [ParentItem_MenuId]    INT            NULL,
    [MenuTitleArbic]       NVARCHAR (250) NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([MenuId] ASC),
    CONSTRAINT [RefMenu01] FOREIGN KEY ([ParentItem_MenuId]) REFERENCES [dbo].[Menu] ([MenuId])
);



