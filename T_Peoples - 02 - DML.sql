-- DML

USE T_Peoples;

INSERT INTO Funcionarios (Nome, Sobrenome)
VALUES ('Catarina', 'Strada'),
	   ('Tadeu', 'Vitelli');


INSERT INTO TipoUsuario (Titulo)
VALUES ('Comum'),
	   ('Administrador');

INSERT INTO Usuarios (Email, Senha, IdTipoUsuario)
VALUES ('carol@gmail.com', 'carol123', 1),
       ('adm@gmail.com', 'adm123', 2);

UPDATE Funcionarios SET DataNascimento = '17/02/1997' WHERE IdFuncionario = 4;

