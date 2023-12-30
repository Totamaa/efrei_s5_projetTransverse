# Utils

- [I. Lancer l'app](#i-lancer-lapp)
  - [I.A. Premier lancement](#ia-premier-lancement)
  - [I.B. Lancer au quotidien](#ib-lancer-au-quotidien)
- [II. Création](#ii-création)
  - [II.A. Entity](#iia-entity)
  - [II.B. Service](#iib-service)
  - [II.C. Controller](#iic-controller)
- [III. Database](#iii-database)

## I. Lancer l'app

### I.A. Premier lancement

1. [Installer .NET](https://dotnet.microsoft.com/en-us/download)
2. Créer une base de données sur MySQL et l'appeler `efrei_s5_projettransverse`
3. Jouer les scripts de migrations sur cette BD (jouer dans l'ordre les fichiers du dossier `Scripts/`)
4. Changer la connection string (DefaultConnection) des fichiers appsettings(.Development).json en remplaçant user et password par votre propre login/mdp

### I.B. Lancer au quotidien

Se mettre à la racine et faire `F5` ou `ctrl + F5`

Pour le front, le détaille de l'api est disponible [à ce lien](https://localhost:7284/swagger/) (il faut que le back run)

## II. Création

### II.A. Entity

1. Créer un nouveau fichier dans `Models/Entity`, l'appeler **ExempleEntity**
2. Faire hériter cette classe de BaseEntity
3. Rajouter la classe dans le contexte (`Models/Context/MySqlContext.cs`):

```cs
public DbSet<ExempleEntity> Exemples { get; set; }
```

### II.B. Service

Créer des services pour la logique métier: BusinessService si ça concerne un objet en BD, Service sinon
Exemple: UtilisateurBusinessService, AuthService
Créer dans `Services/ExempleService.cs` et faire une interface dans `Services/Interfaces/IExempleService.cs`
Rajouter dans Program.cs `builder.Services.AddScoped<IExempleService, ExempleService>();`

### II.C. Controller

Créer un controller:

Pour une classe **ExempleEntity**

1. Créer `Controllers/ExempleController.cs`
2. Faire hériter la classe de `ControllerBase`
3. Ajouter les services si besoin

## III. Database

Pour modifier la base de données:

1. Modifier les entités voulues (qui correspond aux tables de la BD)
2. Créer la migration: `dotnet-ef migrations add nomMigration`
3. Créer le script de migration `dotnet-ef migrations script -o ./Scripts/xxx_nomMigration.sql`
4. Appliquer la migration à la BD: `dotnet-ef database update`
