BEGIN TRANSACTION;
GO

ALTER TABLE [Labels] ADD [PartName] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230906115017_AddsPartNameToLabel', N'7.0.9');
GO

COMMIT;
GO

