using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Driver;
using EcoAmigios.Class;

namespace EcoAmigios.Forms
{
    public partial class Registro_Contraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void BtSiguiente_Click(object sender, EventArgs e)
        {
            if(TbContrasena.Text != "" || TbVerificacionC.Text != "")
            {
                if(TbContrasena.Text == TbVerificacionC.Text)
                {
                    string contraseña = GetSHA256(TbContrasena.Text);
                    //conexion MongoDB
                    var client = new MongoClient("mongodb://127.0.0.1:27017");
                    //Conexion base de datoa
                    var database = client.GetDatabase("EcoAmigos");
                    //Conexion collections
                    var UsuarioCollections = database.GetCollection<Usuario>("EAV_USUARIOS");
                    //agregar datos
                    var usuario = new Usuario() { ID_Tipo_Documento = Int64.Parse(Session["ID_Tipo_Usuario"].ToString()), Documento_Usu = Int64.Parse(Session["ID_Usuario"].ToString()), Nombre_Usu = Session["Nombre_Usu"].ToString(), Apellido_Usu = Session["Apellido_Usu"].ToString(),Correo_Usu = Session["Correo_Usu"].ToString() , Telefono_Usu = Int64.Parse(Session["Telefono_Usu"].ToString()), Usuario_Usu = Session["Usuario_Usu"].ToString(), Contraseña_Usu = contraseña };
                    UsuarioCollections.InsertOne(usuario);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mnesaje Alerta!", "alert('Se ha registrado correctamente')", true);
                    Session["ID_Tipo_Usuario"] = null;
                    Session["ID_Usuario"] = null;
                    Session["Nombre_Usu"] = null;
                    Session["Apellido_Usu"] = null;
                    Session["Correo_Usu"] = null;
                    Session["Telefono_Usu"] = null;
                    Session["Usuario_Usu"] = null; 
                    Response.Redirect("Login_Usu.aspx");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Las contraseñas no coinciden');", true);
            }
        }

        protected void BtRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro_Correo.aspx");
        }
    }
}