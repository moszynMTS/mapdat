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

BEGIN TRANSACTION;
GO

CREATE TABLE [PageViewHistory] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerName] nvarchar(max) NOT NULL,
    [CustomerEmail] nvarchar(max) NOT NULL,
    [PageName] nvarchar(max) NULL,
    [ProjectId] uniqueidentifier NULL,
    [LabelId] uniqueidentifier NULL,
    [CargoId] uniqueidentifier NULL,
    [TransportId] uniqueidentifier NULL,
    [Lp] int NULL,
    [IsActive] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [LastModified] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PageViewHistory] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231012085325_AddPageViewHistory', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE INDEX [IX_PageViewHistory_CargoId] ON [PageViewHistory] ([CargoId]);
GO

CREATE INDEX [IX_PageViewHistory_LabelId] ON [PageViewHistory] ([LabelId]);
GO

CREATE INDEX [IX_PageViewHistory_ProjectId] ON [PageViewHistory] ([ProjectId]);
GO

CREATE INDEX [IX_PageViewHistory_TransportId] ON [PageViewHistory] ([TransportId]);
GO

ALTER TABLE [PageViewHistory] ADD CONSTRAINT [FK_PageViewHistory_CargoTransports_TransportId] FOREIGN KEY ([TransportId]) REFERENCES [CargoTransports] ([Id]);
GO

ALTER TABLE [PageViewHistory] ADD CONSTRAINT [FK_PageViewHistory_Cargoes_CargoId] FOREIGN KEY ([CargoId]) REFERENCES [Cargoes] ([Id]);
GO

ALTER TABLE [PageViewHistory] ADD CONSTRAINT [FK_PageViewHistory_Labels_LabelId] FOREIGN KEY ([LabelId]) REFERENCES [Labels] ([Id]);
GO

ALTER TABLE [PageViewHistory] ADD CONSTRAINT [FK_PageViewHistory_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231013122536_AddVirtualFiledsINPageViewHistory', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PageViewHistory] ADD [SectionId] uniqueidentifier NULL;
GO

CREATE INDEX [IX_PageViewHistory_SectionId] ON [PageViewHistory] ([SectionId]);
GO

ALTER TABLE [PageViewHistory] ADD CONSTRAINT [FK_PageViewHistory_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231017055822_AddSectionIdToPageViewHistory', N'7.0.9');
GO

COMMIT;
GO

