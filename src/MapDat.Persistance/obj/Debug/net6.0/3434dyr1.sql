BEGIN TRANSACTION;
GO

ALTER TABLE [Labels] ADD [LpOffline] int NULL;
GO

ALTER TABLE [Labels] ADD [UserLp] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231123120237_offlineLabelsInfo', N'7.0.9');
GO

COMMIT;
GO

