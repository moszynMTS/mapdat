BEGIN TRANSACTION;
GO

ALTER TABLE [Projects] ADD [UseOldLabels] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231201102340_UseOldLabelsForProject', N'7.0.9');
GO

COMMIT;
GO

