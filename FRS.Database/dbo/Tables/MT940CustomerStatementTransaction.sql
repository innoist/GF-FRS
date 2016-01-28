CREATE TABLE [dbo].[MT940CustomerStatementTransaction] (
    [MT940CustomerStatementTransactionId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [MT940CustomerStatementId]            BIGINT          NOT NULL,
    [Sequence]                            TINYINT         NOT NULL,
    [ReadOnly]                            BIT             CONSTRAINT [DF_MT940CustomerStatementTransaction_ReadOnly] DEFAULT ((0)) NOT NULL,
    [Amount]                              MONEY           NOT NULL,
    [DebitOrCredit]                       CHAR (1)        CONSTRAINT [DF_MT940CustomerStatementTransaction_DebitOrCredit] DEFAULT ('D') NOT NULL,
    [Description]                         NVARCHAR (1024) NULL,
    [EntryDate]                           DATETIME        NULL,
    [FundsCode]                           NVARCHAR (50)   NULL,
    [Reference]                           NVARCHAR (16)   NULL,
    [TransactionType]                     NVARCHAR (4)    NULL,
    [Value]                               NVARCHAR (1024) NULL,
    [ValueDate]                           DATETIME        NOT NULL,
    CONSTRAINT [PK_MT940CustomerStatementTransaction] PRIMARY KEY CLUSTERED ([MT940CustomerStatementTransactionId] ASC),
    CONSTRAINT [FK_MT940CustomerStatementTransaction_MT940CustomerStatement] FOREIGN KEY ([MT940CustomerStatementId]) REFERENCES [dbo].[MT940CustomerStatement] ([MT940CustomerStatementId])
);

