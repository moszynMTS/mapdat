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

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Addresses]') AND [c].[name] = N'Address');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Addresses] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Addresses] DROP COLUMN [Address];
GO

EXEC sp_rename N'[Addresses].[Province]', N'Street', N'COLUMN';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Addresses]') AND [c].[name] = N'PostalCode');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Addresses] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Addresses] ALTER COLUMN [PostalCode] nvarchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Addresses]') AND [c].[name] = N'City');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Addresses] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Addresses] ALTER COLUMN [City] nvarchar(max) NULL;
GO

ALTER TABLE [Addresses] ADD [CustomerId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

ALTER TABLE [Addresses] ADD [Email] nvarchar(max) NULL;
GO

ALTER TABLE [Addresses] ADD [Phone] nvarchar(max) NULL;
GO

CREATE INDEX [IX_Addresses_CustomerId] ON [Addresses] ([CustomerId]);
GO

ALTER TABLE [Addresses] ADD CONSTRAINT [FK_Addresses_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230921080522_AddedAddressesToCustomer', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [HistoryGroup] ADD [AddressId] uniqueidentifier NULL;
GO

CREATE INDEX [IX_HistoryGroup_AddressId] ON [HistoryGroup] ([AddressId]);
GO

ALTER TABLE [HistoryGroup] ADD CONSTRAINT [FK_HistoryGroup_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230921094927_AddedAddressToHistory', N'7.0.9');
GO

COMMIT;
GO

