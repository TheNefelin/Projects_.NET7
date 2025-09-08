SELECT 
	NAME AS LoginName, 
	TYPE_DESC AS AccountType, 
	create_date, 
	modify_date,
	TYPE
FROM sys.server_principals
WHERE TYPE IN ('S', 'U', 'G');
GO

CREATE LOGIN testing WITH PASSWORD = 'testing', CHECK_POLICY = OFF;
GO
CREATE DATABASE db_testing
GO
USE db_testing
GO
CREATE USER testing FOR LOGIN testing;
GO
EXEC sp_addrolemember 'db_owner', 'testing';

-- Tables -------------------------------------------------------
-- --------------------------------------------------------------

CREATE TABLE Mae_Config (
	Id INT PRIMARY KEY IDENTITY(1,1),
	ApiKey VARCHAR(256),
	IsEnableRegister BIT NOT NULL,
) 
GO

CREATE TABLE Auth_Profiles (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(50) NOT NULL,
	UNIQUE(Name),
) 
GO

CREATE TABLE Auth_Users (
	Id UNIQUEIDENTIFIER PRIMARY KEY,
	Email VARCHAR(100) NOT NULL,
	HashLogin VARCHAR(256) NOT NULL,
	SaltLogin VARCHAR(256) NOT NULL,
	HashPM VARCHAR(256),
	SaltPM VARCHAR(256),
	SqlToken VARCHAR(256) ,
	IdProfile INT NOT NULL
	UNIQUE(Email),
	FOREIGN KEY (IdProfile) REFERENCES Auth_Profiles(Id)
) 
GO

DROP TABLE Mae_Config
GO
DROP TABLE Auth_Profiles
GO
DROP TABLE Auth_Users
GO

-- Data ---------------------------------------------------------
-- --------------------------------------------------------------

SET IDENTITY_INSERT Auth_Profiles ON
GO
INSERT INTO Auth_Profiles
	(Id, Name)
VALUES
	(1, 'ADMIN'),
	(2, 'USER')
SET IDENTITY_INSERT Auth_Profiles OFF
GO

-- Stored Procedure ---------------------------------------------
-- --------------------------------------------------------------

CREATE PROCEDURE Auth_Register
	@IdUser VARCHAR(256),
	@Email VARCHAR(100),
	@HashLogin VARCHAR(256),
	@SaltLogin VARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON;

	IF 0 = (SELECT ISNULL(IsEnableRegister, 0) FROM Mae_Config WHERE Id = 1)
		BEGIN
			SELECT 0 AS IsSucces, 401 AS StatusCode, 'El Servicio No Esta Disponible' AS Msge
			RETURN
		END

	IF EXISTS (SELECT Id FROM Auth_Users WHERE Email = @Email)
		BEGIN
			SELECT 0 AS IsSucces, 400 AS StatusCode, 'El Usuario ya Existe' AS Msge
			RETURN
		END

	BEGIN TRY
		INSERT INTO Auth_Users
			(Id, Email, HashLogin, SaltLogin, Id_Profile)
		VALUES
			(@IdUser, @Email, @HashLogin, @SaltLogin, 2)

		SELECT 1 AS IsSucces, 201 AS StatusCode, 'Usuario Registrado Correctamente' AS Msge
    END TRY
    BEGIN CATCH
		SELECT 0 AS IsSucces, ERROR_STATE() AS StatusCode, ERROR_MESSAGE() AS Msge
    END CATCH
END
GO

CREATE PROCEDURE Auth_Login
	@Email VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Auth_Users SET
		SqlToken = NEWID()
	WHERE 
		Email = @Email 

	SELECT 
		a.Id AS IdUser,
		a.Email,
		a.HashLogin,
		a.SaltLogin,
		a.HashPM,
		a.SaltPM,
		a.SqlToken,
		--a.IdPerfil,
		b.Name AS Role
	FROM Auth_Users a 
		INNER JOIN Auth_Profiles b ON a.IdProfile = b.Id
	WHERE 
		a.Email = @Email 
END
GO

DROP PROCEDURE Auth_Register
GO
DROP PROCEDURE Auth_Login
GO

-- Query --------------------------------------------------------
-- --------------------------------------------------------------

SELECT * FROM Mae_Config
SELECT * FROM Auth_Profiles
SELECT * FROM Auth_Users

EXECUTE Auth_Register 'A1', 'user@example.com', 'A2', 'A3'
EXECUTE Auth_Login 'user@example.com'

-- --------------------------------------------------------------
-- --------------------------------------------------------------
