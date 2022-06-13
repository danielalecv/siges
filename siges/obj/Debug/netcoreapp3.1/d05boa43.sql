CREATE TABLE [OsRecurrente] (
    [Id] int NOT NULL IDENTITY,
    [OsOrigenId] int NOT NULL,
    [Periodo] nvarchar(max) NULL,
    CONSTRAINT [PK_OsRecurrente] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Oses] (
    [Id] int NOT NULL IDENTITY,
    [OsId] int NOT NULL,
    [OsRecurrenteId] int NULL,
    CONSTRAINT [PK_Oses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Oses_OsRecurrente_OsRecurrenteId] FOREIGN KEY ([OsRecurrenteId]) REFERENCES [OsRecurrente] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Oses_OsRecurrenteId] ON [Oses] ([OsRecurrenteId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220609161924_Osrecurrente', N'3.1.23');

GO

