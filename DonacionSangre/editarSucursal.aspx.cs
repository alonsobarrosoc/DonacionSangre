using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace DonacionSangre
{
    public partial class editarSucursal : System.Web.UI.Page
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
                DropDownList1.Items.Add("Correo");
                DropDownList1.Items.Add("Hospital");
                DropDownList1.Items.Add("Nombre de sucursal");
                DropDownList1.Items.Add("idSucursal");
                DropDownList1.Items.Add("Ciudad");

            }

            String queryHospitales = "select * from Hospital";
            String queryCiudad = "select * from Ciudad";
            String queryEstado = "select * from Estado";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(queryHospitales, conexion);
            OdbcDataReader lector = comando.ExecuteReader();
            DropDownList2.DataSource = lector;
            DropDownList2.DataTextField = "nombre";
            DropDownList2.DataValueField = "idHospital";
            DropDownList2.DataBind();
            comando = new OdbcCommand(queryCiudad, conexion);
            lector = comando.ExecuteReader();
            DropDownList3.DataSource = lector;
            DropDownList3.DataTextField = "nombre";
            DropDownList3.DataValueField = "idCiudad";
            DropDownList3.DataBind();
            comando = new OdbcCommand(queryEstado, conexion);
            lector = comando.ExecuteReader();
            DropDownList4.DataSource = lector;
            DropDownList4.DataTextField = "nombre";
            DropDownList4.DataValueField = "idEstado";
            DropDownList4.DataBind();
            lector.Close();
            conexion.Close();
            Button6.Visible = false;




        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("inicio.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("dashboardAdmin.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
            String query = "select Sucursal.idSucursal as 'idSucursal', Sucursal.correo as 'Correo', Sucursal.nombre as 'Nombre de Sucursal', Hospital.nombre as 'Nombre del Hospital', Ciudad.nombre as 'Ciudad' from Sucursal inner join Hospital on Hospital.idHospital = Sucursal.idHospital inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad ";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand();
            switch (DropDownList1.SelectedIndex)
            {
                case 0:
                    query = query + "where Sucursal.correo like(?)";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("Sucursal.correo", "%" + TextBox1.Text + "%");
                    break;
                case 1:
                    query = query + "where Hospital.nombre like(?)";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("Hospital.nombre", "%" + TextBox1.Text + "%");
                    break;
                case 2:
                    query = query + "where Sucursal.nombre like(?)";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("Sucursal.nombre", "%" + TextBox1.Text + "%");
                    break;
                case 3:
                    query = query + "where Sucursal.idSucursal like(?)";
                    comando = new OdbcCommand(query, conexion);
                    try
                    {
                        comando.Parameters.AddWithValue("Sucursal.nombre", Int32.Parse(TextBox1.Text));

                    }
                    catch
                    {
                        Label3.Text = "Revisa los parámetros de búsqueda";
                    }
                    break;
                case 4:
                    query = query + "where Ciudad.nombre like(?)";
                    comando = new OdbcCommand(query, conexion);
                    comando.Parameters.AddWithValue("Ciudad.nombre", "%" + TextBox1.Text + "%");
                    break;
            }
            try
            {
                OdbcDataReader lector = comando.ExecuteReader();
                GridView1.DataSource = lector;
                GridView1.DataBind();
                Label3.Text = "Búsqueda de " + DropDownList1.SelectedValue + ": " + TextBox1.Text;
            } catch(Exception ex)
            {
                Label3.Text = "Ocurrió un error" + ex.ToString();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "select Sucursal.idSucursal as 'idSucursal', Sucursal.correo as 'Correo', Sucursal.nombre as 'Nombre de Sucursal', Hospital.nombre as 'Nombre del Hospital', Ciudad.nombre as 'Ciudad' from Sucursal inner join Hospital on Hospital.idHospital = Sucursal.idHospital inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad where Sucursal.idSucursal like(?)";
            String query1 = "select ubicacion, contrasena from Sucursal where idSucursal = ?";
            String query2 = "select Hospital.idHospital, Hospital.nombre from Sucursal inner join Hospital on Hospital.idHospital = Sucursal.idHospital where Sucursal.idSucursal =?";
            String query3 = "select Ciudad.idCiudad from Sucursal inner join Ciudad on Ciudad.idCiudad = Sucursal.idCiudad where Sucursal.idSucursal = ?";
            OdbcConnection conexion = new ConexionBD().con;

            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idSucursal", Int32.Parse(GridView1.SelectedRow.Cells[1].Text));
            OdbcDataReader lector = comando.ExecuteReader();
            GridView2.DataSource = lector;
            GridView2.DataBind();
            TextBox2.Text = GridView2.Rows[0].Cells[1].Text;
            TextBox4.Text = GridView2.Rows[0].Cells[2].Text;

            comando = new OdbcCommand(query1, conexion);
            comando.Parameters.AddWithValue("idSucursal", Int32.Parse(GridView1.SelectedRow.Cells[1].Text));
            lector = comando.ExecuteReader();
            lector.Read();
            TextBox3.Text = HttpUtility.HtmlDecode(lector.GetString(0));
            TextBox5.Text = HttpUtility.HtmlDecode(lector.GetString(1));
            TextBox6.Text = HttpUtility.HtmlDecode(lector.GetString(1));
            lector.Close();

            comando = new OdbcCommand(query2, conexion);
            comando.Parameters.AddWithValue("idSucursal", Int32.Parse(GridView1.SelectedRow.Cells[1].Text.ToString()));
            lector = comando.ExecuteReader();
            lector.Read();
            DropDownList2.SelectedValue = lector.GetString(0);
            lector.Close();

            comando = new OdbcCommand(query3, conexion);
            comando.Parameters.AddWithValue("idSucursal", Int32.Parse(GridView1.SelectedRow.Cells[1].Text.ToString()));
            lector = comando.ExecuteReader();
            lector.Read();
            DropDownList3.SelectedValue = lector.GetString(0);

            lector.Close();
            conexion.Close();

            GridView1.DataSource = null;
            GridView1.DataBind();





        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (TextBox5.Text.Equals(TextBox6.Text))
            {
                String query = "update Sucursal set correo = ?, ubicacion = ?, nombre = ?, idCiudad = ?, idHospital = ?, contrasena = ? where idSucursal = ?";
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query, conexion);
                try
                {
                    comando.Parameters.AddWithValue("correo", TextBox2.Text);
                    comando.Parameters.AddWithValue("ubicacion", TextBox3.Text);
                    comando.Parameters.AddWithValue("nombre", TextBox4.Text);
                    Random r = new Random();
                    Int32 claveH;
                    Int32 claveC;
                    if (CheckBox1.Checked)
                    {
                        String insertaHospital = "insert into Hospital values(?, ?)";
                        OdbcCommand comandoHospital = new OdbcCommand(insertaHospital, conexion);
                        bool bandera = true;
                        while (bandera)
                        {
                            try
                            {
                                claveH = r.Next(1, 999999);
                                comandoHospital.Parameters.AddWithValue("idHospital", claveH);
                                comandoHospital.Parameters.AddWithValue("nombre", TextBox7.Text);
                                comandoHospital.ExecuteNonQuery();
                                comando.Parameters.AddWithValue("idHospital", claveH);
                                bandera = false;

                            }
                            catch { }
                        }
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("idHospital", DropDownList2.SelectedValue);
                    }
                    if (CheckBox2.Checked)
                    {
                        String insertaCiudad = "insert into Ciudad values(?,?,?)";
                        OdbcCommand comandoCiudad = new OdbcCommand(insertaCiudad, conexion);
                        bool bandera = true;
                        while (bandera)
                        {
                            try
                            {
                                claveC = r.Next(1, 999999);
                                comandoCiudad.Parameters.AddWithValue("idCiudad", claveC);
                                comandoCiudad.Parameters.AddWithValue("nombre", TextBox8.Text);
                                comandoCiudad.Parameters.AddWithValue("idEstado", DropDownList4.SelectedValue);
                                comandoCiudad.ExecuteNonQuery();
                                comando.Parameters.AddWithValue("idCiudad", claveC);
                                bandera = false;
                            }
                            catch { }
                        }
                    } else
                    {
                        comando.Parameters.AddWithValue("idCiudad", DropDownList3.SelectedValue);
                    }
                    comando.Parameters.AddWithValue("contrasena", TextBox5.Text);
                    comando.Parameters.AddWithValue("idSucursal", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
                    comando.ExecuteNonQuery();
                    Label12.Text = "Se actualizaron los valores";
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Label12.Text = "Ocurrió un error" + ex.ToString();
                }
            }
            else
            {
                Label12.Text = "Las contraseñas no coinceiden";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Button5.Visible = false;
            Button6.Visible = true;
            String queryPeticiones = "select count(idSucursal) from Peticion where idSucursal = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(queryPeticiones, conexion);
            comando.Parameters.AddWithValue("idsucursal", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            OdbcDataReader lector = comando.ExecuteReader();
            lector.Read();
            Label12.Text = "Existen " + lector.GetString(0) + " peticiones de esta sucursal, ¿estás seguro de querer borrar esta sucursal?";
            lector.Close();
            conexion.Close();



        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            String borrar = "delete from Sucursal where idSucursal = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(borrar, conexion);
            comando.Parameters.AddWithValue("idsucursal", Int32.Parse(GridView2.Rows[0].Cells[0].Text));
            comando.ExecuteNonQuery();
            Label12.Text = "Se borró la sucursal correctamente";
            conexion.Close();
        }
    }
}