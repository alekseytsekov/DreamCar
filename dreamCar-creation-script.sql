IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'DreamCar')
	CREATE DATABASE DreamCar

GO

use DreamCar

CREATE TABLE Dealers (
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	)

GO

CREATE TABLE Cars (
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,  
	Model NVARCHAR(200) NOT NULL,  
	Description NVARCHAR(4000) NOT NULL,
	YearBuilt SMALLINT NOT NULL,  
	HorsePower SMALLINT NOT NULL, 
	DealerId INT,
	FOREIGN KEY (DealerId) REFERENCES Dealers(Id) 
)  

GO 