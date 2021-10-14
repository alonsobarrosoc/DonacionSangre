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
	idHospital int references Hospital)
--Donacion(idDonacion, nombreDonante, mililitros, fechaDonacion, idPeticion(FK))
create table Donacion(
	idDonacion int primary key,
	nombreDonante varchar(140),
	mililitros int,
	fechaDonacion date,
	idPeticion int references Peticion)



insert into Estado values(1,'México')
insert into Estado values(2, 'Querétaro')
insert into Estado values(3, 'Jalisco')

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
insert into Donacion values(1, 'Tere Corral Valdez', 128, CURRENT_TIMESTAMP, 1)

insert into Peticion values(2, CURRENT_TIMESTAMP, 'Sergio López Salas', 123, 3, 1)



select correo, contrasena from Sucursal



