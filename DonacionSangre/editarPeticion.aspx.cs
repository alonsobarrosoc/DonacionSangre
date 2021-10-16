using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Odbc;

namespace DonacionSangre
{
    public partial class editarPeticion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombreSucursal"] == null)
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            if (DropDownList2.Items.Count == 0)
            {
                DropDownList2.Items.Add("idPeticion");
                DropDownList2.Items.Add("Nombre del paciente");
                DropDownList2.Items.Add("Tipo");
            }
            Button6.Visible = false;
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
            Response.Redirect("inicio.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
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
                    query = "select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Mililitros', Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Peticion.idPeticion = ? order by fecha desc";
                    comando = new OdbcCommand(query, conexion);
                    try
                    {
                        comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                        comando.Parameters.AddWithValue("idPeticion", Int32.Parse(TextBox3.Text));
                        
                    }
                    catch (Exception)
                    {
                        Label6.Text = "Ocuurrió un error, revisa los parámetros de la búsqueda";
                    }
                    break;
                case 1:
                    query = "select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Mililitros', Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Peticion.nombrePaciente like(?) order by fecha desc";
                    comando = new OdbcCommand(query, conexion);
                    try
                    {

                        comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                        comando.Parameters.AddWithValue("nombrePaciente", "%" + TextBox3.Text + "%");
                        
                    }
                    catch (Exception)
                    {
                        Label6.Text = "Ocuurrió un error, revisa los parámetros de la búsqueda";
                    }
                    break;
                case 2:
                    query = " select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Mililitros', Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = 1 and Tipo.nombre like(?) order by fecha desc";
                    comando = new OdbcCommand(query, conexion);
                    try
                    {
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "select Peticion.idPeticion as 'idPeticion', Peticion.fechaPublicacion as 'Fecha', Peticion.nombrePaciente as 'Nombre del paciente', Peticion.mililitros as 'Mililitros', Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where Peticion.idSucursal = ? and Peticion.idPeticion = ? order by fecha desc";
            String queryTipo = "select * from Tipo where nombre=?";
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
            lector.Read();
            TextBox4.Text = GridView2.Rows[0].Cells[2].Text;
            TextBox5.Text = GridView2.Rows[0].Cells[3].Text;
            comando = new OdbcCommand(queryTipo, conexion);
            comando.Parameters.AddWithValue("nombre", GridView2.Rows[0].Cells[4].Text);
            lector = comando.ExecuteReader();
            lector.Read();
            DropDownList1.SelectedValue = lector.GetString(0);
            lector.Close();
            conexion.Close();

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            String actualizar = "update Peticion set nombrePaciente = ?, mililitros = ?, idTipo = ? where idPeticion = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(actualizar, conexion);
            try
            {
                comando.Parameters.AddWithValue("nombrePaciente", TextBox4.Text);
                comando.Parameters.AddWithValue("mililitros", TextBox5.Text);
                comando.Parameters.AddWithValue("idTipo", Int32.Parse(DropDownList1.SelectedValue));
                comando.Parameters.AddWithValue("idPetcion", Int32.Parse(GridView2.Rows[0].Cells[0].Text));

                comando.ExecuteNonQuery();
                Label9.Text = "Se actualizaron los valores de la petción correctamente";
                TextBox4.Text = "";
                TextBox5.Text = "";
                conexion.Close();

            } catch(Exception ex)
            {
                Label9.Text = "Ocurrió un probvlema, por favor vuelva a intentar";
            }

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Button5.Visible = false;
            String queryChecarDonaciones = "select count(idPeticion) from Donacion where idPeticion = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(queryChecarDonaciones, conexion);
            OdbcDataReader lector = comando.ExecuteReader();
            lector.Read();
            Label9.Text = "Exitesten " + lector.GetString(0) + " donaciones para esta petición, ¿está seguro de querer borrar esta petición?";
            Button6.Visible = true;
            lector.Close();
            conexion.Close();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            String borrarDoinaciones = "delete from Donaciones where idPeticion = ?";
            String borrarPeticion = "delete from Peticion where idPeticion = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(borrarDoinaciones, conexion);
            try
            {
                comando.Parameters.AddWithValue("idPeticion", GridView2.Rows[0].Cells[0].Text);
                comando.ExecuteNonQuery();
                comando = new OdbcCommand(borrarPeticion, conexion);
                comando.Parameters.AddWithValue("idPeticion", GridView2.Rows[0].Cells[0].Text);
                comando.ExecuteNonQuery();
                Label9.Text = "Se actualizaron los valores de la petción correctamente";
                Button5.Visible = true;
                Button6.Visible = false;
                TextBox4.Text = "";
                TextBox5.Text = "";
                conexion.Close();

            }
            catch (Exception ex)
            {
                Label9.Text = "Ocurrió un probvlema, por favor vuelva a intentar " + ex.ToString();
            }
        }
    }
}