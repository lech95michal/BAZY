-- Tworzenie

CREATE TABLE Client (
    Pesel varchar(11) NOT NULL,
    FirstName varchar(45) NOT NULL,
    LastName varchar(45) NOT NULL,
    CONSTRAINT Client_Pk PRIMARY KEY(Pesel)
);

CREATE TABLE Employee (
    IdEmployee bigint NOT NULL,
    FirstName varchar(45) NOT NULL,
    LastName varchar(45) NOT NULL,
    CONSTRAINT Employee_Pk PRIMARY KEY(IdEmployee)
);

CREATE TABLE Animal (
    IdAnimal bigint NOT NULL,
    Name varchar(45) NOT NULL,
    Type varchar(45) NOT NULL,
    Age int NOT NULL,
    CONSTRAINT Animal_Pk PRIMARY KEY(IdAnimal)
); 

CREATE TABLE Adoption (
    Pesel varchar(11) NOT NULL,
    IdEmployee bigint NOT NULL,
    IdAnimal bigint NOT NULL,
    Date date NOT NULL,
    Remarks varchar(100) NOT NULL,
    CONSTRAINT Adoption_Pk PRIMARY KEY(Pesel, IdEmployee, IdAnimal)
);

ALTER TABLE Adoption
ADD CONSTRAINT Fk_Client_Adoption
FOREIGN KEY (Pesel) REFERENCES Client(Pesel);

ALTER TABLE Adoption
ADD CONSTRAINT Fk_Employee_Adoption
FOREIGN KEY (IdEmployee) REFERENCES Employee(IdEmployee);

ALTER TABLE Adoption
ADD CONSTRAINT Fk_Animal_Adoption
FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal);

-- Usuwanie

ALTER TABLE Adoption
DROP CONSTRAINT Fk_Client_Adoption;

ALTER TABLE Adoption
DROP CONSTRAINT Fk_Employee_Adoption;

ALTER TABLE Adoption
DROP CONSTRAINT Fk_Animal_Adoption;

ALTER TABLE Adoption
DROP CONSTRAINT Adoption_Pk;

ALTER TABLE Animal
DROP CONSTRAINT Animal_Pk;

ALTER TABLE Employee
DROP CONSTRAINT Employee_Pk;

ALTER TABLE Client
DROP CONSTRAINT Client_Pk;

DROP TABLE Adoption; 
DROP TABLE Animal; 
DROP TABLE Employee; 
DROP TABLE Client; 

-- Zapytania

-- Zliczenie adopcji dla klienta
SELECT C.Pesel, FirstName + ' ' + LastName FullName, COUNT(*) Adoptions
FROM Adoption A
JOIN Client C ON A.Pesel = C.Pesel
GROUP BY C.Pesel, FirstName, LastName
ORDER BY Adoptions DESC

-- Zliczenie adopcji wykonanych przez danego pracownika
SELECT FirstName + ' ' + LastName FullName, COUNT(*) Adoptions
FROM Adoption A
JOIN Employee E on A.IdEmployee = E.IdEmployee
GROUP BY FirstName, LastName
ORDER BY Adoptions DESC

-- Wyswietlenie danych o adopcji
SELECT A.Date, C.FirstName + ' ' + C.LastName Client, E.FirstName + ' ' + E.LastName Employee,
       An.Name Animal, A.Remarks
FROM Adoption A
JOIN Client C ON A.Pesel = C.Pesel
JOIN Employee E ON A.IdEmployee = E.IdEmployee
JOIN Animal An ON A.IdAnimal = An.IdAnimal
GROUP BY C.FirstName, C.LastName, E.FirstName, E.LastName, A.Date, An.Name, A.Remarks
ORDER BY A.Date ASC
