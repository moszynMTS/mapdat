BEGIN TRANSACTION;
GO

CREATE TABLE [Comments] (
    [Id] uniqueidentifier NOT NULL,
    [PosXPercent] real NOT NULL,
    [PosYPercent] real NOT NULL,
    [Comment] nvarchar(max) NOT NULL,
    [PhotoId] uniqueidentifier NOT NULL,
    [Lp] int NULL,
    [IsActive] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [LastModified] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Photos_PhotoId] FOREIGN KEY ([PhotoId]) REFERENCES [Photos] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Comments_PhotoId] ON [Comments] ([PhotoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230928073254_createCommentsForPhotos', N'7.0.9');
GO

COMMIT;
GO

