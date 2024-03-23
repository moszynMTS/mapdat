BEGIN TRANSACTION;
GO

CREATE TABLE [Attachments] (
    [Id] uniqueidentifier NOT NULL,
    [AreaId] uniqueidentifier NULL,
    [SectionId] uniqueidentifier NULL,
    [FileName] nvarchar(max) NOT NULL,
    [Lp] int NULL,
    [IsActive] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [LastModified] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231229123356_AddAttachments', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Comments] DROP CONSTRAINT [FK_Comments_Photos_PhotoId];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Comments]') AND [c].[name] = N'PhotoId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Comments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Comments] ALTER COLUMN [PhotoId] uniqueidentifier NULL;
GO

ALTER TABLE [Comments] ADD [AttachmentId] uniqueidentifier NULL;
GO

CREATE INDEX [IX_Comments_AttachmentId] ON [Comments] ([AttachmentId]);
GO

CREATE INDEX [IX_Attachments_AreaId] ON [Attachments] ([AreaId]);
GO

CREATE INDEX [IX_Attachments_SectionId] ON [Attachments] ([SectionId]);
GO

ALTER TABLE [Attachments] ADD CONSTRAINT [FK_Attachments_Areas_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [Areas] ([Id]);
GO

ALTER TABLE [Attachments] ADD CONSTRAINT [FK_Attachments_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]);
GO

ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Attachments_AttachmentId] FOREIGN KEY ([AttachmentId]) REFERENCES [Attachments] ([Id]);
GO

ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Photos_PhotoId] FOREIGN KEY ([PhotoId]) REFERENCES [Photos] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231229134740_UpdateCommentsForAttachments', N'7.0.9');
GO

COMMIT;
GO

