IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cyberharceleurs] (
    [Id] uniqueidentifier NOT NULL,
    [Pseudo] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Cyberharceleurs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TypePreuves] (
    [Id] uniqueidentifier NOT NULL,
    [Nom] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_TypePreuves] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Utilisateurs] (
    [Id] uniqueidentifier NOT NULL,
    [Pseudo] nvarchar(450) NOT NULL,
    [MotDePasse] nvarchar(max) NOT NULL,
    [IsAdmin] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Utilisateurs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Dossiers] (
    [Id] uniqueidentifier NOT NULL,
    [UtilisateurId] uniqueidentifier NOT NULL,
    [CyberharceleurId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Dossiers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Dossiers_Cyberharceleurs_CyberharceleurId] FOREIGN KEY ([CyberharceleurId]) REFERENCES [Cyberharceleurs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Dossiers_Utilisateurs_UtilisateurId] FOREIGN KEY ([UtilisateurId]) REFERENCES [Utilisateurs] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Preuves] (
    [Id] uniqueidentifier NOT NULL,
    [DossierId] uniqueidentifier NOT NULL,
    [TypePreuveId] uniqueidentifier NOT NULL,
    [Contenu] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Preuves] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Preuves_Dossiers_DossierId] FOREIGN KEY ([DossierId]) REFERENCES [Dossiers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Preuves_TypePreuves_TypePreuveId] FOREIGN KEY ([TypePreuveId]) REFERENCES [TypePreuves] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Dossiers_CyberharceleurId] ON [Dossiers] ([CyberharceleurId]);
GO

CREATE INDEX [IX_Dossiers_UtilisateurId] ON [Dossiers] ([UtilisateurId]);
GO

CREATE INDEX [IX_Preuves_DossierId] ON [Preuves] ([DossierId]);
GO

CREATE INDEX [IX_Preuves_TypePreuveId] ON [Preuves] ([TypePreuveId]);
GO

CREATE UNIQUE INDEX [IX_Utilisateurs_Pseudo] ON [Utilisateurs] ([Pseudo]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231204211509_InitialCreate', N'8.0.0');
GO

COMMIT;
GO

