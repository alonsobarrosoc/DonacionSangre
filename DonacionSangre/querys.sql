--Query buscar por categoria idPeticion
select Peticion.idPeticion as 'idPeticion',
	Peticion.fechaPublicacion as 'Fecha',
	Peticion.nombrePaciente as 'Nombre del paciente',
	Peticion.mililitros as 'Necesita',
	Tipo.nombre as 'Tipo' from Peticion
	inner join Tipo on Tipo.idTipo = Peticion.idTipo
	where Peticion.idSucursal = 1 and Tipo.nombre like('%a%')
	--group by Peticion.idPeticion, Peticion.fechaPublicacion, Peticion.nombrePaciente, Peticion.mililitros, Tipo.nombre



select * from Tipo

select Peticion.idPeticion as 'idPeticion',
	Peticion.fechaPublicacion as 'Fecha',
	Peticion.nombrePaciente as 'Nombre del paciente',
	Peticion.mililitros as 'Mililitros',
	Tipo.nombre as 'Tipo' from Peticion
	inner join Tipo on Tipo.idTipo = Peticion.idTipo 
	where Peticion.idSucursal = 1 and Tipo.nombre like(?)
	order by fecha desc
	