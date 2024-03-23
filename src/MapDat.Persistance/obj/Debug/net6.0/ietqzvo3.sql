BEGIN TRANSACTION;
GO

CREATE TABLE [PackingList] (
    [Id] uniqueidentifier NOT NULL,
    [LabelId] uniqueidentifier NOT NULL,
    [TransportId] uniqueidentifier NOT NULL,
    [Weight] real NULL,
    [Info] nvarchar(max) NULL,
    [Lp] int NULL,
    [IsActive] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [LastModified] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PackingList] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231020072923_PackingListCreate', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [HistoryGroup] ADD [PackingId] uniqueidentifier NULL;
GO

CREATE INDEX [IX_PackingList_LabelId] ON [PackingList] ([LabelId]);
GO

CREATE INDEX [IX_PackingList_TransportId] ON [PackingList] ([TransportId]);
GO

CREATE INDEX [IX_HistoryGroup_PackingId] ON [HistoryGroup] ([PackingId]);
GO

ALTER TABLE [HistoryGroup] ADD CONSTRAINT [FK_HistoryGroup_PackingList_PackingId] FOREIGN KEY ([PackingId]) REFERENCES [PackingList] ([Id]);
GO

ALTER TABLE [PackingList] ADD CONSTRAINT [FK_PackingList_CargoTransports_TransportId] FOREIGN KEY ([TransportId]) REFERENCES [CargoTransports] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [PackingList] ADD CONSTRAINT [FK_PackingList_Labels_LabelId] FOREIGN KEY ([LabelId]) REFERENCES [Labels] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231020103117_AddPackingSToHistoryGroup', N'7.0.9');
GO

COMMIT;
GO

