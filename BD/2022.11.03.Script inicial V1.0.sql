use master
GO

CREATE DATABASE pruebanet;
GO

USE pruebanet
GO

CREATE TABLE tblCatSexo
( 
	intIDSexo            integer IDENTITY ( 1,1 ) ,
	strSexo              varchar(50)  NULL 
)
go



ALTER TABLE tblCatSexo
	ADD CONSTRAINT XPKtblCatSexo PRIMARY KEY (intIDSexo ASC)
go



CREATE TABLE tblUsuario
( 
	intIDUsuario         integer IDENTITY ( 1,1 ) ,
	strUsuario           varchar(150)  NOT NULL UNIQUE,
	strCorreo            varchar(300)  NOT NULL UNIQUE,
	strPassword          varchar(150)  NOT NULL ,
	bitEstatus           bit  NOT NULL ,
	dateCreacion         datetime  NOT NULL CONSTRAINT Usuario_dateCreacion_Default Default GETDATE(),
	intIDSexo            integer  NOT NULL 
)
go



ALTER TABLE tblUsuario
	ADD CONSTRAINT XPKtblUsuario PRIMARY KEY (intIDUsuario ASC)
go




ALTER TABLE tblUsuario
	ADD CONSTRAINT tblCatSexo_intIDSexo_tblUsuario FOREIGN KEY (intIDSexo) REFERENCES tblCatSexo(intIDSexo)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go



INSERT INTO tblCatSexo (strSexo)
VALUES ('Masculino')
,('Femenino')
go


SET IDENTITY_INSERT [dbo].[tblUsuario] ON 

INSERT [dbo].[tblUsuario] ([intIDUsuario], [strUsuario], [strCorreo], [strPassword], [bitEstatus], [dateCreacion], [intIDSexo]) VALUES (1, N'eduardo.ac', N'eduardo.cdm@gmail.com', N'1ZMxn41QsrWIilY2WXn3qA==', 1, CAST(N'2022-03-12T11:39:01.597' AS DateTime), 1)
INSERT [dbo].[tblUsuario] ([intIDUsuario], [strUsuario], [strCorreo], [strPassword], [bitEstatus], [dateCreacion], [intIDSexo]) VALUES (2, N'prueba1', N'prueba@gmail.com', N'1ZMxn41QsrWIilY2WXn3qA==', 0, CAST(N'2022-03-12T15:46:06.560' AS DateTime), 1)
INSERT [dbo].[tblUsuario] ([intIDUsuario], [strUsuario], [strCorreo], [strPassword], [bitEstatus], [dateCreacion], [intIDSexo]) VALUES (3, N'prueba3', N'prueba2@gmail.com', N'M0ns3rr@t1', 1, CAST(N'2022-03-12T22:13:34.490' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[tblUsuario] OFF