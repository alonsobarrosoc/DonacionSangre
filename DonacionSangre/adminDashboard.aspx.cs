using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonacionSangre
{
    public partial class adminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["admin"] == null)
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("inicio.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("nuevaSucursal.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("editarSucursal.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("editarCiudades.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("editarHospitales.aspx");
        }
    }
}