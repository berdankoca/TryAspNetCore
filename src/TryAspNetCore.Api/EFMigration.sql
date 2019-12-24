CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE TABLE "Event" (
        "EventId" uuid NOT NULL,
        "Title" text NULL,
        CONSTRAINT "PK_Event" PRIMARY KEY ("EventId")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE TABLE "Role" (
        "Id" uuid NOT NULL,
        "Name" varchar(256) NULL,
        "NormalizedName" varchar(256) NULL,
        "ConcurrencyStamp" text NULL,
        CONSTRAINT "PK_Role" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE TABLE "User" (
        "Id" uuid NOT NULL,
        "UserName" varchar(256) NULL,
        "NormalizedUserName" varchar(256) NULL,
        "Email" varchar(256) NULL,
        "NormalizedEmail" varchar(256) NULL,
        "EmailConfirmed" boolean NOT NULL,
        "PasswordHash" text NULL,
        "SecurityStamp" text NULL,
        "ConcurrencyStamp" text NULL,
        "PhoneNumber" text NULL,
        "PhoneNumberConfirmed" boolean NOT NULL,
        "TwoFactorEnabled" boolean NOT NULL,
        "LockoutEnd" timestamp with time zone NULL,
        "LockoutEnabled" boolean NOT NULL,
        "AccessFailedCount" integer NOT NULL,
        CONSTRAINT "PK_User" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE TABLE "EventRegistration" (
        "EventRegistrationId" uuid NOT NULL,
        "EventId" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        CONSTRAINT "PK_EventRegistration" PRIMARY KEY ("EventRegistrationId"),
        CONSTRAINT "FK_EventRegistration_Event_EventId" FOREIGN KEY ("EventId") REFERENCES "Event" ("EventId") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE TABLE "RoleClaim" (
        "Id" serial NOT NULL,
        "RoleId" uuid NOT NULL,
        "ClaimType" text NULL,
        "ClaimValue" text NULL,
        CONSTRAINT "PK_RoleClaim" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_RoleClaim_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Role" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE TABLE "UserClaim" (
        "Id" serial NOT NULL,
        "UserId" uuid NOT NULL,
        "ClaimType" text NULL,
        "ClaimValue" text NULL,
        CONSTRAINT "PK_UserClaim" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserClaim_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE TABLE "UserLogin" (
        "LoginProvider" text NOT NULL,
        "ProviderKey" text NOT NULL,
        "ProviderDisplayName" text NULL,
        "UserId" uuid NOT NULL,
        CONSTRAINT "PK_UserLogin" PRIMARY KEY ("LoginProvider", "ProviderKey"),
        CONSTRAINT "FK_UserLogin_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE TABLE "UserRole" (
        "UserId" uuid NOT NULL,
        "RoleId" uuid NOT NULL,
        CONSTRAINT "PK_UserRole" PRIMARY KEY ("UserId", "RoleId"),
        CONSTRAINT "FK_UserRole_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Role" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_UserRole_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE TABLE "UserToken" (
        "UserId" uuid NOT NULL,
        "LoginProvider" text NOT NULL,
        "Name" text NOT NULL,
        "Value" text NULL,
        CONSTRAINT "PK_UserToken" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
        CONSTRAINT "FK_UserToken_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE INDEX "IX_EventRegistration_EventId" ON "EventRegistration" ("EventId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE UNIQUE INDEX "RoleNameIndex" ON "Role" ("NormalizedName");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE INDEX "IX_RoleClaim_RoleId" ON "RoleClaim" ("RoleId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE INDEX "EmailIndex" ON "User" ("NormalizedEmail");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE UNIQUE INDEX "UserNameIndex" ON "User" ("NormalizedUserName");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE INDEX "IX_UserClaim_UserId" ON "UserClaim" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE INDEX "IX_UserLogin_UserId" ON "UserLogin" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    CREATE INDEX "IX_UserRole_RoleId" ON "UserRole" ("RoleId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190224104347_initialMigration') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190224104347_initialMigration', '2.1.4-rtm-31024');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306083652_BaseEntityAddedCreatedAndUpdatedInformation') THEN
    ALTER TABLE "EventRegistration" ADD "CreatedBy" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306083652_BaseEntityAddedCreatedAndUpdatedInformation') THEN
    ALTER TABLE "EventRegistration" ADD "CreatedTime" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306083652_BaseEntityAddedCreatedAndUpdatedInformation') THEN
    ALTER TABLE "EventRegistration" ADD "UpdadetBy" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306083652_BaseEntityAddedCreatedAndUpdatedInformation') THEN
    ALTER TABLE "EventRegistration" ADD "UpdatedTime" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306083652_BaseEntityAddedCreatedAndUpdatedInformation') THEN
    ALTER TABLE "Event" ADD "CreatedBy" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306083652_BaseEntityAddedCreatedAndUpdatedInformation') THEN
    ALTER TABLE "Event" ADD "CreatedTime" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306083652_BaseEntityAddedCreatedAndUpdatedInformation') THEN
    ALTER TABLE "Event" ADD "UpdadetBy" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306083652_BaseEntityAddedCreatedAndUpdatedInformation') THEN
    ALTER TABLE "Event" ADD "UpdatedTime" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306083652_BaseEntityAddedCreatedAndUpdatedInformation') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190306083652_BaseEntityAddedCreatedAndUpdatedInformation', '2.1.4-rtm-31024');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306084631_BaseEntityAddedCreatedAndUpdatedInformation2') THEN
    ALTER TABLE "EventRegistration" RENAME COLUMN "UpdatedTime" TO "UpdatedDate";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306084631_BaseEntityAddedCreatedAndUpdatedInformation2') THEN
    ALTER TABLE "EventRegistration" RENAME COLUMN "UpdadetBy" TO "UpdatedBy";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306084631_BaseEntityAddedCreatedAndUpdatedInformation2') THEN
    ALTER TABLE "EventRegistration" RENAME COLUMN "CreatedTime" TO "CreatedDate";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306084631_BaseEntityAddedCreatedAndUpdatedInformation2') THEN
    ALTER TABLE "Event" RENAME COLUMN "UpdatedTime" TO "UpdatedDate";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306084631_BaseEntityAddedCreatedAndUpdatedInformation2') THEN
    ALTER TABLE "Event" RENAME COLUMN "UpdadetBy" TO "UpdatedBy";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306084631_BaseEntityAddedCreatedAndUpdatedInformation2') THEN
    ALTER TABLE "Event" RENAME COLUMN "CreatedTime" TO "CreatedDate";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190306084631_BaseEntityAddedCreatedAndUpdatedInformation2') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190306084631_BaseEntityAddedCreatedAndUpdatedInformation2', '2.1.4-rtm-31024');
    END IF;
END $$;
