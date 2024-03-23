BEGIN TRANSACTION;
GO

ALTER TABLE [Vehicles] ADD [Lp] int NULL;
GO

ALTER TABLE [UserProjectPermissions] ADD [Lp] int NULL;
GO

ALTER TABLE [Tasks] ADD [Lp] int NULL;
GO

ALTER TABLE [Sections] ADD [Lp] int NULL;
GO

ALTER TABLE [Projects] ADD [Lp] int NULL;
GO

ALTER TABLE [Photos] ADD [Lp] int NULL;
GO

ALTER TABLE [Labels] ADD [Lp] int NULL;
GO

ALTER TABLE [Icons] ADD [Lp] int NULL;
GO

ALTER TABLE [HistoryGroup] ADD [Lp] int NULL;
GO

ALTER TABLE [History] ADD [Lp] int NULL;
GO

ALTER TABLE [Employees] ADD [Lp] int NULL;
GO

ALTER TABLE [Customers] ADD [Lp] int NULL;
GO

ALTER TABLE [Codes] ADD [Lp] int NULL;
GO

ALTER TABLE [CargoTransports] ADD [Lp] int NULL;
GO

ALTER TABLE [Cargoes] ADD [Lp] int NULL;
GO

ALTER TABLE [Areas] ADD [Lp] int NULL;
GO

ALTER TABLE [Addresses] ADD [Lp] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230905071448_addingLp', N'7.0.9');
GO

COMMIT;
GO

