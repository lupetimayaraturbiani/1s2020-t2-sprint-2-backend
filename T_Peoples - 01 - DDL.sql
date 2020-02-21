--DDL
CREATE DATABASE T_Peoples;

USE T_Peoples;

CREATE TABLE Funcionarios (
	IdFuncionario	INT PRIMARY KEY IDENTITY, 
	Nome			VARCHAR(200) NOT NULL,
	Sobrenome		VARCHAR(200) NOT NULL 
);



