﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoAmigios.Forms
{
    public partial class Inicio_Usu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Id_Usuario"] == null)
            {
                Response.Redirect("Login_Usu.aspx");
            }
        }

        protected void DataPaginas_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Ver_Pagina")
            {
                Label lbl = (Label)DataPaginas.Items[0].FindControl("Nombre_PaginaLabel");
                LabelNombre.Text = lbl.Text;
                Session["Nombre_Pag"] = LabelNombre.Text;
                Response.Redirect("Ver_Pagina.aspx");
            }
        }

        protected void DataPaginasFiltrar_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Ver_Pagina0")
            {
                Label lbl1 = (Label)DataPaginasFiltrar.Items[0].FindControl("Nombre_PaginaLabel0");
                LabelNombre.Text = lbl1.Text;
                Session["Nombre_Pag"] = LabelNombre.Text;
                Response.Redirect("Ver_Pagina.aspx");
            }
        }

        protected void BtBuscar_Click(object sender, EventArgs e)
        {
            LabelTipo.Text = ListTipoPagina.Text;
            MultiView1.ActiveViewIndex = 0;
            DataPaginas.Visible = false;
            DataPaginasFiltrar.Visible = true;
        }

        protected void IBFiltrar_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void IMBFiltros_Click(object sender, ImageClickEventArgs e)
        {
            DataPaginas.Visible = true;
            DataPaginasFiltrar.Visible = false;
        }

        protected void IMBFiltros0_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Cuenta_Usu.aspx");
        }

        protected void IBCSesion_Click(object sender, ImageClickEventArgs e)
        {
            Session["Id_Usuario"] = null;
            Response.Redirect("Principal.aspx");
        }
    }
}