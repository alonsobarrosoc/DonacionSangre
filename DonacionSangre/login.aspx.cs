using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Odbc;

namespace DonacionSangre
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String queryAdmin = "select * from Admin where correo = ? and contrasena = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(queryAdmin, conexion);
            comando.Parameters.AddWithValue("correo", TextBox1.Text);
            comando.Parameters.AddWithValue("contrasena", TextBox2.Text);
            OdbcDataReader lector = comando.ExecuteReader();
            //lector.Read();
            if(lector.HasRows)
            {
                lector.Read();
                Session.Add("admin", lector.GetString(0));
                Response.Redirect("adminDashboard.aspx");
                lector.Close();
                conexion.Close();
            }



            String query = "select idSucursal, nombre, correo, contrasena from Sucursal where correo = ? and contrasena = ?";
            comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("correo", TextBox1.Text);
            comando.Parameters.AddWithValue("contrasena", TextBox2.Text);
            lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                lector.Read();
                Session.Add("idSucursal", lector.GetInt32(0));
                Session.Add("nombreSucursal", lector.GetString(1));
                lector.Close();
                conexion.Close();
                Response.Redirect("dashboard.aspx");

            }
        }
    }
}