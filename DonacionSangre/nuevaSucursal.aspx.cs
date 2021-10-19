using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace DonacionSangre
{
    public partial class nuevaSucursal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["admin"] == null)
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            String Hospitales = "Select * from Hospital";
            String Ciudades = "select * from Ciudad";
            String Estados = "select * from Estado";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(Hospitales, conexion);
            OdbcDataReader lector = comando.ExecuteReader();
            DropDownList1.DataSource = lector;
            DropDownList1.DataTextField = "nombre";
            DropDownList1.DataValueField = "idHospital";
            DropDownList1.DataBind();

            comando = new OdbcCommand(Ciudades, conexion);
            lector = comando.ExecuteReader();
            DropDownList2.DataSource = lector;
            DropDownList2.DataTextField = "nombre";
            DropDownList2.DataValueField = "idCiudad";
            DropDownList2.DataBind();

            comando = new OdbcCommand(Estados, conexion);
            lector = comando.ExecuteReader();
            DropDownList3.DataSource = lector;
            DropDownList3.DataTextField = "nombre";
            DropDownList3.DataValueField = "idEstado";
            DropDownList3.DataBind();

            lector.Close();
            conexion.Close();


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            String inserta = "insert into Sucursal values(?, ?, ?, ?, ?,1,1)";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(inserta, conexion);
            bool bandera = true;
            Random r = new Random();
            if (TextBox4.Text.Equals(TextBox5.Text))
            {
                while (bandera)
                {
                    try
                    {
                        comando.Parameters.AddWithValue("idSucursal", r.Next(1, 999999));
                        comando.Parameters.AddWithValue("correo", TextBox2.Text);
                        comando.Parameters.AddWithValue("ubicacion", TextBox3.Text);
                        comando.Parameters.AddWithValue("contrasena", TextBox4.Text);
                        comando.Parameters.AddWithValue("ubicacion", TextBox1.Text);
                        //Ciudad
                        if (CheckBox1.Checked)
                        {
                            String agregaC = "inserta into Ciudad values(?,?,?)";
                            OdbcCommand comandoC = new OdbcCommand(agregaC, conexion);
                            bool b = true;
                            Int32 idCiudad;
                            while (b)
                            {
                                try
                                {
                                    idCiudad = r.Next(1, 999999);
                                    comandoC.Parameters.AddWithValue("idCiudad", idCiudad);
                                    comandoC.Parameters.AddWithValue("nombre", TextBox7.Text);
                                    comandoC.Parameters.AddWithValue("idEstado", DropDownList3.SelectedValue);
                                    comandoC.ExecuteNonQuery();
                                    comando.Parameters.AddWithValue("idCiudad", idCiudad);
                                    b = false;
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("idCiudad", DropDownList2.SelectedValue);
                        }
                        if (CheckBox2.Checked)
                        {
                            String agregaC = "inserta into Hospital values(?,?)";
                            OdbcCommand comandoH = new OdbcCommand(agregaC, conexion);
                            bool b = true;
                            Int32 idHospital;
                            while (b)
                            {
                                try
                                {
                                    idHospital = r.Next(1, 999999);
                                    comandoH.Parameters.AddWithValue("idHospital", idHospital);
                                    comandoH.Parameters.AddWithValue("nombre", TextBox6.Text);
                                    comandoH.ExecuteNonQuery();
                                    comando.Parameters.AddWithValue("idHospital", idHospital);
                                    b = false;
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("idHospital", DropDownList1.SelectedValue);
                        }
                        Label11.Text = "Se agregó la sucursal correctamente";
                    }
                    catch
                    {
                        Label11.Text = "Ocurrió un error";
                    }
                }
            }
        }
    }
}