-- Creamos base de datos
CREATE DATABASE DBAPI;
USE DBAPI;

-- Creamos la tabla categoria
CREATE TABLE CATEGORIA(
	IdCategoria int primary key identity(1,1),
	Descripcion varchar(50)
);

-- tabla produtos
CREATE TABLE PRODUCTO(
	IdProducto int primary key identity(1,1),
	CodigoBarra varchar(20),
	Descripcion varchar(50),
	Marca varchar(50),
	IdCategoria int,
	Precio decimal(10,2),
	CONSTRAINT FK_IDCATEGORIA FOREIGN KEY (IdCategoria) REFERENCES CATEGORIA(IdCategoria)
);

-- INSERTAMOS
INSERT INTO CATEGORIA(Descripcion) VALUES 
('Tecnologia'),
('ElectroHogar'),
('Accesorios');

INSERT INTO PRODUCTO(CodigoBarra, Descripcion, Marca, IdCategoria, Precio) VALUES
('50910010','Monitor aoc - Curvo Gaming', 'AOC', 1, 1200),
('50910012','Lavadora 21 KG wLA-21', 'WINIA', 2, 1749);

-- VISUALIZAR
SELECT * FROM CATEGORIA;
SELECT * FROM PRODUCTO;

-- CREAMOS PROYECTO ---> ASP.NET Core Web API
 -- nombre: APIPRUEBA
 -- NET 6.0
 -- Desactivamos Configure for HTTPS
 /*
 Instalamos PAQUETES
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.EntityFrameworkCore.Tools
 */
  /* Para ir a la consola Tools -> NuGet Package Manager -> Package manager Console
  - Obtener todas las clases de Entity Framework segun la BD: 
	connectionString = "Data Source=(local);Initial Catalog=DBBASE;user id= USUARIO ; pwd= CLAVE"
	Scaffold-DbContext "Server=DESKTOP-V3E5ICQ\MSSQLSERVERDEV; DataBase=DBAPI;Integrated Security=true" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models
  */

  /* Creamos el controlador (API Controller - Empty)
  */


