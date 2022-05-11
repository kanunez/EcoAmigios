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
    public partial class Login_Ga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Id_Grupo"] = null;
        }

        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro_Ga.aspx");
        }

        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            if(TbGUsuario.Text != "" || TbGContraseña.Text != "")
            {
                //conexion MongoDB
                var client = new MongoClient("mongodb://127.0.0.1:27017");
                //Conexion base de datoa
                var database = client.GetDatabase("EcoAmigos");
                //Conexion collections
                var GrupoCollections = database.GetCollection<Grupo_Ambiental>("EAV_GRUPO_AMBIENTAL");
                //Mostrar Datos
                var ListGrupo = GrupoCollections.Find(d => d.Nombre_Grupo == TbGUsuario.Text).ToList();

                if (ListGrupo.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El usuario no esta registrado!!');", true);
                }
                else
                {
                    string contraseña = GetSHA256(TbGContraseña.Text);
                    if (contraseña == ListGrupo[0].Contraseña_Grupo.ToString())
                    {
                        Session["Id_Grupo"] = ListGrupo[0].ID_Grupo_Ambiental;
                        Response.Redirect("Inicio_Grupo.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('La contraseña no coincide!!');", true);
                    }

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

        protected void BtnContraseña_Click(object sender, EventArgs e)
        {

        }
    }
}