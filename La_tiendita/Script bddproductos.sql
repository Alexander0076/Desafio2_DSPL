/*Creacion de base de datos */
CREATE DATABASE ProductosDB
go

/*IMPORTANTE: indicar que base de datos se esta utilizando */
use ProductosDB
Go

/*Creacion de tabla Ocupacion */
CREATE TABLE Categoria(
Id_categoria int identity(1,1) PRIMARY KEY,
categoria varchar(50)
)
GO

/*Creacion de tabla Persona */
CREATE TABLE Producto(
Id_producto int identity(1,1) PRIMARY KEY,
produtco varchar(100),
descripcion_producto text,
fecha_caducidad date,
existencia_de_producto int,
precio_compra decimal,
precio_venta decimal,
Id_categoria INT FOREIGN KEY REFERENCES Categoria(Id_categoria)/*Creacion de llave foranea entre ambas tablas */
)
GO

/*Insertar data en tabla Ocupacion */
Insert into Categoria values( 'DIANA')
Insert into Categoria values( 'Bocadeli')

/*Insertar data en tabla Persona */
Insert into Producto values( 'Churritos','botana','2022-03-08',22,30.00,10,1)



Select* from Categoria


Select* from Producto  inner join Categoria  on Producto.Id_categoria = Categoria.Id_categoria 
 /*Consulta JOIN para obtener la data compartida entre ambas tablas*/
  delete from Producto where Id_producto = 2;
  delete from Categoria where Id_categoria  = 2;