ALTER TABLE [AspNetUsers] ADD [Lp] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231123140311_AddLpToUser', N'3.1.4');

GO

