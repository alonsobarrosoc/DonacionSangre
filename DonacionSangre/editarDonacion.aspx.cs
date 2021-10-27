using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace DonacionSangre
{
    public partial class editarDonacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["nombreSucursal"] == null)
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            if(DropDownList1.Items.Count == 0)
            {
                DropDownList1.Items.Add("idDonacion");
                DropDownList1.Items.Add("Nombre Donante");
                DropDownList1.Items.Add("Tipo de sangre");
                DropDownList1.Items.Add("idPeticion");
            }
            if(DropDownList2.Items.Count == 0)
            {
                String queryTipo = "select * from Tipo";
                String queryPeticion = "select idPeticion from Peticion";
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(queryTipo, conexion);
                OdbcDataReader lector = comando.ExecuteReader();
                DropDownList2.DataSource = lector;
                DropDownList2.DataTextField = "nombre";
                DropDownList2.DataValueField = "idTipo";
                DropDownList2.DataBind();
                comando = new OdbcCommand(queryPeticion, conexion);
                lector = comando.ExecuteReader();
                DropDownList3.DataSource = lector;
                DropDownList3.DataTextField = "idPeticion";
                DropDownList3.DataValueField = "idPeticion";
                DropDownList3.DataBind();
                lector.Close();
                conexion.Close();
            }
            Label2.Text = "Busque una donación";
            Button4.Visible = false;
            Button5.Visible = false;


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("inicio.aspx");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Button4.Visible = false;
            Button5.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            TextBox2.Text = "";
            TextBox3.Text = "";

            Label2.Text = "Mostrando resultados de " + DropDownList1.SelectedValue + ": " + TextBox1.Text;
            OdbcConnection conexion = new ConexionBD().con;
            String query = "select Donacion.idDonacion, Donacion.nombreDonante as 'Nombre donante', Donacion.mililitros as 'Donó', Donacion.fechaDonacion as 'Fecha de donación', Donacion.idPeticion as 'idPeticion', Tipo.nombre as 'Tipo' from Donacion inner join Tipo on Tipo.idTipo = Donacion.idTipo inner join peticion on Peticion.idPeticion = Donacion.idPeticion where Peticion.idSucursal = ? and ";
            OdbcCommand comando = new OdbcCommand();
            switch (DropDownList1.SelectedIndex)
            {
                case 0:
                    query = query + "Donacion.idDonacion = ? order by Donacion.fechaDonacion desc";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                    try
                    {
                        comando.Parameters.AddWithValue("param", Int32.Parse(TextBox1.Text));
                    }
                    catch (Exception)
                    {
                        Label2.Text = "Revise sus parámetros de búsqueda";
                    }
                    break;
                case 1:
                    query = query + "Donacion.nombreDonante like(?) order by Donacion.fechaDonacion desc";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                    comando.Parameters.AddWithValue("param", "%"+TextBox1.Text+ "%");
                    break;
                case 2:
                    query = query + "Tipo.nombre like(?) order by Donacion.fechaDonacion desc";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                    comando.Parameters.AddWithValue("param", "%" + TextBox1.Text + "%");
                    break;
                case 3:
                    query = query + "Donacion.idPeticion = ? order by Donacion.fechaDonacion desc";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                    try
                    {
                        comando.Parameters.AddWithValue("param", Int32.Parse(TextBox1.Text));
                    }
                    catch (Exception)
                    {
                        Label2.Text = "Revise sus parámetros de búsqueda";
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
                Label2.Text = "Búsqueda de " + DropDownList1.SelectedValue + " :" + TextBox1.Text;

            }
            catch
            {
                Label2.Text = "Ocurrió un error";
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "select Donacion.idDonacion, Donacion.nombreDonante as 'Nombre donante', Donacion.mililitros as 'Donó', Donacion.fechaDonacion as 'Fecha de donación', Donacion.idPeticion as 'idPeticion', Tipo.nombre as 'Tipo' from Donacion inner join Tipo on Tipo.idTipo = Donacion.idTipo inner join peticion on Peticion.idPeticion = Donacion.idPeticion where Peticion.idSucursal = ? and Donacion.idDonacion = ? order by Donacion.fechaDonacion desc";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idsucursal", Session["idSucursal"]);
            Label2.Text = GridView1.SelectedRow.Cells[0].Text;
            comando.Parameters.AddWithValue("idDonacion", Int32.Parse(GridView1.SelectedRow.Cells[1].Text));
            OdbcDataReader lector = comando.ExecuteReader();
            GridView2.DataSource = lector;
            GridView2.DataBind();
            TextBox2.Text = HttpUtility.HtmlDecode(GridView2.Rows[0].Cells[1].Text);
            TextBox3.Text = GridView2.Rows[0].Cells[2].Text;
            DropDownList3.SelectedValue = GridView2.Rows[0].Cells[4].Text;
            String tipo = GridView2.Rows[0].Cells[5].Text;
            query = "select * from Tipo where nombre = ?";
            comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("nombre", tipo);
            lector = comando.ExecuteReader();
            lector.Read();
            DropDownList2.SelectedValue = lector.GetString(0);
            lector.Close();
            conexion.Close();
            GridView1.DataSource = null;
            GridView1.DataBind();
            Button4.Visible = true;
            Button5.Visible = true;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            String query = "update Donacion set nombreDonante = ?, mililitros = ?, idTipo = ?, idPeticion = ? where idDonacion = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            try
            {
                comando.Parameters.AddWithValue("nombreDonante", TextBox2.Text);
                comando.Parameters.AddWithValue("mililitros", TextBox3.Text);
                comando.Parameters.AddWithValue("idTipo", Int32.Parse(DropDownList2.SelectedValue));
                comando.Parameters.AddWithValue("idPeticion", Int32.Parse(DropDownList3.SelectedValue));
                comando.Parameters.AddWithValue("idDonacion", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
                comando.ExecuteNonQuery();
                Label7.Text = "Los datos se actualizaron correctamente";
                conexion.Close();
            }
            catch (Exception ex)
            {
                Label7.Text = "Ocurrió un error " + ex.ToString();

            }



        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            String query = "delete from Donacion where idDonacion = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idDonacion", Int32.Parse(GridView1.Rows[0].Cells[0].Text));
            comando.ExecuteNonQuery();
            conexion.Close();
            Label7.Text = "Los datos se actualizaron correctamente";
        }
    }
}