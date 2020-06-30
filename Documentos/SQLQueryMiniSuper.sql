--Creamos la base de datos
CREATE DATABASE MINISUPER

--Para trabajar con la BD
USE MINISUPER

--Creamos las tablas de la BD
--Primero se crean las tablas que no tienen llave forenea--

CREATE TABLE CLIENTES
(
	idTelefono VARCHAR(10) PRIMARY KEY,
	contrasena VARCHAR(30) NOT NULL,
	nombre VARCHAR(30) NOT NULL,
	apellido VARCHAR(30) NOT NULL,
	dirColonia VARCHAR(30) NOT NULL,
	dirCalle VARCHAR(30) NOT NULL,
	dirNumero VARCHAR(10) NOT NULL,	
	estado VARCHAR(10) NOT NULL,
	
	CONSTRAINT CK_EstadoC CHECK(estado IN ('Activo','Baja'))
)

CREATE TABLE PROVEEDORES
(
	idTelefono VARCHAR(10) PRIMARY KEY,
	nombre VARCHAR(30) NOT NULL,
	dirColonia VARCHAR(30) NOT NULL,
	dirCalle VARCHAR(30) NOT NULL,
	dirNumero VARCHAR(10) NOT NULL,	
	estado VARCHAR(10) NOT NULL,
	
	CONSTRAINT CK_EstadoP CHECK(estado IN ('Activo','Baja'))
)
ALTER TABLE VENTAS ALTER COLUMN total DECIMAL(10,2) NOT NULL
CREATE TABLE VENTAS
(
	folio INT IDENTITY(1,1) PRIMARY KEY,
	idCliente VARCHAR(10),
	fecha DATETIME NOT NULL,
	total FLOAT NOT NULL,
	
	CONSTRAINT FK_Cliente FOREIGN KEY (idCliente) REFERENCES CLIENTES(idTelefono),
)

CREATE TABLE CATEGORIAS
(
	idCategoria INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR(30) NOT NULL,
	
	UNIQUE (nombre)
)
ALTER TABLE PRODUCTOS ALTER COLUMN precioP DECIMAL(10,2) NOT NULL
CREATE TABLE PRODUCTOS
(
	idProducto VARCHAR(30) PRIMARY KEY,
	nombre VARCHAR(30) NOT NULL,
	categoria INT NOT NULL,
	precioC DECIMAL(10,2) NOT NULL,
	precioP DECIMAL(10,2) NOT NULL,
	existencia INT NOT NULL,
	idProveedor VARCHAR(10) NOT NULL,
	estado VARCHAR(10) NOT NULL,
	
	CONSTRAINT FK_Proveedor FOREIGN KEY (idProveedor) REFERENCES PROVEEDORES(idTelefono),
	CONSTRAINT FK_Categoria FOREIGN KEY (categoria) REFERENCES CATEGORIAS(idCategoria),
	CONSTRAINT CK_EstadoE CHECK(estado IN ('Activo','Baja'))
)
ALTER TABLE PEDIDOSXCLIENTE ALTER COLUMN total DECIMAL(10,2) NOT NULL
CREATE TABLE PEDIDOSXCLIENTE
(
	folio INT IDENTITY(1,1) PRIMARY KEY,
	idCliente VARCHAR(10),
	fecha DATETIME NOT NULL,
	estado VARCHAR(15) NOT NULL,
	total FLOAT NOT NULL,
	comentario VARCHAR(100),
	
	CONSTRAINT FK_Cliente2 FOREIGN KEY (IdCliente) REFERENCES CLIENTES(idTelefono),
	CONSTRAINT CK_Estado CHECK(estado IN ('En Espera','En Proceso','Realizado','Cancelado'))
)
ALTER TABLE DETALLEVENTA ALTER COLUMN precio DECIMAL(10,2) NOT NULL
ALTER TABLE DETALLEVENTA ALTER COLUMN subtotal DECIMAL(10,2) NOT NULL
CREATE TABLE DETALLEVENTA
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	folio INT NOT NULL,
	idProducto VARCHAR(30) NOT NULL,
	cantidad INT NOT NULL,
	precio FLOAT NOT NULL,
	subtotal FLOAT NOT NULL,
	
	CONSTRAINT FK_Producto FOREIGN KEY (idProducto) REFERENCES PRODUCTOS(idProducto),
	CONSTRAINT FK_Venta FOREIGN KEY (folio) REFERENCES VENTAS(folio),
)
ALTER TABLE DETALLEPXC ALTER COLUMN precio DECIMAL(10,2) NOT NULL
ALTER TABLE DETALLEPXC ALTER COLUMN subtotal DECIMAL(10,2) NOT NULL
CREATE TABLE DETALLEPXC
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	folio INT NOT NULL,
	idProducto VARCHAR(30) NOT NULL,
	cantidad INT NOT NULL,
	precio FLOAT NOT NULL,
	subtotal FLOAT NOT NULL,
	
	CONSTRAINT FK_Producto2 FOREIGN KEY (idProducto) REFERENCES PRODUCTOS(idProducto),
	CONSTRAINT FK_Pedido FOREIGN KEY (folio) REFERENCES PEDIDOSXCLIENTE(folio),
)
GO

DROP TABLE DPANDROID
CREATE TABLE DPANDROID
(
	idCliente VARCHAR(10),
	nomProducto VARCHAR(30) NOT NULL,
	idProducto VARCHAR(30) NOT NULL,
	cantidad INT NOT NULL,
	precio DECIMAL(10,2) NOT NULL,
	subtotal DECIMAL(10,2) NOT NULL,	
)
GO

--Procedimiento que inserta una venta y regresa el folio
CREATE PROCEDURE INSERTAVENTA
@cliente VARCHAR(30)='Mostrador',
@folio INT OUTPUT
AS 
BEGIN
	INSERT INTO VENTAS (idCliente,fecha,total) VALUES(@cliente, GETDATE(),0.00)
	SELECT @folio = (SELECT MAX(folio) FROM VENTAS)
END
GO

--Procedimiento que inserta una venta modificado y regresa el folio ESTE ES EL BUENO
CREATE PROCEDURE sp_INSERTAVENTA
@cliente VARCHAR(30)='Mostrador',
@total FLOAT = 0.00,
@folio INT OUTPUT
AS 
BEGIN
	INSERT INTO VENTAS (idCliente,fecha,total) VALUES(@cliente, GETDATE(),@total)
	SELECT @folio = (SELECT MAX(folio) FROM VENTAS)
END
GO

--Trigger que actualiza la tabla productos en la columna cantidad cuando se inserta en DETALLEVENTA
CREATE TRIGGER ACTUALIZAPRODUCTOS
ON DETALLEVENTA
AFTER INSERT
AS
	UPDATE PRODUCTOS SET existencia = PRODUCTOS.existencia - inserted.cantidad
	FROM PRODUCTOS, inserted
	WHERE PRODUCTOS.idProducto = inserted.idProducto
GO

--Trigger que actualiza la tabla productos en la columna existencia cuando se cancela un PEDIDO
CREATE TRIGGER ACTUALIZAPRODUCTOS2
ON PEDIDOSXCLIENTE
AFTER UPDATE
AS
	IF((SELECT estado FROM inserted) = 'Cancelado')
	BEGIN
		UPDATE PRODUCTOS SET existencia = PRODUCTOS.existencia + DETALLEPXC.cantidad
		FROM PRODUCTOS,DETALLEPXC,inserted
		WHERE inserted.folio = DETALLEPXC.folio AND PRODUCTOS.idProducto = DETALLEPXC.idProducto;
	END
GO

--Procedimiento para inserta pedido y regresa el folio
CREATE PROCEDURE INSERTAPEDIDO
@cliente VARCHAR(30),
@folio INT OUTPUT
AS 
BEGIN
	INSERT INTO PEDIDOSXCLIENTE(idCliente,fecha,estado,total) VALUES(@cliente, GETDATE(),'En Espera',0.00)
	SELECT @folio = (SELECT MAX(folio) FROM PEDIDOSXCLIENTE)
END
GO

--Procedimiento que inserta un Pedido modificado y regresa el folio ESTE ES EL BUENO
CREATE PROCEDURE sp_INSERTAPEDIDO
@cliente VARCHAR(30)='Mostrador',
@total FLOAT = 0.00,
@folio INT OUTPUT
AS 
BEGIN
	INSERT INTO PEDIDOSXCLIENTE(idCliente,fecha,estado,total) VALUES(@cliente, GETDATE(),'En Espera',@total)
	SELECT @folio = (SELECT MAX(folio) FROM PEDIDOSXCLIENTE)
END
GO

--Trigger que actualiza la tabla productos en la columna cantidad cuando se inserta en DETALLEPXC
CREATE TRIGGER ACTUALIZAPRODUCTOS3
ON DETALLEPXC
AFTER INSERT
AS
	UPDATE PRODUCTOS SET existencia = PRODUCTOS.existencia - inserted.cantidad
	FROM PRODUCTOS, inserted
	WHERE PRODUCTOS.idProducto = inserted.idProducto
GO

--Procedimiento para inisiar sesion desde android
CREATE PROCEDURE INICIOSESION
@user VARCHAR(30),
@clave VARCHAR(15),
@msje VARCHAR(100) output
AS BEGIN
IF exists(SELECT * FROM CLIENTES WHERE idTelefono=@user and contrasena=@clave)
	begin	
		set @Msje='Gracias por Iniciar Sesion'
	end
	else
		set @msje='Error, Usuario no existe o datos incorrectos'
end
go

---Pruebas

DECLARE @folio INT
EXEC INSERTAVENTA 'Mostrador', @folio output
SELECT @folio
GO

DECLARE @folio INT
EXEC INSERTAPEDIDO 'Mostrador', @folio output
SELECT @folio
GO

INSERT INTO CLIENTES (idTelefono,contrasena,nombre,apellido,dirColonia,dirCalle,dirNumero,estado) VALUES('Mostrador','null','null','null','null','null','null','Baja')
SELECT * FROM CLIENTES
TRUNCATE TABLE CLIENTES

INSERT INTO PROVEEDORES VALUES('1','Coca cola company','cece','cwecwe','cwecwe')
SELECT * FROM PROVEEDORES
TRUNCATE TABLE PROVEEDORES

INSERT INTO PRODUCTOS (nombre, precio, cantidad, idProveedor) VALUES('coca', 12.00, 10,'1')
INSERT INTO DETALLEVENTA(folio,idProducto,cantidad,precio,subtotal) VALUES(6,'1',5,12.00,60.00)
SELECT * FROM PRODUCTOS

SELECT * FROM VENTAS WHERE fecha > convert(datetime, '2016-01-02 16:00:00', 121) AND fecha < convert(datetime, '2016-01-02 16:59:59', 121)

SELECT CLIENTES.idTelefono, CLIENTES.nombre, CLIENTES.apellido,SUM(VENTAS.total) as suma 
FROM CLIENTES,VENTAS 
WHERE CLIENTES.idTelefono=VENTAS.idCliente 
GROUP BY CLIENTES.idTelefono,CLIENTES.nombre,CLIENTES.apellido 
HAVING COUNT(idTelefono)>0

SELECT CLIENTES.idTelefono, CLIENTES.nombre, CLIENTES.apellido, SUM(VENTAS.total) AS SUMA  
FROM CLIENTES,VENTAS
WHERE CLIENTES.idTelefono=VENTAS.idCliente 
GROUP BY CLIENTES.idTelefono,CLIENTES.nombre, CLIENTES.apellido

SELECT * FROM PRODUCTOS WHERE idProveedor = 1111111111

SELECT p.idProducto, p.nombre, p.categoria, p.precioC, p.precioP, p.existencia, p.idProveedor, SUM(v.cantidad) AS SUMA  
FROM PRODUCTOS AS p, DETALLEVENTA AS v
WHERE p.idProducto=v.idProducto
GROUP BY p.idProducto, p.nombre, p.categoria, p.precioC, p.precioP, p.existencia, p.idProveedor

SELECT * FROM DETALLEVENTA

SELECT * FROM PRODUCTOS WHERE existencia < 5 ORDER BY existencia

SELECT nombre FROM CATEGORIAS ORDER BY nombre

UPDATE PROVEEDORES set estado='Activo'
UPDATE PRODUCTOS set estado='Activo'

SELECT * FROM VENTAS
SELECT * FROM CLIENTES
SELECT * FROM PEDIDOSXCLIENTE
INSERT INTO PEDIDOSXCLIENTE(idCliente,fecha,estado,total) VALUES('4443016998', GETDATE(),'En Espera',0.00)

SELECT * FROM PRODUCTOS as P, CATEGORIAS as C
WHERE P.categoria=C.idCategoria AND P.estado = 'Activo' AND C.nombre='Bebidas' ORDER BY P.nombre
