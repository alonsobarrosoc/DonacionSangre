using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace DonacionSangre
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String query = "select Hospital.nombre as 'Hospital', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', isnull(sum(case when Tipo.idTipo = 1 then Peticion.mililitros end), 0) as 'A+', isnull(sum(case when Tipo.idTipo = 2 then Peticion.mililitros end),0) as 'A-', isnull(sum(case when Tipo.idTipo = 3 then Peticion.mililitros end), 0) as 'B+', isnull(sum(case when Tipo.idTipo = 4 then Peticion.mililitros end), 0) as 'B-', isnull(sum(case when Tipo.idTipo = 5 then Peticion.mililitros end), 0) as 'AB+', isnull(sum(case when Tipo.idTipo = 6 then Peticion.mililitros end), 0) as 'AB-', isnull(sum(case when Tipo.idTipo = 7 then Peticion.mililitros end), 0) as 'O+', isnull(sum(case when Tipo.idTipo = 8 then Peticion.mililitros end), 0) as 'O-', Sucursal.ubicacion as 'Ubicación' from Sucursal inner join Hospital on Hospital.idHospital = Sucursal.idHospital inner join Peticion on Peticion.idSucursal = Sucursal.idSucursal inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado group by Hospital.nombre, Sucursal.nombre, Sucursal.ubicacion, Ciudad.nombre, Estado.nombre";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            OdbcDataReader lector = comando.ExecuteReader();
            GridView1.DataSource = lector;
            GridView1.DataBind();
            lector.Close();
            conexion.Close();
            if(DropDownList1.Items.Count == 0)
            {
                DropDownList1.Items.Add("Nombre del paciente");
                DropDownList1.Items.Add("Ciudad");
                DropDownList1.Items.Add("Estado");
                DropDownList1.Items.Add("Hospital");
                DropDownList1.Items.Add("Sucursal");
                DropDownList1.Items.Add("Tipo de sangre");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label3.Text = "";
            String query = "";
            switch (DropDownList1.SelectedIndex)
            {
                case 0:
                    //Nombre
                    query = "select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - sum(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Donacion on donacion.idPeticion = Peticion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Peticion.nombrePaciente like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion having Peticion.mililitros - sum(Donacion.mililitros) > 0 union select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Peticion.idPeticion not in(select idPeticion from Donacion) and Peticion.nombrePaciente like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion";
                    break;
                case 1:
                    //Ciudad
                    query = "select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - sum(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Donacion on donacion.idPeticion = Peticion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Ciudad.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion having Peticion.mililitros - sum(Donacion.mililitros) > 0 union select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Peticion.idPeticion not in(select idPeticion from Donacion) and Ciudad.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion";
                    break;
                case 2:
                    //Estado
                    query = "select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - sum(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Donacion on donacion.idPeticion = Peticion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Estado.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion having Peticion.mililitros - sum(Donacion.mililitros) > 0 union select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Peticion.idPeticion not in(select idPeticion from Donacion) and Estado.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion";
                    break;
                case 3:
                    //Hospital
                    query = "select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - sum(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Donacion on donacion.idPeticion = Peticion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Hospital.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion having Peticion.mililitros - sum(Donacion.mililitros) > 0 union select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Peticion.idPeticion not in(select idPeticion from Donacion) and Hospital.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion";
                    break;
                case 4:
                    //sucursal
                    query = "select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - sum(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Donacion on donacion.idPeticion = Peticion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Sucursal.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion having Peticion.mililitros - sum(Donacion.mililitros) > 0 union select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Peticion.idPeticion not in(select idPeticion from Donacion) and Sucursal.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion";
                    break;
                case 5:
                    //tipo de sangre
                    query = "select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - sum(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Donacion on donacion.idPeticion = Peticion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Tipo.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion having Peticion.mililitros - sum(Donacion.mililitros) > 0 union select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo', Sucursal.nombre as 'Sucursal', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado', Sucursal.ubicacion as 'Ubicación' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Peticion.idPeticion not in(select idPeticion from Donacion) and Tipo.nombre like(?) group by Peticion.nombrePaciente,Peticion.mililitros,Tipo.nombre,Sucursal.nombre,Ciudad.nombre,Estado.nombre,Sucursal.ubicacion";
                    break;
            }
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("v1", "%" + TextBox1.Text + "%");
            comando.Parameters.AddWithValue("v2", "%" + TextBox1.Text + "%");
            try
            {
                OdbcDataReader lector = comando.ExecuteReader();
                GridView2.DataSource = lector;
                GridView2.DataBind();
                lector.Close();
                conexion.Close();
                Label3.Text = "Mostrando resultados de " + DropDownList1.SelectedValue + " :" +TextBox1.Text;
            }
            catch
            {
                Label3.Text = "Ocurrió un error";
            }
        }
    }
}