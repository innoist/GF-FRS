CREATE TABLE [dbo].[MT940CustomerStatement] (
    [MT940CustomerStatementId]  BIGINT          IDENTITY (1, 1) NOT NULL,
    [MT940LoadId]               BIGINT          NOT NULL,
    [Sequence]                  TINYINT         NOT NULL,
    [ReadOnly]                  BIT             CONSTRAINT [DF_MT940CustomerStatement_ReadOnly] DEFAULT ((0)) NOT NULL,
    [AccountNumber]             NVARCHAR (16)   NOT NULL,
    [ClosingAvailableBalanceId] BIGINT          NOT NULL,
    [ClosingBalanceId]          BIGINT          NULL,
    [Description]               NVARCHAR (1024) NULL,
    [ForwardAvailableBalanceId] BIGINT          NULL,
    [OpeningBalanceId]          BIGINT          NULL,
    [ReleatedMessage]           NVARCHAR (1024) NULL,
    [SequenceNumber]            INT             NOT NULL,
    [StatementNumber]           INT             NOT NULL,
    [TransactionReference]      NVARCHAR (20)   NOT NULL,
    [TransactionCount]          INT             NOT NULL,
    CONSTRAINT [PK_CustomerStatement] PRIMARY KEY CLUSTERED ([MT940CustomerStatementId] ASC),
    CONSTRAINT [FK_CustomerStatement_Balance_ClosingAviBal] FOREIGN KEY ([ClosingAvailableBalanceId]) REFERENCES [dbo].[MT940Balance] ([MT940BalanceId]),
    CONSTRAINT [FK_CustomerStatement_Balance_ClosingBal] FOREIGN KEY ([ClosingBalanceId]) REFERENCES [dbo].[MT940Balance] ([MT940BalanceId]),
    CONSTRAINT [FK_CustomerStatement_Balance_ForAvailBal] FOREIGN KEY ([ForwardAvailableBalanceId]) REFERENCES [dbo].[MT940Balance] ([MT940BalanceId]),
    CONSTRAINT [FK_CustomerStatement_Balance_OpenBal] FOREIGN KEY ([OpeningBalanceId]) REFERENCES [dbo].[MT940Balance] ([MT940BalanceId]),
    CONSTRAINT [FK_MT940CustomerStatement_MT940Load] FOREIGN KEY ([MT940LoadId]) REFERENCES [dbo].[MT940Load] ([MT940LoadId])
);

