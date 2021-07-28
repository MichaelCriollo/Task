
USE Tasks
CREATE TABLE Tasks(
	IdTask INT NOT NULL IDENTITY(1,1),
	IdEmployee INT NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	[Status] BIT NOT NULL,
	DateStart DATETIME NOT NULL,
	DateEnd DATETIME
)

CREATE TABLE Employees(
	IdEmployee		INT NOT NULL IDENTITY(1,1),
	FirstName		VARCHAR(50) NOT NULL,
	LastName		VARCHAR(50) NOT NULL,
	Email			VARCHAR(MAX) NOT NULL,
	Mobile			VARCHAR(12) NOT NULL,
)

ALTER TABLE Employees ADD CONSTRAINT PK_IdEmployee PRIMARY KEY(IdEmployee)

ALTER TABLE Tasks ADD CONSTRAINT PK_IdTask PRIMARY KEY(IdTask)
ALTER TABLE Tasks ADD CONSTRAINT FK_IdEmployee_Tasks_Employees FOREIGN KEY (IdEmployee) REFERENCES Employees(IdEmployee)

SET IDENTITY_INSERT Employees ON 
	INSERT INTO Employees(IdEmployee, FirstName, LastName, Email, Mobile) VALUES(1, 'Michael', 'Criollo', 'alejo9851@gmail.com', '3223845351')
	INSERT INTO Employees(IdEmployee, FirstName, LastName, Email, Mobile) VALUES(2, 'Alejandro', 'Gutierrez', 'alejo9852@gmail.com', '3223845352')
	INSERT INTO Employees(IdEmployee, FirstName, LastName, Email, Mobile) VALUES(3, 'Juanito', 'Perez', 'alejo9853@gmail.com', '3223845353')
SET IDENTITY_INSERT Employees OFF 