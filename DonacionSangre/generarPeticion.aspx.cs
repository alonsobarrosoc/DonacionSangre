using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace DonacionSangre
{
    public partial class generarPeticion : System.Web.UI.Page
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
                String queryTipo = "select * from Tipo";
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(queryTipo, conexion);
                OdbcDataReader lector = comando.ExecuteReader();
                DropDownList1.DataSource = lector;
                DropDownList1.DataValueField = "idTipo";
                DropDownList1.DataTextField = "nombre";
                DropDownList1.DataBind();
                lector.Close();
                conexion.Close();
            }
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
            Random r = new Random();
            String query = "insert into Peticion values(?, CURRENT_TIMESTAMP, ?, ?, ?, ?)";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idPeticion", r.Next(1, 999999));
            comando.Parameters.AddWithValue("nombrePaciente", TextBox1.Text);
            comando.Parameters.AddWithValue("mililitros", Int32.Parse(TextBox2.Text));
            comando.Parameters.AddWithValue("idTipo", Int32.Parse(DropDownList1.SelectedValue));
            comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
            try
            {
                comando.ExecuteNonQuery();
                TextBox1.Text = "";
                TextBox2.Text = "";
                Label4.Text = "Se registró la petición correctamente";
            } catch(Exception)
            {
                Label4.Text = "Ocurrió un error";
            }
        }
    }
}