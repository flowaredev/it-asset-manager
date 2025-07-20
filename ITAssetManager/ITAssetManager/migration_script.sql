CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "AspNetRoles" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetRoles" PRIMARY KEY,
    "Name" TEXT NULL,
    "NormalizedName" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL
);

CREATE TABLE "AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "UserName" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "Email" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "PasswordHash" TEXT NULL,
    "SecurityStamp" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "AccessFailedCount" INTEGER NOT NULL
);

CREATE TABLE "CommonAssets" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_CommonAssets" PRIMARY KEY AUTOINCREMENT,
    "ManagementTag" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Role" TEXT NOT NULL,
    "ApplyDateTime" TEXT NOT NULL,
    "ResponsibleCompany" TEXT NOT NULL,
    "ResponsiblePerson" TEXT NOT NULL,
    "ResponsiblePersonPhone" TEXT NOT NULL,
    "OnSiteManager" TEXT NOT NULL,
    "OnSiteManagerPhone" TEXT NOT NULL
);

CREATE TABLE "ServiceLevelAgreements" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ServiceLevelAgreements" PRIMARY KEY AUTOINCREMENT,
    "EndDateOfMonth" TEXT NOT NULL,
    "DisabilityHoursAverage" REAL NOT NULL,
    "DisabilityHoursLevel" INTEGER NOT NULL,
    "UptimeRate" REAL NOT NULL,
    "UptimeLevel" INTEGER NOT NULL,
    "RoutineCheckRate" REAL NOT NULL,
    "RoutineCheckLevel" INTEGER NOT NULL,
    "TechnicalSupportCompletionRate" REAL NOT NULL,
    "TechnicalSupportLevel" INTEGER NOT NULL,
    "SecurityIssues" INTEGER NOT NULL,
    "SecurityIssuesLevel" INTEGER NOT NULL
);

CREATE TABLE "AspNetRoleClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY AUTOINCREMENT,
    "RoleId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY AUTOINCREMENT,
    "UserId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" TEXT NOT NULL,
    "ProviderKey" TEXT NOT NULL,
    "ProviderDisplayName" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserRoles" (
    "UserId" TEXT NOT NULL,
    "RoleId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserTokens" (
    "UserId" TEXT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Value" TEXT NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "BackupEquipments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_BackupEquipments" PRIMARY KEY AUTOINCREMENT,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_BackupEquipments_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Failures" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Failures" PRIMARY KEY AUTOINCREMENT,
    "FailureDateTime" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "VisitDateTime" TEXT NOT NULL,
    "ResolveDateTime" TEXT NOT NULL,
    "DisabilityHours" INTEGER NOT NULL,
    "ResolveDescription" TEXT NOT NULL,
    "IsResolved" INTEGER NOT NULL,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_Failures_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Maintenances" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Maintenances" PRIMARY KEY AUTOINCREMENT,
    "Description" TEXT NOT NULL,
    "VisitDateTime" TEXT NOT NULL,
    "ResolveDateTime" TEXT NOT NULL,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_Maintenances_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "MiscellaneousEquipments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_MiscellaneousEquipments" PRIMARY KEY AUTOINCREMENT,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_MiscellaneousEquipments_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "NetworkEquipments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_NetworkEquipments" PRIMARY KEY AUTOINCREMENT,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_NetworkEquipments_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "RoutineChecks" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_RoutineChecks" PRIMARY KEY AUTOINCREMENT,
    "Detail" TEXT NOT NULL,
    "StartDateTime" TEXT NOT NULL,
    "EndDateTime" TEXT NOT NULL,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_RoutineChecks_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "SecurityEquipments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_SecurityEquipments" PRIMARY KEY AUTOINCREMENT,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_SecurityEquipments_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "SecurityVulnerabilities" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_SecurityVulnerabilities" PRIMARY KEY AUTOINCREMENT,
    "DiscoveryDateTime" TEXT NOT NULL,
    "VulnerabilityDetail" TEXT NOT NULL,
    "VisitDateTime" TEXT NOT NULL,
    "ResolveDateTime" TEXT NOT NULL,
    "TaskDetail" TEXT NOT NULL,
    "IsResolved" INTEGER NOT NULL,
    "Level" INTEGER NOT NULL,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_SecurityVulnerabilities_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Servers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Servers" PRIMARY KEY AUTOINCREMENT,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_Servers_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Softwares" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Softwares" PRIMARY KEY AUTOINCREMENT,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_Softwares_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Storages" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Storages" PRIMARY KEY AUTOINCREMENT,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_Storages_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "SupportEquipments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_SupportEquipments" PRIMARY KEY AUTOINCREMENT,
    "CommonAssetId" INTEGER NOT NULL,
    CONSTRAINT "FK_SupportEquipments_CommonAssets_CommonAssetId" FOREIGN KEY ("CommonAssetId") REFERENCES "CommonAssets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ServerDevices" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ServerDevices" PRIMARY KEY AUTOINCREMENT,
    "Manufacturer" TEXT NOT NULL,
    "Model" TEXT NOT NULL,
    "SerialNumber" TEXT NOT NULL,
    "Cpu" REAL NOT NULL,
    "Ram" REAL NOT NULL,
    "Disk" REAL NOT NULL,
    "Rack" TEXT NOT NULL,
    "ServerId" INTEGER NOT NULL,
    CONSTRAINT "FK_ServerDevices_Servers_ServerId" FOREIGN KEY ("ServerId") REFERENCES "Servers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");

CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

CREATE UNIQUE INDEX "IX_BackupEquipments_CommonAssetId" ON "BackupEquipments" ("CommonAssetId");

CREATE INDEX "IX_Failures_CommonAssetId" ON "Failures" ("CommonAssetId");

CREATE INDEX "IX_Maintenances_CommonAssetId" ON "Maintenances" ("CommonAssetId");

CREATE UNIQUE INDEX "IX_MiscellaneousEquipments_CommonAssetId" ON "MiscellaneousEquipments" ("CommonAssetId");

CREATE UNIQUE INDEX "IX_NetworkEquipments_CommonAssetId" ON "NetworkEquipments" ("CommonAssetId");

CREATE INDEX "IX_RoutineChecks_CommonAssetId" ON "RoutineChecks" ("CommonAssetId");

CREATE UNIQUE INDEX "IX_SecurityEquipments_CommonAssetId" ON "SecurityEquipments" ("CommonAssetId");

CREATE INDEX "IX_SecurityVulnerabilities_CommonAssetId" ON "SecurityVulnerabilities" ("CommonAssetId");

CREATE INDEX "IX_ServerDevices_ServerId" ON "ServerDevices" ("ServerId");

CREATE UNIQUE INDEX "IX_Servers_CommonAssetId" ON "Servers" ("CommonAssetId");

CREATE UNIQUE INDEX "IX_Softwares_CommonAssetId" ON "Softwares" ("CommonAssetId");

CREATE UNIQUE INDEX "IX_Storages_CommonAssetId" ON "Storages" ("CommonAssetId");

CREATE UNIQUE INDEX "IX_SupportEquipments_CommonAssetId" ON "SupportEquipments" ("CommonAssetId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250206084419_InitialCreate', '9.0.7');

CREATE TABLE "UserAppointments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_UserAppointments" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "Start" TEXT NOT NULL,
    "End" TEXT NOT NULL,
    "AllDay" INTEGER NOT NULL,
    "RecurrenceRule" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "FK_UserAppointments_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_UserAppointments_UserId" ON "UserAppointments" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250716070954_AddUserAppointment', '9.0.7');

COMMIT;

