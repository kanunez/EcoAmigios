using EcoAmigios.Class;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoAmigios.Forms
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSiguiente_Click(object sender, EventArgs e)
        {
            if (TipoDocumento.Text != "Seleccione uno...")
            {
                if (TbIdentificacion.Text != "" || TbNombre.Text != "" || TbApellido.Text != "" || TbTelefono.Text != "")
                {
                    //conexion MongoDB
                    var client = new MongoClient("mongodb://127.0.0.1:27017");
                    //Conexion base de datoa
                    var database = client.GetDatabase("EcoAmigos");
                    //Conexion collections
                    var UsuarioCollections = database.GetCollection<Usuario>("EAV_USUARIOS");
                    //Mostrar Datos
                    var ListUsuario = UsuarioCollections.Find(d => d.Documento_Usu == Int64.Parse(TbIdentificacion.Text)).ToList();
                    if (ListUsuario.Count == 0)
                    {
                        Session["ID_Tipo_Usuario"] = TipoDocumento.SelectedIndex;
                        Session["ID_Usuario"] = TbIdentificacion.Text;
                        Session["Nombre_Usu"] = TbNombre.Text;
                        Session["Apellido_Usu"] = TbApellido.Text;
                        Session["Telefono_Usu"] = TbTelefono.Text;
                        Response.Redirect("Registro_Correo.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ya existe un usuario con este Documento!!!');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Faltan Datos');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Seleccione el tipo de documento!!');", true);
            }
        }
    }
}