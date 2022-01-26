use books;
DROP DATABASE ESFS;
CREATE DATABASE ESFS;
USE ESFS;

CREATE TABLE Account(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Login] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[MiddleName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[IsAdmin] BIT NOT NULL
);

CREATE TABLE Test(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[TitleTest] NVARCHAR(100) NOT NULL,
);

CREATE TABLE Theory(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[TitleTheory] NVARCHAR(100) NOT NULL,
	[TextTheory] VARCHAR(8000) NOT NULL
);

CREATE TABLE Question(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[TitleQuestion] NVARCHAR(300) NOT NULL,
	[Test_Id] INT NOT NULL,
	CONSTRAINT [FK_Question_TO_Test] FOREIGN KEY (Test_Id) REFERENCES Question(Id)
);

CREATE TABLE Answer(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[TextAnswer] NVARCHAR(150) NOT NULL,
	[Question_Id] INT NOT NULL,
	CONSTRAINT [FK_Answer_TO_Question] FOREIGN KEY (Question_Id) REFERENCES Question(Id)
);

INSERT INTO Account([Login], [Password], [FirstName], [MiddleName], [LastName], [IsAdmin])VALUES
('admin', 'admin', 'admin', 'admin', 'admin', 1),
('asd', 'asd', 'asd', 'asd', 'asd', 0);

INSERT INTO Test([TitleTest])VALUES
('testTest1'),
('testTest2');

INSERT INTO Question([TitleQuestion] ,[Test_Id])VALUES
('testQuestion1', 1),
('testQuestion2', 2);

INSERT INTO Theory([TitleTheory], [TextTheory]) VALUES
('testTheory1', 'asdadadadadadadadadada'),
('testTheory2', 'asdadadadadadadadadadada'),
('testTheory3', 'asdadadadadadadadadadadauasdjndasjs
nuadsadjinsaijnsaijnsaijndasdsainjdasijnsadinjsadjinsadjindsaijsadij
ndsaijnsdaijnsadijnsdaijnsadijndasijnasdijnsadijndasijndasijnadsijndasijn
dasijndasijndasijndasijndasijndasijndasijnasdijndasijndasijndsaijnsdaij
ndasijnsad'),
('testTheory4', 'asdadadadadadadadadadadauasdjndasjs
nuadsadjinsaijnsaijnsaijndasdsainjdasijnsadinjsadjinsadjindsaijsadij
ndsaijnsdaijnsadijnsdaijnsadijndasijnasdijnsadijndasijndasijnadsijndasijn
dasijndasijndasijndasijndasijndasijndasijnasdijndasijndasijndsaijnsdaij
ndasijnsad');

INSERT INTO Answer([TextAnswer], [Question_Id])VALUES
('testQuestion1', 1),
('testQuestion2', 2);

