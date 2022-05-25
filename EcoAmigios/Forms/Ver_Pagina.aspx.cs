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
    public partial class Ver_Pagina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var PagCollections = database.GetCollection<Pagina>("EAV_PAGINA");
            //Mostrar Datos
            var ListPag = PagCollections.Find(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString()).ToList();

            TbTipoPag.Text = ListPag[0].Tipo_Pagina.ToString();
            TbNombreP.Text = ListPag[0].Nombre_Pagina.ToString();
            TbDescP.Text = ListPag[0].Descripcion_Pagina.ToString();
            ImageP.ImageUrl = "~/Imagenes/" + ListPag[0].Logo_Grupo.ToString();

            if (Session["Id_Usuario"] == null)
            {
                IBMensaje.Enabled = false;
                Label49.Enabled = false;
            }

        }

        protected void IBRegresar1_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["Id_Usuario"] == null)
            {
                Response.Redirect("Editar_Pagina.aspx");
            }
            else
            {
                Response.Redirect("Inicio_Usu.aspx");
            }
               
        }

        protected void IBMensaje_Click(object sender, ImageClickEventArgs e)
        {
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var PagCollections = database.GetCollection<Pagina>("EAV_PAGINA");
            //Mostrar Datos
            var ListPag = PagCollections.Find(d => d.Nombre_Pagina == Session["Nombre_Pag"].ToString()).ToList();
            //Conexion collections
            var PagCollections1 = database.GetCollection<Grupo_Ambiental>("EAV_GRUPO_AMBIENTAL");
            //Mostrar Datos
            var listpag1 = PagCollections1.Find(d => d.ID_Grupo_Ambiental == ListPag[0].ID_Grupo_Ambiental).ToList();
            Response.Redirect("https://wa.me/57" + listpag1[0].Telefono_Grupo.ToString());
        }
    }
}