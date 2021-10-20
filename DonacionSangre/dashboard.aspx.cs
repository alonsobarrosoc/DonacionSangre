using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonacionSangre
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["nombreSucursal"] == null)
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }

         
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("generarPeticion.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("inicio.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("registrarDonacion.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("editarPeticion.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("editarDonacion.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("reportes.aspx");
        }
    }
}