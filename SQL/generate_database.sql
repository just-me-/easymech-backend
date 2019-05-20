CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE public."Kunden" (
    "Id" bigserial NOT NULL,
    "Firma" varchar(128) NOT NULL,
    "Vorname" varchar(128) NULL,
    "Nachname" varchar(128) NULL,
    "Adresse" varchar(128) NULL,
    "PLZ" varchar(128) NULL,
    "Ort" varchar(128) NULL,
    "Email" varchar(128) NULL,
    "Telefon" varchar(128) NULL,
    "Notiz" text NULL,
    "IstAktiv" boolean NOT NULL,
    CONSTRAINT "PK_Kunden" PRIMARY KEY ("Id")
);

CREATE TABLE public."Maschinentyp" (
    "Id" bigserial NOT NULL,
    "Fabrikat" varchar(128) NOT NULL,
    "Motortyp" varchar(128) NULL,
    "Nutzlast" integer NULL,
    "Hubkraft" integer NULL,
    "Hubhoehe" integer NULL,
    "Eigengewicht" integer NULL,
    "Maschinenhoehe" integer NULL,
    "Maschinenlaenge" integer NULL,
    "Maschinenbreite" integer NULL,
    "Pneugroesse" integer NULL,
    CONSTRAINT "PK_Maschinentyp" PRIMARY KEY ("Id")
);

CREATE TABLE public."Maschine" (
    "Id" bigserial NOT NULL,
    "Seriennummer" varchar(128) NULL,
    "Mastnummer" varchar(128) NULL,
    "Motorennummer" varchar(128) NULL,
    "Betriebsdauer" integer NULL,
    "Jahrgang" integer NULL,
    "Notiz" text NULL,
    "IstAktiv" boolean NOT NULL,
    "BesitzerId" bigint NOT NULL,
    "MaschinentypId" bigint NOT NULL,
    CONSTRAINT "PK_Maschine" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Maschine_Kunden_BesitzerId" FOREIGN KEY ("BesitzerId") REFERENCES public."Kunden" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Maschine_Maschinentyp_MaschinentypId" FOREIGN KEY ("MaschinentypId") REFERENCES public."Maschinentyp" ("Id") ON DELETE CASCADE
);

CREATE TABLE public."Reservationen" (
    "Id" bigserial NOT NULL,
    "Standort" varchar(256) NULL,
    "Startdatum" timestamp without time zone NULL,
    "Enddatum" timestamp without time zone NULL,
    "MaschinenId" bigint NOT NULL,
    "KundenId" bigint NULL,
    CONSTRAINT "PK_Reservationen" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Reservationen_Kunden_KundenId" FOREIGN KEY ("KundenId") REFERENCES public."Kunden" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Reservationen_Maschine_MaschinenId" FOREIGN KEY ("MaschinenId") REFERENCES public."Maschine" ("Id") ON DELETE CASCADE
);

CREATE TABLE public."Services" (
    "Id" bigserial NOT NULL,
    "Bezeichnung" varchar(128) NULL,
    "Beginn" timestamp without time zone NOT NULL,
    "Ende" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "MaschinenId" bigint NOT NULL,
    "KundenId" bigint NOT NULL,
    CONSTRAINT "PK_Services" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Services_Kunden_KundenId" FOREIGN KEY ("KundenId") REFERENCES public."Kunden" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Services_Maschine_MaschinenId" FOREIGN KEY ("MaschinenId") REFERENCES public."Maschine" ("Id") ON DELETE CASCADE
);

CREATE TABLE public."Transaktionen" (
    "Id" bigserial NOT NULL,
    "Preis" double precision NOT NULL,
    "Typ" integer NOT NULL,
    "Datum" timestamp without time zone NULL,
    "MaschinenId" bigint NOT NULL,
    "KundenId" bigint NULL,
    CONSTRAINT "PK_Transaktionen" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Transaktionen_Kunden_KundenId" FOREIGN KEY ("KundenId") REFERENCES public."Kunden" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Transaktionen_Maschine_MaschinenId" FOREIGN KEY ("MaschinenId") REFERENCES public."Maschine" ("Id") ON DELETE CASCADE
);

CREATE TABLE public."MaschinenRuecknahme" (
    "Id" bigserial NOT NULL,
    "Datum" timestamp without time zone NOT NULL,
    "Notiz" text NULL,
    "ReservationsId" bigint NOT NULL,
    CONSTRAINT "PK_MaschinenRuecknahme" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_MaschinenRuecknahme_Reservationen_ReservationsId" FOREIGN KEY ("ReservationsId") REFERENCES public."Reservationen" ("Id") ON DELETE CASCADE
);

CREATE TABLE public."MaschinenUebergabe" (
    "Id" bigserial NOT NULL,
    "Datum" timestamp without time zone NOT NULL,
    "Notiz" text NULL,
    "ReservationsId" bigint NOT NULL,
    CONSTRAINT "PK_MaschinenUebergabe" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_MaschinenUebergabe_Reservationen_ReservationsId" FOREIGN KEY ("ReservationsId") REFERENCES public."Reservationen" ("Id") ON DELETE CASCADE
);

CREATE TABLE public."Arbeitsschritte" (
    "Id" bigserial NOT NULL,
    "Bezeichnung" varchar(256) NULL,
    "Stundenansatz" double precision NULL,
    "Arbeitsstunden" double precision NULL,
    "ServiceId" bigint NOT NULL,
    CONSTRAINT "PK_Arbeitsschritte" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Arbeitsschritte_Services_ServiceId" FOREIGN KEY ("ServiceId") REFERENCES public."Services" ("Id") ON DELETE CASCADE
);

CREATE TABLE public."Materialposten" (
    "Id" bigserial NOT NULL,
    "Stueckpreis" double precision NOT NULL,
    "Anzahl" integer NOT NULL,
    "Bezeichnung" varchar(256) NULL,
    "ServiceId" bigint NOT NULL,
    CONSTRAINT "PK_Materialposten" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Materialposten_Services_ServiceId" FOREIGN KEY ("ServiceId") REFERENCES public."Services" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Arbeitsschritte_ServiceId" ON public."Arbeitsschritte" ("ServiceId");

CREATE INDEX "IX_Maschine_BesitzerId" ON public."Maschine" ("BesitzerId");

CREATE INDEX "IX_Maschine_MaschinentypId" ON public."Maschine" ("MaschinentypId");

CREATE UNIQUE INDEX "IX_MaschinenRuecknahme_ReservationsId" ON public."MaschinenRuecknahme" ("ReservationsId");

CREATE UNIQUE INDEX "IX_MaschinenUebergabe_ReservationsId" ON public."MaschinenUebergabe" ("ReservationsId");

CREATE INDEX "IX_Materialposten_ServiceId" ON public."Materialposten" ("ServiceId");

CREATE INDEX "IX_Reservationen_KundenId" ON public."Reservationen" ("KundenId");

CREATE INDEX "IX_Reservationen_MaschinenId" ON public."Reservationen" ("MaschinenId");

CREATE INDEX "IX_Services_KundenId" ON public."Services" ("KundenId");

CREATE INDEX "IX_Services_MaschinenId" ON public."Services" ("MaschinenId");

CREATE INDEX "IX_Transaktionen_KundenId" ON public."Transaktionen" ("KundenId");

CREATE INDEX "IX_Transaktionen_MaschinenId" ON public."Transaktionen" ("MaschinenId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190515170616_init', '2.1.8-servicing-32085');

