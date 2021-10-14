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

            String query = "select * from Peticion";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            OdbcDataReader lector = comando.ExecuteReader();
            GridView1.DataSource = lector;
            //GridView1.SelectedValue("idPeticion");
            GridView1.DataBind();


            lector.Close();
            conexion.Close();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FUNCIONA EL INDICE, QUEREMOS QUE NOS REGRESE EL NUMERO DE PETICION
            Label4.Text = GridView1.SelectedIndex.ToString();
        }
    }
}