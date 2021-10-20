--Query buscar por categoria idPeticion
select Peticion.idPeticion as 'idPeticion',
	Peticion.fechaPublicacion as 'Fecha',
	Peticion.nombrePaciente as 'Nombre del paciente',
	Peticion.mililitros as 'Necesita',
	Tipo.nombre as 'Tipo' from Peticion
	inner join Tipo on Tipo.idTipo = Peticion.idTipo
	where Peticion.idSucursal = 1 and Tipo.nombre like('%a%')
	--group by Peticion.idPeticion, Peticion.fechaPublicacion, Peticion.nombrePaciente, Peticion.mililitros, Tipo.nombre



select * from Peticion

select Peticion.idPeticion as 'idPeticion',
	Peticion.fechaPublicacion as 'Fecha',
	Peticion.nombrePaciente as 'Nombre del paciente',
	Peticion.mililitros as 'Mililitros',
	Tipo.nombre as 'Tipo' from Peticion
	inner join Tipo on Tipo.idTipo = Peticion.idTipo 
	where Peticion.idSucursal = 1 and Tipo.nombre like(?)
	order by fecha desc
	
select * from Donacion

select Donacion.idDonacion,
	Donacion.nombreDonante as 'Nombre donante',
	Donacion.mililitros as 'Donó',
	Donacion.fechaDonacion as 'Fecha de donación',
	Donacion.idPeticion as 'idPeticion', 
	Tipo.nombre as 'Tipo'
	from Donacion inner join Tipo on Tipo.idTipo = Donacion.idTipo
	inner join peticion on Peticion.idPeticion = Donacion.idPeticion
	where Peticion.idSucursal = 1 and Donacion.idDonacion = 1
	order by Donacion.fechaDonacion desc


select * from Admin where correo = 'admin@admin.com' and contrasena = 'admin'

select * from Sucursal

select count(idPeticion) from Donacion where idPeticion = 1


select * from Peticion
select * from Sucursal
select * from Hospital where nombre ='Ángeles'

select Sucursal.idSucursal as 'idSucursal',
	Sucursal.correo as 'Correo',
	Sucursal.nombre as 'Nombre de Sucursal',
	Hospital.nombre as 'Nombre del Hospital'
	from Sucursal inner join Hospital on Hospital.idHospital = Sucursal.idHospital

select Hospital.idHospital, Hospital.nombre from Sucursal inner join Hospital on Hospital.idHospital = Sucursal.idHospital where idSucursal =1
	
	
	select ubicacion, contrasena from Sucursal where idSucursal = 1

select count(idSucursal) from Peticion where idSucursal = 1

select * from Sucursal

select * from Peticion where mililitros > 0
intersect
select * from Peticion

select Peticion.nombrePaciente as 'Nombre del paciente',
	Peticion.fechaPublicacion as 'Fecha de publicación',
	Peticion.mililitros as 'Necesita',
	Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo
	where Peticion.fechaPublicacion between '2000-01-01' and '2021-10-19'

select * from Peticion
intersect
select mililitros from Peticion where mililitros between 0 and 500


select Peticion.idPeticion as 'idPeticion', Peticion.nombrePaciente as 'Nombre del paciente' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where idSucursal = 1

select * from Donacion

select Donacion.idDonacion as 'idDonacion',
	Donacion.nombreDonante as 'Nombre del donante',
	Donacion.mililitros as 'Dono',
	Donacion.fechaDonacion as 'Fecha',
	Peticion.nombrePaciente as 'Paciente al que le dono',
	Tipo.nombre as 'Tipo de sangre'
	from Donacion
	inner join Peticion on Peticion.idPeticion = Donacion.idDonacion
	inner join Tipo on Tipo.idTipo = donacion.idTipo
	where Peticion.idSucursal = 1

select * from tIPO

select Hospital.nombre as 'Hospital', 
	Sucursal.nombre as 'Sucursal',
	Ciudad.nombre as 'Ciudad',
	Estado.nombre as 'Estado',
	Sucursal.ubicacion, 
	isnull(sum(case when Tipo.idTipo = 1 then Peticion.mililitros end), 0) as 'A+',
	isnull(sum(case when Tipo.idTipo = 2 then Peticion.mililitros end),0) as 'A-',
	isnull(sum(case when Tipo.idTipo = 3 then Peticion.mililitros end), 0) as 'B+',
	isnull(sum(case when Tipo.idTipo = 4 then Peticion.mililitros end), 0) as 'B-',
	isnull(sum(case when Tipo.idTipo = 5 then Peticion.mililitros end), 0) as 'AB+',
	isnull(sum(case when Tipo.idTipo = 6 then Peticion.mililitros end), 0) as 'AB-',
	isnull(sum(case when Tipo.idTipo = 7 then Peticion.mililitros end), 0) as 'O+',
	isnull(sum(case when Tipo.idTipo = 8 then Peticion.mililitros end), 0) as 'O-'
	from Sucursal
	inner join Hospital on Hospital.idHospital = Sucursal.idHospital
	inner join Peticion on Peticion.idSucursal = Sucursal.idSucursal
	inner join Tipo on Tipo.idTipo = Peticion.idTipo
	inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad
	inner join Estado on Estado.idEstado = Ciudad.idEstado
	group by Hospital.nombre, Sucursal.nombre, Sucursal.ubicacion, Ciudad.nombre, Estado.nombre

select * from Peticion

select Peticion.nombrePaciente as 'Nombre del paciente',
	Peticion.mililitros - sum(Donacion.mililitros) as 'Necesita',
	Tipo.nombre as 'Tipo',
	Sucursal.nombre as 'Sucursal',
	Ciudad.nombre as 'Ciudad',
	Estado.nombre as 'Estado',
	Sucursal.ubicacion as 'Ubicación'
	from Peticion
	inner join Tipo on Tipo.idTipo = Peticion.idTipo
	inner join Donacion on donacion.idPeticion = Peticion.idPeticion
	inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal
	inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad
	inner join Estado on Estado.idEstado = Ciudad.idEstado
	group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion
	having Peticion.mililitros - sum(Donacion.mililitros) > 0
union
select Peticion.nombrePaciente as 'Nombre del paciente',
	Peticion.mililitros as 'Necesita',
	Tipo.nombre as 'Tipo',
	Sucursal.nombre as 'Sucursal',
	Ciudad.nombre as 'Ciudad',
	Estado.nombre as 'Estado',
	Sucursal.ubicacion as 'Ubicación'
	from Peticion
	inner join Tipo on Tipo.idTipo = Peticion.idTipo
	inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal
	inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad
	inner join Estado on Estado.idEstado = Ciudad.idEstado
	where Peticion.idPeticion not in(select idPeticion from Donacion)
	group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion

