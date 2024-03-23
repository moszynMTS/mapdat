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

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Labels]') AND [c].[name] = N'Quantity');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Labels] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Labels] DROP COLUMN [Quantity];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231117082034_DeleteQuantityPropertyFromLabel', N'7.0.9');
GO

COMMIT;
GO

