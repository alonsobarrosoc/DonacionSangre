using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
namespace DonacionSangre
{
    public partial class registrarDonacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombreSucursal"] == null)
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            if(DropDownList2.Items.Count == 0)
            {
                DropDownList2.Items.Add("idPeticion");
                DropDownList2.Items.Add("Nombre del paciente");
                DropDownList2.Items.Add("Tipo");
            }


            Label6.Text = "Busque y seleccione la petición a la que quiere registrar una donación";
            String queryTipo = "select * from Tipo";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(queryTipo, conexion);
            OdbcDataReader lector = comando.ExecuteReader();

            DropDownList1.DataSource = lector;
            DropDownList1.DataTextField = "nombre";
            DropDownList1.DataValueField = "idTipo";
            DropDownList1.DataBind();

            lector.Close();
            conexion.Close();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("home.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FUNCIONA EL INDICE, QUEREMOS QUE NOS REGRESE EL NUMERO DE PETICION
            String query = "select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - SUM(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo' from Peticion inner join Donacion on Donacion.idPeticion = Peticion.idPeticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Peticion.idPeticion = ? group by Peticion.idPeticion, Peticion.fechaPublicacion, Peticion.nombrePaciente, Peticion.mililitros, Tipo.nombre having Peticion.mililitros - SUM(Donacion.mililitros) > 0 union select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Peticion.idPeticion = ? and idPeticion not in( select idPeticion from Donacion) order by fecha desc";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
            comando.Parameters.AddWithValue("idPeticion", Int32.Parse(GridView1.SelectedRow.Cells[1].Text));
            comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
            comando.Parameters.AddWithValue("idPeticion", Int32.Parse(GridView1.SelectedRow.Cells[1].Text));
            OdbcDataReader lector = comando.ExecuteReader();
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = lector;
            GridView2.DataBind();
            Label6.Text = "La petición seleccionada para donar es:";
            lector.Close();
            conexion.Close();

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
            String query = "";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand();
            Label6.Text = "Mostrando resultados de la búsqueda " + DropDownList2.SelectedValue + ": " + TextBox3.Text;
            switch (DropDownList2.SelectedIndex)
            {
                case 0:
                    query = "select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - SUM(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo' from Peticion inner join Donacion on Donacion.idPeticion = Peticion.idPeticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Peticion.idPeticion = ? group by Peticion.idPeticion, Peticion.fechaPublicacion, Peticion.nombrePaciente, Peticion.mililitros, Tipo.nombre having Peticion.mililitros - SUM(Donacion.mililitros) > 0 union select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Peticion.idPeticion = ? and idPeticion not in( select idPeticion from Donacion) order by fecha desc";
                    comando = new OdbcCommand(query, conexion);
                    try
                    {
                        comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                        comando.Parameters.AddWithValue("idPeticion", Int32.Parse(TextBox3.Text));
                        comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                        comando.Parameters.AddWithValue("idPeticion", Int32.Parse(TextBox3.Text));
                    }
                    catch (Exception)
                    {
                        Label6.Text = "Ocuurrió un error, revisa los parámetros de la búsqueda";
                    }
                    break;
                case 1:
                    query = "select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - SUM(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo' from Peticion inner join Donacion on Donacion.idPeticion = Peticion.idPeticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Peticion.nombrePaciente like(?) group by Peticion.idPeticion, Peticion.fechaPublicacion, Peticion.nombrePaciente, Peticion.mililitros, Tipo.nombre having Peticion.mililitros - SUM(Donacion.mililitros) > 0 union select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Peticion.nombrePaciente like(?) and idPeticion not in( select idPeticion from Donacion) order by fecha desc";
                    comando = new OdbcCommand(query, conexion);
                    try
                    {

                        comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                        comando.Parameters.AddWithValue("nombrePaciente", "%" + TextBox3.Text + "%");
                        comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                        comando.Parameters.AddWithValue("nombrePaciente", "%" + TextBox3.Text + "%");
                    }
                    catch (Exception)
                    {
                        Label6.Text = "Ocuurrió un error, revisa los parámetros de la búsqueda";
                    }
                    break;
                case 2:
                    query = "select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros - SUM(Donacion.mililitros) as 'Necesita', Tipo.nombre as 'Tipo' from Peticion inner join Donacion on Donacion.idPeticion = Peticion.idPeticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Tipo.nombre like(?) group by Peticion.idPeticion, Peticion.fechaPublicacion, Peticion.nombrePaciente, Peticion.mililitros, Tipo.nombre having Peticion.mililitros - SUM(Donacion.mililitros) > 0 union select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Tipo.nombre like(?) and idPeticion not in( select idPeticion from Donacion) order by fecha desc";
                    comando = new OdbcCommand(query, conexion);
                    try
                    {
                        comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                        comando.Parameters.AddWithValue("nombrePaciente", "%" + TextBox3.Text + "%");
                        comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                        comando.Parameters.AddWithValue("nombrePaciente", "%" + TextBox3.Text + "%");
                    }
                    catch (Exception)
                    {
                        Label6.Text = "Ocuurrió un error, revisa los parámetros de la búsqueda";
                    }
                    break;
            }

            try
            {
                OdbcDataReader lector = comando.ExecuteReader();
                GridView1.DataSource = lector;
                GridView1.DataBind();
                lector.Close();
                conexion.Close();
            }
            catch (Exception)
            {
                Label6.Text = "Ocuurrió un error, revisa los parámetros de la búsqueda";
            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //insert into Donacion values(1, 'Tere Corral Valdez', 128, CURRENT_TIMESTAMP, 1,1)
            String query = "insert into Donacion values(?, ?, ?, CURRENT_TIMESTAMP, ?,?)";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            Boolean bandera = true;
            Random r = new Random();
            while (bandera)
            {
                try
                {
                    comando.Parameters.AddWithValue("idPeticion", r.Next(1, 999999));
                    comando.Parameters.AddWithValue("nombreDonante", TextBox1.Text);
                    comando.Parameters.AddWithValue("mililitros", TextBox2.Text);
                    comando.Parameters.AddWithValue("idPeticion", GridView1.Rows[0].Cells[0].Text);
                    comando.Parameters.AddWithValue("idTipo", Int32.Parse(DropDownList1.SelectedValue));
                    comando.ExecuteNonQuery();
                    Label7.Text = "Registro exitoso";
                    bandera = false;
                }
                catch (Exception) { }
            }

        }
    }
}