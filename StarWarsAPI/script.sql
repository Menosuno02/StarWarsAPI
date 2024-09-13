CREATE TABLE Species
(
	IdSpecies INT PRIMARY KEY,
	NameSpecies NVARCHAR(100) NOT NULL
);

CREATE TABLE Planets
(
	IdPlanet INT PRIMARY KEY,
	NamePlanet NVARCHAR(100) NOT NULL
);

CREATE TABLE Habitants
(
	IdHabitant INT PRIMARY KEY,
	NameHabitant NVARCHAR(100) NOT NULL,
	IdSpecies INT NOT NULL,
	IdHomePlanet INT NOT NULL,
	IsRebel BIT NOT NULL,
	CONSTRAINT FK_SPECIES FOREIGN KEY (IdSpecies) REFERENCES Species(IdSpecies) ON DELETE CASCADE,
	CONSTRAINT FK_HOMEPLANET FOREIGN KEY (IdHomePlanet) REFERENCES Planets(IdPlanet) ON DELETE CASCADE
);

INSERT INTO Species VALUES (1, 'Human');
INSERT INTO Species VALUES (2, 'Wookiee');
INSERT INTO Species VALUES (3, 'Twilek');
INSERT INTO Species VALUES (4, 'Droid');

INSERT INTO Planets VALUES (1, 'Tatooine');
INSERT INTO Planets VALUES (2, 'Kashyyyk');
INSERT INTO Planets VALUES (3, 'Ryloth');
INSERT INTO Planets VALUES (4, 'Coruscant');

INSERT INTO Habitants (IdHabitant, NameHabitant, IdSpecies, IdHomePlanet, IsRebel) VALUES (1, 'Luke Skywalker', 1, 1, 1);
INSERT INTO Habitants VALUES (2, 'Chewbacca', 2, 2, 1);
INSERT INTO Habitants VALUES (3, 'Aayla Secura', 3, 3, 1);
INSERT INTO Habitants VALUES (4, 'C-3PO', 4, 4, 0);
INSERT INTO Habitants VALUES (5, 'Leia Organa', 1, 4, 1);
