CREATE TABLE [dbo].[LoadMetaData] (
    [LoadMetaDataId] TINYINT         IDENTITY (1, 1) NOT NULL,
    [LoadTypeId]     TINYINT         NOT NULL,
    [SourceId]       TINYINT         NOT NULL,
    [Header]         NVARCHAR (256)  NOT NULL,
    [Footer]         NVARCHAR (256)  NOT NULL,
    [Name]           NVARCHAR (100)  NOT NULL,
    [CurrencyId]     TINYINT         NOT NULL,
    [Description]    NVARCHAR (1024) NULL,
    [CreatedBy]      NVARCHAR (128)  NOT NULL,
    [CreatedOn]      DATETIME        CONSTRAINT [DF_LoadMetaData_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [ModifiedBy]     NVARCHAR (128)  NOT NULL,
    [ModifiedOn]     DATETIME        CONSTRAINT [DF_LoadMetaData_ModifiedOn] DEFAULT (getdate()) NOT NULL,
    [StatusId]       TINYINT         NOT NULL,
    CONSTRAINT [PK_LoadMetaData] PRIMARY KEY CLUSTERED ([LoadMetaDataId] ASC),
    CONSTRAINT [FK_LoadMetaData_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency] ([Value]),
    CONSTRAINT [FK_LoadMetaData_LoadType] FOREIGN KEY ([LoadTypeId]) REFERENCES [dbo].[LoadType] ([Value]),
    CONSTRAINT [FK_LoadMetaData_Source] FOREIGN KEY ([SourceId]) REFERENCES [dbo].[Source] ([Value]),
    CONSTRAINT [FK_LoadMetaData_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Value])
);

