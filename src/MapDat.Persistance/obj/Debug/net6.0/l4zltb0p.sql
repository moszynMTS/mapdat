BEGIN TRANSACTION;
GO

ALTER TABLE [CargoTransports] ADD [LoadingDate] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240105112617_AddLoadingDateToTransport', N'7.0.9');
GO

COMMIT;
GO

