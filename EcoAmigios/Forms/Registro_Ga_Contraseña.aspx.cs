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
    public partial class Registro_Ga_Contraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro_Ga_Correo.aspx");
        }

        protected void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if(TbGContrasena.Text != "" || TbGVerificacionC.Text != "")
            {
                if(TbGVerificacionC.Text == TbGContrasena.Text)
                {
                    string contraseña = GetSHA256(TbGContrasena.Text);
                    //conexion MongoDB
                    var client = new MongoClient("mongodb://127.0.0.1:27017");
                    //Conexion base de datoa
                    var database = client.GetDatabase("EcoAmigos");
                    //Conexion collections
                    var GrupoCollections = database.GetCollection<Grupo_Ambiental>("EAV_GRUPO_AMBIENTAL");
                    //agregar datos
                    var GrupoA = new Grupo_Ambiental() { ID_Grupo_Ambiental  = Int64.Parse(Session["ID_Grupo_Ambiental"].ToString()), Nombre_Grupo = Session["Nombre_Grupo"].ToString(), ID_Tipo_Grupo = Int64.Parse(Session["Tipo_Grupo"].ToString()), Telefono_Grupo = Int64.Parse(Session["Telefono_Grupo"].ToString()), Logo_Grupo = Session["Logo_Grupo"].ToString(), Correo_Grupo = Session["Correo_Grupo"].ToString(), Contraseña_Grupo = contraseña, Confirmado = "No"};
                    GrupoCollections.InsertOne(GrupoA);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mnesaje Alerta!", "alert('Se ha registrado correctamente')", true);
                    Session["ID_Grupo_Ambiental"] = null;
                    Session["Nombre_Grupo"] = null;
                    Session["Tipo_Grupo"] = null;
                    Session["Telefono_Grupo"] = null;
                    Session["Logo_Grupo"] = null;
                    Session["Correo_Grupo"] = null;
                    Response.Redirect("Login_Ga.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Las contraseñas no coinciden!!');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Debe llenar todos los campos!!');", true);
            }
        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}