BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Codes].[Sample]', N'Description', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230919123912_ChangedSampleToDescInCode', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Labels] ADD [PhotosNeeded] bit NOT NULL DEFAULT CAST(1 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230920072215_Labels-PhtosNeededColumn', N'7.0.9');
GO

COMMIT;
GO

