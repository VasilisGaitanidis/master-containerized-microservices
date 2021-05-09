IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'ordering') IS NULL EXEC(N'CREATE SCHEMA [ordering];');

GO

CREATE TABLE [ordering].[Buyers] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NULL,
    [Country] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [ZipCode] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Buyers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ordering].[Orders] (
    [Id] uniqueidentifier NOT NULL,
    [Username] nvarchar(450) NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [ShippingAddress] nvarchar(max) NOT NULL,
    [BuyerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Buyers_BuyerId] FOREIGN KEY ([BuyerId]) REFERENCES [ordering].[Buyers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ordering].[OrderItems] (
    [Id] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [ProductName] nvarchar(max) NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [ordering].[Orders] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_OrderItems_OrderId] ON [ordering].[OrderItems] ([OrderId]);

GO

CREATE INDEX [IX_Orders_BuyerId] ON [ordering].[Orders] ([BuyerId]);

GO

CREATE INDEX [IX_Orders_Username] ON [ordering].[Orders] ([Username]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210504193946_InitialMigration', N'3.1.9');

GO

