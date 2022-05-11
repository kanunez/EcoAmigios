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
    public partial class Cuenta_Usu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Id_Usuario"] = "1193391491";
            btnGuardar.Visible = false;
        }

        protected void Regresar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Inicio_Usu.aspx");
        }

        protected void C_contraseña_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void Actualizar_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            //conexion MongoDB
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var UsuarioCollections = database.GetCollection<Usuario>("EAV_USUARIOS");
            //Mostrar Datos
            var ListUsuario = UsuarioCollections.Find(d => d.Documento_Usu == Int64.Parse(Session["Id_Usuario"].ToString())).ToList();

            ListDoc.SelectedIndex = int.Parse(ListUsuario[0].ID_Tipo_Documento.ToString());
            TbDocumento.Text = ListUsuario[0].Documento_Usu.ToString();
            TbNombres.Text = ListUsuario[0].Nombre_Usu.ToString();
            TbApellido.Text = ListUsuario[0].Apellido_Usu.ToString();
            TbCorreo.Text = ListUsuario[0].Correo_Usu.ToString();
            TbTelefono.Text = ListUsuario[0].Telefono_Usu.ToString();
            TbUsuario.Text = ListUsuario[0].Usuario_Usu.ToString();
            
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            btnGuardar.Visible = true;
            ListDoc.Enabled = true;
            TbNombres.ReadOnly = false;
            TbApellido.ReadOnly = false;
            TbCorreo.ReadOnly = false;
            TbTelefono.ReadOnly = false;
            TbDocumento.ReadOnly = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ListDoc.SelectedIndex != 0)
            {
                if (TbDocumento.Text != "" || TbNombres.Text != "" || TbApellido.Text != "" || TbCorreo.Text != "" || TbUsuario.Text != "" )
                {
                    var client = new MongoClient("mongodb://127.0.0.1:27017");
                    //Conexion base de datoa
                    var database = client.GetDatabase("EcoAmigos");
                    //Conexion collections
                    var UsuarioCollections = database.GetCollection<Usuario>("EAV_USUARIOS");
                    //Mostrar
                    var ListUsuario = UsuarioCollections.Find(d => d.Documento_Usu == Int64.Parse(Session["Id_Usuario"].ToString())).ToList();
                    //Datos
                    var usuario = new Usuario() {id = ListUsuario[0].id.ToString(), ID_Tipo_Documento = ListDoc.SelectedIndex, Documento_Usu = Int64.Parse(TbDocumento.Text), Nombre_Usu = TbNombres.Text, Apellido_Usu = TbApellido.Text, Correo_Usu = TbCorreo.Text, Telefono_Usu = Int64.Parse(TbTelefono.Text), Usuario_Usu = TbUsuario.Text, Contraseña_Usu = ListUsuario[0].Contraseña_Usu.ToString() };
                    //Actualizar
                    UsuarioCollections.ReplaceOne(d => d.Documento_Usu == Int64.Parse(Session["Id_Usuario"].ToString()), usuario);
                    MultiView1.ActiveViewIndex = -1;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mnesaje Alerta!", "alert('Seleccione un tipo de documento!!')", true);
                MultiView1.ActiveViewIndex = -1;
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            //Conexion base de datoa
            var database = client.GetDatabase("EcoAmigos");
            //Conexion collections
            var UsuarioCollections = database.GetCollection<Usuario>("EAV_USUARIOS");
            //Mostrar
            var ListUsuario = UsuarioCollections.Find(d => d.Documento_Usu == Int64.Parse(Session["Id_Usuario"].ToString())).ToList();
            string contraseña = GetSHA256(TbContraseñaA.Text);

            if(ListUsuario[0].Contraseña_Usu.ToString() == contraseña)
            {
                if(TbContraseñaN.Text == TbContraseñaR.Text)
                {
                    contraseña = GetSHA256(TbContraseñaN.Text);
                    //Datos
                    var usuario = new Usuario() { id = ListUsuario[0].id.ToString(), ID_Tipo_Documento = ListUsuario[0].ID_Tipo_Documento, Documento_Usu = ListUsuario[0].Documento_Usu, Nombre_Usu = ListUsuario[0].Nombre_Usu, Apellido_Usu = ListUsuario[0].Apellido_Usu, Correo_Usu = ListUsuario[0].Correo_Usu, Telefono_Usu = ListUsuario[0].Telefono_Usu, Usuario_Usu = ListUsuario[0].Usuario_Usu, Contraseña_Usu = contraseña };
                    //Actualizar
                    UsuarioCollections.ReplaceOne(d => d.Documento_Usu == Int64.Parse(Session["Id_Usuario"].ToString()), usuario);
                    MultiView1.ActiveViewIndex = -1;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mnesaje Alerta!", "alert('Las contraseñas nuevas no coinciden!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mnesaje Alerta!", "alert('Verifica tu contraseña antigua!!')", true);
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