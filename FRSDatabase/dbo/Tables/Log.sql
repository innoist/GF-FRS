CREATE TABLE [dbo].[Log] (
    [LogID]              INT            IDENTITY (1, 1) NOT NULL,
    [EventID]            INT            NOT NULL,
    [Priority]           INT            NOT NULL,
    [Severity]           NVARCHAR (MAX) NULL,
    [Title]              NVARCHAR (MAX) NULL,
    [Timestamp]          DATETIME       NOT NULL,
    [MachineName]        NVARCHAR (MAX) NULL,
    [AppDomainName]      NVARCHAR (MAX) NULL,
    [ProcessID]          NVARCHAR (MAX) NULL,
    [ProcessName]        NVARCHAR (MAX) NULL,
    [ThreadName]         NVARCHAR (MAX) NULL,
    [Win32ThreadId]      NVARCHAR (MAX) NULL,
    [Message]            NVARCHAR (MAX) NULL,
    [FormattedMessage]   NVARCHAR (MAX) NULL,
    [HandlingInstanceId] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([LogID] ASC)
);

