This is the test project
Home page you can upload document:
<img width="1689" alt="image" src="https://github.com/ntv317/DocumnetParserProject/assets/17463862/da95b319-00ee-4760-957a-0a3b0c86d7ca">
Operation page to see opearations and can filter it using diffirent parameters:
<img width="1732" alt="image" src="https://github.com/ntv317/DocumnetParserProject/assets/17463862/32a96d14-5790-4837-bf1e-ec74465408c3">


To create DB and table for project this script saved in Script.sql on the Database project:
```
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
```
