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
    public partial class Login_Usu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Id_Usuario"] = null;
        }

        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            if(TbUsuario.Text != "" || TbContraseña.Text != "")
            {
                //conexion MongoDB
                var client = new MongoClient("mongodb://127.0.0.1:27017");
                //Conexion base de datoa
                var database = client.GetDatabase("EcoAmigos");
                //Conexion collections
                var UsuarioCollections = database.GetCollection<Usuario>("EAV_USUARIOS");
                //Mostrar Datos
                var ListUsuario = UsuarioCollections.Find(d => d.Usuario_Usu == TbUsuario.Text).ToList();

                if (ListUsuario.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El usuario no esta registrado!!');", true);
                }
                else
                {
                    string contraseña = GetSHA256(TbContraseña.Text);
                    if(contraseña == ListUsuario[0].Contraseña_Usu.ToString())
                    {
                        Session["Id_Usuario"] = ListUsuario[0].Documento_Usu;
                        Response.Redirect("Inicio_Usu.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('La contraseña no coincide!!');", true);
                    }

                }
            }
            else
            {
                LabelError.Text = "Ingrese todos los campos!!";
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

        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
        }

        protected void BtnContraseña_Click(object sender, EventArgs e)
        {

        }
    }
}