IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'discount') IS NULL EXEC(N'CREATE SCHEMA [discount];');

GO

CREATE TABLE [discount].[Coupons] (
    [Id] int NOT NULL IDENTITY,
    [ProductName] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Amount] int NOT NULL,
    CONSTRAINT [PK_Coupons] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210411121516_InitialMigration', N'3.1.9');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[discount].[Coupons]') AND [c].[name] = N'ProductName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [discount].[Coupons] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [discount].[Coupons] ALTER COLUMN [ProductName] nvarchar(450) NULL;

GO

CREATE UNIQUE INDEX [IX_Coupons_ProductName] ON [discount].[Coupons] ([ProductName]) WHERE [ProductName] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210411182728_UpdateCouponProductName', N'3.1.9');

GO

