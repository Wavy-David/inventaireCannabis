drop database Canabis;

-- Create the database
CREATE DATABASE Canabis;

-- Switch to the Canabis database
USE Canabis;

-- Create the Entreposage table
CREATE TABLE Entreposage (
    idEntreposage VARCHAR(255) PRIMARY KEY,
    nom VARCHAR(255)
);

-- Create the Responsable table
CREATE TABLE CompteUtilisateur (
    idUtilisateur VARCHAR(255) PRIMARY KEY,
    prenom VARCHAR(255),
    nom VARCHAR(255),
	Email VARCHAR(255),
	--role VARCHAR(255),
	motdepass VARCHAR(255)
);

CREATE TABLE Responsable (
    nom VARCHAR(255) PRIMARY KEY
);

-- Create the Plante table
CREATE TABLE Plante (
    idPlante VARCHAR(255) PRIMARY KEY,
    etatSante VARCHAR(255),
    dateAjout DATE,
    provenance VARCHAR(255),
    description TEXT,
    stade VARCHAR(255),
    entreposage VARCHAR(255),
    active_Inactive INT,
    itemRetireInventaire VARCHAR(255),
    note TEXT,
    --CodeQr VARCHAR(255),
    Responsable VARCHAR(255),
	--dateRetrait DATE,
    FOREIGN KEY (entreposage) REFERENCES Entreposage(idEntreposage)
    --FOREIGN KEY (Responsable) REFERENCES CompteUtilisateur(idUtilisateur)
	--FOREIGN KEY (Responsable) REFERENCES Responsable(nom)
);

-- Archive
CREATE TABLE PlanteArchive (
    idPlante VARCHAR(255) PRIMARY KEY,
    etatSante VARCHAR(255),
    dateAjout DATE,
    provenance VARCHAR(255),
    description TEXT,
    stade VARCHAR(255),
    entreposage VARCHAR(255),
    active_Inactive INT,
    itemRetireInventaire VARCHAR(255),
    note TEXT,
    --CodeQr VARCHAR(255),
    Responsable VARCHAR(255),
	dateRetrait DATE,
    FOREIGN KEY (entreposage) REFERENCES Entreposage(idEntreposage),
    --FOREIGN KEY (Responsable) REFERENCES CompteUtilisateur(idUtilisateur)
	--FOREIGN KEY (Responsable) REFERENCES Responsable(nom)
);

-- Historique
--drop table HistoriquePlante
CREATE TABLE HistoriquePlante (
  id INT PRIMARY KEY,
  idPlante VARCHAR(255),
  action VARCHAR(255),
  Date DATE,
  champ VARCHAR(255),
  ancienneValeur VARCHAR(255),
  nouvelleValeur VARCHAR(255)
);

-- counter for plant ID's
CREATE TABLE PlanteCounter (
  nombreDePlante INT NOT NULL PRIMARY KEY
);

-- ======================================================================================

-- Insert into Entreposage
INSERT INTO Entreposage (idEntreposage, nom) VALUES
('B3200', 'LABORATOIRE GÉNÉTIQUE'),
('B3080.01', 'LABORATOIRE MICROBIOLOGIE NIVEAU 2'),
('B3070', 'LABORATOIRE INSTRUMENTATION'),
('F1260.01', 'SERRE INTÉRIEUR'),
('F1260.04', 'CHAMBRE DE CROISSANCE'),
('B3320', 'ENTREPOT');

-- Insert into CompteUtilisateur
--INSERT INTO CompteUtilisateur (idUtilisateur, prenom, nom, email, motdepass) VALUES ('U1', 'Ryan', 'Simmons', 'rya@gmail.com', '123');
--INSERT INTO CompteUtilisateur (idUtilisateur, prenom, nom, email, motdepass) VALUES ('U2', 'Leah', 'Palmer', 'lea@gmail.com', '123');
--INSERT INTO CompteUtilisateur (idUtilisateur, prenom, nom, email, motdepass) VALUES ('U3', 'Kadija', 'Houssein Youssouf', 'kadija@gmail.com', '123');
--INSERT INTO CompteUtilisateur (idUtilisateur, prenom, nom) VALUES ('Kadija Houssein Youssouf', 'Kadija', 'Houssein Youssouf');

-- Insert into Responsable
--INSERT INTO Responsable(nom) VALUES ('Kadija Houssein Youssouf');
--INSERT INTO Responsable(nom) VALUES ('Alexandre Tromas');
-- Insert into Plante
--INSERT INTO Plante (idPlante, etatSante, dateAjout, provenance, description, stade, entreposage, quantite, itemRetireInventaire, note, CodeQr, Responsable) VALUES ('P1', 'Unhealthy', '2020-05-19', 'holland', 'desc 1', 'Vegetative', 'B3200', 1, 'No', 'my note1', 'CQR1', 'U1');
--INSERT INTO Plante (idPlante, etatSante, dateAjout, provenance, description, stade, entreposage, quantite, itemRetireInventaire, note, CodeQr, Responsable) VALUES ('P2', 'Unhealthy', '2020-04-20', 'canada', 'desc 2', 'Budding', 'B3200', 0, 'Yes', 'my note2', 'CQR2', 'U2');

-- Insert into Plante
--INSERT INTO Plante (idPlante, etatSante, dateAjout, provenance, description, stade, entreposage, active_Inactive, itemRetireInventaire, note, Responsable) VALUES ('SLH5', 'rouge', '2020-05-19', 'Entreprise1', 'ACDC', 'magenta', 'F1260.04', 1, 'DESTRUCTION PAR AUTOCLAVE', 'my note1', 'Kadija Houssein Youssouf');
--INSERT INTO Plante (idPlante, etatSante, dateAjout, provenance, description, stade, entreposage, active_Inactive, itemRetireInventaire, note, Responsable) VALUES ('SLH6', 'rouge', '2020-04-20', 'Entreprise1', 'ACDC', 'initiation', 'F1260.04', 0, 'DESTRUCTION PAR AUTOCLAVE', 'my note2', 'Kadija Houssein Youssouf');


INSERT INTO PlanteCounter (nombreDePlante) VALUES (0);
-- =============================================================================================================
select * from Entreposage;

select * from CompteUtilisateur;

select * from Responsable;

select * from plante;

select * from PlanteArchive;

select * from planteCounter;

select * from HistoriquePlante;