CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Cyberharceleurs` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Pseudo` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FirstName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Cyberharceleurs` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TypePreuves` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nom` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_TypePreuves` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Utilisateurs` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Pseudo` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `MotDePasse` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IsAdmin` tinyint(1) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Utilisateurs` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Dossiers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UtilisateurId` int NOT NULL,
    `CyberharceleurId` int NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Dossiers` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Dossiers_Cyberharceleurs_CyberharceleurId` FOREIGN KEY (`CyberharceleurId`) REFERENCES `Cyberharceleurs` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Dossiers_Utilisateurs_UtilisateurId` FOREIGN KEY (`UtilisateurId`) REFERENCES `Utilisateurs` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Preuves` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `DossierId` int NOT NULL,
    `TypePreuveId` int NOT NULL,
    `Contenu` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Preuves` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Preuves_Dossiers_DossierId` FOREIGN KEY (`DossierId`) REFERENCES `Dossiers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Preuves_TypePreuves_TypePreuveId` FOREIGN KEY (`TypePreuveId`) REFERENCES `TypePreuves` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Dossiers_CyberharceleurId` ON `Dossiers` (`CyberharceleurId`);

CREATE INDEX `IX_Dossiers_UtilisateurId` ON `Dossiers` (`UtilisateurId`);

CREATE INDEX `IX_Preuves_DossierId` ON `Preuves` (`DossierId`);

CREATE INDEX `IX_Preuves_TypePreuveId` ON `Preuves` (`TypePreuveId`);

CREATE UNIQUE INDEX `IX_Utilisateurs_Pseudo` ON `Utilisateurs` (`Pseudo`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231206001354_InitDatabase', '8.0.0');

COMMIT;

START TRANSACTION;

ALTER TABLE `Preuves` MODIFY COLUMN `Contenu` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Cyberharceleurs` MODIFY COLUMN `LastName` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Cyberharceleurs` MODIFY COLUMN `FirstName` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Cyberharceleurs` MODIFY COLUMN `Description` longtext CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231206172532_Infos', '8.0.0');

COMMIT;

