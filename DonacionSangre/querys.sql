--Query buscar por categoria idPeticion
select Peticion.idPeticion as 'idPeticion',
	Peticion.fechaPublicacion as 'Fecha',
	Peticion.nombrePaciente as 'Nombre del paciente',
	Peticion.mililitros - SUM(Donacion.mililitros) as 'Necesita',
	Tipo.nombre as 'Tipo' from Peticion
	inner join Donacion on Donacion.idPeticion = Peticion.idPeticion
	inner join Tipo on Tipo.idTipo = Peticion.idTipo
	where Peticion.idSucursal = 1 and Tipo.nombre like('%a%')
	group by Peticion.idPeticion, Peticion.fechaPublicacion, Peticion.nombrePaciente, Peticion.mililitros, Tipo.nombre
	having Peticion.mililitros - SUM(Donacion.mililitros) > 0
union
select Peticion.idPeticion as 'idPeticion',
	Peticion.fechaPublicacion as 'Fecha',
	Peticion.nombrePaciente as 'Nombre del paciente',
	Peticion.mililitros as 'Necesita',
	Tipo.nombre as 'Tipo' from Peticion
	inner join Tipo on Tipo.idTipo = Peticion.idTipo
	where Peticion.idSucursal = 1 and Tipo.nombre like('%a%') and idPeticion not in(
		select idPeticion from Donacion)
order by fecha desc

select * from Donacion