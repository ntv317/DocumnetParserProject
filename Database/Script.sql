CREATE DATABASE DocumentParserProject;
go
CREATE TABLE Operations (
                              Id NVARCHAR(50) PRIMARY KEY,
                              Amount DECIMAL(28, 2) NOT NULL,
                              CurrencyCode CHAR(3) NOT NULL,
                              Date DATETIME NOT NULL,
                              Status INT NOT NULL
);
Go