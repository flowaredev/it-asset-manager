CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;
DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `AspNetRoles` (
        `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
        `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
        `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `AspNetUsers` (
        `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Name` longtext CHARACTER SET utf8mb4 NULL,
        `Department` longtext CHARACTER SET utf8mb4 NULL,
        `UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
        `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
        `Email` varchar(256) CHARACTER SET utf8mb4 NULL,
        `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
        `EmailConfirmed` tinyint(1) NOT NULL,
        `PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
        `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
        `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
        `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
        `PhoneNumberConfirmed` tinyint(1) NOT NULL,
        `TwoFactorEnabled` tinyint(1) NOT NULL,
        `LockoutEnd` datetime(6) NULL,
        `LockoutEnabled` tinyint(1) NOT NULL,
        `AccessFailedCount` int NOT NULL,
        CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `CommonAssets` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `ManagementTag` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Role` longtext CHARACTER SET utf8mb4 NOT NULL,
        `ApplyDateTime` datetime(6) NOT NULL,
        `ResponsibleCompany` longtext CHARACTER SET utf8mb4 NOT NULL,
        `ResponsiblePerson` longtext CHARACTER SET utf8mb4 NOT NULL,
        `ResponsiblePersonPhone` longtext CHARACTER SET utf8mb4 NOT NULL,
        `OnSiteManager` longtext CHARACTER SET utf8mb4 NOT NULL,
        `OnSiteManagerPhone` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_CommonAssets` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `ServiceLevelAgreements` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `EndDateOfMonth` date NOT NULL,
        `DisabilityHoursAverage` double NOT NULL,
        `DisabilityHoursLevel` int NOT NULL,
        `UptimeRate` double NOT NULL,
        `UptimeLevel` int NOT NULL,
        `RoutineCheckRate` double NOT NULL,
        `RoutineCheckLevel` int NOT NULL,
        `TechnicalSupportCompletionRate` double NOT NULL,
        `TechnicalSupportLevel` int NOT NULL,
        `SecurityIssues` int NOT NULL,
        `SecurityIssuesLevel` int NOT NULL,
        CONSTRAINT `PK_ServiceLevelAgreements` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `AspNetRoleClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
        `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `AspNetUserClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
        `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `AspNetUserLogins` (
        `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ProviderKey` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
        CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `AspNetUserRoles` (
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
        CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `AspNetUserTokens` (
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Value` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
        CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `UserAppointments` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
        `Start` datetime(6) NOT NULL,
        `End` datetime(6) NOT NULL,
        `AllDay` tinyint(1) NOT NULL,
        `RecurrenceRule` longtext CHARACTER SET utf8mb4 NULL,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_UserAppointments` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UserAppointments_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `BackupEquipments` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_BackupEquipments` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_BackupEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `Failures` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `FailureDateTime` datetime(6) NOT NULL,
        `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
        `VisitDateTime` datetime(6) NOT NULL,
        `ResolveDateTime` datetime(6) NOT NULL,
        `DisabilityHours` int NOT NULL,
        `ResolveDescription` longtext CHARACTER SET utf8mb4 NOT NULL,
        `IsResolved` tinyint(1) NOT NULL,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_Failures` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Failures_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `Maintenances` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
        `VisitDateTime` datetime(6) NOT NULL,
        `ResolveDateTime` datetime(6) NOT NULL,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_Maintenances` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Maintenances_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `MiscellaneousEquipments` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_MiscellaneousEquipments` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_MiscellaneousEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `NetworkEquipments` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_NetworkEquipments` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_NetworkEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `RoutineChecks` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Detail` longtext CHARACTER SET utf8mb4 NOT NULL,
        `StartDateTime` datetime(6) NOT NULL,
        `EndDateTime` datetime(6) NOT NULL,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_RoutineChecks` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_RoutineChecks_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `SecurityEquipments` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_SecurityEquipments` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SecurityEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `SecurityVulnerabilities` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `DiscoveryDateTime` datetime(6) NOT NULL,
        `VulnerabilityDetail` longtext CHARACTER SET utf8mb4 NOT NULL,
        `VisitDateTime` datetime(6) NOT NULL,
        `ResolveDateTime` datetime(6) NOT NULL,
        `TaskDetail` longtext CHARACTER SET utf8mb4 NOT NULL,
        `IsResolved` tinyint(1) NOT NULL,
        `Level` int NOT NULL,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_SecurityVulnerabilities` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SecurityVulnerabilities_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `Servers` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_Servers` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Servers_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `Softwares` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_Softwares` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Softwares_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `Storages` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_Storages` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Storages_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `SupportEquipments` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CommonAssetId` int NOT NULL,
        CONSTRAINT `PK_SupportEquipments` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SupportEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE TABLE `ServerDevices` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Manufacturer` longtext CHARACTER SET utf8mb4 NULL,
        `Model` longtext CHARACTER SET utf8mb4 NULL,
        `SerialNumber` longtext CHARACTER SET utf8mb4 NULL,
        `Ram` double NULL,
        `Disk` double NULL,
        `Rack` longtext CHARACTER SET utf8mb4 NULL,
        `NetworkType` longtext CHARACTER SET utf8mb4 NULL,
        `MountedPhysicalServer` longtext CHARACTER SET utf8mb4 NULL,
        `OsType` longtext CHARACTER SET utf8mb4 NULL,
        `OsVersion` longtext CHARACTER SET utf8mb4 NULL,
        `OsBit` longtext CHARACTER SET utf8mb4 NULL,
        `CpuClockGhz` double NULL,
        `CpuCores` int NULL,
        `InternalDisk` longtext CHARACTER SET utf8mb4 NULL,
        `ExternalDisk` longtext CHARACTER SET utf8mb4 NULL,
        `NicCount` int NULL,
        `HbaCount` int NULL,
        `IpAddress` longtext CHARACTER SET utf8mb4 NULL,
        `UnitSize` int NULL,
        `Notes` longtext CHARACTER SET utf8mb4 NULL,
        `ServerId` int NOT NULL,
        CONSTRAINT `PK_ServerDevices` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_ServerDevices_Servers_ServerId` FOREIGN KEY (`ServerId`) REFERENCES `Servers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `IX_BackupEquipments_CommonAssetId` ON `BackupEquipments` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_Failures_CommonAssetId` ON `Failures` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_Maintenances_CommonAssetId` ON `Maintenances` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `IX_MiscellaneousEquipments_CommonAssetId` ON `MiscellaneousEquipments` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `IX_NetworkEquipments_CommonAssetId` ON `NetworkEquipments` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_RoutineChecks_CommonAssetId` ON `RoutineChecks` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `IX_SecurityEquipments_CommonAssetId` ON `SecurityEquipments` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_SecurityVulnerabilities_CommonAssetId` ON `SecurityVulnerabilities` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_ServerDevices_ServerId` ON `ServerDevices` (`ServerId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `IX_Servers_CommonAssetId` ON `Servers` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `IX_Softwares_CommonAssetId` ON `Softwares` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `IX_Storages_CommonAssetId` ON `Storages` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE UNIQUE INDEX `IX_SupportEquipments_CommonAssetId` ON `SupportEquipments` (`CommonAssetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    CREATE INDEX `IX_UserAppointments_UserId` ON `UserAppointments` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218153505_CreateInitial') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20251218153505_CreateInitial', '9.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218161136_AddStorageDevices') THEN

    CREATE TABLE `StorageDevices` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Manufacturer` longtext CHARACTER SET utf8mb4 NULL,
        `Model` longtext CHARACTER SET utf8mb4 NULL,
        `SerialNumber` longtext CHARACTER SET utf8mb4 NULL,
        `PhysicalDiskInfo` longtext CHARACTER SET utf8mb4 NULL,
        `DiskBackupInfo` longtext CHARACTER SET utf8mb4 NULL,
        `IpAddress` longtext CHARACTER SET utf8mb4 NULL,
        `Rack` longtext CHARACTER SET utf8mb4 NULL,
        `UnitSize` int NULL,
        `Notes` longtext CHARACTER SET utf8mb4 NULL,
        `StorageId` int NOT NULL,
        CONSTRAINT `PK_StorageDevices` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_StorageDevices_Storages_StorageId` FOREIGN KEY (`StorageId`) REFERENCES `Storages` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218161136_AddStorageDevices') THEN

    CREATE INDEX `IX_StorageDevices_StorageId` ON `StorageDevices` (`StorageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218161136_AddStorageDevices') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20251218161136_AddStorageDevices', '9.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218163229_AddNetworkDevices') THEN

    CREATE TABLE `NetworkDevices` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Manufacturer` longtext CHARACTER SET utf8mb4 NULL,
        `Model` longtext CHARACTER SET utf8mb4 NULL,
        `SerialNumber` longtext CHARACTER SET utf8mb4 NULL,
        `OsVersion` longtext CHARACTER SET utf8mb4 NULL,
        `MainMemory` longtext CHARACTER SET utf8mb4 NULL,
        `FlashMemory` longtext CHARACTER SET utf8mb4 NULL,
        `IpAddress` longtext CHARACTER SET utf8mb4 NULL,
        `Rack` longtext CHARACTER SET utf8mb4 NULL,
        `UnitSize` int NULL,
        `Notes` longtext CHARACTER SET utf8mb4 NULL,
        `NetworkEquipmentId` int NOT NULL,
        CONSTRAINT `PK_NetworkDevices` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_NetworkDevices_NetworkEquipments_NetworkEquipmentId` FOREIGN KEY (`NetworkEquipmentId`) REFERENCES `NetworkEquipments` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218163229_AddNetworkDevices') THEN

    CREATE INDEX `IX_NetworkDevices_NetworkEquipmentId` ON `NetworkDevices` (`NetworkEquipmentId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218163229_AddNetworkDevices') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20251218163229_AddNetworkDevices', '9.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218165345_AddSecurityDevices') THEN

    CREATE TABLE `SecurityDevices` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Manufacturer` longtext CHARACTER SET utf8mb4 NULL,
        `Model` longtext CHARACTER SET utf8mb4 NULL,
        `SerialNumber` longtext CHARACTER SET utf8mb4 NULL,
        `DeviceSpec` longtext CHARACTER SET utf8mb4 NULL,
        `IpAddress` longtext CHARACTER SET utf8mb4 NULL,
        `Rack` longtext CHARACTER SET utf8mb4 NULL,
        `UnitSize` int NULL,
        `Notes` longtext CHARACTER SET utf8mb4 NULL,
        `SecurityEquipmentId` int NOT NULL,
        CONSTRAINT `PK_SecurityDevices` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SecurityDevices_SecurityEquipments_SecurityEquipmentId` FOREIGN KEY (`SecurityEquipmentId`) REFERENCES `SecurityEquipments` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218165345_AddSecurityDevices') THEN

    CREATE INDEX `IX_SecurityDevices_SecurityEquipmentId` ON `SecurityDevices` (`SecurityEquipmentId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218165345_AddSecurityDevices') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20251218165345_AddSecurityDevices', '9.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218170553_AddSoftwareDevices') THEN

    CREATE TABLE `SoftwareDevices` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Manufacturer` longtext CHARACTER SET utf8mb4 NULL,
        `ProgramName` longtext CHARACTER SET utf8mb4 NULL,
        `SerialNumber` longtext CHARACTER SET utf8mb4 NULL,
        `Notes` longtext CHARACTER SET utf8mb4 NULL,
        `SoftwareId` int NOT NULL,
        CONSTRAINT `PK_SoftwareDevices` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SoftwareDevices_Softwares_SoftwareId` FOREIGN KEY (`SoftwareId`) REFERENCES `Softwares` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218170553_AddSoftwareDevices') THEN

    CREATE INDEX `IX_SoftwareDevices_SoftwareId` ON `SoftwareDevices` (`SoftwareId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218170553_AddSoftwareDevices') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20251218170553_AddSoftwareDevices', '9.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218171459_AddSupportDevices') THEN

    CREATE TABLE `SupportDevices` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Manufacturer` longtext CHARACTER SET utf8mb4 NULL,
        `Model` longtext CHARACTER SET utf8mb4 NULL,
        `SerialNumber` longtext CHARACTER SET utf8mb4 NULL,
        `DeviceSpec` longtext CHARACTER SET utf8mb4 NULL,
        `IpAddress` longtext CHARACTER SET utf8mb4 NULL,
        `Location` longtext CHARACTER SET utf8mb4 NULL,
        `Notes` longtext CHARACTER SET utf8mb4 NULL,
        `SupportEquipmentId` int NOT NULL,
        CONSTRAINT `PK_SupportDevices` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SupportDevices_SupportEquipments_SupportEquipmentId` FOREIGN KEY (`SupportEquipmentId`) REFERENCES `SupportEquipments` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218171459_AddSupportDevices') THEN

    CREATE INDEX `IX_SupportDevices_SupportEquipmentId` ON `SupportDevices` (`SupportEquipmentId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218171459_AddSupportDevices') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20251218171459_AddSupportDevices', '9.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218173223_RemoveBackupEquipments') THEN

    DROP TABLE `BackupEquipments`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20251218173223_RemoveBackupEquipments') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20251218173223_RemoveBackupEquipments', '9.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260420025622_ChangeServerDeviceDiskToString') THEN

    ALTER TABLE `ServerDevices` MODIFY COLUMN `Disk` longtext CHARACTER SET utf8mb4 NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260420025622_ChangeServerDeviceDiskToString') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260420025622_ChangeServerDeviceDiskToString', '9.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

