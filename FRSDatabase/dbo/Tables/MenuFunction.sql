CREATE TABLE [dbo].[MenuFunction] (
    [MenuFunctionID]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [MenuFunctionCode] NVARCHAR (100) NOT NULL,
    [MenuFunctionName] NVARCHAR (255) NULL,
    [Description]      NVARCHAR (500) NULL,
    [NavigationKey]    NVARCHAR (100) NULL,
    [IsMenuItem]       BIT            NULL,
    [ParentFunctionID] BIGINT         NULL,
    [Depth]            INT            NOT NULL,
    [SortOrder]        INT            NOT NULL,
    [IsActive]         BIT            CONSTRAINT [DF__MenuFunct__IsAct__093F5D4E] DEFAULT ((1)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF__MenuFunct__IsDel__0A338187] DEFAULT ((0)) NOT NULL,
    [IsReadOnly]       BIT            CONSTRAINT [DF__MenuFunct__IsRea__0B27A5C0] DEFAULT ((1)) NOT NULL,
    [IsPrivate]        BIT            CONSTRAINT [DF__MenuFunct__IsPri__0C1BC9F9] DEFAULT ((0)) NOT NULL,
    [UserDomainKey]    BIGINT         NOT NULL,
    CONSTRAINT [PK115] PRIMARY KEY NONCLUSTERED ([MenuFunctionID] ASC),
    CONSTRAINT [RefMenuFunction305] FOREIGN KEY ([ParentFunctionID]) REFERENCES [dbo].[MenuFunction] ([MenuFunctionID])
);

