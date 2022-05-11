using EcoAmigios.Class;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoAmigios.Forms
{
    public partial class Inicio_Grupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Id_Grupo"] == null)
            {
                Response.Redirect("Login_Ga.aspx");
            }
        }
        protected void DataPaginas_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName == "Ver_Pagina")
            {
                Label lbl = (Label)DataPaginas.Items[0].FindControl("Nombre_PaginaLabel");
                LabelNombre.Text = lbl.Text;
                Session["Nombre_Pag"] = LabelNombre.Text;
                Response.Redirect("Editar_Pagina.aspx");
            }
        }

        protected void IBFiltrar_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void IMBFiltros_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void IMBCrearP_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Crear_P_Grupo.aspx");
        }

        protected void IBCSesion_Click(object sender, ImageClickEventArgs e)
        {
            Session["Id_Grupo"] = null;
            Response.Redirect("Principal.aspx");
        }

        protected void BtBuscar_Click(object sender, EventArgs e)
        {
          
        }
    }
}