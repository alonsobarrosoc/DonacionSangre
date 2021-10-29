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
            lector = comando.ExecuteReader();
            DropDownList2.DataSource = lector;
            DropDownList2.DataTextField = "nombre";
            DropDownList2.DataValueField = "idTipo";
            DropDownList2.DataBind();
            lector.Close();
            conexion.Close();
            if(CheckBoxList1.Items.Count == 0)
            {
                CheckBoxList1.Items.Add(new ListItem("Fecha", " Peticion.fechaPublicacion as 'Fecha'"));
                CheckBoxList1.Items.Add(new ListItem("Nombre del paciente", " Peticion.nombrePaciente as 'Nombre del paciente'"));
                CheckBoxList1.Items.Add(new ListItem("Mililitros", " Peticion.mililitros as 'Mililitros'"));
                CheckBoxList1.Items.Add(new ListItem("Tipo de sangre", " Tipo.nombre as 'Tipo de sangre'"));
            }
            if(CheckBoxList2.Items.Count == 0)
            {
                CheckBoxList2.Items.Add(new ListItem("Nombre del donante", " Donacion.nombreDonante as 'Nombre del donante'"));
                CheckBoxList2.Items.Add(new ListItem("Dono", " Donacion.mililitros as 'Dono'"));
                CheckBoxList2.Items.Add(new ListItem("Fecha", " Donacion.fechaDonacion as 'Fecha'"));
                CheckBoxList2.Items.Add(new ListItem("Paciente al que le dono", " Peticion.nombrePaciente as 'Paciente al que le dono'"));
                CheckBoxList2.Items.Add(new ListItem("Tipo de sangre", " Tipo.nombre as 'Tipo de sangre'"));
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("inicio.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            String query = "select Peticion.idPeticion";
            String from = " from Peticion inner join Tipo on Tipo.idTipo = Peticion.idTipo where idSucursal = ?";
            
            for(int i = 0; i< CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    query = query + ", " + CheckBoxList1.Items[i].Value;
                }
            }
            query = query + from;

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
                comando.Parameters.AddWithValue("idTipo", DropDownList1.SelectedValue);
            }
            OdbcDataReader lector = comando.ExecuteReader();
            GridView1.DataSource = lector;
            GridView1.DataBind();
            lector.Close();
            conexion.Close();
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            String query = "select Donacion.idDonacion as 'idDonacion'";
            String from = "from Donacion inner join Peticion on Peticion.idPeticion = Donacion.idDonacion inner join Tipo on Tipo.idTipo = donacion.idTipo where Peticion.idSucursal = ?";
            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {
                if (CheckBoxList2.Items[i].Selected)
                {
                    query = query + ", " + CheckBoxList2.Items[i].Value;
                }
            }
            query = query + from;
            if (CheckBox4.Checked)
            {
                query = query + "and Donacion.fechaDonacion bewtween ? and ? ";
            }
            if (CheckBox5.Checked)
            {
                query = query + "and Donacion.mililitros bewtween ? and ? ";
            }
            if (CheckBox6.Checked)
            {
                query = query + "and Peticion.nombrePaciente like(?)";
            }
            if (CheckBox7.Checked)
            {
                query = query + "and Tipo.idTipo = ?";
            }
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idSucursal", Session["idSucursal"]);
            if (CheckBox4.Checked)
            {
                comando.Parameters.AddWithValue("fecha1", TextBox5.Text);
                comando.Parameters.AddWithValue("fecha1", TextBox6.Text);
            }
            if (CheckBox5.Checked)
            {
                comando.Parameters.AddWithValue("mil1", Int32.Parse(TextBox7.Text));
                comando.Parameters.AddWithValue("mil1", Int32.Parse(TextBox8.Text));
            }
            if (CheckBox6.Checked)
            {
                comando.Parameters.AddWithValue("mil1", "%"+TextBox9.Text +"%");
            }
            if (CheckBox7.Checked)
            {
                comando.Parameters.AddWithValue("idTipo", DropDownList2.SelectedValue);
            }




            OdbcDataReader lector = comando.ExecuteReader();
            GridView2.DataSource = lector;
            GridView2.DataBind();
            lector.Close();
            conexion.Close();


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }
    }
}
