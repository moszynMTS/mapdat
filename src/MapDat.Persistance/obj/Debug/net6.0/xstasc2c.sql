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

