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
    public partial class Registro_Ga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if(ListGAmbiental.SelectedIndex != 0)
            {
                if (TbGNombre.Text != "" || TbGTelefono.Text != "" || TbGIdentificacion.Text != "")
                {
                    if(FileGAmbiental.HasFiles == true)
                    {
                        //conexion MongoDB
                        var client = new MongoClient("mongodb://127.0.0.1:27017");
                        //Conexion base de datoa
                        var database = client.GetDatabase("EcoAmigos");
                        //Conexion collections
                        var GrupoCollections = database.GetCollection<Grupo_Ambiental>("EAV_GRUPO_AMBIENTAL");
                        //Mostrar Datos
                        var ListGrupo = GrupoCollections.Find(d => d.ID_Grupo_Ambiental == Int64.Parse(TbGIdentificacion.Text)).ToList();
                        var ListGrupoN = GrupoCollections.Find(d => d.Nombre_Grupo == TbGNombre.Text).ToList();

                        if (ListGrupo.Count == 0 && ListGrupoN.Count == 0)
                        {
                            Session["ID_Grupo_Ambiental"] = TbGIdentificacion.Text;
                            Session["Nombre_Grupo"] = TbGNombre.Text;
                            Session["Tipo_Grupo"] = ListGAmbiental.SelectedIndex;
                            Session["Telefono_Grupo"] = TbGTelefono.Text;
                            Session["Logo_Grupo"] = FileGAmbiental.FileName;
                            Response.Redirect("Registro_Ga_Correo.aspx");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ya existe un grupo ambientalista con esta identificacion o con este nombre');", true);
                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Falta el logo de tu Grupo ambiental');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Escriba todos los datos!!');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ingrese a que tipo de grupo pertenece!!!');", true);
            }
        }

        protected void BtnRegresar_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Login_Ga.aspx");
        }
    }
}