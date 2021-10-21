using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
namespace DonacionSangre
{
    public partial class editarHospitales : System.Web.UI.Page
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
                DropDownList1.Items.Add("idHospital");
                DropDownList1.Items.Add("Nombre");
            }
            Button6.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminDashboard.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            GridView2.DataSource = null;
            GridView2.DataBind();
            String query = "select * from Hospital";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand();

            switch (DropDownList1.SelectedIndex)
            {
                case 0:
                    query = query + " where idHospital = ?";
                    comando = new OdbcCommand(query, conexion);
                    try
                    {
                        comando.Parameters.AddWithValue("idHospital", Int32.Parse(TextBox1.Text));
                    }
                    catch
                    {
                        Label3.Text = "Revise los parámetros de búsqueda";
                    }
                    break;
                case 1:
                    query = query + " where nombre like(?)";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("nombre", "%" + TextBox1.Text + "%");
                    break;
            }
            try
            {
                OdbcDataReader lector = comando.ExecuteReader();
                GridView1.DataSource = lector;
                GridView1.AutoGenerateSelectButton = true;
                GridView1.DataBind();
                lector.Close();
                conexion.Close();
                Label3.Text = "Búsqueda por " + DropDownList1.SelectedValue + " :" + TextBox1.Text;
            }
            catch
            {
                Label3.Text = "Ocurrió un error";
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "select * from Hospital where idHospital = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idHospital", Int32.Parse(GridView1.SelectedRow.Cells[1].Text));
            OdbcDataReader lector = comando.ExecuteReader();
            GridView2.DataSource = lector;
            GridView2.DataBind();
            TextBox2.Text = HttpUtility.HtmlDecode(GridView2.Rows[0].Cells[1].Text);
            lector.Close();
            conexion.Close();
            GridView1.DataSource = null;
            GridView1.DataBind();

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            String query = "update Hospital set nombre = ? where idHospital = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            try
            {
                comando.Parameters.AddWithValue("nombre", TextBox2.Text);
                comando.Parameters.AddWithValue("idHospital", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
                comando.ExecuteNonQuery();
                Label5.Text = "Se actualizaron los datos correctamente";
            }
            catch
            {
                Label5.Text = "Ocurrió un error";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Button5.Visible = false;
            String queryDonacion = "select count(Donacion.idPeticion) from Donacion inner join Peticion on Peticion.idPeticion = Donacion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal where Sucursal.idHospital = ?";
            String queryPeticion = "select count(Peticion.idPeticion) from Peticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal where Sucursal.idHospital = ?";
            String querySucursales = "select count(Sucursal.idHospital) from Sucursal where Sucursal.idHospital = ?";

            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(queryDonacion, conexion);
            comando.Parameters.AddWithValue("idHospital", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            OdbcDataReader lector = comando.ExecuteReader();
            lector.Read();
            Label5.Text = "Existen " + lector.GetString(0) + " donaciones" + "<br />";
            lector.Close();

            comando = new OdbcCommand(queryPeticion, conexion);
            comando.Parameters.AddWithValue("idHospital", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            lector = comando.ExecuteReader();
            lector.Read();
            Label5.Text = Label5.Text + "Existen " + lector.GetString(0) + " peticiones" + "<br />";
            lector.Close();

            comando = new OdbcCommand(querySucursales, conexion);
            comando.Parameters.AddWithValue("idHospital", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            lector = comando.ExecuteReader();
            lector.Read();
            Label5.Text = Label5.Text + "Existen " + lector.GetString(0) + " sucursales" + "<br />";
            lector.Close();
            conexion.Close();
            Button6.Visible = true;

        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            String deleteDonacion = "delete from Donacion where idDonacion in( select idDonacion from Donacion inner join Peticion on Peticion.idPeticion = Donacion.idPeticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Hospital on Hospital.idHospital= Sucursal.idHospital where Hospital.idHospital = ?)";
            String deletePeticion = "delete from Peticion where idPeticion in( select idPeticion from Peticion inner join Sucursal on Sucursal.idSucursal = Peticion.idSucursal inner join Hospital on Hospital.idHospital= Sucursal.idHospital where Hospital.idHospital = ?)";
            String deleteSucursal = "delete from Sucursal where idHospital = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(deleteDonacion, conexion);
            try
            {
                comando.Parameters.AddWithValue("idHospital", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
                comando.ExecuteNonQuery();

                comando = new OdbcCommand(deletePeticion, conexion);
                comando.Parameters.AddWithValue("idHospital", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
                comando.ExecuteNonQuery();

                comando = new OdbcCommand(deleteSucursal, conexion);
                comando.Parameters.AddWithValue("idHospital", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
                comando.ExecuteNonQuery();
                Label5.Text = "Se borraron los datos correctamente";
            }
            catch
            {
                Label5.Text = "Ocurrió un error";
            }

        }
    }
}