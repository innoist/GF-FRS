CREATE TABLE [dbo].[Employee] (
    [EmployeeId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [EmployeeName] NVARCHAR (128) NULL,
    [Designation]  NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);

