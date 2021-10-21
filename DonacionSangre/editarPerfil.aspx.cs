using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
namespace DonacionSangre
{
    public partial class editarPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["idSucursal"] == null)
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            String query = "select correo, ubicacion, nombre, contrasena from Sucursal where idSucursal = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
            OdbcDataReader lector = comando.ExecuteReader();
            lector.Read();
            TextBox5.Text = lector.GetString(0);
            TextBox4.Text = lector.GetString(1);
            TextBox3.Text = lector.GetString(2);
            TextBox2.Text = lector.GetString(3);
            TextBox1.Text = lector.GetString(3);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text.Equals(TextBox1.Text)){

                String act = "update Sucursal set correo = ?, ubicacion = ?, nombre = ?, contrasena = ? where idSucursal = ?";
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(act, conexion);
                comando.Parameters.AddWithValue("correo", TextBox5.Text);
                comando.Parameters.AddWithValue("ubicacion", TextBox4.Text);
                comando.Parameters.AddWithValue("nombre", TextBox3.Text);
                comando.Parameters.AddWithValue("contrasena", TextBox2.Text);
                comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
                try
                {
                    comando.ExecuteNonQuery();
                    Label1.Text = "Se actualizaron los valores correctamente";
                } 
                catch
                {
                    Label1.Text = "Ocurrió un error";
                }
            }
            else
            {
                Label1.Text = "Las contraseñas no coinciden";
            }
        }
    }
}