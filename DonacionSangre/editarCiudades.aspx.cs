using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace DonacionSangre
{
    public partial class editarCiudades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["admin"] == null)
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            if(DropDownList1.Items.Count == 0)
            {
                DropDownList1.Items.Add("idCiudad");
                DropDownList1.Items.Add("Ciudad");
                DropDownList1.Items.Add("Estado");
            }
            if(DropDownList2.Items.Count == 0)
            {
                String query = "select * from Estado";
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query, conexion);
                OdbcDataReader lector = comando.ExecuteReader();
                DropDownList2.DataSource = lector;
                DropDownList2.DataTextField = "nombre";
                DropDownList2.DataValueField = "idEstado";
                DropDownList2.DataBind();
                lector.Close();
                conexion.Close();

            }
            Button6.Visible = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
            String query = "";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand();
            switch (DropDownList1.SelectedIndex)
            {
                case 0:
                    query = "select Ciudad.idCiudad as 'idCiudad', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado'  from Ciudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Ciudad.idCiudad = ?";
                    comando = new OdbcCommand(query, conexion);
                    try
                    {
                        comando.Parameters.AddWithValue("idCiudad", Int32.Parse(TextBox1.Text));
                    }
                    catch
                    {
                        Label3.Text = "Revise los parámetros de búsqueda";
                    }
                    break;
                case 1:
                    query = "select Ciudad.idCiudad as 'idCiudad', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado'  from Ciudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Ciudad.nombre like(?)";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("Ciudad.nombre", "%" + TextBox1.Text + "%");
                    break;
                case 2:
                    query = "select Ciudad.idCiudad as 'idCiudad', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado'  from Ciudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Estado.nombre like(?)";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("Estado.nombre", "%" + TextBox1.Text + "%");
                    break;
            }
            try
            {
                OdbcDataReader lector = comando.ExecuteReader();
                GridView1.DataSource = lector;
                GridView1.AutoGenerateSelectButton = true;
                GridView1.DataBind();
                Label3.Text = "Mostrando resultados de " + DropDownList1.SelectedValue + ": " + TextBox1.Text;
                lector.Close();
                conexion.Close();
            }
            catch
            {
                Label3.Text = "Ocurrió un error";
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("inicio.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminDashboard.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "select Ciudad.idCiudad as 'idCiudad', Ciudad.nombre as 'Ciudad', Estado.nombre as 'Estado'  from Ciudad inner join Estado on Estado.idEstado = Ciudad.idEstado where Ciudad.idCiudad = ?";
            String queryEstado = "select idEstado from Ciudad where idCiudad = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idCiudad", Int32.Parse(GridView1.SelectedRow.Cells[1].Text));
            OdbcDataReader lector = comando.ExecuteReader();


            GridView2.DataSource = lector;
            GridView2.DataBind();

            TextBox2.Text = GridView2.Rows[0].Cells[1].Text;
            comando = new OdbcCommand(queryEstado, conexion);
            comando.Parameters.AddWithValue("idCiudad", Int32.Parse(GridView1.SelectedRow.Cells[1].Text));
            lector = comando.ExecuteReader();
            lector.Read();
            DropDownList2.SelectedValue = lector.GetString(0);

            GridView1.DataSource = null;
            GridView1.DataBind();
            lector.Close();
            conexion.Close();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            String queryDonaciones = "select count(Donacion.idPeticion) from Donacion inner join Peticion on Peticion.idPeticion = Donacion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal where Sucursal.idCiudad = ?";
            String queryPeticiones = "select count(Peticion.idPeticion) from Peticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal where Sucursal.idCiudad = ?";
            String querySucursales = "select count(Sucursal.idCiudad) from Sucursal where Sucursal.idCiudad = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(queryDonaciones, conexion);
            comando.Parameters.AddWithValue("idCiudad", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            OdbcDataReader lector = comando.ExecuteReader();
            lector.Read();
            Label6.Text = "Existen " + lector.GetString(0) + " donaciones" + "<br />";
            lector.Close();
            comando = new OdbcCommand(queryPeticiones, conexion);
            comando.Parameters.AddWithValue("idCiudad", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            lector = comando.ExecuteReader();
            lector.Read();
            Label6.Text = Label6.Text + "Existen " + lector.GetString(0) + " peticiones" + "<br />";
            lector.Close();

            comando = new OdbcCommand(querySucursales, conexion);
            comando.Parameters.AddWithValue("idCiudad", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            lector = comando.ExecuteReader();
            lector.Read();
            Label6.Text = Label6.Text + "Existen " + lector.GetString(0) + " sucursales, seguro quiere borrar estos datos?" + "<br />";
            lector.Close();
            conexion.Close();
            Button5.Visible = false;
            Button6.Visible = true;
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            String deleteDonaciones = "delete from Donacion inner join Peticion on Peticion.idPeticion = Donacion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal where Sucursal.idCiudad = ?";
            String deletePeticiones = "delete from Peticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal where Sucursal.idCiudad = ?";
            String deleteSucursales = "select count(Sucursal.idCiudad) from Sucursal where Sucursal.idCiudad = ?";

            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(deleteDonaciones, conexion);
            comando.Parameters.AddWithValue("idCiudad", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            comando.ExecuteNonQuery();

            comando = new OdbcCommand(deletePeticiones, conexion);
            comando.Parameters.AddWithValue("idCiudad", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            comando.ExecuteNonQuery();

            comando = new OdbcCommand(deleteSucursales, conexion);
            comando.Parameters.AddWithValue("idCiudad", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            comando.ExecuteNonQuery();
            Label6.Text = "Se borró correctamente";
            conexion.Close();

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            String query = "update Ciudad set nombre = ?, idEstado = ? where idCiudad = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("nombre", TextBox2.Text);
            comando.Parameters.AddWithValue("idEstado", DropDownList2.SelectedValue);
            comando.Parameters.AddWithValue("idCiudad", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            try
            {
                comando.ExecuteNonQuery();
                Label6.Text = "Se actualizaron los valores correctamente";
                conexion.Close();
            }
            catch
            {
                Label6.Text = "Ocurrió un error";
            }
        }
    }
}