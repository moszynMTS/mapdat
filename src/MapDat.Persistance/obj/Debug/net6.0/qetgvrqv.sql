BEGIN TRANSACTION;
GO

ALTER TABLE [Projects] ADD [FullName] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230830075142_AddFullNameColumnToProject', N'7.0.9');
GO

COMMIT;
GO

