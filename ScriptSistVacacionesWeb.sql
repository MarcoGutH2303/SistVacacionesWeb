-- DROP DATABASE BDVacacionesWeb
-- Script BD Sistema Vacaciones
--
-- Creación de Base de Datos
CREATE DATABASE BDVacacionesWeb;
GO
-- Uso de Base de Datos
USE BDVacacionesWeb;
GO
-- Creación de Tablas
CREATE TABLE SVEmpresa
(
	IdEmpresa INT IDENTITY PRIMARY KEY,
	CodEmpresa CHAR(3), -- 000
	RazonSocial VARCHAR(50),
	Ruc VARCHAR(11),
	DomicilioFiscal VARCHAR(100),
	Telefono VARCHAR(20),
	CorreoElectronico VARCHAR(100),
	Estado INT, -- Activo - Inactivo
	Logo VARBINARY(MAX),
	NombreLogo VARCHAR(300),
	EstaBorrado BIT
);
GO
CREATE TABLE SVTipoDocumento
(
	IdTipoDocumento INT IDENTITY PRIMARY KEY,
	CodTipoDocumento CHAR(12), -- TPD
	Nombre VARCHAR(50),
	Estado INT, -- Activo - Inactivo
	CodEmpresa CHAR(3),
	EstaBorrado BIT
);
GO
CREATE TABLE SVCentroCosto
(
	IdCentroCosto INT IDENTITY PRIMARY KEY,
	CodCentroCosto CHAR(12), -- CCT
	CodigoAnterior VARCHAR(50),
	Nombre VARCHAR(50),
	Estado INT, -- Activo - Inactivo
	CodEmpresa CHAR(3),
	EstaBorrado BIT
);
GO
CREATE TABLE SVArea
(
	IdArea INT IDENTITY PRIMARY KEY,
	CodArea CHAR(12), -- ARA
	Nombre VARCHAR(50),
	CodCentroCosto CHAR(12),
	Estado INT, -- Activo - Inactivo
	CodEmpresa CHAR(3),
	EstaBorrado BIT	
);
GO
CREATE TABLE SVCargo
(
	IdCargo INT IDENTITY PRIMARY KEY,
	CodCargo CHAR(12), -- CRG
	Nombre VARCHAR(50),
	CodArea CHAR(12),
	Estado INT, -- Activo - Inactivo
	CodEmpresa CHAR(3),
	EstaBorrado BIT	
);
GO
CREATE TABLE SVConcepto
(
	IdConcepto INT IDENTITY PRIMARY KEY,
	CodConcepto CHAR(12), -- CPT
	Nombre VARCHAR(50),
	Recuperable INT,
	Estado INT, -- Activo - Inactivo
	CodEmpresa CHAR(3),
	EstaBorrado BIT	
);
GO
CREATE TABLE SVPersonal
(
	IdPersonal INT IDENTITY PRIMARY KEY,
	CodPersonal CHAR(12), -- PRL
	CodigoAnterior VARCHAR(50),
	Nombre VARCHAR(50),
	Apellido VARCHAR(50),
	CodTipoDocumento CHAR(12),
	NumeroDocumento VARCHAR(20),
	Sexo INT,
	FechaNacimiento DATETIME,
	Direccion VARCHAR(100),
	Telefono VARCHAR(20),
	CorreoElectronico VARCHAR(100),
	CodCentroCosto CHAR(12),
	CodArea CHAR(12),
	CodCargo CHAR(12),
	FechaIngreso DATETIME,
	CodJefe CHAR(12),
	Estado INT, -- Activo - Inactivo
	CodEmpresa CHAR(3),
	EstaBorrado BIT,
);
GO
CREATE TABLE SVSolicitud
(
	IdSolicitud INT IDENTITY PRIMARY KEY,
	CodSolicitud CHAR(12), -- SLT
	FechaSolicitud DATETIME,
	CodPersonal CHAR(12),
	CodConcepto CHAR(12),
	FechaSalida DATETIME,
	FechaRetorno DATETIME,
	NumeroDias INT,
	NumeroHoras INT,
	NumeroMinutos INT,
	Estado INT, 
	CodEmpresa CHAR(3),
	EstaBorrado BIT
);
CREATE TABLE SVAutorizacion
(
	IdAutorizacion INT IDENTITY PRIMARY KEY,
	CodAutorizacion CHAR(12), -- ATR
	CodSolicitud CHAR(12),
	CodPersonalAutorizante CHAR(12),
	FechaAutorizacion DATETIME,
	Estado INT,
	CodEmpresa CHAR(3),
	EstaBorrado BIT
);
CREATE TABLE SVVacacionesPeriodo
(
	IdVacacionesPeriodo INT IDENTITY PRIMARY KEY,
	CodVacacionesPeriodo CHAR(12), -- VAP
	FechaInicioPeriodo DATETIME,
	FechaFinPeriodo DATETIME,
	AplicarAumentoDiasAdquiridosAutomatico BIT, -- Aumentar Dias Adquiridos Automatico
	AplicarConsumoDiasAdquiridos BIT, -- Consumir Dias Adquiridos Antes De Cumplir El Año
	DiasAdquiridos DECIMAL(6, 2),
	DiasConsumidos DECIMAL(6, 2),
	DiasPorConsumir DECIMAL(6, 2),
	Estado INT,
	CodPersonal CHAR(12),
	CodEmpresa CHAR(3),
	EstaBorrado BIT
);
CREATE TABLE SVVacacionesConsumo
(
	IdVacacionesConsumo INT IDENTITY PRIMARY KEY,
	CodVacacionesPeriodo CHAR(12), 
	CodVacacionesConsumo CHAR(12), -- VAC
	FechaInicio DATETIME,
	FechaFin DATETIME,
	DiasUso INT,
	CodPersonal CHAR(12),
	CodEmpresa CHAR(3),
	EstaBorrado BIT
);
CREATE TABLE SVUsuario
(
	IdUsuario INT IDENTITY PRIMARY KEY,
	CodUsuario CHAR(12), -- USR
	CodPersonal CHAR(12),
	Usuario VARCHAR(50),
	Pass VARBINARY(MAX),
	Rol INT,
	Estado INT,
	Foto VARBINARY(MAX),
	NombreFoto VARCHAR(300),
	CodEmpresa CHAR(3),
	EstaBorrado BIT
);
CREATE TABLE SVRol
(
	IdRol INT IDENTITY PRIMARY KEY,
	CodUsuario CHAR(12),
	Empresa INT,
	Mantenimiento INT,
	Concepto INT,
	Personal INT,
	Autorizacion INT,
	Vacaciones INT,
	Reporte INT,
	Usuario INT,
	CodEmpresa CHAR(3),
	EstaBorrado BIT
);
GO
-- Creación de Funciones
--
-- Generador de Codigo - Empresa
CREATE FUNCTION funcGenerarCodigoEmpresa(@UCodigo CHAR(3))
RETURNS CHAR(3)
AS
BEGIN
	DECLARE @ACodigo CHAR(3);
	IF @UCodigo = '' OR @UCodigo IS NULL
		BEGIN
			SET @ACodigo = '001';
		END
	ELSE 
		BEGIN
			
			DECLARE @Numero INT;
			SET @Numero = CONVERT(INT, @UCodigo) + 1;
			SET @ACodigo = CONCAT(REPLICATE('0', 3 - LEN(@Numero)), @Numero);
		END
	RETURN @ACodigo;
END;
GO
--
-- Generador de Codigo Generico
CREATE FUNCTION funcGenerarCodigo(@UCodigo CHAR(12), @Siglas VARCHAR(3))
RETURNS CHAR(12)
AS
BEGIN
	DECLARE @ACodigo CHAR(12);
	IF @UCodigo = '' OR @UCodigo IS NULL
		BEGIN
			SET @ACodigo = CONCAT(@Siglas, REPLICATE('0', 8), CONVERT(CHAR(1),1));
		END
	ELSE
		BEGIN
			DECLARE @CadNumero CHAR(9);
			DECLARE @Numero INT;
			SET @CadNumero = SUBSTRING(@UCodigo,4, 9);
			SET @Numero = CONVERT(INT, @CadNumero) + 1;
			SET @ACodigo = CONCAT(SUBSTRING(@UCodigo, 0, 4), REPLICATE('0', 9 - LEN(@Numero)), @Numero);
		END	
	RETURN @ACodigo;
END;
GO
--
-- Creacion de Procedimientos Almacenados
--
-- Empresa
CREATE PROC spListarEmpresa
AS
BEGIN
	SELECT	IdEmpresa,
			CodEmpresa,
			RazonSocial,
			Ruc,
			DomicilioFiscal,
			Telefono,
			CorreoElectronico,
			Estado,
			Logo,
			NombreLogo,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdEmpresa) AS Correlativo
	FROM SVEmpresa
	WHERE EstaBorrado=0
	ORDER BY IdEmpresa;
END;
GO
CREATE PROC spGrabarEmpresa
@CodEmpresa CHAR(3),
@RazonSocial VARCHAR(50),
@Ruc VARCHAR(11),
@DomicilioFiscal VARCHAR(100),
@Telefono VARCHAR(20),
@CorreoElectronico VARCHAR(100),
@Estado INT,
@Logo VARBINARY(MAX),
@NombreLogo VARCHAR(300),
@EstaBorrado BIT
AS
BEGIN
	IF @CodEmpresa = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(3) = (SELECT ISNULL(MAX(CodEmpresa), '')
											FROM SVEmpresa);
			SET @CodEmpresa = (SELECT dbo.funcGenerarCodigoEmpresa(@UCodigo));
			INSERT INTO SVEmpresa VALUES
			(
				@CodEmpresa,
				@RazonSocial,
				@Ruc,
				@DomicilioFiscal,
				@Telefono,
				@CorreoElectronico,
				@Estado,
				@Logo,
				@NombreLogo,
				@EstaBorrado
			)
			SELECT @CodEmpresa;
		END
	ELSE
		IF @NombreLogo = ''
			BEGIN
				UPDATE SVEmpresa SET
				RazonSocial=@RazonSocial,
				Ruc=@Ruc,
				DomicilioFiscal=@DomicilioFiscal,
				Telefono=@Telefono,
				CorreoElectronico=@CorreoElectronico,
				Estado=@Estado,
				EstaBorrado=@EstaBorrado
				WHERE CodEmpresa=@CodEmpresa;
			END
		ELSE
			BEGIN
				UPDATE SVEmpresa SET
				CodEmpresa=@CodEmpresa,
				RazonSocial=@RazonSocial,
				Ruc=@Ruc,
				DomicilioFiscal=@DomicilioFiscal,
				Telefono=@Telefono,
				CorreoElectronico=@CorreoElectronico,
				Estado=@Estado,
				Logo=@Logo,
				NombreLogo=@NombreLogo,
				EstaBorrado=@EstaBorrado
				WHERE CodEmpresa=@CodEmpresa;
			END
END;
GO
CREATE PROC spRecuperarEmpresa
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	IdEmpresa,
			CodEmpresa,
			RazonSocial,
			Ruc,
			DomicilioFiscal,
			Telefono,
			CorreoElectronico,
			Estado,
			Logo,
			NombreLogo,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdEmpresa) AS Correlativo
	FROM SVEmpresa
	WHERE CodEmpresa=@CodEmpresa
	AND EstaBorrado=0
	ORDER BY IdEmpresa;
END;
GO
CREATE PROC spEliminarEmpresa
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVEmpresa
	WHERE CodEmpresa=@CodEmpresa;
END;
GO
--
-- Tipo Documento
CREATE PROC spListarTipoDocumento
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	IdTipoDocumento,
			CodTipoDocumento,
			Nombre,
			Estado,
			CodEmpresa,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdTipoDocumento) AS Correlativo
	FROM SVTipoDocumento
	WHERE CodEmpresa=@CodEmpresa
	AND EstaBorrado=0
	ORDER BY IdTipoDocumento;
END;
GO
CREATE PROC spGrabarTipoDocumento
@CodTipoDocumento CHAR(12),
@Nombre VARCHAR(50),
@Estado INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodTipoDocumento = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodTipoDocumento, '')
											FROM SVTipoDocumento
											WHERE CodEmpresa=@CodEmpresa
											ORDER BY CodTipoDocumento DESC);
			SET @CodTipoDocumento = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'TPD'));
			INSERT INTO SVTipoDocumento VALUES
			(
				@CodTipoDocumento,
				@Nombre,
				@Estado,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVTipoDocumento SET
			Nombre=@Nombre,
			Estado=@Estado,
			EstaBorrado=@EstaBorrado
			WHERE CodTipoDocumento=@CodTipoDocumento
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarTipoDocumento
@CodTipoDocumento CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	IdTipoDocumento,
			CodTipoDocumento,
			Nombre,
			Estado,
			CodEmpresa,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdTipoDocumento) AS Correlativo
	FROM SVTipoDocumento
	WHERE CodTipoDocumento=@CodTipoDocumento
	AND CodEmpresa=@CodEmpresa
	AND EstaBorrado=0
	ORDER BY IdTipoDocumento;
END;
GO
CREATE PROC spEliminarTipoDocumento
@CodTipoDocumento CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVTipoDocumento
	WHERE CodTipoDocumento=@CodTipoDocumento
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Centro Costo
CREATE PROC spListarCentroCosto
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	IdCentroCosto,
			CodCentroCosto,
			CodigoAnterior,
			Nombre,
			Estado,
			CodEmpresa,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdCentroCosto) AS Correlativo
	FROM SVCentroCosto
	WHERE CodEmpresa=@CodEmpresa
	AND EstaBorrado=0
	ORDER BY IdCentroCosto;
END;
GO
CREATE PROC spGrabarCentroCosto
@CodCentroCosto CHAR(12),
@CodigoAnterior VARCHAR(50),
@Nombre VARCHAR(50),
@Estado INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodCentroCosto = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodCentroCosto, '')
											FROM SVCentroCosto
											WHERE CodEmpresa=@CodEmpresa
											ORDER BY CodCentroCosto DESC);
			SET @CodCentroCosto = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'CCT'));
			INSERT INTO SVCentroCosto VALUES
			(
				@CodCentroCosto,
				@CodigoAnterior,
				@Nombre,
				@Estado,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVCentroCosto SET
			CodigoAnterior=@CodigoAnterior,
			Nombre=@Nombre,
			Estado=@Estado,
			EstaBorrado=@EstaBorrado
			WHERE CodCentroCosto=@CodCentroCosto
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarCentroCosto
@CodCentroCosto CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	IdCentroCosto,
			CodCentroCosto,
			CodigoAnterior,
			Nombre,
			Estado,
			CodEmpresa,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdCentroCosto) AS Correlativo
	FROM SVCentroCosto
	WHERE CodCentroCosto=@CodCentroCosto
	AND CodEmpresa=@CodEmpresa
	AND EstaBorrado=0
	ORDER BY IdCentroCosto;
END;
GO
CREATE PROC spEliminarCentroCosto
@CodCentroCosto CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVCentroCosto
	WHERE CodCentroCosto=@CodCentroCosto
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Area
CREATE PROC spListarArea
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdArea,
			T0.CodArea,
			T0.Nombre,
			T0.CodCentroCosto,
			T1.Nombre AS NombreCentroCosto,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdArea) AS Correlativo
	FROM SVArea T0
	LEFT JOIN SVCentroCosto T1 ON T0.CodCentroCosto=T1.CodCentroCosto AND T0.CodEmpresa=T1.CodEmpresa
	WHERE T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdArea;
END;
GO
CREATE PROC spGrabarArea
@CodArea CHAR(12),
@Nombre VARCHAR(50),
@CodCentroCosto CHAR(12),
@Estado INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodArea = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodArea, '')
											FROM SVArea
											WHERE CodEmpresa=@CodEmpresa
											ORDER BY CodArea DESC);
			SET @CodArea = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'ARA'));
			INSERT INTO SVArea VALUES
			(
				@CodArea,
				@Nombre,
				@CodCentroCosto,
				@Estado,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVArea SET
			Nombre=@Nombre,
			CodCentroCosto=@CodCentroCosto,
			Estado=@Estado,
			EstaBorrado=@EstaBorrado
			WHERE CodArea=@CodArea
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarArea
@CodArea CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdArea,
			T0.CodArea,
			T0.Nombre,
			T0.CodCentroCosto,
			T1.Nombre AS NombreCentroCosto,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdArea) AS Correlativo
	FROM SVArea T0
	LEFT JOIN SVCentroCosto T1 ON T0.CodCentroCosto=T1.CodCentroCosto AND T0.CodEmpresa=T1.CodEmpresa
	WHERE T0.CodArea=@CodArea
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdArea;
END;
GO
CREATE PROC spEliminarArea
@CodArea CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVArea
	WHERE CodArea=@CodArea
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Cargo
CREATE PROC spListarCargo
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdCargo,
			T0.CodCargo,
			T0.Nombre,
			T0.CodArea,
			T1.Nombre AS NombreArea,
			T2.CodCentroCosto,
			T2.Nombre AS NombreCentroCosto,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdCargo) AS Correlativo
	FROM SVCargo T0
	LEFT JOIN SVArea T1 ON T0.CodArea=T1.CodArea AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVCentroCosto T2 ON T1.CodCentroCosto=T2.CodCentroCosto AND T1.CodEmpresa=T2.CodEmpresa
	WHERE T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdCargo;
END;
GO
CREATE PROC spGrabarCargo
@CodCargo CHAR(12),
@Nombre VARCHAR(50),
@CodArea CHAR(12),
@Estado INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodCargo = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodCargo, '')
											FROM SVCargo
											WHERE CodEmpresa=@CodEmpresa
											ORDER BY CodCargo DESC);
			SET @CodCargo = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'CRG'));
			INSERT INTO SVCargo VALUES
			(
				@CodCargo,
				@Nombre,
				@CodArea,
				@Estado,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVCargo SET
			Nombre=@Nombre,
			CodArea=@CodArea,
			Estado=@Estado,
			EstaBorrado=@EstaBorrado
			WHERE CodCargo=@CodCargo
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarCargo
@CodCargo CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdCargo,
			T0.CodCargo,
			T0.Nombre,
			T0.CodArea,
			T1.Nombre AS NombreArea,
			T2.CodCentroCosto,
			T2.Nombre AS NombreCentroCosto,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdCargo) AS Correlativo
	FROM SVCargo T0
	LEFT JOIN SVArea T1 ON T0.CodArea=T1.CodArea AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVCentroCosto T2 ON T1.CodCentroCosto=T2.CodCentroCosto AND T1.CodEmpresa=T2.CodEmpresa
	WHERE T0.CodCargo=@CodCargo
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdCargo;
END;
GO
CREATE PROC spEliminarCargo
@CodCargo CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVCargo
	WHERE CodCargo=@CodCargo
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Concepto
CREATE PROC spListarConcepto
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	IdConcepto,
			CodConcepto,
			Nombre,
			Recuperable,
			Estado,
			CodEmpresa,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdConcepto) AS Correlativo
	FROM SVConcepto
	WHERE CodEmpresa=@CodEmpresa
	AND EstaBorrado=0
	ORDER BY IdConcepto;
END;
GO
CREATE PROC spGrabarConcepto
@CodConcepto CHAR(12),
@Nombre VARCHAR(50),
@Recuperable INT,
@Estado INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodConcepto = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodConcepto, '')
											FROM SVConcepto
											WHERE CodEmpresa=@CodEmpresa
											ORDER BY CodConcepto DESC);
			SET @CodConcepto = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'CPT'));
			INSERT INTO SVConcepto VALUES
			(
				@CodConcepto,
				@Nombre,
				@Recuperable,
				@Estado,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVConcepto SET
			Nombre=@Nombre,
			Recuperable=@Recuperable,
			Estado=@Estado,
			EstaBorrado=@EstaBorrado
			WHERE CodConcepto=@CodConcepto
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarConcepto
@CodConcepto CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	IdConcepto,
			CodConcepto,
			Nombre,
			Recuperable,
			Estado,
			CodEmpresa,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdConcepto) AS Correlativo
	FROM SVConcepto
	WHERE CodConcepto=@CodConcepto
	AND CodEmpresa=@CodEmpresa
	AND EstaBorrado=0
	ORDER BY IdConcepto;
END;
GO
CREATE PROC spEliminarConcepto
@CodConcepto CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVConcepto
	WHERE CodConcepto=@CodConcepto
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Personal
CREATE PROC spListarPersonal
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdPersonal,
			T0.CodPersonal,
			T0.CodigoAnterior,
			T0.Nombre,
			T0.Apellido,
			T0.CodTipoDocumento,
			T1.Nombre AS NombreTipoDocumento,
			T0.NumeroDocumento,
			T0.Sexo,
			T0.FechaNacimiento,
			T0.Direccion,
			T0.Telefono,
			T0.CorreoElectronico,
			T0.CodCentroCosto,
			T4.Nombre AS NombreCentroCosto,
			T0.CodArea,
			T3.Nombre AS NombreArea,
			T0.CodCargo,
			T2.Nombre AS NombreCargo,
			T0.FechaIngreso,
			T0.CodJefe,
			T5.Nombre AS NombreJefe, 
			T5.Apellido AS ApellidoJefe,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdPersonal) AS Correlativo
	FROM SVPersonal T0
	LEFT JOIN SVTipoDocumento T1 ON T0.CodTipoDocumento=T1.CodTipoDocumento AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVCargo T2 ON T0.CodCargo=T2.CodCargo AND T0.CodEmpresa=T2.CodEmpresa
	LEFT JOIN SVArea T3 ON T2.CodArea=T3.CodArea AND T2.CodEmpresa=T3.CodEmpresa
	LEFT JOIN SVCentroCosto T4 ON T3.CodCentroCosto=T4.CodCentroCosto AND T3.CodEmpresa=T4.CodEmpresa
	LEFT JOIN SVPersonal T5 ON T0.CodJefe=T5.CodPersonal AND T0.CodEmpresa=T5.CodEmpresa
	WHERE T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdPersonal;
END;
GO
CREATE PROC spGrabarPersonal
@CodPersonal CHAR(12),
@CodigoAnterior VARCHAR(50),
@Nombre VARCHAR(50),
@Apellido VARCHAR(50),
@CodTipoDocumento CHAR(12),
@NumeroDocumento VARCHAR(20),
@Sexo INT,
@FechaNacimiento DATETIME,
@Direccion VARCHAR(100),
@Telefono VARCHAR(20),
@CorreoElectronico VARCHAR(100),
@CodCentroCosto CHAR(12),
@CodArea CHAR(12),
@CodCargo CHAR(12),
@FechaIngreso DATETIME,
@CodJefe CHAR(12),
@Estado INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodPersonal = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodPersonal, '')
											FROM SVPersonal
											WHERE CodEmpresa=@CodEmpresa
											ORDER BY CodPersonal DESC);
			SET @CodPersonal = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'PRL'));
			INSERT INTO SVPersonal VALUES
			(
				@CodPersonal,
				@CodigoAnterior,
				@Nombre,
				@Apellido,
				@CodTipoDocumento,
				@NumeroDocumento,
				@Sexo,
				@FechaNacimiento,
				@Direccion,
				@Telefono,
				@CorreoElectronico,
				@CodCentroCosto,
				@CodArea,
				@CodCargo,
				@FechaIngreso,
				@CodJefe,
				@Estado,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVPersonal SET
			CodigoAnterior=@CodigoAnterior,
			Nombre=@Nombre,
			Apellido=@Apellido,
			CodTipoDocumento=@CodTipoDocumento,
			NumeroDocumento=@NumeroDocumento,
			Sexo=@Sexo,
			FechaNacimiento=@FechaNacimiento,
			Direccion=@Direccion,
			Telefono=@Telefono,
			CorreoElectronico=@CorreoElectronico,
			CodCentroCosto=@CodCentroCosto,
			CodArea=@CodArea,
			CodCargo=@CodCargo,
			FechaIngreso=@FechaIngreso,
			CodJefe=@CodJefe,
			Estado=@Estado,
			EstaBorrado=@EstaBorrado
			WHERE CodPersonal=@CodPersonal
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarPersonal
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdPersonal,
			T0.CodPersonal,
			T0.CodigoAnterior,
			T0.Nombre,
			T0.Apellido,
			T0.CodTipoDocumento,
			T1.Nombre AS NombreTipoDocumento,
			T0.NumeroDocumento,
			T0.Sexo,
			T0.FechaNacimiento,
			T0.Direccion,
			T0.Telefono,
			T0.CorreoElectronico,
			T0.CodCentroCosto,
			T4.Nombre AS NombreCentroCosto,
			T0.CodArea,
			T3.Nombre AS NombreArea,
			T0.CodCargo,
			T2.Nombre AS NombreCargo,
			T0.FechaIngreso,
			T0.CodJefe,
			T5.Nombre AS NombreJefe, 
			T5.Apellido AS ApellidoJefe,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdPersonal) AS Correlativo
	FROM SVPersonal T0
	LEFT JOIN SVTipoDocumento T1 ON T0.CodTipoDocumento=T1.CodTipoDocumento AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVCargo T2 ON T0.CodCargo=T2.CodCargo AND T0.CodEmpresa=T2.CodEmpresa
	LEFT JOIN SVArea T3 ON T2.CodArea=T3.CodArea AND T2.CodEmpresa=T3.CodEmpresa
	LEFT JOIN SVCentroCosto T4 ON T3.CodCentroCosto=T4.CodCentroCosto AND T3.CodEmpresa=T4.CodEmpresa
	LEFT JOIN SVPersonal T5 ON T0.CodJefe=T5.CodPersonal AND T0.CodEmpresa=T5.CodEmpresa
	WHERE T0.CodPersonal=@CodPersonal
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdPersonal;
END;
GO
CREATE PROC spEliminarPersonal
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVPersonal
	WHERE CodPersonal=@CodPersonal
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Solicitud
CREATE PROC spListarSolicitud
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdSolicitud,
			T0.CodSolicitud,
			T0.FechaSolicitud,
			T0.CodPersonal,
			T1.Nombre AS NombrePersonal,
			T1.Apellido AS ApellidoPersonal,
			T0.CodConcepto,
			T2.Nombre AS NombreConcepto,
			T0.FechaSalida,
			T0.FechaRetorno,
			T0.NumeroDias,
			T0.NumeroHoras,
			T0.NumeroMinutos,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdSolicitud) AS Correlativo
	FROM SVSolicitud T0
	LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVConcepto T2 ON T0.CodConcepto=T2.CodConcepto AND T0.CodEmpresa=T2.CodEmpresa
	WHERE T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdSolicitud;
END;
GO
CREATE PROC spGrabarSolicitud
@CodSolicitud CHAR(12),
@FechaSolicitud DATETIME,
@CodPersonal CHAR(12),
@CodConcepto CHAR(12),
@FechaSalida DATETIME,
@FechaRetorno DATETIME,
@NumeroDias INT,
@NumeroHoras INT,
@NumeroMinutos INT,
@Estado INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodSolicitud = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodSolicitud, '')
											FROM SVSolicitud
											WHERE CodEmpresa=@CodEmpresa
											ORDER BY CodSolicitud DESC);
			SET @CodSolicitud = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'SLT'));
			INSERT INTO SVSolicitud VALUES
			(
				@CodSolicitud,
				@FechaSolicitud,
				@CodPersonal,
				@CodConcepto,
				@FechaSalida,
				@FechaRetorno,
				@NumeroDias,
				@NumeroHoras,
				@NumeroMinutos,
				@Estado,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVSolicitud SET
			FechaSolicitud=@FechaSolicitud,
			CodPersonal=@CodPersonal,
			CodConcepto=@CodConcepto,
			FechaSalida=@FechaSalida,
			FechaRetorno=@FechaRetorno,
			NumeroDias=@NumeroDias,
			NumeroHoras=@NumeroHoras,
			NumeroMinutos=@NumeroMinutos,
			Estado=@Estado,
			EstaBorrado=@EstaBorrado
			WHERE CodSolicitud=@CodSolicitud
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarSolicitud
@CodSolicitud CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdSolicitud,
			T0.CodSolicitud,
			T0.FechaSolicitud,
			T0.CodPersonal,
			T1.Nombre AS NombrePersonal,
			T1.Apellido AS ApellidoPersonal,
			T0.CodConcepto,
			T2.Nombre AS NombreConcepto,
			T0.FechaSalida,
			T0.FechaRetorno,
			T0.NumeroDias,
			T0.NumeroHoras,
			T0.NumeroMinutos,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdSolicitud) AS Correlativo
	FROM SVSolicitud T0
	LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVConcepto T2 ON T0.CodConcepto=T2.CodConcepto AND T0.CodEmpresa=T2.CodEmpresa
	WHERE T0.CodSolicitud=@CodSolicitud
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdSolicitud;
END;
GO
CREATE PROC spEliminarSolicitud
@CodSolicitud CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVSolicitud
	WHERE CodSolicitud=@CodSolicitud
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Autorización
CREATE PROC spListarAutorizacion
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdAutorizacion,
			T0.CodAutorizacion,
			T0.CodSolicitud,
			T1.FechaSolicitud AS FechaSolicitud,
			T2.Nombre AS NombrePersonalSolicitante,
			T2.Apellido AS ApellidoPersonalSolicitante,
			T0.CodPersonalAutorizante,
			T3.Nombre AS NombrePersonalAutorizante,
			T3.Apellido AS ApellidoPersonalAutorizante,
			T0.FechaAutorizacion,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdAutorizacion) AS Correlativo
	FROM SVAutorizacion T0
	LEFT JOIN SVSolicitud T1 ON T0.CodSolicitud=T1.CodSolicitud AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVPersonal T2 ON T1.CodPersonal=T2.CodPersonal AND T1.CodEmpresa=T2.CodEmpresa
	LEFT JOIN SVPersonal T3 ON T0.CodPersonalAutorizante=T3.CodPersonal AND T0.CodEmpresa=T3.CodEmpresa
	WHERE T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdAutorizacion;
END;
GO
CREATE PROC spGrabarAutorizacion
@CodAutorizacion CHAR(12),
@CodSolicitud CHAR(12),
@CodPersonalAutorizante CHAR(12),
@FechaAutorizacion DATETIME,
@Estado INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodAutorizacion = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodAutorizacion, '')
											FROM SVAutorizacion
											WHERE CodEmpresa=@CodEmpresa
											ORDER BY CodAutorizacion DESC);
			SET @CodAutorizacion = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'ATR'));
			INSERT INTO SVAutorizacion VALUES
			(
				@CodAutorizacion,
				@CodSolicitud,
				@CodPersonalAutorizante,
				@FechaAutorizacion,
				@Estado,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVAutorizacion SET
			CodSolicitud=@CodSolicitud,
			CodPersonalAutorizante=@CodPersonalAutorizante,
			FechaAutorizacion=@FechaAutorizacion,
			Estado=@Estado,
			EstaBorrado=@EstaBorrado
			WHERE CodAutorizacion=@CodAutorizacion
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarAutorizacion
@CodAutorizacion CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdAutorizacion,
			T0.CodAutorizacion,
			T0.CodSolicitud,
			T1.FechaSolicitud AS FechaSolicitud,
			T2.Nombre AS NombrePersonalSolicitante,
			T2.Apellido AS ApellidoPersonalSolicitante,
			T0.CodPersonalAutorizante,
			T3.Nombre AS NombrePersonalAutorizante,
			T3.Apellido AS ApellidoPersonalAutorizante,
			T0.FechaAutorizacion,
			T0.Estado,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdAutorizacion) AS Correlativo
	FROM SVAutorizacion T0
	LEFT JOIN SVSolicitud T1 ON T0.CodSolicitud=T1.CodSolicitud AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVPersonal T2 ON T1.CodPersonal=T2.CodPersonal AND T1.CodEmpresa=T2.CodEmpresa
	LEFT JOIN SVPersonal T3 ON T0.CodPersonalAutorizante=T3.CodPersonal AND T0.CodEmpresa=T3.CodEmpresa
	WHERE T0.CodAutorizacion=@CodAutorizacion
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdAutorizacion;
END;
GO
CREATE PROC spEliminarAutorizacion
@CodAutorizacion CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVAutorizacion
	WHERE CodAutorizacion=@CodAutorizacion
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Vacaciones Periodo
CREATE PROC spListarVacacionesPeriodo
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdVacacionesPeriodo,
			T0.CodVacacionesPeriodo,
			T0.FechaInicioPeriodo,
			T0.FechaFinPeriodo,
			T0.AplicarAumentoDiasAdquiridosAutomatico,
			T0.AplicarConsumoDiasAdquiridos,
			T0.DiasAdquiridos,
			T0.DiasConsumidos,
			T0.DiasPorConsumir,
			T0.Estado,
			T0.CodPersonal,
			T1.Nombre AS NombrePersonal,
			T1.Apellido AS ApellidoPersonal,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdVacacionesPeriodo) AS Correlativo
	FROM SVVacacionesPeriodo T0
	LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa
	WHERE T0.CodPersonal=@CodPersonal
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdVacacionesPeriodo;
END;
GO
CREATE PROC spGrabarVacacionesPeriodo
@CodVacacionesPeriodo CHAR(12),
@FechaInicioPeriodo DATETIME,
@FechaFinPeriodo DATETIME,
@AplicarAumentoDiasAdquiridosAutomatico BIT,
@AplicarConsumoDiasAdquiridos BIT,
@DiasAdquiridos INT,
@DiasConsumidos INT,
@DiasPorConsumir INT,
@Estado INT,
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodVacacionesPeriodo = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodVacacionesPeriodo, '')
											FROM SVVacacionesPeriodo
											WHERE CodPersonal=@CodPersonal
											AND CodEmpresa=@CodEmpresa
											ORDER BY CodVacacionesPeriodo DESC);
			SET @CodVacacionesPeriodo = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'VAP'));
			INSERT INTO SVVacacionesPeriodo VALUES
			(
				@CodVacacionesPeriodo,
				@FechaInicioPeriodo,
				@FechaFinPeriodo,
				@AplicarAumentoDiasAdquiridosAutomatico,
				@AplicarConsumoDiasAdquiridos,
				@DiasAdquiridos,
				@DiasConsumidos,
				@DiasPorConsumir,
				@Estado,
				@CodPersonal,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVVacacionesPeriodo SET
			FechaInicioPeriodo=@FechaInicioPeriodo,
			FechaFinPeriodo=@FechaFinPeriodo,
			AplicarAumentoDiasAdquiridosAutomatico=@AplicarAumentoDiasAdquiridosAutomatico,
			AplicarConsumoDiasAdquiridos=@AplicarConsumoDiasAdquiridos,
			DiasAdquiridos=@DiasAdquiridos,
			DiasConsumidos=@DiasConsumidos,
			DiasPorConsumir=@DiasPorConsumir,
			Estado=@Estado,
			EstaBorrado=@EstaBorrado
			WHERE CodVacacionesPeriodo=@CodVacacionesPeriodo
			AND CodPersonal=@CodPersonal
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarVacacionesPeriodo
@CodVacacionesPeriodo CHAR(12),
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdVacacionesPeriodo,
			T0.CodVacacionesPeriodo,
			T0.FechaInicioPeriodo,
			T0.FechaFinPeriodo,
			T0.AplicarAumentoDiasAdquiridosAutomatico,
			T0.AplicarConsumoDiasAdquiridos,
			T0.DiasAdquiridos,
			T0.DiasConsumidos,
			T0.DiasPorConsumir,
			T0.Estado,
			T0.CodPersonal,
			T1.Nombre AS NombrePersonal,
			T1.Apellido AS ApellidoPersonal,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdVacacionesPeriodo) AS Correlativo
	FROM SVVacacionesPeriodo T0
	LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa
	WHERE T0.CodVacacionesPeriodo=@CodVacacionesPeriodo
	AND T0.CodPersonal=@CodPersonal
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdVacacionesPeriodo;
END;
GO
CREATE PROC spEliminarVacacionesPeriodo
@CodVacacionesPeriodo CHAR(12),
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVVacacionesPeriodo
	WHERE CodVacacionesPeriodo=@CodVacacionesPeriodo
	AND CodPersonal=@CodPersonal
	AND CodEmpresa=@CodEmpresa;
END;
GO
CREATE PROC spAumentoDiasAdquiridosPeriodoAutomatico
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @TBPersonal AS TABLE
	(
		IdPersonal INT,
		CodPersonal CHAR(12),
		Nombre VARCHAR(50),
		Apellido VARCHAR(50),
		Estado INT,
		CodEmpresa CHAR(3),
		EstaBorrado BIT
	);
	BEGIN
		DECLARE @sqlConsultaPersonal NVARCHAR(MAX);
		SET @sqlConsultaPersonal = 'SELECT IdPersonal,
									CodPersonal,
									Nombre,
									Apellido,
									Estado,
									CodEmpresa,
									EstaBorrado
									FROM SVPersonal
									WHERE CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + '''
									AND EstaBorrado=0
									ORDER BY IdPersonal';
		INSERT INTO @TBPersonal
		EXECUTE sp_executesql @sqlConsultaPersonal;
	END;
	
	DECLARE @aumentador INT;
	DECLARE @CodPersonal CHAR(12);
	DECLARE cursorPersonal CURSOR 
		FOR SELECT CodPersonal FROM @TBPersonal;
	OPEN cursorPersonal;
	FETCH NEXT FROM cursorPersonal INTO @CodPersonal;
	IF @@FETCH_STATUS <> 0
		PRINT '<<None>>'
	WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE @TBVacacionesPeriodo AS TABLE
			(
				IdVacacionesPeriodo INT,
				CodVacacionesPeriodo CHAR(12), -- VAP
				FechaInicioPeriodo DATETIME,
				FechaFinPeriodo DATETIME,
				DiasAdquiridos DECIMAL(6, 2),
				DiasConsumidos DECIMAL(6, 2),
				DiasPorConsumir DECIMAL(6, 2),
				Estado INT,
				CodPersonal CHAR(12),
				CodEmpresa CHAR(3),
				EstaBorrado BIT
			);
			BEGIN
				DECLARE @sqlConsultaVacacionesPeriodo NVARCHAR(MAX);
				SET @sqlConsultaVacacionesPeriodo = 'SELECT IdVacacionesPeriodo,
													CodVacacionesPeriodo,
													FechaInicioPeriodo,
													FechaFinPeriodo,
													DiasAdquiridos,
													DiasConsumidos,
													DiasPorConsumir,
													Estado,
													CodPersonal,
													CodEmpresa,
													EstaBorrado
													FROM SVVacacionesPeriodo
													WHERE CodPersonal=''' + CAST(@CodPersonal AS NVARCHAR(12)) + '''
													AND CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + '''
													AND AplicarAumentoDiasAdquiridosAutomatico=1
													AND EstaBorrado=0
													ORDER BY IdVacacionesPeriodo';
				INSERT INTO @TBVacacionesPeriodo
				EXECUTE sp_executesql @sqlConsultaVacacionesPeriodo;
			END;

			DECLARE @CodPersonalVP CHAR(12);
			DECLARE @CodVacacionesPeriodoVP CHAR(12);
			DECLARE @FechaInicioVP DATETIME;
			DECLARE @FechaFinVP DATETIME;
			DECLARE cursorVacacionesPeriodo CURSOR 
				FOR SELECT CodPersonal, CodVacacionesPeriodo, FechaInicioPeriodo, FechaFinPeriodo FROM @TBVacacionesPeriodo;
			OPEN cursorVacacionesPeriodo;
			FETCH NEXT FROM cursorVacacionesPeriodo INTO @CodPersonalVP, @CodVacacionesPeriodoVP, @FechaInicioVP, @FechaFinVP;
			IF @@FETCH_STATUS <> 0
				PRINT '<<None>>'
			WHILE @@FETCH_STATUS = 0
			BEGIN
				--SELECT @CodPersonalVP;
				IF GETDATE() < @FechaFinVP
					BEGIN
						DECLARE @diasAdquiridos DECIMAL(6, 2) = 0;  
						DECLARE @fecha DATETIME = @FechaInicioVP;
						SET @fecha = DATEADD(DAY, -1, DATEADD(MONTH, 1, @fecha));
						WHILE @fecha <= GETDATE()
							BEGIN
								SET @diasAdquiridos = @diasAdquiridos + 2.50;
								SET @fecha = DATEADD(MONTH, 1, @fecha);
							END;
						UPDATE SVVacacionesPeriodo SET
						DiasAdquiridos=@diasAdquiridos,
						DiasPorConsumir=@diasAdquiridos-DiasConsumidos
						WHERE CodVacacionesPeriodo=@CodVacacionesPeriodoVP
						AND CodPersonal=@CodPersonalVP
						AND CodEmpresa=@CodEmpresa;
					END;
				ELSE 
					UPDATE SVVacacionesPeriodo SET
					AplicarConsumoDiasAdquiridos=1,
					DiasAdquiridos=30.00,
					DiasPorConsumir=30.00-DiasConsumidos
					WHERE CodVacacionesPeriodo=@CodVacacionesPeriodoVP
					AND CodPersonal=@CodPersonalVP
					AND CodEmpresa=@CodEmpresa;
				FETCH NEXT FROM cursorVacacionesPeriodo INTO @CodPersonalVP, @CodVacacionesPeriodoVP, @FechaInicioVP, @FechaFinVP;
			END
			CLOSE cursorVacacionesPeriodo;
			DEALLOCATE cursorVacacionesPeriodo;
			DELETE FROM @TBVacacionesPeriodo;
			FETCH NEXT FROM cursorPersonal INTO @CodPersonal;
		END;
	CLOSE cursorPersonal;
	DEALLOCATE cursorPersonal;
END;
GO
--
-- Vacaciones Consumo
CREATE PROC spListarVacacionesConsumo
@CodVacacionesPeriodo CHAR(12),
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdVacacionesConsumo,
			T0.CodVacacionesPeriodo,
			T0.CodVacacionesConsumo,
			T0.FechaInicio,
			T0.FechaFin,
			T0.DiasUso,
			T0.CodPersonal,
			T1.Nombre AS NombrePersonal,
			T1.Apellido AS ApellidoPersonal,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdVacacionesConsumo) AS Correlativo
	FROM SVVacacionesConsumo T0
	LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa
	WHERE CodVacacionesPeriodo=@CodVacacionesPeriodo
	AND T0.CodPersonal=@CodPersonal
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdVacacionesConsumo;
END;
GO
CREATE PROC spGrabarVacacionesConsumo
@CodVacacionesPeriodo CHAR(12),
@CodVacacionesConsumo CHAR(12),
@FechaInicio DATETIME,
@FechaFin DATETIME,
@DiasUso INT,
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodVacacionesConsumo = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodVacacionesConsumo, '')
											FROM SVVacacionesConsumo
											WHERE CodVacacionesPeriodo=@CodVacacionesPeriodo
											AND CodPersonal=@CodPersonal
											AND CodEmpresa=@CodEmpresa
											ORDER BY CodVacacionesConsumo DESC);
			SET @CodVacacionesConsumo = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'VAC'));
			INSERT INTO SVVacacionesConsumo VALUES
			(
				@CodVacacionesPeriodo,
				@CodVacacionesConsumo,
				@FechaInicio,
				@FechaFin,
				@DiasUso,
				@CodPersonal,
				@CodEmpresa,
				@EstaBorrado
			);
			DECLARE @diasadquiridosg DECIMAL(6, 2);
			DECLARE @diasconsumidosg DECIMAL(6, 2);
			DECLARE @diasporconsumirg DECIMAL(6, 2);
			SET @diasadquiridosg = (SELECT ISNULL(DiasAdquiridos, 0) 
									FROM SVVacacionesPeriodo 
									WHERE CodPersonal=@CodPersonal 
									AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
									AND CodEmpresa=@CodEmpresa
									AND EstaBorrado=0);

			SET @diasconsumidosg = (SELECT ISNULL(SUM(DiasUso), 0) 
									FROM SVVacacionesConsumo 
									WHERE CodPersonal=@CodPersonal 
									AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
									AND CodEmpresa=@CodEmpresa
									AND EstaBorrado=0);

			SET @diasporconsumirg = @diasadquiridosg - @diasconsumidosg;
			IF @diasporconsumirg = 0
				UPDATE SVVacacionesPeriodo SET
				DiasConsumidos=@diasconsumidosg,
				DiasPorConsumir=@diasporconsumirg,
				Estado=2
				WHERE CodPersonal=@CodPersonal 
				AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
				AND CodEmpresa=@CodEmpresa;
			ELSE IF @diasporconsumirg > 0
				UPDATE SVVacacionesPeriodo set
				DiasConsumidos=@diasconsumidosg,
				DiasPorConsumir=@diasporconsumirg,
				Estado=1
				WHERE CodPersonal=@CodPersonal 
				AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
				AND CodEmpresa=@CodEmpresa;
		END
	ELSE
		BEGIN
			UPDATE SVVacacionesConsumo SET
			FechaInicio=@FechaInicio,
			FechaFin=@FechaFin,
			DiasUso=@DiasUso,
			EstaBorrado=@EstaBorrado
			WHERE CodVacacionesConsumo=@CodVacacionesConsumo
			AND CodVacacionesPeriodo=@CodVacacionesPeriodo
			AND CodPersonal=@CodPersonal
			AND CodEmpresa=@CodEmpresa;
			DECLARE @diasadquiridosa DECIMAL(6, 2);
			DECLARE @diasconsumidosa DECIMAL(6, 2);
			DECLARE @diasporconsumira DECIMAL(6, 2);
			SET @diasadquiridosa = (SELECT ISNULL(DiasAdquiridos, 0) 
									FROM SVVacacionesPeriodo 
									WHERE CodPersonal=@CodPersonal 
									AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
									AND CodEmpresa=@CodEmpresa
									AND EstaBorrado=0);

			SET @diasconsumidosa = (SELECT ISNULL(SUM(DiasUso), 0) 
									FROM SVVacacionesConsumo 
									WHERE CodPersonal=@CodPersonal 
									AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
									AND CodEmpresa=@CodEmpresa
									AND EstaBorrado=0);
			SET @diasporconsumira = @diasadquiridosa - @diasconsumidosa;
			IF @diasporconsumira = 0 
				UPDATE SVVacacionesPeriodo SET
				DiasConsumidos=@diasconsumidosa,
				DiasPorConsumir=@diasporconsumira,
				Estado=2,
				CodEmpresa=@CodEmpresa
				WHERE CodPersonal=@CodPersonal
				AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
				AND CodEmpresa=@CodEmpresa;
			ELSE
				UPDATE SVVacacionesPeriodo SET
				DiasConsumidos=@diasconsumidosa,
				DiasPorConsumir=@diasporconsumira,
				Estado=1,
				CodEmpresa=@CodEmpresa
				WHERE CodPersonal=@CodPersonal
				AND CodVacacionesPeriodo=@CodVacacionesPeriodo
				AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarVacacionesConsumo
@CodVacacionesPeriodo CHAR(12),
@CodVacacionesConsumo CHAR(12),
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdVacacionesConsumo,
			T0.CodVacacionesPeriodo,
			T0.CodVacacionesConsumo,
			T0.FechaInicio,
			T0.FechaFin,
			T0.DiasUso,
			T0.CodPersonal,
			T1.Nombre AS NombrePersonal,
			T1.Apellido AS ApellidoPersonal,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdVacacionesConsumo) AS Correlativo
	FROM SVVacacionesConsumo T0
	LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa
	WHERE T0.CodVacacionesConsumo=@CodVacacionesConsumo
	AND T0.CodVacacionesPeriodo=@CodVacacionesPeriodo
	AND T0.CodPersonal=@CodPersonal
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdVacacionesConsumo;
END;
GO
CREATE PROC spEliminarVacacionesConsumo
@CodVacacionesConsumo CHAR(12),
@CodVacacionesPeriodo CHAR(12),
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVVacacionesConsumo
	WHERE CodVacacionesConsumo=@CodVacacionesConsumo
	AND CodVacacionesPeriodo=@CodVacacionesPeriodo
	AND CodPersonal=@CodPersonal
	AND CodEmpresa=@CodEmpresa;

	DECLARE @diasadquiridosa DECIMAL(6, 2);
	DECLARE @diasconsumidosa DECIMAL(6, 2);
	DECLARE @diasporconsumira DECIMAL(6, 2);
	SET @diasadquiridosa = (SELECT ISNULL(DiasAdquiridos, 0) 
							FROM SVVacacionesPeriodo 
							WHERE CodPersonal=@CodPersonal 
							AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
							AND CodEmpresa=@CodEmpresa
							AND EstaBorrado=0);

	SET @diasconsumidosa = (SELECT ISNULL(SUM(DiasUso), 0) 
							FROM SVVacacionesConsumo 
							WHERE CodPersonal=@CodPersonal 
							AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
							AND CodEmpresa=@CodEmpresa
							AND EstaBorrado=0);

	SET @diasporconsumira = @diasadquiridosa - @diasconsumidosa;
	IF @diasporconsumira = 0 
		UPDATE SVVacacionesPeriodo SET
		DiasConsumidos=@diasconsumidosa,
		DiasPorConsumir=@diasporconsumira,
		Estado=2,
		CodEmpresa=@CodEmpresa
		WHERE CodPersonal=@CodPersonal
		AND CodVacacionesPeriodo=@CodVacacionesPeriodo 
		AND CodEmpresa=@CodEmpresa;
	ELSE
		UPDATE SVVacacionesPeriodo SET
		DiasConsumidos=@diasconsumidosa,
		DiasPorConsumir=@diasporconsumira,
		Estado=1,
		CodEmpresa=@CodEmpresa
		WHERE CodPersonal=@CodPersonal
		AND CodVacacionesPeriodo=@CodVacacionesPeriodo
		AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Usuario
CREATE PROC spListarUsuario
@Llave VARCHAR(30),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdUsuario,
			T0.CodUsuario,
			T0.CodPersonal,
			T1.Nombre AS NombrePersonal,
			T1.Apellido AS ApellidoPersonal,
			T2.Nombre AS NombreTipoDocumentoPersonal,
			T1.NumeroDocumento AS NumeroDocumentoPersonal,
			T0.Usuario,
			DECRYPTBYPASSPHRASE(@Llave, T0.Pass) AS Pass,
			T0.Rol,
			T0.Estado,
			T0.Foto,
			T0.NombreFoto,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdUsuario) AS Correlativo
	FROM SVUsuario T0
	LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVTipoDocumento T2 ON T1.CodTipoDocumento=T2.CodTipoDocumento AND T1.CodEmpresa=T2.CodEmpresa
	WHERE T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdUsuario;
END;
GO
CREATE PROC spGrabarUsuario
@CodUsuario CHAR(12),
@CodPersonal CHAR(12),
@Usuario VARCHAR(50),
@Pass VARCHAR(50),
@Llave VARCHAR(30),
@Rol INT,
@Foto VARBINARY(MAX),
@NombreFoto VARCHAR(300),
@Estado INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @CodUsuario = ''
		BEGIN
			DECLARE @UCodigo AS CHAR(12) = (SELECT TOP 1 ISNULL(CodUsuario, '')
											FROM SVUsuario
											WHERE CodEmpresa=@CodEmpresa
											ORDER BY CodUsuario DESC);
			SET @CodUsuario = (SELECT dbo.funcGenerarCodigo(@UCodigo, 'USR'));
			INSERT INTO SVUsuario VALUES
			(
				@CodUsuario,
				@CodPersonal,
				@Usuario,
				CONVERT(VARBINARY(MAX), ENCRYPTBYPASSPHRASE(@Llave, @Pass)),
				@Rol,
				@Estado,
				@Foto,
				@NombreFoto,
				@CodEmpresa,
				@EstaBorrado
			)
			SELECT @CodUsuario;
		END
	ELSE
		IF @NombreFoto = ''
			BEGIN
				UPDATE SVUsuario SET
				CodPersonal=@CodPersonal,
				Usuario=@Usuario,
				Pass=CONVERT(VARBINARY(MAX), ENCRYPTBYPASSPHRASE(@Llave, @Pass)),
				Rol=@Rol,
				Estado=@Estado,
				EstaBorrado=@EstaBorrado
				WHERE CodUsuario=@CodUsuario
				AND CodEmpresa=@CodEmpresa;
			END
		ELSE
			BEGIN
				UPDATE SVUsuario SET
				CodPersonal=@CodPersonal,
				Usuario=@Usuario,
				Pass=CONVERT(VARBINARY(MAX), ENCRYPTBYPASSPHRASE(@Llave, @Pass)),
				Rol=@Rol,
				Foto=@Foto,
				NombreFoto=@NombreFoto,
				Estado=@Estado,
				EstaBorrado=@EstaBorrado
				WHERE CodUsuario=@CodUsuario
				AND CodEmpresa=@CodEmpresa;
			END
END;
GO
CREATE PROC spRecuperarUsuario
@CodUsuario CHAR(12),
@Llave VARCHAR(30),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdUsuario,
			T0.CodUsuario,
			T0.CodPersonal,
			T1.Nombre AS NombrePersonal,
			T1.Apellido AS ApellidoPersonal,
			T2.Nombre AS NombreTipoDocumentoPersonal,
			T1.NumeroDocumento AS NumeroDocumentoPersonal,
			T0.Usuario,
			DECRYPTBYPASSPHRASE(@Llave, T0.Pass) AS Pass,
			T0.Rol,
			T0.Estado,
			T0.Foto,
			T0.NombreFoto,
			T0.CodEmpresa,
			T0.EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY T0.IdUsuario) AS Correlativo
	FROM SVUsuario T0
	LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVTipoDocumento T2 ON T1.CodTipoDocumento=T2.CodTipoDocumento AND T1.CodEmpresa=T2.CodEmpresa
	WHERE T0.CodUsuario=@CodUsuario
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0
	ORDER BY T0.IdUsuario;
END;
GO
CREATE PROC spEliminarUsuario
@CodUsuario CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVUsuario
	WHERE CodUsuario=@CodUsuario
	AND CodEmpresa=@CodEmpresa;

	DELETE
	FROM SVRol
	WHERE CodUsuario=@CodUsuario
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Rol
CREATE PROC spListarRol
@CodUsuario CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	IdRol,
			CodUsuario,
			Empresa,
			Mantenimiento,
			Concepto,
			Personal,
			Autorizacion,
			Vacaciones,
			Reporte,
			Usuario,
			CodEmpresa,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdRol) AS Correlativo
	FROM SVRol
	WHERE CodUsuario=@CodUsuario
	AND CodEmpresa=@CodEmpresa
	AND EstaBorrado=0
	ORDER BY IdRol;
END;
GO
CREATE PROC spGrabarRol
@IdRol INT,
@CodUsuario CHAR(12),
@Empresa INT,
@Mantenimiento INT,
@Concepto INT,
@Personal INT,
@Autorizacion INT,
@Vacaciones INT,
@Reporte INT,
@Usuario INT,
@CodEmpresa CHAR(3),
@EstaBorrado BIT
AS
BEGIN
	IF @IdRol = 0
		BEGIN
			INSERT INTO SVRol VALUES
			(
				@CodUsuario,
				@Empresa,
				@Mantenimiento,
				@Concepto,
				@Personal,
				@Autorizacion,
				@Vacaciones,
				@Reporte,
				@Usuario,
				@CodEmpresa,
				@EstaBorrado
			);
		END
	ELSE
		BEGIN
			UPDATE SVRol SET
			Empresa=@Empresa,
			Mantenimiento=@Mantenimiento,
			Concepto=@Concepto,
			Personal=@Personal,
			Autorizacion=@Autorizacion,
			Vacaciones=@Vacaciones,
			Reporte=@Reporte,
			Usuario=@Usuario,
			EstaBorrado=@EstaBorrado
			WHERE CodUsuario=@CodUsuario
			AND CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spRecuperarRol
@CodUsuario CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	IdRol,
			CodUsuario,
			Empresa,
			Mantenimiento,
			Concepto,
			Personal,
			Autorizacion,
			Vacaciones,
			Reporte,
			Usuario,
			CodEmpresa,
			EstaBorrado,
			ROW_NUMBER() OVER (ORDER BY IdRol) AS Correlativo
	FROM SVRol
	WHERE CodUsuario=@CodUsuario
	AND CodEmpresa=@CodEmpresa
	AND EstaBorrado=0
	ORDER BY IdRol;
END;
GO
CREATE PROC spEliminarRol
@CodUsuario CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DELETE
	FROM SVRol
	WHERE CodUsuario=@CodUsuario
	AND CodEmpresa=@CodEmpresa;
END;
GO
--
-- Login
CREATE PROC spValidarLogin
@Usuario VARCHAR(50),
@Pass VARCHAR(50),
@Llave VARCHAR(30),
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @CodUsuario CHAR(12);
	SET @CodUsuario = (ISNULL((	SELECT ISNULL(CodUsuario, '') AS Usuario 
							FROM SVUsuario	
							WHERE Usuario=@Usuario COLLATE Latin1_General_CS_AS 
							AND CONVERT(VARBINARY(MAX), DECRYPTBYPASSPHRASE(@Llave, Pass))=CONVERT(VARBINARY(MAX), @Pass)
							AND CodEmpresa=@CodEmpresa), ''));
	SELECT @CodUsuario AS Usuario;
END;
GO
CREATE PROC spRecuperarLogin
@CodUsuario CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T0.IdUsuario,
			T0.CodUsuario,
			T1.CodPersonal,
			T1.Nombre,
			T1.Apellido,
			T0.Usuario,
			T0.Pass,
			T0.Rol,
			T0.Foto,
			T0.NombreFoto,
			T0.EstaBorrado,
			T0.CodEmpresa,
			T2.RazonSocial,
			T2.Ruc,
			T2.CorreoElectronico,
			T2.Logo,
			T2.NombreLogo,
			ROW_NUMBER() OVER (ORDER BY T0.IdUsuario) AS Correlativo
	FROM SVUsuario T0
	LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVEmpresa T2 ON T0.CodEmpresa=T2.CodEmpresa
	WHERE T0.CodUsuario=@CodUsuario
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.EstaBorrado=0;
END;
GO
--
-- Panel De Control
CREATE PROC spPanelControlAdministrador
@CodEmpresa	CHAR(3)
AS
BEGIN
	DECLARE @CantSolicitudPendiente INT;
	DECLARE @CantSolicitudResuelto INT;
	DECLARE @CantAutorizacionRealizado INT;
	DECLARE @CantVacacionesPeriodo INT;
	SET @CantSolicitudPendiente = (SELECT COUNT(*)
									FROM SVSolicitud
									WHERE CodEmpresa=@CodEmpresa
									AND Estado = 1
									AND EstaBorrado = 0);
	SET @CantSolicitudResuelto = (SELECT COUNT(*)
									FROM SVSolicitud
									WHERE CodEmpresa=@CodEmpresa
									AND Estado = 2
									AND EstaBorrado = 0);
	SET @CantAutorizacionRealizado = (SELECT COUNT(*)
									FROM SVAutorizacion
									WHERE CodEmpresa=@CodEmpresa
									AND EstaBorrado = 0);
	SET @CantVacacionesPeriodo = (SELECT COUNT(*)
									FROM SVVacacionesPeriodo
									WHERE CodEmpresa=@CodEmpresa
									AND FechaFinPeriodo < GETDATE()
									AND Estado = 1
									AND EstaBorrado = 0);
	SELECT	@CantSolicitudPendiente AS CantSolicitudPendiente,
			@CantSolicitudResuelto AS CantSolicitudResuelto,
			@CantAutorizacionRealizado AS CantAutorizacionRealizado,
			@CantVacacionesPeriodo AS CantVacacionesPeriodo;
END;
GO
CREATE PROC spPanelControlEmpleado
@CodPersonal CHAR(12),
@CodEmpresa	CHAR(3)
AS
BEGIN
	DECLARE @CantSolicitudPendiente INT;
	DECLARE @CantAutorizacionRealizado INT;
	DECLARE @CantVacacionesPeriodo INT;
	SET @CantSolicitudPendiente = (SELECT COUNT(*)
									FROM SVSolicitud
									WHERE CodPersonal=@CodPersonal
									AND CodEmpresa=@CodEmpresa
									AND Estado = 1
									AND EstaBorrado = 0);
	SET @CantAutorizacionRealizado = (SELECT COUNT(*)
									FROM SVSolicitud
									WHERE CodPersonal=@CodPersonal
									AND CodEmpresa=@CodEmpresa
									AND Estado = 2
									AND EstaBorrado = 0);
	SET @CantVacacionesPeriodo = (SELECT COUNT(*)
									FROM SVVacacionesPeriodo
									WHERE CodPersonal=@CodPersonal
									AND CodEmpresa=@CodEmpresa
									AND EstaBorrado = 0);
	SELECT	@CantSolicitudPendiente AS CantSolicitudPendiente,
			@CantAutorizacionRealizado AS CantAutorizacionRealizado,
			@CantVacacionesPeriodo AS CantVacacionesPeriodo;
END;
GO
--
-- Reportes - Solicitud
CREATE PROC spReporteSolicitudGeneral
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @Table AS TABLE
	(
		IdSolicitud INT,
		CodSolicitud CHAR(12),
		FechaSolicitud DATETIME,
		CodPersonal CHAR(12),
		NombrePersonal VARCHAR(100),
		CodTipoDocumento CHAR(12),
		NombreTipoDocumento VARCHAR(50),
		NumeroDocumento VARCHAR(20),
		NombreJefe VARCHAR(100),	
		CodConcepto CHAR(12),
		NombreConcepto VARCHAR(50),
		FechaSalida DATETIME,
		FechaRetorno DATETIME,
		NumeroDias INT,
		NumeroHoras INT,
		NumeroMinutos INT,
		Estado INT,
		CodEmpresa CHAR(3),
		Correlativo INT
	);
	BEGIN
		DECLARE @Ssql NVARCHAR(MAX);
		SET @Ssql = 'SELECT	T0.IdSolicitud,
							T0.CodSolicitud,
							T0.FechaSolicitud,
							T0.CodPersonal,
							CONCAT(T1.Nombre, '' ' + ''', T1.Apellido) AS NombrePersonal,
							T1.CodTipoDocumento,
							T2.Nombre AS NombreTipoDocumento,
							T1.NumeroDocumento,
							CONCAT(T3.Nombre, '' ' + ''', T3.Apellido) AS NombreJefe,	
							T0.CodConcepto,
							T4.Nombre AS NombreConcepto,
							T0.FechaSalida,
							T0.FechaRetorno,
							T0.NumeroDias,
							T0.NumeroHoras,
							T0.NumeroMinutos,
							T0.Estado,
							T0.CodEmpresa,
							ROW_NUMBER() OVER (ORDER BY T0.IdSolicitud) AS Correlativo
					FROM SVSolicitud T0
					LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa AND T1.EstaBorrado=0
					LEFT JOIN SVTipoDocumento T2 ON T1.CodTipoDocumento=T2.CodTipoDocumento AND T1.CodEmpresa=T2.CodEmpresa AND T2.EstaBorrado=0
					LEFT JOIN SVPersonal T3 ON T1.CodJefe=T3.CodPersonal AND T1.CodEmpresa=T3.CodEmpresa AND T3.EstaBorrado=0
					LEFT JOIN SVConcepto T4 ON T0.CodConcepto=T4.CodConcepto AND T0.CodEmpresa=T4.CodEmpresa AND T4.EstaBorrado=0
					WHERE T0.CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + ''' 
					AND T0.EstaBorrado=0
					ORDER BY T0.IdSolicitud;';
		INSERT INTO @Table
		EXECUTE sp_executesql @Ssql;
	END
	IF (SELECT COUNT(*) FROM @Table) > 0
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM @Table T0, SVEmpresa T1
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
	ELSE
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM SVEmpresa T1 
			LEFT JOIN @Table T0 ON T1.CodEmpresa=T0.CodEmpresa
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spReporteSolicitudPersonal
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @Table AS TABLE
	(
		IdSolicitud INT,
		CodSolicitud CHAR(12),
		FechaSolicitud DATETIME,
		CodPersonal CHAR(12),
		NombrePersonal VARCHAR(100),
		CodTipoDocumento CHAR(12),
		NombreTipoDocumento VARCHAR(50),
		NumeroDocumento VARCHAR(20),
		NombreJefe VARCHAR(100),	
		CodConcepto CHAR(12),
		NombreConcepto VARCHAR(50),
		FechaSalida DATETIME,
		FechaRetorno DATETIME,
		NumeroDias INT,
		NumeroHoras INT,
		NumeroMinutos INT,
		Estado INT,
		CodEmpresa CHAR(3),
		Correlativo INT
	);
	BEGIN
		DECLARE @Ssql NVARCHAR(MAX);
		SET @Ssql = 'SELECT	T0.IdSolicitud,
							T0.CodSolicitud,
							T0.FechaSolicitud,
							T0.CodPersonal,
							CONCAT(T1.Nombre, '' ' + ''', T1.Apellido) AS NombrePersonal,
							T1.CodTipoDocumento,
							T2.Nombre AS NombreTipoDocumento,
							T1.NumeroDocumento,
							CONCAT(T3.Nombre, '' ' + ''', T3.Apellido) AS NombreJefe,	
							T0.CodConcepto,
							T4.Nombre AS NombreConcepto,
							T0.FechaSalida,
							T0.FechaRetorno,
							T0.NumeroDias,
							T0.NumeroHoras,
							T0.NumeroMinutos,
							T0.Estado,
							T0.CodEmpresa,
							ROW_NUMBER() OVER (ORDER BY T0.IdSolicitud) AS Correlativo
					FROM SVSolicitud T0
					LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa AND T1.EstaBorrado=0
					LEFT JOIN SVTipoDocumento T2 ON T1.CodTipoDocumento=T2.CodTipoDocumento AND T1.CodEmpresa=T2.CodEmpresa AND T2.EstaBorrado=0
					LEFT JOIN SVPersonal T3 ON T1.CodJefe=T3.CodPersonal AND T1.CodEmpresa=T3.CodEmpresa AND T3.EstaBorrado=0
					LEFT JOIN SVConcepto T4 ON T0.CodConcepto=T4.CodConcepto AND T0.CodEmpresa=T4.CodEmpresa AND T4.EstaBorrado=0
					WHERE T0.CodPersonal=''' + CAST(@CodPersonal AS NVARCHAR(12)) + '''
					AND T0.CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + ''' 
					AND T0.EstaBorrado=0
					ORDER BY T0.IdSolicitud;';
		INSERT INTO @Table
		EXECUTE sp_executesql @Ssql;
	END
	IF (SELECT COUNT(*) FROM @Table) > 0
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM @Table T0, SVEmpresa T1
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
	ELSE
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM SVEmpresa T1 
			LEFT JOIN @Table T0 ON T1.CodEmpresa=T0.CodEmpresa
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spReporteSolicitudFecha
@FechaInicio DATETIME,
@FechaFin DATETIME,
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @Table AS TABLE
	(
		IdSolicitud INT,
		CodSolicitud CHAR(12),
		FechaSolicitud DATETIME,
		CodPersonal CHAR(12),
		NombrePersonal VARCHAR(100),
		CodTipoDocumento CHAR(12),
		NombreTipoDocumento VARCHAR(50),
		NumeroDocumento VARCHAR(20),
		NombreJefe VARCHAR(100),	
		CodConcepto CHAR(12),
		NombreConcepto VARCHAR(50),
		FechaSalida DATETIME,
		FechaRetorno DATETIME,
		NumeroDias INT,
		NumeroHoras INT,
		NumeroMinutos INT,
		Estado INT,
		CodEmpresa CHAR(3),
		Correlativo INT
	);
	BEGIN
		DECLARE @Desde VARCHAR(20);
		SET @Desde = convert(VARCHAR(20), @FechaInicio, 101);
		DECLARE @Hasta VARCHAR(20);
		SET @Hasta = convert(VARCHAR(20), @FechaFin, 101);
		DECLARE @Ssql NVARCHAR(MAX);
		SET @Ssql = 'SELECT	T0.IdSolicitud,
							T0.CodSolicitud,
							T0.FechaSolicitud,
							T0.CodPersonal,
							CONCAT(T1.Nombre, '' ' + ''', T1.Apellido) AS NombrePersonal,
							T1.CodTipoDocumento,
							T2.Nombre AS NombreTipoDocumento,
							T1.NumeroDocumento,
							CONCAT(T3.Nombre, '' ' + ''', T3.Apellido) AS NombreJefe,	
							T0.CodConcepto,
							T4.Nombre AS NombreConcepto,
							T0.FechaSalida,
							T0.FechaRetorno,
							T0.NumeroDias,
							T0.NumeroHoras,
							T0.NumeroMinutos,
							T0.Estado,
							T0.CodEmpresa,
							ROW_NUMBER() OVER (ORDER BY T0.IdSolicitud) AS Correlativo
					FROM SVSolicitud T0
					LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa AND T1.EstaBorrado=0
					LEFT JOIN SVTipoDocumento T2 ON T1.CodTipoDocumento=T2.CodTipoDocumento AND T1.CodEmpresa=T2.CodEmpresa AND T2.EstaBorrado=0
					LEFT JOIN SVPersonal T3 ON T1.CodJefe=T3.CodPersonal AND T1.CodEmpresa=T3.CodEmpresa AND T3.EstaBorrado=0
					LEFT JOIN SVConcepto T4 ON T0.CodConcepto=T4.CodConcepto AND T0.CodEmpresa=T4.CodEmpresa AND T4.EstaBorrado=0
					WHERE T0.FechaSolicitud>=''' + @Desde + '''
					AND T0.FechaSolicitud<DATEADD(DAY, 1, ''' + @Hasta + ''') 
					AND T0.CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + ''' 
					AND T0.EstaBorrado=0
					ORDER BY T0.IdSolicitud;';
		INSERT INTO @Table
		EXECUTE sp_executesql @Ssql;
	END
	IF (SELECT COUNT(*) FROM @Table) > 0
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM @Table T0, SVEmpresa T1
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
	ELSE
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM SVEmpresa T1 
			LEFT JOIN @Table T0 ON T1.CodEmpresa=T0.CodEmpresa
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
END;
GO
--
-- Reportes - Autorizacion
CREATE PROC spReporteAutorizacionGeneral
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @Table AS TABLE
	(
		IdAutorizacion INT,
		CodAutorizacion CHAR(12),
		CodSolicitud CHAR(12),
		FechaSolicitud DATETIME,
		NombrePersonal VARCHAR(100),
		CodTipoDocumento CHAR(12),
		NombreTipoDocumento VARCHAR(50),
		NumeroDocumento VARCHAR(20),
		CodPersonalAutorizante CHAR(12),
		NombreAutorizante VARCHAR(100),	
		FechaAutorizacion DATETIME,
		Estado INT,
		CodEmpresa CHAR(3),
		Correlativo INT
	);
	BEGIN
		DECLARE @Ssql NVARCHAR(MAX);
		SET @Ssql = 'SELECT	T0.IdAutorizacion,
							T0.CodAutorizacion,
							T0.CodSolicitud,
							T1.FechaSolicitud AS FechaSolicitud,
							CONCAT(T2.Nombre, '' ' + ''', T2.Apellido) AS NombrePersonal,
							T2.CodTipoDocumento,
							T3.Nombre AS NombreTipoDocumento,
							T2.NumeroDocumento,
							T0.CodPersonalAutorizante,
							CONCAT(T4.Nombre, '' ' + ''', T4.Apellido) AS NombreAutorizante,
							T0.FechaAutorizacion,
							T0.Estado,
							T0.CodEmpresa,
							ROW_NUMBER() OVER (ORDER BY T0.IdAutorizacion) AS Correlativo
					FROM SVAutorizacion T0
					LEFT JOIN SVSolicitud T1 ON T0.CodSolicitud=T1.CodSolicitud AND T0.CodEmpresa=T1.CodEmpresa AND T1.EstaBorrado=0
					LEFT JOIN SVPersonal T2 ON T1.CodPersonal=T2.CodPersonal AND T1.CodEmpresa=T2.CodEmpresa AND T2.EstaBorrado=0
					LEFT JOIN SVTipoDocumento T3 ON T2.CodTipoDocumento=T3.CodTipoDocumento AND T2.CodEmpresa=T3.CodEmpresa AND T3.EstaBorrado=0
					LEFT JOIN SVPersonal T4 ON T0.CodPersonalAutorizante=T4.CodPersonal AND T0.CodEmpresa=T4.CodEmpresa AND T4.EstaBorrado=0
					WHERE T0.CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + '''
					AND T0.EstaBorrado=0
					ORDER BY T0.IdAutorizacion;';
		INSERT INTO @Table
		EXECUTE sp_executesql @Ssql;
	END
	IF (SELECT COUNT(*) FROM @Table) > 0
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM @Table T0, SVEmpresa T1
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
	ELSE
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM SVEmpresa T1 
			LEFT JOIN @Table T0 ON T1.CodEmpresa=T0.CodEmpresa
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spReporteAutorizacionPersonal
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @Table AS TABLE
	(
		IdAutorizacion INT,
		CodAutorizacion CHAR(12),
		CodSolicitud CHAR(12),
		FechaSolicitud DATETIME,
		NombrePersonal VARCHAR(100),
		CodTipoDocumento CHAR(12),
		NombreTipoDocumento VARCHAR(50),
		NumeroDocumento VARCHAR(20),
		CodPersonalAutorizante CHAR(12),
		NombreAutorizante VARCHAR(100),	
		FechaAutorizacion DATETIME,
		Estado INT,
		CodEmpresa CHAR(3),
		Correlativo INT
	);
	BEGIN
		DECLARE @Ssql NVARCHAR(MAX);
		SET @Ssql = 'SELECT	T0.IdAutorizacion,
							T0.CodAutorizacion,
							T0.CodSolicitud,
							T1.FechaSolicitud AS FechaSolicitud,
							CONCAT(T2.Nombre, '' ' + ''', T2.Apellido) AS NombrePersonal,
							T2.CodTipoDocumento,
							T3.Nombre AS NombreTipoDocumento,
							T2.NumeroDocumento,
							T0.CodPersonalAutorizante,
							CONCAT(T4.Nombre, '' ' + ''', T4.Apellido) AS NombreAutorizante,
							T0.FechaAutorizacion,
							T0.Estado,
							T0.CodEmpresa,
							ROW_NUMBER() OVER (ORDER BY T0.IdAutorizacion) AS Correlativo
					FROM SVAutorizacion T0
					LEFT JOIN SVSolicitud T1 ON T0.CodSolicitud=T1.CodSolicitud AND T0.CodEmpresa=T1.CodEmpresa AND T1.EstaBorrado=0
					LEFT JOIN SVPersonal T2 ON T1.CodPersonal=T2.CodPersonal AND T1.CodEmpresa=T2.CodEmpresa AND T2.EstaBorrado=0
					LEFT JOIN SVTipoDocumento T3 ON T2.CodTipoDocumento=T3.CodTipoDocumento AND T2.CodEmpresa=T3.CodEmpresa AND T3.EstaBorrado=0
					LEFT JOIN SVPersonal T4 ON T0.CodPersonalAutorizante=T4.CodPersonal AND T0.CodEmpresa=T4.CodEmpresa AND T4.EstaBorrado=0
					WHERE T1.CodPersonal=''' + CAST(@CodPersonal AS NVARCHAR(12)) + '''
					AND T0.CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + '''
					AND T0.EstaBorrado=0
					ORDER BY T0.IdAutorizacion;';
		INSERT INTO @Table
		EXECUTE sp_executesql @Ssql;
	END
	IF (SELECT COUNT(*) FROM @Table) > 0
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM @Table T0, SVEmpresa T1
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
	ELSE
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM SVEmpresa T1 
			LEFT JOIN @Table T0 ON T1.CodEmpresa=T0.CodEmpresa
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spReporteAutorizacionFecha
@FechaInicio DATETIME,
@FechaFin DATETIME,
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @Table AS TABLE
	(
		IdAutorizacion INT,
		CodAutorizacion CHAR(12),
		CodSolicitud CHAR(12),
		FechaSolicitud DATETIME,
		NombrePersonal VARCHAR(100),
		CodTipoDocumento CHAR(12),
		NombreTipoDocumento VARCHAR(50),
		NumeroDocumento VARCHAR(20),
		CodPersonalAutorizante CHAR(12),
		NombreAutorizante VARCHAR(100),	
		FechaAutorizacion DATETIME,
		Estado INT,
		CodEmpresa CHAR(3),
		Correlativo INT
	);
	BEGIN
		DECLARE @Desde VARCHAR(20);
		SET @Desde = convert(VARCHAR(20), @FechaInicio, 101);
		DECLARE @Hasta VARCHAR(20);
		SET @Hasta = convert(VARCHAR(20), @FechaFin, 101);
		DECLARE @Ssql NVARCHAR(MAX);
		SET @Ssql = 'SELECT	T0.IdAutorizacion,
							T0.CodAutorizacion,
							T0.CodSolicitud,
							T1.FechaSolicitud AS FechaSolicitud,
							CONCAT(T2.Nombre, '' ' + ''', T2.Apellido) AS NombrePersonal,
							T2.CodTipoDocumento,
							T3.Nombre AS NombreTipoDocumento,
							T2.NumeroDocumento,
							T0.CodPersonalAutorizante,
							CONCAT(T4.Nombre, '' ' + ''', T4.Apellido) AS NombreAutorizante,
							T0.FechaAutorizacion,
							T0.Estado,
							T0.CodEmpresa,
							ROW_NUMBER() OVER (ORDER BY T0.IdAutorizacion) AS Correlativo
					FROM SVAutorizacion T0
					LEFT JOIN SVSolicitud T1 ON T0.CodSolicitud=T1.CodSolicitud AND T0.CodEmpresa=T1.CodEmpresa AND T1.EstaBorrado=0
					LEFT JOIN SVPersonal T2 ON T1.CodPersonal=T2.CodPersonal AND T1.CodEmpresa=T2.CodEmpresa AND T2.EstaBorrado=0
					LEFT JOIN SVTipoDocumento T3 ON T2.CodTipoDocumento=T3.CodTipoDocumento AND T2.CodEmpresa=T3.CodEmpresa AND T3.EstaBorrado=0
					LEFT JOIN SVPersonal T4 ON T0.CodPersonalAutorizante=T4.CodPersonal AND T0.CodEmpresa=T4.CodEmpresa AND T4.EstaBorrado=0
					WHERE T0.FechaAutorizacion>=''' + @Desde + '''
					AND T0.FechaAutorizacion<DATEADD(DAY, 1, ''' + @Hasta + ''')
					AND T0.CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + '''
					AND T0.EstaBorrado=0
					ORDER BY T0.IdAutorizacion;';
		INSERT INTO @Table
		EXECUTE sp_executesql @Ssql;
	END
	IF (SELECT COUNT(*) FROM @Table) > 0
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM @Table T0, SVEmpresa T1
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
	ELSE
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM SVEmpresa T1 
			LEFT JOIN @Table T0 ON T1.CodEmpresa=T0.CodEmpresa
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
END;
GO
--
-- Reportes - Vacaciones
CREATE PROC spReporteVacacionesGeneral
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @Table AS TABLE
	(
		IdVacacionesPeriodo INT,
		CodVacacionesPeriodo CHAR(12),
		FechaInicioPeriodo DATETIME,
		FechaFinPeriodo DATETIME,
		DiasAdquiridos DECIMAL(6, 2),
		DiasConsumidos DECIMAL(6, 2),
		DiasPorConsumir DECIMAL(6, 2),
		Estado INT,
		CodPersonal CHAR(12),
		NombrePersonal VARCHAR(100),
		CodTipoDocumento CHAR(12),
		NombreTipoDocumento VARCHAR(50),
		NumeroDocumento VARCHAR(20),
		CodEmpresa CHAR(3),
		CodVacacionesConsumo CHAR(12),
		FechaInicioConsumo DATETIME,
		FechaFinConsumo DATETIME,
		DiasUso INT,
		Correlativo INT
	);
	BEGIN
		DECLARE @Ssql NVARCHAR(MAX);
		SET @Ssql = 'SELECT	T0.IdVacacionesPeriodo,
							T0.CodVacacionesPeriodo,
							T0.FechaInicioPeriodo,
							T0.FechaFinPeriodo,
							T0.DiasAdquiridos,
							T0.DiasConsumidos,
							T0.DiasPorConsumir,
							T0.Estado,
							T0.CodPersonal,
							CONCAT(T1.Nombre, '' ' + ''', T1.Apellido) AS NombrePersonal,
							T1.CodTipoDocumento,
							T2.Nombre AS NombreTipoDocumento,
							T1.NumeroDocumento,
							T0.CodEmpresa,
							T3.CodVacacionesConsumo,
							T3.FechaInicio AS FechaInicioConsumo,
							T3.FechaFin AS FechaFinConsumo,
							T3.DiasUso,
							ROW_NUMBER() OVER (ORDER BY T0.IdVacacionesPeriodo) AS Correlativo
					FROM SVVacacionesPeriodo T0
					LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa AND T1.EstaBorrado=0
					LEFT JOIN SVTipoDocumento T2 ON T1.CodTipoDocumento=T2.CodTipoDocumento AND T1.CodEmpresa=T2.CodEmpresa AND T2.EstaBorrado=0
					LEFT JOIN SVVacacionesConsumo T3 ON T0.CodVacacionesPeriodo=T3.CodVacacionesPeriodo AND T0.CodPersonal=T3.CodPersonal AND T0.CodEmpresa=T3.CodEmpresa AND T3.EstaBorrado=0
					WHERE T0.CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + '''
					AND T0.EstaBorrado=0
					ORDER BY T0.IdVacacionesPeriodo;';
		INSERT INTO @Table
		EXECUTE sp_executesql @Ssql;
	END
	IF (SELECT COUNT(*) FROM @Table) > 0
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM @Table T0, SVEmpresa T1
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
	ELSE
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM SVEmpresa T1 
			LEFT JOIN @Table T0 ON T1.CodEmpresa=T0.CodEmpresa
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spReporteVacacionesPersonal
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	DECLARE @Table AS TABLE
	(
		IdVacacionesPeriodo INT,
		CodVacacionesPeriodo CHAR(12),
		FechaInicioPeriodo DATETIME,
		FechaFinPeriodo DATETIME,
		DiasAdquiridos DECIMAL(6, 2),
		DiasConsumidos DECIMAL(6, 2),
		DiasPorConsumir DECIMAL(6, 2),
		Estado INT,
		CodPersonal CHAR(12),
		NombrePersonal VARCHAR(100),
		CodTipoDocumento CHAR(12),
		NombreTipoDocumento VARCHAR(50),
		NumeroDocumento VARCHAR(20),
		CodEmpresa CHAR(3),
		CodVacacionesConsumo CHAR(12),
		FechaInicioConsumo DATETIME,
		FechaFinConsumo DATETIME,
		DiasUso INT,
		Correlativo INT
	);
	BEGIN
		DECLARE @Ssql NVARCHAR(MAX);
		SET @Ssql = 'SELECT	T0.IdVacacionesPeriodo,
							T0.CodVacacionesPeriodo,
							T0.FechaInicioPeriodo,
							T0.FechaFinPeriodo,
							T0.DiasAdquiridos,
							T0.DiasConsumidos,
							T0.DiasPorConsumir,
							T0.Estado,
							T0.CodPersonal,
							CONCAT(T1.Nombre, '' ' + ''', T1.Apellido) AS NombrePersonal,
							T1.CodTipoDocumento,
							T2.Nombre AS NombreTipoDocumento,
							T1.NumeroDocumento,
							T0.CodEmpresa,
							T3.CodVacacionesConsumo,
							T3.FechaInicio AS FechaInicioConsumo,
							T3.FechaFin AS FechaFinConsumo,
							T3.DiasUso,
							ROW_NUMBER() OVER (ORDER BY T0.IdVacacionesPeriodo) AS Correlativo
					FROM SVVacacionesPeriodo T0
					LEFT JOIN SVPersonal T1 ON T0.CodPersonal=T1.CodPersonal AND T0.CodEmpresa=T1.CodEmpresa AND T1.EstaBorrado=0
					LEFT JOIN SVTipoDocumento T2 ON T1.CodTipoDocumento=T2.CodTipoDocumento AND T1.CodEmpresa=T2.CodEmpresa AND T2.EstaBorrado=0
					LEFT JOIN SVVacacionesConsumo T3 ON T0.CodVacacionesPeriodo=T3.CodVacacionesPeriodo AND T0.CodPersonal=T3.CodPersonal  AND T0.CodEmpresa=T3.CodEmpresa AND T3.EstaBorrado=0
					WHERE T0.CodPersonal=''' + CAST(@CodPersonal AS NVARCHAR(12)) + '''
					AND T0.CodEmpresa=''' + CAST(@CodEmpresa AS NVARCHAR(3)) + '''
					AND T0.EstaBorrado=0
					ORDER BY T0.IdVacacionesPeriodo;';
		INSERT INTO @Table
		EXECUTE sp_executesql @Ssql;
	END
	IF (SELECT COUNT(*) FROM @Table) > 0
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM @Table T0, SVEmpresa T1
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
	ELSE
		BEGIN
			SELECT T0.*,
			T1.RazonSocial,
			T1.Ruc,
			T1.DomicilioFiscal,
			T1.Telefono,
			T1.CorreoElectronico,
			T1.Logo
			FROM SVEmpresa T1 
			LEFT JOIN @Table T0 ON T1.CodEmpresa=T0.CodEmpresa
			WHERE T1.CodEmpresa=@CodEmpresa;
		END
END;
GO
CREATE PROC spListarAutorizacionPersonal
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T1.CodAutorizacion,
			T0.CodSolicitud,
			T0.FechaSolicitud,
			T0.CodPersonal,
			CONCAT(T2.Nombre, ' ', T2.Apellido) AS NombreCompletoPersonal,
			T0.CodConcepto,
			T3.Nombre AS NombreConcepto,
			T0.FechaSalida,
			T0.FechaRetorno,
			T0.NumeroDias,
			T0.NumeroHoras,
			T0.NumeroMinutos,
			CONCAT(T4.Nombre, ' ', T4.Apellido) AS NombreCompletoAutorizante,
			T1.FechaAutorizacion,
			T1.Estado AS EstadoAutorizacion,
			T0.CodEmpresa,
			ROW_NUMBER() OVER (ORDER BY T0.IdSolicitud) AS Correlativo
	FROM SVSolicitud T0
	LEFT JOIN SVAutorizacion T1 ON T0.CodSolicitud=T1.CodSolicitud AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVPersonal T2 ON T0.CodPersonal=T2.CodPersonal AND T0.CodEmpresa=T2.CodEmpresa
	LEFT JOIN SVConcepto T3 ON T0.CodConcepto=T3.CodConcepto AND T0.CodEmpresa=T3.CodEmpresa
	LEFT JOIN SVPersonal T4 ON T1.CodPersonalAutorizante=T4.CodPersonal AND T1.CodEmpresa=T4.CodEmpresa
	WHERE T0.CodPersonal=@CodPersonal
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.Estado!=1 
	AND T0.EstaBorrado=0
	ORDER BY T0.IdSolicitud;
END;
GO
CREATE PROC spRecuperarAutorizacionPersonal
@CodAutorizacion CHAR(12),
@CodPersonal CHAR(12),
@CodEmpresa CHAR(3)
AS
BEGIN
	SELECT	T1.CodAutorizacion,
			T0.CodSolicitud,
			T0.FechaSolicitud,
			T0.CodPersonal,
			CONCAT(T2.Nombre, ' ', T2.Apellido) AS NombreCompletoPersonal,
			T0.CodConcepto,
			T3.Nombre AS NombreConcepto,
			T0.FechaSalida,
			T0.FechaRetorno,
			T0.NumeroDias,
			T0.NumeroHoras,
			T0.NumeroMinutos,
			CONCAT(T4.Nombre, ' ', T4.Apellido) AS NombreCompletoAutorizante,
			T1.FechaAutorizacion,
			T1.Estado AS EstadoAutorizacion,
			T0.CodEmpresa,
			ROW_NUMBER() OVER (ORDER BY T0.IdSolicitud) AS Correlativo
	FROM SVSolicitud T0
	LEFT JOIN SVAutorizacion T1 ON T0.CodSolicitud=T1.CodSolicitud AND T0.CodEmpresa=T1.CodEmpresa
	LEFT JOIN SVPersonal T2 ON T0.CodPersonal=T2.CodPersonal AND T0.CodEmpresa=T2.CodEmpresa
	LEFT JOIN SVConcepto T3 ON T0.CodConcepto=T3.CodConcepto AND T0.CodEmpresa=T3.CodEmpresa
	LEFT JOIN SVPersonal T4 ON T1.CodPersonalAutorizante=T4.CodPersonal AND T1.CodEmpresa=T4.CodEmpresa
	WHERE T1.CodAutorizacion=@CodAutorizacion
	AND T0.CodPersonal=@CodPersonal
	AND T0.CodEmpresa=@CodEmpresa
	AND T0.Estado!=1 
	AND T0.EstaBorrado=0
	ORDER BY T0.IdSolicitud;
END;
GO
--
-- Valores por Defecto
INSERT INTO SVEmpresa VALUES
('001', 'Prueba', '123', '', '', '', 1, NULL, '', 0);

INSERT INTO SVUsuario VALUES
('USR000000001', '', 'admin', CONVERT(VARBINARY(MAX), ENCRYPTBYPASSPHRASE('SistVacacionesWeb', '123')), 2, 1, NULL, '', '001', 0);

INSERT INTO SVRol VALUES
('USR000000001', 1, 1, 1, 1, 1, 1, 1, 1, '001', 0);

INSERT INTO SVTipoDocumento VALUES
('TPD000000001', 'Dni', 1, '001', 0),
('TPD000000002', 'Ruc', 1, '001', 0),
('TPD000000003', 'Carnet De Extranjeria', 1, '001', 0),
('TPD000000004', 'Pasaporte', 1, '001', 0);
