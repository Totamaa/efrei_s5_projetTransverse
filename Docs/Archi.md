# Architecture

Ne pas modifier les dossiers en *\*italique*

- ***\*bin/***: Fichier binaire
- **Controllers/**: Controller pour gérer les endpoints de l'API
- **Docs/**: Documentation
- ***\*Migrations/***: fichier généré pour la base de données
- **Models/**:
  - ***\*Context/***: Contexte pour la base de données
  - **DTO/**: Objects échangés avec le front
  - **Entity/**: Objet de notre BD
- ***\*Properties***: fichier de settings
- **Scripts/**: scripts SQL pour la base de données
- **wwwroot/**: front-end
- **appsettings(.Development).json**: fichier de configuration (notamment la string pour la BD)
- ***\*Program.cs***: fichier de lancement de l'app
