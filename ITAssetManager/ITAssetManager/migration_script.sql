CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
CREATE TABLE `AspNetRoles` (
    `Id` varchar(255) NOT NULL,
    `Name` varchar(256) NULL,
    `NormalizedName` varchar(256) NULL,
    `ConcurrencyStamp` longtext NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` varchar(255) NOT NULL,
    `Name` longtext NULL,
    `Department` longtext NULL,
    `UserName` varchar(256) NULL,
    `NormalizedUserName` varchar(256) NULL,
    `Email` varchar(256) NULL,
    `NormalizedEmail` varchar(256) NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `PasswordHash` longtext NULL,
    `SecurityStamp` longtext NULL,
    `ConcurrencyStamp` longtext NULL,
    `PhoneNumber` longtext NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled` tinyint(1) NOT NULL,
    `LockoutEnd` datetime NULL,
    `LockoutEnabled` tinyint(1) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `CommonAssets` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ManagementTag` longtext NOT NULL,
    `Name` longtext NOT NULL,
    `Role` longtext NOT NULL,
    `ApplyDateTime` datetime(6) NOT NULL,
    `ResponsibleCompany` longtext NOT NULL,
    `ResponsiblePerson` longtext NOT NULL,
    `ResponsiblePersonPhone` longtext NOT NULL,
    `OnSiteManager` longtext NOT NULL,
    `OnSiteManagerPhone` longtext NOT NULL,
    PRIMARY KEY (`Id`)
);

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
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` varchar(255) NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` varchar(255) NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(255) NOT NULL,
    `ProviderKey` varchar(255) NOT NULL,
    `ProviderDisplayName` longtext NULL,
    `UserId` varchar(255) NOT NULL,
    PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` varchar(255) NOT NULL,
    `RoleId` varchar(255) NOT NULL,
    PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` varchar(255) NOT NULL,
    `LoginProvider` varchar(255) NOT NULL,
    `Name` varchar(255) NOT NULL,
    `Value` longtext NULL,
    PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `UserAppointments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Title` longtext NOT NULL,
    `Description` longtext NOT NULL,
    `Start` datetime(6) NOT NULL,
    `End` datetime(6) NOT NULL,
    `AllDay` tinyint(1) NOT NULL,
    `RecurrenceRule` longtext NULL,
    `UserId` varchar(255) NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserAppointments_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `BackupEquipments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_BackupEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Failures` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `FailureDateTime` datetime(6) NOT NULL,
    `Description` longtext NOT NULL,
    `VisitDateTime` datetime(6) NOT NULL,
    `ResolveDateTime` datetime(6) NOT NULL,
    `DisabilityHours` int NOT NULL,
    `ResolveDescription` longtext NOT NULL,
    `IsResolved` tinyint(1) NOT NULL,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Failures_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Maintenances` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` longtext NOT NULL,
    `VisitDateTime` datetime(6) NOT NULL,
    `ResolveDateTime` datetime(6) NOT NULL,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Maintenances_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `MiscellaneousEquipments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_MiscellaneousEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `NetworkEquipments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_NetworkEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `RoutineChecks` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Detail` longtext NOT NULL,
    `StartDateTime` datetime(6) NOT NULL,
    `EndDateTime` datetime(6) NOT NULL,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RoutineChecks_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `SecurityEquipments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SecurityEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `SecurityVulnerabilities` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `DiscoveryDateTime` datetime(6) NOT NULL,
    `VulnerabilityDetail` longtext NOT NULL,
    `VisitDateTime` datetime(6) NOT NULL,
    `ResolveDateTime` datetime(6) NOT NULL,
    `TaskDetail` longtext NOT NULL,
    `IsResolved` tinyint(1) NOT NULL,
    `Level` int NOT NULL,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SecurityVulnerabilities_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Servers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Servers_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Softwares` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Softwares_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Storages` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Storages_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `SupportEquipments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CommonAssetId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SupportEquipments_CommonAssets_CommonAssetId` FOREIGN KEY (`CommonAssetId`) REFERENCES `CommonAssets` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ServerDevices` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Manufacturer` longtext NOT NULL,
    `Model` longtext NOT NULL,
    `SerialNumber` longtext NOT NULL,
    `Cpu` double NOT NULL,
    `Ram` double NOT NULL,
    `Disk` double NOT NULL,
    `Rack` longtext NOT NULL,
    `ServerId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ServerDevices_Servers_ServerId` FOREIGN KEY (`ServerId`) REFERENCES `Servers` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE UNIQUE INDEX `IX_BackupEquipments_CommonAssetId` ON `BackupEquipments` (`CommonAssetId`);

CREATE INDEX `IX_Failures_CommonAssetId` ON `Failures` (`CommonAssetId`);

CREATE INDEX `IX_Maintenances_CommonAssetId` ON `Maintenances` (`CommonAssetId`);

CREATE UNIQUE INDEX `IX_MiscellaneousEquipments_CommonAssetId` ON `MiscellaneousEquipments` (`CommonAssetId`);

CREATE UNIQUE INDEX `IX_NetworkEquipments_CommonAssetId` ON `NetworkEquipments` (`CommonAssetId`);

CREATE INDEX `IX_RoutineChecks_CommonAssetId` ON `RoutineChecks` (`CommonAssetId`);

CREATE UNIQUE INDEX `IX_SecurityEquipments_CommonAssetId` ON `SecurityEquipments` (`CommonAssetId`);

CREATE INDEX `IX_SecurityVulnerabilities_CommonAssetId` ON `SecurityVulnerabilities` (`CommonAssetId`);

CREATE INDEX `IX_ServerDevices_ServerId` ON `ServerDevices` (`ServerId`);

CREATE UNIQUE INDEX `IX_Servers_CommonAssetId` ON `Servers` (`CommonAssetId`);

CREATE UNIQUE INDEX `IX_Softwares_CommonAssetId` ON `Softwares` (`CommonAssetId`);

CREATE UNIQUE INDEX `IX_Storages_CommonAssetId` ON `Storages` (`CommonAssetId`);

CREATE UNIQUE INDEX `IX_SupportEquipments_CommonAssetId` ON `SupportEquipments` (`CommonAssetId`);

CREATE INDEX `IX_UserAppointments_UserId` ON `UserAppointments` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20251128055841_InitialCreate', '9.0.9');

COMMIT;

