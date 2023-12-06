# Utils

- [I. Database](#i-database)
- [II. Création](#ii-création)
  - [II.A. Entity](#iia-entity)
  - [II.B. Controller](#iib-controller)
- [III. Lancer l'app](#iii-lancer-lapp)

## I. Database

Pour modifier la base de données:

1. Modifier les entités voulues (qui correspond aux tables de la BD)
2. Créer la migration: `dotnet-ef migrations add nomMigration`
3. Créer le script de migration `dotnet-ef migrations script -o ./Scripts/xxx_nomMigration.sql`
4. Appliquer la migration à la BD: `dotnet-ef database update`

## II. Création

### II.A. Entity

1. Créer un nouveau fichier dans `Models/Entity`, l'appeler **ExempleEntity**
2. Rajouter la classe dans le contexte (`Models/Context/MySqlContext.cs`):

```cs
public DbSet<ExempleEntity> Exemples { get; set; }
```

### II.B. Controller

Créer un controller:

Pour une classe **ExempleEntity**

`dotnet aspnet-codegenerator controller -name ExempleController -async -api -m ExempleEntity -dc MySqlContext -outDir Controllers`

## III. Lancer l'app

Se mettre à la racine et faire `F5` ou `ctrl + F5`

Pour le front, le détaille de l'api est disponible [à ce lien](https://localhost:7284/swagger/)
