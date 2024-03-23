BEGIN TRANSACTION;
GO

ALTER TABLE [Projects] ADD [AddressId] uniqueidentifier NULL;
GO

CREATE INDEX [IX_Projects_AddressId] ON [Projects] ([AddressId]);
GO

ALTER TABLE [Projects] ADD CONSTRAINT [FK_Projects_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230929081321_AddAddressIdToProject', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Cargoes] ADD [AddressId] uniqueidentifier NULL;
GO

ALTER TABLE [Cargoes] ADD [Departure] nvarchar(max) NOT NULL DEFAULT N'';
GO

CREATE INDEX [IX_Cargoes_AddressId] ON [Cargoes] ([AddressId]);
GO

ALTER TABLE [Cargoes] ADD CONSTRAINT [FK_Cargoes_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230929115536_AddDepartureToCargo', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CargoTransports]') AND [c].[name] = N'Departure');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CargoTransports] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [CargoTransports] ALTER COLUMN [Departure] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230929141107_changeDateDepartureToNullable', N'7.0.9');
GO

COMMIT;
GO

