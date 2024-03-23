BEGIN TRANSACTION;
GO

ALTER TABLE [Projects] ADD [UseSpecialLabels] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231116083420_UseSpecialLabelsToProject', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Labels] ADD [OrderItem] nvarchar(max) NULL;
GO

ALTER TABLE [Labels] ADD [OrderNr] nvarchar(max) NULL;
GO

ALTER TABLE [Labels] ADD [Quantity] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231116091317_addNewValuesToLabelEntityUsedWhenSpecialLabel', N'7.0.9');
GO

COMMIT;
GO

