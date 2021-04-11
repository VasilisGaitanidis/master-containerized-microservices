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

