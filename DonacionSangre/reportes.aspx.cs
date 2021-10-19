using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace DonacionSangre
{
    public partial class reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Session["idSucursal"] == null)
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }

            String query = "select * from Tipo";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            String query = "select Peticion.nombrePaciente as 'Nombre del paciente', Peticion.fechaPublicacion as 'Fecha de publicación', Peticion.mililitros as 'Necesita', Tipo.nombre as 'Tipo' from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where idSucursal = ?";
            

            if (CheckBox1.Checked)
            {
                query = query + "and Peticion.fechaPublicacion bewtween ? and ? ";
            }
            if (CheckBox2.Checked)
            {
                query = query + "and Peticion.mililitros bewtween ? and ? ";
            }
            if (CheckBox3.Checked)
            {
                query = query + "and Tipo.idTipo = ?";
            }

            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idSucursal", Session["idsucursal"]);
            if (CheckBox1.Checked)
            {
                comando.Parameters.AddWithValue("fecha1", TextBox1.Text);
                comando.Parameters.AddWithValue("fecha1", TextBox2.Text);
            }
            if (CheckBox2.Checked)
            {
                comando.Parameters.AddWithValue("mil1", Int32.Parse(TextBox3.Text));
                comando.Parameters.AddWithValue("mil1", Int32.Parse(TextBox4.Text));
            }
            if (CheckBox3.Checked)
            {
                query = query + "and Tipo.idTipo = ?";
                comando.Parameters.AddWithValue("idTipo", DropDownList1.SelectedValue);
            }
            OdbcDataReader lector = comando.ExecuteReader();
            GridView1.DataSource = lector;
            GridView1.DataBind();
        }
    }
}
