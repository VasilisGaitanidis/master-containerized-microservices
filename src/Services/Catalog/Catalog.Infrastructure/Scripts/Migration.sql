IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'catalog') IS NULL EXEC(N'CREATE SCHEMA [catalog];');

GO

CREATE TABLE [catalog].[CatalogTypes] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CatalogTypes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [catalog].[CatalogItems] (
    [Id] uniqueidentifier NOT NULL,
    [CatalogTypeId] uniqueidentifier NOT NULL,
    [Description] nvarchar(max) NULL,
    [Name] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Stock] int NOT NULL,
    CONSTRAINT [PK_CatalogItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CatalogItems_CatalogTypes_CatalogTypeId] FOREIGN KEY ([CatalogTypeId]) REFERENCES [catalog].[CatalogTypes] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_CatalogItems_CatalogTypeId] ON [catalog].[CatalogItems] ([CatalogTypeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201025181258_InitialMigration', N'3.1.9');

GO

