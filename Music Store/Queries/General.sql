DROP TABLE Songs;
DROP Table Artists;

CREATE TABLE Songs (
	Id INT PRIMARY KEY,
	Title VARCHAR(255),
	Artist VARCHAR(255),
	ArtistId INT REFERENCES Artists(Id),
	Art VARCHAR(255),
	Cost INT,
	Popular BIT DEFAULT 0,
	OnSale BIT DEFAULT 0,
	Link VARCHAR(255) NOT NULL DEFAULT 'https://youtu.be/embed/VBlFHuCzPgY/'
);

CREATE TABLE Users (
	Username VARCHAR(255) PRIMARY KEY,
	Password VARCHAR(255),
	Name VARCHAR(255),
	Email Varchar(255),
	Money INT DEFAULT 100,
	Owned VARCHAR(255) DEFAULT '',
	IsAdmin BIT DEFAULT 0
);

CREATE TABLE Artists (
	Id INT PRIMARY KEY,
	Name VARCHAR(255),
	Photo VARCHAR(255)
);

ALTER TABLE Users ADD IsAdmin BIT DEFAULT 0;
UPDATE Users SET IsAdmin = 1 WHERE Username = 'Xanthis';

SELECT * FROM Users WHERE Username = 'Xanthis';
SELECT * FROM Songs;
SELECT * FROM Artists;

--DELETE FROM Songs WHERE Id = 23;

UPDATE Songs SET Link='https://youtu.be/embed/VBlFHuCzPgY';