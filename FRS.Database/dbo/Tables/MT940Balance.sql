CREATE TABLE [dbo].[MT940Balance] (
    [MT940BalanceId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [CurrencyId]     TINYINT         NOT NULL,
    [DebitOrCredit]  CHAR (1)        CONSTRAINT [DF_Balance_DebitOrCredit] DEFAULT ('D') NOT NULL,
    [EntryDate]      DATETIME        NOT NULL,
    [Value]          DECIMAL (30, 2) NOT NULL,
    CONSTRAINT [PK_Balance] PRIMARY KEY CLUSTERED ([MT940BalanceId] ASC),
    CONSTRAINT [FK_Balance_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency] ([Value])
);

