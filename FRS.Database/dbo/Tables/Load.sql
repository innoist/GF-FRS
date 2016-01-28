CREATE TABLE [dbo].[Load] (
    [LoadId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [LoadMetaDataId] TINYINT        NOT NULL,
    [MT940LoadId]    BIGINT         NULL,
    [Start]          DATETIME       NOT NULL,
    [Finish]         DATETIME       NULL,
    [InProgress]     BIT            CONSTRAINT [DF_Load_InProgress] DEFAULT ((1)) NOT NULL,
    [ReadOnly]       BIT            CONSTRAINT [DF_Load_Locked] DEFAULT ((0)) NOT NULL,
    [CreatedBy]      NVARCHAR (128) NOT NULL,
    [CreatedOn]      DATETIME       CONSTRAINT [DF_Load_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [ModifiedBy]     NVARCHAR (128) NOT NULL,
    [ModifiedOn]     DATETIME       CONSTRAINT [DF_Load_ModifiedOn] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Load] PRIMARY KEY CLUSTERED ([LoadId] ASC),
    CONSTRAINT [FK_Load_LoadMetaData] FOREIGN KEY ([LoadMetaDataId]) REFERENCES [dbo].[LoadMetaData] ([LoadMetaDataId]),
    CONSTRAINT [FK_Load_MT940Load] FOREIGN KEY ([MT940LoadId]) REFERENCES [dbo].[MT940Load] ([MT940LoadId])
);

