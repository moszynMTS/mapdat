BEGIN TRANSACTION;
GO

ALTER TABLE [Customers] DROP CONSTRAINT [FK_Customers_Projects_ProjectId];
GO

EXEC sp_rename N'[Customers].[ProjectId]', N'ProjectEntityId', N'COLUMN';
GO

EXEC sp_rename N'[Customers].[IX_Customers_ProjectId]', N'IX_Customers_ProjectEntityId', N'INDEX';
GO

ALTER TABLE [Customers] ADD [CustomerId] uniqueidentifier NULL;
GO

ALTER TABLE [Customers] ADD CONSTRAINT [FK_Customers_Projects_ProjectEntityId] FOREIGN KEY ([ProjectEntityId]) REFERENCES [Projects] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230913094403_ChangeProjectIdToCustimerIdInCustomer', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customers] ADD [Nip] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230913120238_AddNIPFieldToCustomer', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Labels] ADD [ParentLabelId] uniqueidentifier NULL;
GO

ALTER TABLE [Labels] ADD [PrintingCount] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_Labels_ParentLabelId] ON [Labels] ([ParentLabelId]);
GO

ALTER TABLE [Labels] ADD CONSTRAINT [FK_Labels_Labels_ParentLabelId] FOREIGN KEY ([ParentLabelId]) REFERENCES [Labels] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230914084150_printCountAndParentForLabel', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Projects] ADD [Departure] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Projects] ADD [Destination] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230914141237_DestinationAndDepartureToProject', N'7.0.9');
GO

COMMIT;
GO

