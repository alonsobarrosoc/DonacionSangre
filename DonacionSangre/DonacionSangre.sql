use DonacionSangre

--Estado(idEstado, nombre)
create table Estado(
	idEstado int primary key,
	nombre varchar(50))
	
--Ciudad(idCiudad, nombre, idEstado(FK))
create table Ciudad(
	idCiudad int primary key,
	nombre varchar(50),
	idEstado int references Estado)

--Hospital(idHospital, nombre)
create table Hospital(
	idHospital int primary key,
	nombre varchar(40))

--Sucursal(idSucursal, correo, ubicacion, contrasena, nombre, idCiudad(FK), idHospital(FK))
create table Sucursal(
	idSucursal int primary key,
	correo varchar(40),
	ubicacion varchar(120),
	contrasena varchar(40),
	nombre varchar(40),
	idCiudad int references Ciudad,
	idHospital int references Hospital)

--Tipo(idTipo, nombre)
create table Tipo(
	idTipo int primary key,
	nombre varchar(40))
--Peticion(idPeticion,fechaPublicacion, nombrePaciente, mililitros ,idTipo(FK), idHospital(FK))
create table Peticion(
	idPeticion int primary key,
	fechaPublicacion date,
	nombrePaciente varchar(120),
	mililitros int,
	idTipo int references Tipo,
	idSucursal int references Sucursal)
--Donacion(idDonacion, nombreDonante, mililitros, fechaDonacion, idPeticion(FK))
create table Donacion(
	idDonacion int primary key,
	nombreDonante varchar(140),
	mililitros int,
	fechaDonacion date,
	idPeticion int references Peticion,
	idTipo int references Tipo)

create table Admin(
	idAdmin int primary key,
	correo varchar(40),
	contrasena varchar(40))


insert into Admin values(1, 'admin@admin.com', 'admin')






insert into Estado values(1, 'Aguascalientes')
insert into Estado values(2, 'Baja California')
insert into Estado values(3, 'Baja California Sur')
insert into Estado values(4, 'Campeche')
insert into Estado values(5, 'Coahuila de Zaragoza')
insert into Estado values(6, 'Colima')
insert into Estado values(7, 'Chiapas')
insert into Estado values(8, 'Chihuahua')
insert into Estado values(9, 'Distrito Federal')
insert into Estado values(10, 'Durango')
insert into Estado values(11, 'Guanajuato')
insert into Estado values(12, 'Guerrero')
insert into Estado values(13, 'Hidalgo')
insert into Estado values(14, 'Jalisco')
insert into Estado values(15, 'México')
insert into Estado values(16, 'Michoacán de Ocampo')
insert into Estado values(17, 'Morelos')
insert into Estado values(18, 'Nayarit')
insert into Estado values(19, 'Nuevo León')
insert into Estado values(20, 'Oaxaca de Juárez')
insert into Estado values(21, 'Puebla')
insert into Estado values(22, 'Querétaro')
insert into Estado values(23, 'Quintana Roo')
insert into Estado values(24, 'San Luis Potosí')
insert into Estado values(25, 'Sinaloa')
insert into Estado values(26, 'Sonora')
insert into Estado values(27, 'Tabasco')
insert into Estado values(28, 'Tamaulipas')
insert into Estado values(29, 'Tlaxcala')
insert into Estado values(30, 'Veracruz de Ignacio de la Llave')
insert into Estado values(31, 'Yucatán')
insert into Estado values(32, 'Zacatecas')

insert into Ciudad values(1, 'Ciudad de México', 1)
insert into Ciudad values(2, 'Querétaro', 2)
insert into Ciudad values(3, 'Guadalajara', 3)




insert into Hospital values(1, 'Ángeles')
insert into Hospital values(2, 'Tec 100')
insert into Hospital values(3, 'San José')
insert into Hospital values(4, 'Star Médica')
insert into Hospital values(5, 'ABC')

insert into Sucursal values(1, 'cdmx@angeles.com', 'Agrarismo 208, Escandón II Secc, Miguel Hidalgo, 11800 Ciudad de México, CDMX', 'angelescdmx','Agrarismo',1,1)

insert into Tipo values(1,'A+')
insert into Tipo values(2,'A-')
insert into Tipo values(3,'B+')
insert into Tipo values(4,'B-')
insert into Tipo values(5,'AB+')
insert into Tipo values(6,'AB-')
insert into Tipo values(7,'O+')
insert into Tipo values(8,'O-')



insert into Peticion values(1,CURRENT_TIMESTAMP, 'Alonso Barroso Corral', 892, 1, 1)
insert into Donacion values(1, 'Tere Corral Valdez', 128, CURRENT_TIMESTAMP, 1,1)

insert into Peticion values(2, CURRENT_TIMESTAMP, 'Sergio López Salas', 123, 3, 1)



select correo, ubicacion, nombre, contrasena from Sucursal where idSucursal = 1 and contrasena = 'angelescdmx'
